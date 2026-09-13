using System.Net.Http;
using Nextcalibur.Core.Security;

namespace Nextcalibur.Core.Dependencies;

/// <summary>What a check found for one dependency.</summary>
/// <param name="Dependency">Which.</param>
/// <param name="Installed">The version here, or null when absent.</param>
/// <param name="Latest">The newest release, and where to get it.</param>
public sealed record DependencyStatus(Dependency Dependency, Version? Installed, (Version Version, Uri Download) Latest)
{
    public bool Missing => Installed is null;
    public bool Outdated => Installed is { } v && v < Latest.Version;
    public bool NeedsAction => Missing || Outdated;

    /// <summary>"PawnIO driver 2.3.0 is available (you have 2.2.0)" or "... is not installed".</summary>
    public string Describe() => Missing
        ? Words.Get("S.Core.Dependency.NotInstalled", "{0} {1} is not installed. It {2}.", Dependency.Name, Latest.Version, Dependency.Purpose)
        : Words.Get("S.Core.Dependency.Available", "{0} {1} is available (you have {2}). It {3}.", Dependency.Name, Latest.Version, Installed!, Dependency.Purpose);
}

/// <summary>
/// Checks, fetches and installs dependencies, the same way for each. One
/// request per dependency per check; nothing downloaded until asked.
/// </summary>
public sealed class DependencyManager
{
    /// <summary>Everything the application knows how to keep current. Add here.</summary>
    public static IReadOnlyList<Dependency> All { get; } = [new PawnIoDependency()];

    private static readonly HttpClient Http = new()
    {
        Timeout = TimeSpan.FromSeconds(30),
        DefaultRequestHeaders = { { "User-Agent", "Nextcalibur" } },
    };

    /// <summary>Every dependency that is missing or behind. Empty when all is well or nothing could be reached.</summary>
    public static async Task<IReadOnlyList<DependencyStatus>> CheckAsync(CancellationToken ct = default)
    {
        var needing = new List<DependencyStatus>();
        foreach (var dependency in All)
        {
            try
            {
                var latest = await dependency.LatestAsync(Http, ct);
                if (latest is null) continue;
                var status = new DependencyStatus(dependency, dependency.InstalledVersion(), latest.Value);
                if (status.NeedsAction) needing.Add(status);
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
            {
                // Unreachable or unreadable: the next check's business.
            }
        }
        return needing;
    }

    /// <summary>An installer larger than this is not what it claims to be; the download stops there.</summary>
    private const long LargestInstallerBytes = 200L * 1024 * 1024;

    /// <summary>
    /// Downloads the installer with progress, verifies its signature, runs
    /// it quietly. Returns a sentence for the person; the installer file is
    /// removed either way.
    ///
    /// This process is elevated, and what it downloads it then runs. So the
    /// file goes where only administrators can reach it, under a name nobody
    /// can guess, and stays open - shared for reading only - from the first
    /// byte to the end of the install, so that nothing can swap it between
    /// the signature check and the run.
    /// </summary>
    public static async Task<(bool Ok, string Message)> InstallAsync(DependencyStatus status, IProgress<int> progress, CancellationToken ct = default)
    {
        string? file = null;
        try
        {
            await using (var target = SystemTools.CreateProtectedTemporaryFile(".exe", out file))
            using (var response = await Http.GetAsync(status.Latest.Download, HttpCompletionOption.ResponseHeadersRead, ct))
            {
                response.EnsureSuccessStatusCode();
                var total = response.Content.Headers.ContentLength ?? -1;
                if (total > LargestInstallerBytes) throw new IOException("The download is larger than any installer should be.");
                await using var source = await response.Content.ReadAsStreamAsync(ct);
                var buffer = new byte[81920];
                long done = 0;
                int read;
                while ((read = await source.ReadAsync(buffer, ct)) > 0)
                {
                    done += read;
                    if (done > LargestInstallerBytes) throw new IOException("The download is larger than any installer should be.");
                    await target.WriteAsync(buffer.AsMemory(0, read), ct);
                    if (total > 0) progress.Report((int)(done * 100 / total));
                }
                await target.FlushAsync(ct);
            }
            progress.Report(100);

            // Held open, shared for reading only, while the signature is checked
            // and the installer runs: no rename, no rewrite in between.
            using var guard = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (!status.Dependency.SignatureIsTrusted(file))
                return (false, Words.Get("S.Core.Dependency.Unsigned", "The {0} download is not signed by {1}; it was not installed.", status.Dependency.Name, status.Dependency.ExpectedSigner));

            return status.Dependency.Install(file)
                ? (true, Words.Get("S.Core.Dependency.Installed", "{0} {1} is installed.", status.Dependency.Name, status.Latest.Version))
                : (false, Words.Get("S.Core.Dependency.InstallerFailed", "The {0} installer did not finish successfully.", status.Dependency.Name));
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or IOException or UnauthorizedAccessException)
        {
            return (false, Words.Get("S.Core.Dependency.FetchFailed", "Could not fetch {0}: {1}", status.Dependency.Name, ex.Message));
        }
        finally
        {
            if (file is not null)
                try { File.Delete(file); } catch (IOException) { }
        }
    }
}
