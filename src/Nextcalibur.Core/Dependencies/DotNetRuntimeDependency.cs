using System.Net.Http;
using System.Text.Json;
using Microsoft.Win32;

namespace Nextcalibur.Core.Dependencies;

/// <summary>
/// The .NET desktop runtime this application runs on, kept current like
/// anything else it depends on.
///
/// It used to be carried inside the package. That was decided on
/// 11 September 2026 for a good reason - Velopack's own runtime bootstrap
/// was watched failing silently on a clean Windows, leaving somebody with a
/// finished wizard and no application - and it turned out to have a cost
/// nobody had priced: nothing on a machine patches a runtime that lives
/// inside an application, so 0.5.3 carried .NET 8.0.30 for as long as it
/// was installed, five days after 8.0.31 fixed five security holes.
///
/// So the runtime is a dependency now, and the two things that failed then
/// are both gone: the wizard installs it itself, the way it installs
/// PawnIO, and this class keeps it current afterwards through the same
/// check, the same button and the same notification as everything else. On
/// top of that, Windows patches a shared runtime on its own - which is the
/// whole point of not carrying one.
///
/// What is downloaded is Microsoft's own installer, from Microsoft's own
/// release index: verified against the SHA-512 that index publishes and
/// against its Authenticode signature before it is run.
/// </summary>
public sealed class DotNetRuntimeDependency : Dependency
{
    /// <summary>
    /// The channel this application is built against - the major and minor
    /// of the target framework in <c>Nextcalibur.App.csproj</c>. A test
    /// holds the two together.
    /// </summary>
    public const string Channel = "8.0";

    /// <summary>Nothing older than this can run the application.</summary>
    public static readonly Version Minimum = new(8, 0, 0);

    public override string Id => "dotnet";
    public override string Name => Words.Get("S.Core.DotNet.Name", ".NET {0} desktop runtime", Channel);
    public override string Purpose => Words.Get("S.Core.DotNet.Purpose", "is what Nextcalibur runs on");
    /// <summary>
    /// The organisation, not the common name. Microsoft signs .NET with
    /// <c>CN=.NET, O=Microsoft Corporation, L=Redmond, S=Washington, C=US</c> -
    /// the common name is the product and moves with it, while the
    /// organisation is what the certificate authority actually validated.
    /// Expecting <c>CN=Microsoft Corporation</c> refused every genuine
    /// download, which is what 0.5.4 shipped doing; the installer wizard had
    /// it right from the start and the application did not.
    /// </summary>
    public override string ExpectedSigner => "O=Microsoft Corporation";
    public override string SilentInstallArguments => "/install /quiet /norestart";

    /// <summary>Not ours to remove: every other .NET application on the machine uses it.</summary>
    public override bool MayBeRemoved => false;

    /// <summary>Removed by Windows' own list of installed programs, not by this application.</summary>
    public override string UninstallKey => string.Empty;

    /// <summary>
    /// The runtime this application actually runs on, or null when there is
    /// none that could - in which case the application is not running, so
    /// the question is really the installer's.
    ///
    /// Which is the newest patch of the channel it was built for, not the
    /// newest runtime on the machine. .NET only rolls forward to another
    /// major when the one asked for is missing, so a machine with 8.0.30 and
    /// 10.0.0 runs this on 8.0.30 - and reporting 10.0.0 would say the
    /// runtime is current while the one in use is a security patch behind.
    /// That is exactly what it did say, on this machine, on 13 September.
    /// </summary>
    public override Version? InstalledVersion()
    {
        try
        {
            var folder = Path.Combine(DotNetRoot(), "shared", "Microsoft.WindowsDesktop.App");
            if (!Directory.Exists(folder)) return null;

            var channel = Version.Parse(Channel + ".0");
            Version? inChannel = null;    // what this runs on
            Version? anyNewer = null;     // what it would roll forward to

            foreach (var each in Directory.EnumerateDirectories(folder))
            {
                if (!Version.TryParse(Path.GetFileName(each), out var version) || version < Minimum) continue;

                if (version.Major == channel.Major && version.Minor == channel.Minor)
                {
                    if (inChannel is null || version > inChannel) inChannel = version;
                }
                else if (anyNewer is null || version > anyNewer)
                {
                    anyNewer = version;
                }
            }
            return inChannel ?? anyNewer;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            return null;
        }
    }

    /// <summary>Where .NET is installed: what the installer recorded, or the usual place.</summary>
    private static string DotNetRoot()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\dotnet\Setup\InstalledVersions\x64");
            if (key?.GetValue("InstallLocation") is string location && location.Length > 0) return location;
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException or IOException)
        {
        }
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "dotnet");
    }

    /// <summary>
    /// The newest patch of the channel, from Microsoft's index of channels:
    /// 813 bytes compressed, and nothing when it has not changed.
    ///
    /// The exact link is worked out from the version - Microsoft's own
    /// index uses the same shape - so the check never reads the channel's
    /// own index, which is a megabyte and a half of JSON (310 kB
    /// compressed) and carries the hashes. That one is read once, in
    /// <see cref="ResolveBeforeDownloadAsync"/>, when somebody has said yes
    /// to the offer. A check four times a day should not cost what a
    /// download costs.
    /// </summary>
    public override async Task<ReleaseFile?> LatestAsync(HttpClient http, CancellationToken ct)
    {
        var latest = LatestInIndex(await ReadJsonAsync(http, ChannelsUrl, ct), Channel);
        return latest is null ? null : new ReleaseFile(latest, DownloadFor(latest));
    }

    /// <summary>
    /// The hash, which is worth 310 kB once and nothing four times a day.
    /// Microsoft publishes a SHA-512 for every file in the channel's index;
    /// if it cannot be had, the download still has to be signed by
    /// Microsoft, which is the check that was there before this one.
    /// </summary>
    public override async Task<ReleaseFile> ResolveBeforeDownloadAsync(HttpClient http, ReleaseFile found, CancellationToken ct)
    {
        if (found.Sha512 is { Length: > 0 }) return found;

        var exact = Newest(await ReadJsonAsync(http, ReleasesUrl, ct));
        return exact is not null && exact.Version == found.Version ? exact : found;
    }

    /// <summary>
    /// A JSON answer, or a null element when there is none to be had. Asked
    /// conditionally, so an unchanged file is answered with 304 and no body.
    /// Never throws: this runs from a timer.
    /// </summary>
    private static async Task<JsonElement> ReadJsonAsync(HttpClient http, string url, CancellationToken ct)
    {
        try
        {
            if (await Conditional.GetAsync(http, url, ct) is not { } body) return default;
            using var json = JsonDocument.Parse(body);
            return json.RootElement.Clone();
        }
        catch (Exception ex) when (ex is JsonException or HttpRequestException or TaskCanceledException or IOException)
        {
            return default;
        }
    }

    /// <summary>Where a given version lives, in the shape Microsoft's own index uses.</summary>
    internal static Uri DownloadFor(Version version) =>
        new($"https://builds.dotnet.microsoft.com/dotnet/WindowsDesktop/{version}/windowsdesktop-runtime-{version}-win-x64.exe");

    /// <summary>The seven-kilobyte index of channels; it names the newest patch of each.</summary>
    private const string ChannelsUrl = "https://raw.githubusercontent.com/dotnet/core/main/release-notes/releases-index.json";

    /// <summary>The megabyte-and-a-half index of this channel's releases.</summary>
    private static string ReleasesUrl =>
        $"https://raw.githubusercontent.com/dotnet/core/main/release-notes/{Channel}/releases.json";

    /// <summary>The newest patch a channel has, read from the index of channels.</summary>
    internal static Version? LatestInIndex(JsonElement root, string channel)
    {
        if (root.ValueKind != JsonValueKind.Object
            || !root.TryGetProperty("releases-index", out var channels) || channels.ValueKind != JsonValueKind.Array)
            return null;

        foreach (var each in channels.EnumerateArray())
        {
            if (each.ValueKind != JsonValueKind.Object) continue;
            if (!each.TryGetProperty("channel-version", out var name) || name.ValueKind != JsonValueKind.String) continue;
            if (name.GetString() != channel) continue;
            if (!each.TryGetProperty("latest-release", out var latest) || latest.ValueKind != JsonValueKind.String) continue;
            return Version.TryParse(latest.GetString(), out var version) && version >= Minimum ? version : null;
        }
        return null;
    }

    /// <summary>
    /// Reads the newest release out of the channel's index and finds the
    /// x64 desktop runtime installer in it. Written to answer null rather
    /// than throw on anything unexpected: this runs from a timer.
    /// </summary>
    internal static ReleaseFile? Newest(JsonElement root)
    {
        if (root.ValueKind != JsonValueKind.Object
            || !root.TryGetProperty("releases", out var releases) || releases.ValueKind != JsonValueKind.Array)
            return null;

        foreach (var release in releases.EnumerateArray())
        {
            if (release.ValueKind != JsonValueKind.Object) continue;
            if (!release.TryGetProperty("windowsdesktop", out var desktop) || desktop.ValueKind != JsonValueKind.Object) continue;
            if (!desktop.TryGetProperty("version", out var versionText) || versionText.ValueKind != JsonValueKind.String) continue;
            if (!Version.TryParse(versionText.GetString(), out var version) || version < Minimum) continue;
            if (!desktop.TryGetProperty("files", out var files) || files.ValueKind != JsonValueKind.Array) continue;

            foreach (var file in files.EnumerateArray())
            {
                if (file.ValueKind != JsonValueKind.Object) continue;
                if (!file.TryGetProperty("name", out var name) || name.ValueKind != JsonValueKind.String) continue;
                if (!name.GetString()!.EndsWith("win-x64.exe", StringComparison.OrdinalIgnoreCase)) continue;
                if (!file.TryGetProperty("url", out var url) || url.ValueKind != JsonValueKind.String) continue;
                if (!IsMicrosoftDownload(url.GetString(), out var download)) continue;

                var hash = file.TryGetProperty("hash", out var h) && h.ValueKind == JsonValueKind.String ? h.GetString() : null;
                return new ReleaseFile(version, download, hash);
            }
        }
        return null;
    }

    /// <summary>
    /// Whether a link is one of Microsoft's own download hosts, over HTTPS.
    /// The index is fetched over TLS, so this is a second lock on the same
    /// door - and what comes through it is run as administrator.
    /// </summary>
    internal static bool IsMicrosoftDownload(string? link, out Uri url)
    {
        url = null!;
        if (!Uri.TryCreate(link, UriKind.Absolute, out var candidate)) return false;
        if (candidate.Scheme != Uri.UriSchemeHttps) return false;

        foreach (var host in Hosts)
            if (candidate.Host.Equals(host, StringComparison.OrdinalIgnoreCase))
            {
                url = candidate;
                return true;
            }
        return false;
    }

    private static readonly string[] Hosts =
    [
        "builds.dotnet.microsoft.com",
        "download.visualstudio.microsoft.com",
        "dotnetcli.azureedge.net",
    ];
}
