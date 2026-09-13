using System.Windows.Threading;
using Velopack;
using Velopack.Sources;

namespace Nextcalibur.App;

/// <summary>
/// Looks for new releases, and installs one when asked.
///
/// Without this the application is a dead end: everybody who installs a version
/// keeps it, with no way to learn there is a newer one. That matters more here
/// than for most software, because this is meant to sit in the notification
/// area and be forgotten - nobody is going to think to check.
///
/// The shape, set by the owner on 12 September 2026 after the first update
/// arrived: find quietly, then ask. A check costs one small request; finding
/// a release costs nothing more until the person says yes. Yes means download
/// with a progress bar in front of them, then restart into the new version.
/// No means a button stays in the corner for later. Nothing is downloaded and
/// nothing restarts without that yes.
/// </summary>
public sealed class UpdateService : IDisposable
{
    public const string Repository = "https://github.com/efebedelcigil/nextcalibur-control-center";

    /// <summary>The same repository, in the two pieces GitHub's API wants.</summary>
    private const string Owner = "efebedelcigil";
    private const string Repo = "nextcalibur-control-center";

    /// <summary>
    /// How long after startup the first check waits. Startup is when the
    /// person is most likely looking; a network round trip can wait a minute.
    /// </summary>
    private static readonly TimeSpan FirstCheckDelay = TimeSpan.FromMinutes(1);

    /// <summary>
    /// How often to look afterwards. Releases are days apart at the fastest;
    /// six hours finds one the same day without troubling GitHub.
    /// </summary>
    private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(6);

    /// <summary>How soon to look again after leaving a game alone. Costs nothing: the check does not run.</summary>
    private static readonly TimeSpan WhileBusyDelay = TimeSpan.FromMinutes(20);

    private readonly UpdateManager? _manager;
    private readonly DispatcherTimer? _timer;
    private bool _busy;
    private bool _disposed;

    /// <summary>Whether the timer is allowed to check. Read on every tick, so a change takes effect at the next one.</summary>
    public Func<bool> AutomaticChecksEnabled { get; set; } = () => true;

    /// <summary>True when this copy was installed and can update itself at all.</summary>
    public bool CanUpdate => _manager is not null;

    /// <summary>The release found and not yet installed, if any.</summary>
    public UpdateInfo? Available { get; private set; }

    /// <summary>The version of <see cref="Available"/>, for the person: "v0.5.2".</summary>
    public string? AvailableVersion => Available?.TargetFullRelease?.Version is { } v ? "v" + v : null;

    public UpdateService()
    {
        // Only a copy installed by the installer can update itself. Running
        // from a build output or a portable unzip, there is nothing to replace
        // and Velopack would throw rather than say so.
        try
        {
            var manager = new UpdateManager(new GithubSource(Repository, null, false));
            if (!manager.IsInstalled) return;
            _manager = manager;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            return;
        }

        _timer = new DispatcherTimer { Interval = FirstCheckDelay };
        _timer.Tick += async (_, _) =>
        {
            _timer.Interval = CheckInterval;
            if (!AutomaticChecksEnabled()) return;

            // Not while a game has the screen. A check is a few requests and
            // a few hundred bytes, but it can end in a download, an
            // installer and a question - none of which belong in the middle
            // of a round, and the tab-out alone would cost more than the
            // update is worth. Windows is asked, rather than guessed at, and
            // the next look is soon rather than in six hours so the end of
            // the session is not missed.
            if (Nextcalibur.Core.Hardware.UserPresence.WouldRatherNotBeDisturbed())
            {
                _timer.Interval = WhileBusyDelay;
                return;
            }

            await CheckAsync(report: false);
            await CheckDependenciesAsync();
        };
    }

    /// <summary>Raised once per release found, with its version. Nothing has been downloaded.</summary>
    public event EventHandler<string>? UpdateFound;

    /// <summary>Raised after a check the person asked for that found nothing, or failed, with what to tell them.</summary>
    public event EventHandler<string>? CheckedByHand;

    /// <summary>Raised for each dependency found missing or behind. Nothing has been downloaded.</summary>
    public event EventHandler<Nextcalibur.Core.Dependencies.DependencyStatus>? DependencyFound;

    /// <summary>Dependencies declined this session; asked again at the next start, not before.</summary>
    private readonly HashSet<string> _declinedDependencies = new(StringComparer.Ordinal);

    /// <summary>Remembers a "no" for the session.</summary>
    public void DeclineDependency(Nextcalibur.Core.Dependencies.DependencyStatus status) =>
        _declinedDependencies.Add(status.Dependency.Id + status.Latest.Version);

    /// <summary>
    /// The same check for what the application depends on. Runs on the
    /// installed copy and the development build alike - a driver does not
    /// care how the application was started.
    /// </summary>
    public async Task<bool> CheckDependenciesAsync()
    {
        var found = false;
        foreach (var status in await Nextcalibur.Core.Dependencies.DependencyManager.CheckAsync())
        {
            if (_declinedDependencies.Contains(status.Dependency.Id + status.Latest.Version)) continue;
            found = true;
            DependencyFound?.Invoke(this, status);
        }
        return found;
    }

    public void Start() => _timer?.Start();

    /// <summary>A check now, because somebody clicked. Reports either way.</summary>
    public async Task CheckNowAsync()
    {
        if (_manager is null)
        {
            // Not an installed copy: only the dependencies can be checked.
            if (!await CheckDependenciesAsync())
                CheckedByHand?.Invoke(this, Strings.Get("S.Update.PortableCurrent"));
            return;
        }

        if (Available is not null)
        {
            // Already found; the caller offers it again.
            UpdateFound?.Invoke(this, AvailableVersion ?? Strings.Get("S.Update.ANewVersion"));
            return;
        }

        var app = await CheckAsync(report: true);
        var dependencies = await CheckDependenciesAsync();
        if (!app && !dependencies)
            CheckedByHand?.Invoke(this, Strings.Get("S.Update.Latest"));
    }

    private async Task<bool> CheckAsync(bool report)
    {
        if (_manager is null || _busy || _disposed) return false;
        _busy = true;

        try
        {
            // The cheap question first. Velopack's check downloads the whole
            // release list - ninety kilobytes, and it grows with every
            // release published - and on a machine that is already current
            // the answer is always the same. Asking GitHub for the newest
            // tag costs three and a half compressed kilobytes, or nothing at
            // all when it answers "not modified", and settles it on every
            // machine nobody has left behind.
            //
            // Fail open: an answer that cannot be had or read means the full
            // check runs, exactly as it did before this.
            var mine = typeof(UpdateService).Assembly.GetName().Version;
            var published = await Nextcalibur.Core.Dependencies.DependencyManager.LatestReleaseAsync(Owner, Repo);
            if (mine is not null && published is not null
                && published <= new Version(mine.Major, mine.Minor, mine.Build))
                return false;

            var available = await _manager.CheckForUpdatesAsync();
            if (available is null) return false;

            // Forward only. Velopack refuses a downgrade by default; this
            // says it a second time in our own code, because the cost of
            // being wrong is an installed copy walked back onto a version
            // whose holes are known and published.
            var current = typeof(UpdateService).Assembly.GetName().Version;
            if (current is not null && available.TargetFullRelease?.Version is { } offered
                && new Version(offered.Major, offered.Minor, offered.Patch) <= new Version(current.Major, current.Minor, current.Build))
            {
                Nextcalibur.Core.Configuration.Log.Warn("update", $"The release page offered {offered}, which is not newer than {current.ToString(3)}; ignored");
                return false;
            }

            var announce = Available is null;
            Available = available;
            if (announce) UpdateFound?.Invoke(this, AvailableVersion ?? Strings.Get("S.Update.ANewVersion"));
            return true;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            // No network, GitHub unreachable, a rate limit. Said only when
            // somebody asked; a timer's failure is the next check's business.
            if (report) CheckedByHand?.Invoke(this, Strings.Get("S.Update.Unreachable") + " " + ex.Message);
            return false;
        }
        finally
        {
            _busy = false;
        }
    }

    /// <summary>
    /// Downloads the found release, reporting progress 0-100, and restarts
    /// the application into it. Returns only on failure; on success the
    /// process is gone.
    /// </summary>
    /// <exception cref="InvalidOperationException">Nothing has been found.</exception>
    public async Task DownloadAndRestartAsync(IProgress<int> progress)
    {
        if (_manager is null || Available is not { } update)
            throw new InvalidOperationException("There is no update to install.");

        await _manager.DownloadUpdatesAsync(update, p => progress.Report(p));
        _manager.ApplyUpdatesAndRestart(update);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _timer?.Stop();
    }
}
