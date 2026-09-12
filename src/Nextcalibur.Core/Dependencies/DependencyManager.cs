using System.Net.Http;

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
        ? $"{Dependency.Name} {Latest.Version} is not installed. It {Dependency.Purpose}."
        : $"{Dependency.Name} {Latest.Version} is available (you have {Installed}). It {Dependency.Purpose}.";
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

    /// <summary>
    /// Downloads the installer with progress, verifies its signature, runs
    /// it quietly. Returns a sentence for the person; the installer file is
    /// removed either way.
    /// </summary>
    public static async Task<(bool Ok, string Message)> InstallAsync(DependencyStatus status, IProgress<int> progress, CancellationToken ct = default)
    {
        var file = Path.Combine(Path.GetTempPath(), $"nextcalibur-{status.Dependency.Id}-{status.Latest.Version}.exe");
        try
        {
            using (var response = await Http.GetAsync(status.Latest.Download, HttpCompletionOption.ResponseHeadersRead, ct))
            {
                response.EnsureSuccessStatusCode();
                var total = response.Content.Headers.ContentLength ?? -1;
                await using var source = await response.Content.ReadAsStreamAsync(ct);
                await using var target = File.Create(file);
                var buffer = new byte[81920];
                long done = 0;
                int read;
                while ((read = await source.ReadAsync(buffer, ct)) > 0)
                {
                    await target.WriteAsync(buffer.AsMemory(0, read), ct);
                    done += read;
                    if (total > 0) progress.Report((int)(done * 100 / total));
                }
            }
            progress.Report(100);

            if (!status.Dependency.SignatureIsTrusted(file))
                return (false, $"The {status.Dependency.Name} download is not signed by {status.Dependency.ExpectedSigner}; it was not installed.");

            return status.Dependency.Install(file)
                ? (true, $"{status.Dependency.Name} {status.Latest.Version} is installed.")
                : (false, $"The {status.Dependency.Name} installer did not finish successfully.");
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or IOException or UnauthorizedAccessException)
        {
            return (false, $"Could not fetch {status.Dependency.Name}: {ex.Message}");
        }
        finally
        {
            try { File.Delete(file); } catch (IOException) { }
        }
    }
}
