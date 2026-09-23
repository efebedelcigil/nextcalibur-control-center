using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using Nextcalibur.Core.Security;

namespace Nextcalibur.Core.Dependencies;

/// <summary>
/// A release to fetch: which version, where it lives, and the hash its
/// publisher states for it, when one is published.
/// </summary>
/// <param name="Version">The version this file installs.</param>
/// <param name="Download">Where to get it, checked before it is used.</param>
/// <param name="Sha512">The publisher's hash, hexadecimal, or null when there is none to check against.</param>
public sealed record ReleaseFile(Version Version, Uri Download, string? Sha512 = null);

/// <summary>
/// Something the application needs from outside itself, kept current.
///
/// Set by the owner on 12 September 2026: what is not carried inside the
/// package - today the PawnIO driver, tomorrow whatever comes - is checked
/// alongside the application's own updates, offered when missing or old,
/// downloaded, verified and installed on a yes. Each dependency describes
/// itself through this class: where it lives on the machine, where its
/// releases are, who signs it, how it installs quietly. The manager does the
/// rest the same way for all of them.
/// </summary>
public abstract class Dependency
{
    /// <summary>Short id, stable, for settings and logs.</summary>
    public abstract string Id { get; }

    /// <summary>What the person sees: "PawnIO driver".</summary>
    public abstract string Name { get; }

    /// <summary>One sentence: what it is for, so the offer explains itself.</summary>
    public abstract string Purpose { get; }

    /// <summary>The version on the machine, or null when it is not installed.</summary>
    public abstract Version? InstalledVersion();

    /// <summary>The newest release: version, where to get it, and what it should hash to.</summary>
    public abstract Task<ReleaseFile?> LatestAsync(HttpClient http, CancellationToken ct);

    /// <summary>
    /// Whether this application's uninstall may offer to remove it. False
    /// for anything the machine as a whole depends on - the .NET runtime is
    /// not ours to take away because we were the ones who needed it.
    /// </summary>
    public virtual bool MayBeRemoved => true;

    /// <summary>
    /// One component of the signer's subject, whole, e.g. "CN=namazso.eu".
    /// A download whose Authenticode signature is invalid or signed by
    /// anyone else is deleted, not run.
    /// </summary>
    public abstract string ExpectedSigner { get; }

    /// <summary>
    /// A last look before the file is fetched, for anything too dear to ask
    /// for on every check. The default answers with what the check found.
    ///
    /// The .NET runtime is why this exists: knowing that a newer patch
    /// exists costs 813 bytes, while knowing its exact link and hash costs
    /// 310 kB - so the cheap question is asked four times a day and the
    /// dear one only when somebody says yes.
    /// </summary>
    public virtual Task<ReleaseFile> ResolveBeforeDownloadAsync(HttpClient http, ReleaseFile found, CancellationToken ct) =>
        Task.FromResult(found);

    /// <summary>Arguments that make the installer run without a wizard.</summary>
    public abstract string SilentInstallArguments { get; }

    /// <summary>The name of its key under Windows' Uninstall registry key, where its version and uninstaller are.</summary>
    public abstract string UninstallKey { get; }

    /// <summary>
    /// Removes it, quietly, through the uninstaller it registered. Elevated
    /// only. True when it reported success or was already gone. Offered at
    /// this application's own uninstall, and only on a yes: another program
    /// may be using it.
    /// </summary>
    public bool Uninstall()
    {
        if (!MayBeRemoved) return false;

        string? command;
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + UninstallKey);
            if (key is null) return true;
            command = key.GetValue("QuietUninstallString") as string ?? key.GetValue("UninstallString") as string;
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException)
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(command)) return false;

        // "C:\...\uninstall.exe" -uninstall -silent : the file, then its arguments.
        string file, arguments;
        if (command.StartsWith('"'))
        {
            var close = command.IndexOf('"', 1);
            if (close < 0) return false;
            file = command[1..close];
            arguments = command[(close + 1)..].Trim();
        }
        else
        {
            var space = command.IndexOf(' ');
            file = space < 0 ? command : command[..space];
            arguments = space < 0 ? string.Empty : command[(space + 1)..].Trim();
        }

        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = file,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
            });
            if (process is null) return false;
            process.WaitForExit(5 * 60 * 1000);
            return process.HasExited && process.ExitCode == 0;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Verifies the file's Authenticode signature - the hash, the signature,
    /// the chain, the revocation, through <c>WinVerifyTrust</c> - and that
    /// the signer is <see cref="ExpectedSigner"/>. Reading the certificate
    /// out of the file and checking its chain is not enough: that does not
    /// prove the signature covers this file.
    /// </summary>
    public bool SignatureIsTrusted(string file) => Authenticode.IsSignedBy(file, ExpectedSigner);

    /// <summary>Runs the installer quietly and waits. True when it reported success.</summary>
    public bool Install(string file) => Install(file, out _);

    /// <summary>Runs the installer quietly and waits. True when it reported success, indicating whether a reboot is required.</summary>
    public bool Install(string file, out bool restartRequired)
    {
        restartRequired = false;
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = file,
                Arguments = SilentInstallArguments,
                UseShellExecute = false,
                CreateNoWindow = true,
            });
            if (process is null) return false;
            process.WaitForExit(5 * 60 * 1000);
            if (!process.HasExited) return false;

            // 3010 is ERROR_SUCCESS_REBOOT_REQUIRED: the installation succeeded, but needs a reboot
            if (process.ExitCode == 3010)
            {
                restartRequired = true;
                return true;
            }

            // 183 is ERROR_ALREADY_EXISTS (PawnIO) and 1638 is "another version
            // of this product is already installed" (Microsoft's bootstrapper,
            // when a newer patch got there first): either way the machine has
            // what was wanted - the wizard accepts both for the same reason.
            return process.ExitCode is 0 or 183 or 1638;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Whether a download link is what it should be: HTTPS, on GitHub, and
    /// under the releases of the repository that was asked about.
    ///
    /// The answer that carries it arrives over TLS from api.github.com, so
    /// this is a second lock on the same door - but the thing behind the door
    /// is a file this application downloads and runs as administrator, and a
    /// link is the cheapest part of an answer to change.
    /// </summary>
    internal static bool IsReleaseAssetOf(string? link, string owner, string repo, out Uri url)
    {
        url = null!;
        if (!Uri.TryCreate(link, UriKind.Absolute, out var candidate)) return false;
        if (candidate.Scheme != Uri.UriSchemeHttps) return false;
        if (!candidate.Host.Equals("github.com", StringComparison.OrdinalIgnoreCase)) return false;
        if (!candidate.AbsolutePath.StartsWith($"/{owner}/{repo}/releases/download/", StringComparison.OrdinalIgnoreCase)) return false;
        url = candidate;
        return true;
    }

    /// <summary>The newest GitHub release of a repository, for dependencies hosted there.</summary>
    protected static async Task<ReleaseFile?> LatestGitHubReleaseAsync(
        HttpClient http, string owner, string repo, string assetName, CancellationToken ct)
    {
        // Conditional: after the first check GitHub answers 304 with no
        // body, which costs a few hundred bytes and nothing at all against
        // the rate limit.
        var body = await Conditional.GetAsync(http, $"https://api.github.com/repos/{owner}/{repo}/releases/latest", ct);
        if (body is null) return null;

        // Read as the shape it should have; a rate-limit notice, a changed
        // API or a body that is not JSON at all is a null answer, not an
        // exception out of a timer.
        JsonDocument json;
        try
        {
            json = JsonDocument.Parse(body);
        }
        catch (JsonException)
        {
            return null;
        }

        using var _ = json;
        var root = json.RootElement;
        if (root.ValueKind != JsonValueKind.Object
            || !root.TryGetProperty("tag_name", out var tagElement) || tagElement.ValueKind != JsonValueKind.String
            || !root.TryGetProperty("assets", out var assets) || assets.ValueKind != JsonValueKind.Array)
            return null;
        var tag = tagElement.GetString()!.TrimStart('v', 'V');
        if (!Version.TryParse(tag.Contains('.') ? tag : tag + ".0", out var version)) return null;

        foreach (var asset in assets.EnumerateArray())
        {
            if (asset.ValueKind == JsonValueKind.Object
                && asset.TryGetProperty("name", out var name) && name.ValueKind == JsonValueKind.String
                && string.Equals(name.GetString(), assetName, StringComparison.OrdinalIgnoreCase)
                && asset.TryGetProperty("browser_download_url", out var link) && link.ValueKind == JsonValueKind.String
                && IsReleaseAssetOf(link.GetString(), owner, repo, out var url))
                return new ReleaseFile(version, url);
        }
        return null;
    }
}
