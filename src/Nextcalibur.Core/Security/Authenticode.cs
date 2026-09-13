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
public static class Authenticode
{
    /// <summary>
    /// True when the file carries a valid signature whose chain Windows
    /// trusts and whose signing certificate has exactly the given subject
    /// component, e.g. <c>CN=namazso.eu</c>.
    /// </summary>
    public static bool IsSignedBy(string file, string subjectComponent)
    {
        if (!SignatureIsValid(file)) return false;
        try
        {
            using var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(file));
            return HasSubjectComponent(certificate, subjectComponent);
        }
        catch (Exception ex) when (ex is System.Security.Cryptography.CryptographicException or IOException)
        {
            return false;
        }
    }

    /// <summary>Whole component, not substring: <c>CN=namazso.eu</c> does not match <c>CN=namazso.eu.example</c>.</summary>
    internal static bool HasSubjectComponent(X509Certificate2 certificate, string component)
    {
        foreach (var part in certificate.SubjectName.Format(true).Split('\n'))
            if (part.Trim().Equals(component, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    /// <summary>The signature, the chain and the revocation, as Windows checks them.</summary>
    public static bool SignatureIsValid(string file)
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
                fdwRevocationChecks = WtdRevokeWholeChain,
                dwUnionChoice = WtdChoiceFile,
                pFile = fileInfoPointer,
                dwStateAction = WtdStateActionVerify,
                dwProvFlags = WtdRevocationCheckChain,
            };
            var action = WintrustActionGenericVerifyV2;
            var result = WinVerifyTrust(IntPtr.Zero, ref action, ref data);
            data.dwStateAction = WtdStateActionClose;
            WinVerifyTrust(IntPtr.Zero, ref action, ref data);
            return result == 0;
        }
        finally
        {
            Marshal.FreeHGlobal(fileInfoPointer);
        }
    }

    private static readonly Guid WintrustActionGenericVerifyV2 = new("00AAC56B-CD44-11d0-8CC2-00C04FC295EE");
    private const uint WtdUiNone = 2;
    private const uint WtdRevokeWholeChain = 1;
    private const uint WtdChoiceFile = 1;
    private const uint WtdStateActionVerify = 1;
    private const uint WtdStateActionClose = 2;
    private const uint WtdRevocationCheckChain = 0x40;

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
}
