using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Nextcalibur.Core.Security;

/// <summary>
/// Whether a file's Authenticode signature is valid and who made it.
///
/// <see cref="X509Certificate.CreateFromSignedFile"/> only reads the
/// certificate out of a file's signature block; it does not check that
/// the signature matches the file. A signature block copied from a signed
/// installer onto any other file passes that call with the right name on
/// it. <c>WinVerifyTrust</c> is what checks the hash, the signature, the
/// chain and the revocation, the way Windows itself does before it trusts
/// a driver; this is that call and nothing else.
/// </summary>
// SYSLIB0057 (.NET 9+): X509CertificateLoader has no counterpart for reading the
// signer out of a signed PE file or catalog. The signature itself is verified
// by WinVerifyTrust before this; the certificate is read only for its subject.
public static class Authenticode
{
    /// <summary>
    /// True when the file carries a valid signature whose chain Windows
    /// trusts and whose signing certificate has exactly the given subject
    /// component, e.g. <c>CN=namazso.eu</c>.
    /// </summary>
    public static bool IsSignedBy(string file, string subjectComponent, bool offline = false)
    {
        if (!SignatureIsValid(file, offline)) return false;
        try
        {
            #pragma warning disable SYSLIB0057
            using var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(file));
            #pragma warning restore SYSLIB0057
            return HasSubjectComponent(certificate, subjectComponent);
        }
        catch (Exception ex) when (ex is System.Security.Cryptography.CryptographicException or IOException)
        {
            return false;
        }
    }

    /// <summary>
    /// For a file Windows put on the machine - a driver, a library from a
    /// driver package: signed in the file itself, or listed in a catalog in
    /// Windows' catalog database whose signature is valid. Driver packages
    /// are signed the second way - PawnIO.sys and nvml.dll both are - and a
    /// check of the file alone calls them unsigned. Either way the signer
    /// must have the given subject component. Offline: see
    /// <see cref="SignatureIsValid"/>.
    ///
    /// Not for downloads: an installer about to be run has to carry its
    /// signature itself, and <see cref="IsSignedBy"/> is what checks that.
    /// </summary>
    public static bool IsSystemFileSignedBy(string file, string subjectComponent)
    {
        if (!File.Exists(file)) return false;
        if (IsSignedBy(file, subjectComponent, offline: true)) return true;
        return CatalogFor(file) is { } catalog && HasCatalogSigner(catalog, subjectComponent);
    }

    private static bool HasCatalogSigner(string catalog, string subjectComponent)
    {
        try
        {
            #pragma warning disable SYSLIB0057
            using var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(catalog));
            #pragma warning restore SYSLIB0057
            return HasSubjectComponent(certificate, subjectComponent);
        }
        catch (Exception ex) when (ex is System.Security.Cryptography.CryptographicException or IOException)
        {
            return false;
        }
    }

    /// <summary>
    /// The catalog that lists this file, when the catalog's signature and the
    /// file's entry in it both verify; null otherwise. SHA-256 first, then
    /// SHA-1, which older catalogs still use.
    /// </summary>
    internal static string? CatalogFor(string file)
    {
        foreach (var algorithm in new[] { "SHA256", null })
        {
            if (!CryptCATAdminAcquireContext2(out var admin, IntPtr.Zero, algorithm, IntPtr.Zero, 0)) continue;
            try
            {
                byte[] hash;
                using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var size = 64u;
                    hash = new byte[size];
                    if (!CryptCATAdminCalcHashFromFileHandle2(admin, stream.SafeFileHandle, ref size, hash, 0)) continue;
                    Array.Resize(ref hash, (int)size);
                }

                var previous = IntPtr.Zero;
                var catalogContext = CryptCATAdminEnumCatalogFromHash(admin, hash, (uint)hash.Length, 0, ref previous);
                if (catalogContext == IntPtr.Zero) continue;
                try
                {
                    var info = new CatalogInfo { cbStruct = (uint)Marshal.SizeOf<CatalogInfo>() };
                    if (!CryptCATCatalogInfoFromContext(catalogContext, ref info, 0)) continue;
                    if (VerifyCatalogMember(info.wszCatalogFile, file, hash, admin)) return info.wszCatalogFile;
                }
                finally
                {
                    CryptCATAdminReleaseCatalogContext(admin, catalogContext, 0);
                }
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                return null;
            }
            finally
            {
                CryptCATAdminReleaseContext(admin, 0);
            }
        }
        return null;
    }

    /// <summary>WinVerifyTrust over the catalog entry: the catalog's signature and chain, and the file's hash in it.</summary>
    private static bool VerifyCatalogMember(string catalog, string file, byte[] hash, IntPtr admin)
    {
        var hashPointer = Marshal.AllocHGlobal(hash.Length);
        var infoPointer = Marshal.AllocHGlobal(Marshal.SizeOf<WinTrustCatalogInfo>());
        try
        {
            Marshal.Copy(hash, 0, hashPointer, hash.Length);
            var catalogInfo = new WinTrustCatalogInfo
            {
                cbStruct = (uint)Marshal.SizeOf<WinTrustCatalogInfo>(),
                pcwszCatalogFilePath = catalog,
                pcwszMemberTag = Convert.ToHexString(hash),
                pcwszMemberFilePath = file,
                pbCalculatedFileHash = hashPointer,
                cbCalculatedFileHash = (uint)hash.Length,
                hCatAdmin = admin,
            };
            Marshal.StructureToPtr(catalogInfo, infoPointer, false);
            try
            {
                var data = new WinTrustData
                {
                    cbStruct = (uint)Marshal.SizeOf<WinTrustData>(),
                    dwUIChoice = WtdUiNone,
                    fdwRevocationChecks = WtdRevokeNone,
                    dwUnionChoice = WtdChoiceCatalog,
                    pFile = infoPointer,
                    dwStateAction = WtdStateActionVerify,
                    dwProvFlags = WtdCacheOnlyUrlRetrieval,
                };
                var action = WintrustActionGenericVerifyV2;
                var result = WinVerifyTrust(IntPtr.Zero, ref action, ref data);
                data.dwStateAction = WtdStateActionClose;
                WinVerifyTrust(IntPtr.Zero, ref action, ref data);
                return result == 0;
            }
            finally
            {
                Marshal.DestroyStructure<WinTrustCatalogInfo>(infoPointer);
            }
        }
        finally
        {
            Marshal.FreeHGlobal(infoPointer);
            Marshal.FreeHGlobal(hashPointer);
        }
    }

    /// <summary>Whole component, not substring: <c>CN=namazso.eu</c> does not match <c>CN=namazso.eu.example</c>.</summary>
    internal static bool HasSubjectComponent(X509Certificate2 certificate, string component)
    {
        foreach (var part in certificate.SubjectName.Format(true).Split('\n'))
            if (part.Trim().Equals(component, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    /// <summary>
    /// The signature, the chain and the revocation, as Windows checks them.
    ///
    /// <paramref name="offline"/>: hash, signature and chain, without the
    /// revocation check and without any request to the network. For the checks at start and on the timer,
    /// which look at files already on the machine and must not wait on - or
    /// announce themselves to - a certificate authority every hour. A
    /// download about to be run is checked online.
    /// </summary>
    public static bool SignatureIsValid(string file, bool offline = false)
    {
        var fileInfo = new WinTrustFileInfo
        {
            cbStruct = (uint)Marshal.SizeOf<WinTrustFileInfo>(),
            pcwszFilePath = file,
        };
        var fileInfoPointer = Marshal.AllocHGlobal(Marshal.SizeOf<WinTrustFileInfo>());
        try
        {
            Marshal.StructureToPtr(fileInfo, fileInfoPointer, false);
            var data = new WinTrustData
            {
                cbStruct = (uint)Marshal.SizeOf<WinTrustData>(),
                dwUIChoice = WtdUiNone,
                // Offline: no revocation (a CRL not in the cache would fail
                // the check outright), and nothing fetched to build the chain.
                fdwRevocationChecks = offline ? WtdRevokeNone : WtdRevokeWholeChain,
                dwUnionChoice = WtdChoiceFile,
                pFile = fileInfoPointer,
                dwStateAction = WtdStateActionVerify,
                dwProvFlags = offline ? WtdCacheOnlyUrlRetrieval : WtdRevocationCheckChain,
            };
            var action = WintrustActionGenericVerifyV2;
            var result = WinVerifyTrust(IntPtr.Zero, ref action, ref data);
            data.dwStateAction = WtdStateActionClose;
            WinVerifyTrust(IntPtr.Zero, ref action, ref data);
            return result == 0;
        }
        finally
        {
            // StructureToPtr made a native copy of the path; it is freed here
            // or leaks once per check.
            Marshal.DestroyStructure<WinTrustFileInfo>(fileInfoPointer);
            Marshal.FreeHGlobal(fileInfoPointer);
        }
    }

    private static readonly Guid WintrustActionGenericVerifyV2 = new("00AAC56B-CD44-11d0-8CC2-00C04FC295EE");
    private const uint WtdUiNone = 2;
    private const uint WtdRevokeWholeChain = 1;
    private const uint WtdChoiceFile = 1;
    private const uint WtdChoiceCatalog = 2;
    private const uint WtdStateActionVerify = 1;
    private const uint WtdStateActionClose = 2;
    private const uint WtdRevocationCheckChain = 0x40;
    private const uint WtdCacheOnlyUrlRetrieval = 0x1000;
    private const uint WtdRevokeNone = 0;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct WinTrustFileInfo
    {
        public uint cbStruct;
        public string pcwszFilePath;
        public IntPtr hFile;
        public IntPtr pgKnownSubject;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct WinTrustData
    {
        public uint cbStruct;
        public IntPtr pPolicyCallbackData;
        public IntPtr pSIPClientData;
        public uint dwUIChoice;
        public uint fdwRevocationChecks;
        public uint dwUnionChoice;
        public IntPtr pFile;
        public uint dwStateAction;
        public IntPtr hWVTStateData;
        public IntPtr pwszURLReference;
        public uint dwProvFlags;
        public uint dwUIContext;
        public IntPtr pSignatureSettings;
    }

    [DllImport("wintrust.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int WinVerifyTrust(IntPtr window, ref Guid action, ref WinTrustData data);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct WinTrustCatalogInfo
    {
        public uint cbStruct;
        public uint dwCatalogVersion;
        public string pcwszCatalogFilePath;
        public string pcwszMemberTag;
        public string pcwszMemberFilePath;
        public IntPtr hMemberFile;
        public IntPtr pbCalculatedFileHash;
        public uint cbCalculatedFileHash;
        public IntPtr pcCatalogContext;
        public IntPtr hCatAdmin;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CatalogInfo
    {
        public uint cbStruct;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string wszCatalogFile;
    }

    [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CryptCATAdminAcquireContext2(out IntPtr admin, IntPtr subsystem, string? hashAlgorithm, IntPtr strongHashPolicy, uint flags);

    [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CryptCATAdminCalcHashFromFileHandle2(IntPtr admin, Microsoft.Win32.SafeHandles.SafeFileHandle file, ref uint hashSize, byte[] hash, uint flags);

    [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern IntPtr CryptCATAdminEnumCatalogFromHash(IntPtr admin, byte[] hash, uint hashSize, uint flags, ref IntPtr previous);

    [DllImport("wintrust.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Unicode), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CryptCATCatalogInfoFromContext(IntPtr catalogContext, ref CatalogInfo info, uint flags);

    [DllImport("wintrust.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CryptCATAdminReleaseCatalogContext(IntPtr admin, IntPtr catalogContext, uint flags);

    [DllImport("wintrust.dll", ExactSpelling = true), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CryptCATAdminReleaseContext(IntPtr admin, uint flags);
}
