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
/// The shape, set by the owner on 28 September 2026 (it replaces "find
/// quietly, then ask" of 12 September): checking cannot be switched off, and
/// an update can be put off once but not refused. A release reached machines
/// only when somebody clicked "Check now" - the checks were six hours apart. Now the
/// question is asked every quarter of an hour and after every wake, and it
/// costs next to nothing: GitHub answers "not modified", with no body, to a
/// conditional request, and documents that answer as not counting against
/// its rate limit.
/// Nothing is downloaded here: the window asks first, and the second time
/// installs without asking (see MainWindow.OnUpdateFound).
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
    /// How often to look afterwards. The look is a conditional request that
    /// GitHub answers "not modified" until something is published, so a
    /// quarter of an hour costs nothing worth counting.
    /// </summary>
    private static readonly TimeSpan CheckInterval = TimeSpan.FromMinutes(15);

    /// <summary>
    /// The dependencies are asked after less often: several sources, and a
    /// driver or a runtime a few hours late is no emergency.
    /// </summary>
    private static readonly TimeSpan DependencyInterval = TimeSpan.FromHours(6);

    /// <summary>After a wake the network needs a moment before the first look.</summary>
    private static readonly TimeSpan AfterWakeDelay = TimeSpan.FromMinutes(1);

    /// <summary>Passed to the restarted copy when the window was away, so it stays away.</summary>
    public const string StayHiddenArgument = "--stay-hidden";

    private DateTime _lastDependencyCheck = DateTime.MinValue;

    private readonly UpdateManager? _manager;
    private readonly DispatcherTimer? _timer;
    private bool _busy;
    private bool _disposed;

    /// <summary>True when this copy was installed and can update itself at all.</summary>
    public bool CanUpdate => _manager is not null;

    /// <summary>The release found and not yet installed, if any.</summary>
    public UpdateInfo? Available { get; private set; }

    /// <summary>True once <see cref="Available"/> has been downloaded and only waits to be applied.</summary>
    public bool Downloaded { get; private set; }

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
            try
            {
                // Only when it differs: assigning the interval restarts the countdown.
                if (_timer.Interval != CheckInterval) _timer.Interval = CheckInterval;

                // Checked even while a game has the screen: one conditional
                // request. What waits for the game is the offer, and that is
                // the window's call.
                await CheckAsync(report: false);

                // The dependencies' answer is a question on screen, so it
                // waits for a quiet moment and comes at most every six hours.
                if (DateTime.UtcNow - _lastDependencyCheck >= DependencyInterval
                    && !Nextcalibur.Core.Hardware.UserPresence.WouldRatherNotBeDisturbed()
                    && !Nextcalibur.Core.Hardware.UserPresence.NobodyIsWatching()
                    && !Nextcalibur.Core.Hardware.UserPresence.IsGamingOrHeavyLoad())
                {
                    _lastDependencyCheck = DateTime.UtcNow;
                    await CheckDependenciesAsync();
                }
            }
            catch (Exception ex) when (ex is not OutOfMemoryException)
            {
                Nextcalibur.Core.Configuration.Log.Warn("updates", $"Update check failed: {ex.Message}");
            }
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
        try
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
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Nextcalibur.Core.Configuration.Log.Warn("dependencies", $"Dependency check failed: {ex.Message}");
            return false;
        }
    }

    public void Start() => _timer?.Start();

    /// <summary>
    /// Looks again in a minute - after a wake, when a machine that slept
    /// through a release should not wait out the rest of a quarter hour.
    /// </summary>
    public void CheckSoon()
    {
        if (_timer is null || _disposed) return;
        _timer.Stop();
        _timer.Interval = AfterWakeDelay;
        _timer.Start();
    }

    /// <summary>
    /// A release downloaded by an earlier run and never applied - that run
    /// was waiting on a graphics change's restart, or ended first. Applied
    /// now, before anything else starts, and the process restarts into it.
    /// Tried once per version: an apply that fails and comes back to the old
    /// version must not turn every start into another attempt.
    /// </summary>
    /// <returns>False when there is nothing to apply, or it was already tried.</returns>
    public bool ApplyPendingAtStart(Nextcalibur.Core.Configuration.AppSettings settings, string[] restartArgs)
    {
        if (_manager is null) return false;
        try
        {
            if (_manager.UpdatePendingRestart is not { } pending) return false;
            var version = pending.Version?.ToString() ?? string.Empty;
            if (settings.UpdateApplyTried == version) return false;

            settings.UpdateApplyTried = version;
            settings.Save();
            Nextcalibur.Core.Configuration.Log.Info("update", $"Applying {version}, downloaded by an earlier run");
            _manager.ApplyUpdatesAndRestart(pending, restartArgs);
            return true;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Nextcalibur.Core.Configuration.Log.Warn("update", "Could not apply the downloaded update at start: " + ex.Message);
            return false;
        }
    }

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
            if (!announce && Available?.TargetFullRelease?.Version != available.TargetFullRelease?.Version)
            {
                // A newer release than the one found before, perhaps already downloaded.
                announce = true;
                Downloaded = false;
            }
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
    /// Downloads the found release, reporting progress 0-100 when asked to.
    /// Velopack checks the package against the release's hash before keeping it.
    /// </summary>
    /// <exception cref="InvalidOperationException">Nothing has been found.</exception>
    public async Task DownloadAsync(IProgress<int>? progress = null)
    {
        if (_manager is null || Available is not { } update)
            throw new InvalidOperationException("There is no update to install.");
        if (Downloaded) { progress?.Report(100); return; }

        await _manager.DownloadUpdatesAsync(update, p => progress?.Report(p));
        Downloaded = true;
    }

    /// <summary>Restarts into the downloaded release. Returns only on failure; on success the process is gone.</summary>
    /// <exception cref="InvalidOperationException">Nothing has been downloaded.</exception>
    public void ApplyAndRestart(string[] restartArgs)
    {
        if (_manager is null || Available is not { } update || !Downloaded)
            throw new InvalidOperationException("There is no downloaded update to install.");
        _manager.ApplyUpdatesAndRestart(update.TargetFullRelease, restartArgs);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _timer?.Stop();
    }
}
