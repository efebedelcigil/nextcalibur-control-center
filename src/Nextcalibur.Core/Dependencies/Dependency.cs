using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Nextcalibur.Core.Dependencies;

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

    /// <summary>The newest release: version and the installer's download URL.</summary>
    public abstract Task<(Version Version, Uri Download)?> LatestAsync(HttpClient http, CancellationToken ct);

    /// <summary>
    /// A distinctive part of the signer's subject, e.g. "CN=namazso.eu".
    /// A download whose Authenticode signature is invalid or signed by
    /// anyone else is deleted, not run.
    /// </summary>
    public abstract string ExpectedSigner { get; }

    /// <summary>Arguments that make the installer run without a wizard.</summary>
    public abstract string SilentInstallArguments { get; }

    /// <summary>
    /// Verifies the file's signature against <see cref="ExpectedSigner"/>.
    /// The chain has to be valid too; a self-signed certificate with the
    /// right name is exactly what an attacker would make.
    /// </summary>
    public bool SignatureIsTrusted(string file)
    {
        try
        {
            using var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(file));
            if (!certificate.Subject.Contains(ExpectedSigner, StringComparison.OrdinalIgnoreCase)) return false;

            using var chain = new X509Chain();
            chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
            return chain.Build(certificate);
        }
        catch (Exception ex) when (ex is System.Security.Cryptography.CryptographicException or IOException)
        {
            return false;
        }
    }

    /// <summary>Runs the installer quietly and waits. True when it reported success.</summary>
    public bool Install(string file)
    {
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
            // 183 is ERROR_ALREADY_EXISTS: the same version was there, which
            // is the outcome wanted, not a failure.
            return process.HasExited && process.ExitCode is 0 or 183;
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>The newest GitHub release of a repository, for dependencies hosted there.</summary>
    protected static async Task<(Version Version, Uri Download)?> LatestGitHubReleaseAsync(
        HttpClient http, string owner, string repo, string assetName, CancellationToken ct)
    {
        using var response = await http.GetAsync($"https://api.github.com/repos/{owner}/{repo}/releases/latest", ct);
        if (!response.IsSuccessStatusCode) return null;

        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync(ct));
        var root = json.RootElement;
        var tag = root.GetProperty("tag_name").GetString()?.TrimStart('v', 'V');
        if (tag is null || !Version.TryParse(tag.Contains('.') ? tag : tag + ".0", out var version)) return null;

        foreach (var asset in root.GetProperty("assets").EnumerateArray())
        {
            if (string.Equals(asset.GetProperty("name").GetString(), assetName, StringComparison.OrdinalIgnoreCase)
                && asset.GetProperty("browser_download_url").GetString() is { } url)
                return (version, new Uri(url));
        }
        return null;
    }
}
