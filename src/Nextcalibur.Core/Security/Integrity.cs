using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.Json;
using Microsoft.Win32;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Security;

/// <summary>What an integrity check found.</summary>
public enum IntegrityKind
{
    /// <summary>A file of the application differs from what the release shipped.</summary>
    ProgramChanged,

    /// <summary>An installed copy without the release's list of hashes - an older package, or the list removed.</summary>
    ProgramUnverified,

    /// <summary>Something other than administrators may write where the application is installed.</summary>
    FolderWritable,

    /// <summary>A .NET runtime library loaded into this process is not signed by Microsoft.</summary>
    RuntimeUntrusted,

    /// <summary>A library from outside Windows' folder, loaded into this process, carries no valid signature.</summary>
    ModuleUnsigned,

    /// <summary>NVIDIA's nvml.dll is not signed as Windows installed it; it is not loaded.</summary>
    NvmlUntrusted,

    /// <summary>PawnIO's driver file is not signed as Windows installed it; it is not used.</summary>
    PawnIoUntrusted,
}

/// <param name="Kind">What was found.</param>
/// <param name="Subject">The file or folder it was found on.</param>
/// <param name="Serious">
/// True when the application itself may not be what was released: firmware
/// writes stop until it is reinstalled. False for what is reported only.
/// </param>
public sealed record IntegrityFinding(IntegrityKind Kind, string Subject, bool Serious);

/// <summary>
/// Whether the application and what it depends on are what they should be,
/// checked at start and on a timer.
///
/// <list type="bullet">
/// <item>The application: its executable against the SHA-256 the release
/// recorded beside it (<see cref="ManifestName"/>), and its install folder's
/// permissions - nothing but administrators may write there.</item>
/// <item>The runtime: every library of the .NET runtime loaded into this
/// process signed by Microsoft; anything else loaded from outside Windows'
/// folder signed by someone.</item>
/// <item>The dependencies: nvml.dll and PawnIO's driver signed as Windows
/// installed them - in the file or through Windows' catalog - checked
/// before either is used, and not used when they fail.</item>
/// </list>
///
/// What a check inside a program cannot do is stand guard over that
/// program: something able to replace the executable can replace the check
/// with it. The folder's permissions are what stop that, and those are
/// checked and put back here. The hash catches everything else - a damaged
/// file, an update half applied, a file swapped by something that did not
/// also know to rewrite the list.
/// </summary>
public static class Integrity
{
    /// <summary>Written by the release workflow beside the executable: file name to SHA-256.</summary>
    public const string ManifestName = "Nextcalibur.integrity.json";

    private const string Microsoft = "O=Microsoft Corporation";
    private const string Nvidia = "O=NVIDIA Corporation";

    private static readonly object Gate = new();
    private static readonly HashSet<string> ModulesSeen = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Lazy<bool> NvmlTrusted = new(() => Check("nvml", NvmlPath, Microsoft, Nvidia));

    public static string NvmlPath => Path.Combine(Environment.SystemDirectory, "nvml.dll");

    /// <summary>
    /// Whether nvml.dll may be loaded. Answered once per process: once
    /// loaded, a library is what it was when it was loaded, whatever happens
    /// to the file afterwards.
    /// </summary>
    public static bool NvmlIsTrusted() => NvmlTrusted.Value;

    /// <summary>Whether PawnIO's driver file is signed as Windows installed it. Asked before the device is opened.</summary>
    public static bool PawnIoIsTrusted() =>
        PawnIoDriverPath() is { } path && Check("PawnIO", path, Microsoft);

    /// <summary>Everything, in one pass. Off the interface thread: it hashes the executable and reads signatures.</summary>
    public static IReadOnlyList<IntegrityFinding> CheckAll(string? executablePath)
    {
        var findings = new List<IntegrityFinding>();
        findings.AddRange(CheckProgram(executablePath));
        findings.AddRange(CheckLoadedModules(executablePath));
        if (File.Exists(NvmlPath) && !NvmlIsTrusted())
            findings.Add(new(IntegrityKind.NvmlUntrusted, NvmlPath, Serious: false));
        if (PawnIoDriverPath() is { } pawnIo && !PawnIoIsTrusted())
            findings.Add(new(IntegrityKind.PawnIoUntrusted, pawnIo, Serious: false));
        return findings;
    }

    /// <summary>
    /// The executable against the release's record, and the install folder's
    /// permissions. A development build - no updater beside it - has neither
    /// and is not checked.
    /// </summary>
    public static IReadOnlyList<IntegrityFinding> CheckProgram(string? executablePath)
    {
        var findings = new List<IntegrityFinding>();
        if (executablePath is null || InstallFolderGuard.RootOf(executablePath) is not { } root
            || !File.Exists(Path.Combine(root, "Update.exe")))
            return findings;

        var folder = Path.GetDirectoryName(executablePath)!;
        var manifest = Path.Combine(folder, ManifestName);
        var expected = ReadManifest(manifest);
        if (expected is null)
        {
            // Every release from 0.5.9 carries the list, and the release
            // workflow will not package one without it: an installed copy
            // without it had it taken away.
            findings.Add(new(IntegrityKind.ProgramUnverified, manifest, Serious: true));
        }
        else
        {
            // The executable must be on the list: a list without it proves nothing.
            if (!expected.ContainsKey(Path.GetFileName(executablePath)))
                findings.Add(new(IntegrityKind.ProgramChanged, executablePath, Serious: true));

            foreach (var (name, hash) in expected)
            {
                // A name, not a path: the list may not point outside its folder.
                if (name.Length == 0 || name != Path.GetFileName(name) || name is "." or "..")
                {
                    findings.Add(new(IntegrityKind.ProgramChanged, manifest, Serious: true));
                    continue;
                }
                var file = Path.Combine(folder, name);
                if (Sha256(file) is not { } actual || !actual.Equals(hash, StringComparison.OrdinalIgnoreCase))
                    findings.Add(new(IntegrityKind.ProgramChanged, file, Serious: true));
            }
        }

        // Signed one day: then the signature has to hold as well.
        if (HasEmbeddedSignature(executablePath) && !Authenticode.SignatureIsValid(executablePath, offline: true))
            findings.Add(new(IntegrityKind.ProgramChanged, executablePath, Serious: true));

        // Under Program Files only administrators may write; a copy elsewhere
        // is prompted at every start and has its own guard.
        if (InstallFolderGuard.IsUnderProgramFiles(root))
        {
            foreach (var path in new[] { root, folder, executablePath, Path.Combine(root, "Update.exe"), Path.Combine(root, "Nextcalibur.exe") })
            {
                if (!File.Exists(path) && !Directory.Exists(path)) continue;
                if (!InstallFolderGuard.WritableByOthers(path)) continue;
                // Take back what can be taken back, then look again.
                InstallFolderGuard.RemoveOthersWrite(path);
                if (InstallFolderGuard.WritableByOthers(path))
                    findings.Add(new(IntegrityKind.FolderWritable, path, Serious: true));
                else
                    Log.Warn("integrity", $"Write access for non-administrators found on {path}; removed");
            }
        }

        return findings;
    }

    /// <summary>
    /// Every library loaded into this process from outside Windows' own
    /// folder: the .NET runtime's signed by Microsoft, anything else signed by
    /// someone. Each is looked at once; a library, once loaded, stays what it
    /// was.
    /// </summary>
    public static IReadOnlyList<IntegrityFinding> CheckLoadedModules(string? executablePath)
    {
        var findings = new List<IntegrityFinding>();
        var windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows).TrimEnd('\\') + "\\";
        var runtime = DotnetRoot();

        List<string> paths;
        try
        {
            using var process = Process.GetCurrentProcess();
            paths = process.Modules.Cast<ProcessModule>().Select(m =>
            {
                using (m) return m.FileName;
            }).Where(p => !string.IsNullOrEmpty(p)).ToList()!;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException or NotSupportedException)
        {
            return findings;
        }

        foreach (var path in paths)
        {
            if (path.StartsWith(windows, StringComparison.OrdinalIgnoreCase)) continue;       // Windows' own, and its protection
            if (executablePath is not null && path.Equals(executablePath, StringComparison.OrdinalIgnoreCase)) continue;   // the manifest's job
            lock (Gate)
            {
                if (!ModulesSeen.Add(path)) continue;
            }

            if (runtime is not null && path.StartsWith(runtime, StringComparison.OrdinalIgnoreCase))
            {
                if (!Authenticode.IsSystemFileSignedBy(path, Microsoft))
                    findings.Add(new(IntegrityKind.RuntimeUntrusted, path, Serious: true));
            }
            else if (!Authenticode.SignatureIsValid(path, offline: true) && Authenticode.CatalogFor(path) is null)
            {
                // Security software and overlays load into every process;
                // an unsigned one is worth saying, not worth stopping over.
                findings.Add(new(IntegrityKind.ModuleUnsigned, path, Serious: false));
            }
        }
        return findings;
    }

    /// <summary>The PawnIO driver's file, from its service entry; null when PawnIO is not installed.</summary>
    internal static string? PawnIoDriverPath()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PawnIO");
            if (key?.GetValue("ImagePath") is not string image || image.Length == 0) return null;
            return ResolveDriverPath(image);
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException or IOException)
        {
            return null;
        }
    }

    /// <summary>The forms a service's ImagePath takes, as a file path.</summary>
    internal static string ResolveDriverPath(string image)
    {
        var windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        var path = image.Trim().Trim('"');
        if (path.StartsWith(@"\??\", StringComparison.Ordinal)) path = path[4..];
        if (path.StartsWith(@"\SystemRoot\", StringComparison.OrdinalIgnoreCase)) path = Path.Combine(windows, path[12..]);
        else if (path.StartsWith(@"System32\", StringComparison.OrdinalIgnoreCase)) path = Path.Combine(windows, path);
        return path;
    }

    private static bool Check(string what, string path, params string[] signers)
    {
        var trusted = File.Exists(path) && signers.Any(signer => Authenticode.IsSystemFileSignedBy(path, signer));
        if (!trusted && File.Exists(path))
            Log.Warn("integrity", $"{what}: {path} is not signed as Windows installed it; not used");
        return trusted;
    }

    /// <summary>...\dotnet\ for the runtime this process runs on; null for one carried inside the application.</summary>
    private static string? DotnetRoot()
    {
        var coreLibrary = typeof(object).Assembly.Location;
        if (string.IsNullOrEmpty(coreLibrary)) return null;
        // ...\dotnet\shared\Microsoft.NETCore.App\<version>\System.Private.CoreLib.dll
        var root = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(coreLibrary))));
        return root is null ? null : root.TrimEnd('\\') + "\\";
    }

    internal static Dictionary<string, string>? ReadManifest(string manifest)
    {
        try
        {
            if (!File.Exists(manifest)) return null;
            return JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(manifest));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            return new Dictionary<string, string>();   // unreadable: nothing on it matches, and the executable is missing from it
        }
    }

    internal static string? Sha256(string file)
    {
        try
        {
            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            return Convert.ToHexString(SHA256.HashData(stream));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return null;
        }
    }

    private static bool HasEmbeddedSignature(string file)
    {
        try
        {
            using var certificate = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(file);
            return true;
        }
        catch (Exception ex) when (ex is CryptographicException or IOException)
        {
            return false;
        }
    }
}
