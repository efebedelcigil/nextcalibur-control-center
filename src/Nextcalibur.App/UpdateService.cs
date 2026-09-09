using System.Windows.Threading;
using Velopack;
using Velopack.Sources;

namespace Nextcalibur.App;

/// <summary>
/// Looks for new releases and stages them.
///
/// Without this the application is a dead end: everybody who installs a version
/// keeps it, with no way to learn there is a newer one. That matters more here
/// than for most software, because this is meant to sit in the notification
/// area and be forgotten — nobody is going to think to check.
///
/// The behaviour is deliberately quiet. It checks, downloads, and says once
/// that a version is waiting; it never interrupts, never restarts the
/// application under the user, and never shows a progress bar for something
/// nobody asked for. The update is written on the way out, when the user closes
/// the application themselves.
/// </summary>
public sealed class UpdateService : IDisposable
{
    private const string Repository = "https://github.com/efebedelcigil/nextcalibur-control-center";

    /// <summary>
    /// How long after startup the first check waits.
    ///
    /// Startup is the one moment this application is allowed to be busy, and it
    /// is also when the user is most likely to be looking at it. A network
    /// round trip can wait a minute.
    /// </summary>
    private static readonly TimeSpan FirstCheckDelay = TimeSpan.FromMinutes(1);

    /// <summary>
    /// How often to look afterwards. Releases here arrive every few days at
    /// best; asking more often would be traffic spent on nothing.
    /// </summary>
    private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(6);

    private readonly UpdateManager? _manager;
    private readonly DispatcherTimer? _timer;
    private bool _busy;
    private bool _announced;
    private bool _disposed;

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
            await CheckAsync();
        };
    }

    /// <summary>Raised once, when a version has been downloaded and is waiting.</summary>
    public event EventHandler<string>? UpdateReady;

    /// <summary>True when a version is staged and will be written on the way out.</summary>
    public bool IsReady => _manager?.UpdatePendingRestart is not null;

    public void Start()
    {
        if (_manager is null) return;

        // A previous run may already have staged one that was never applied,
        // because the application was killed rather than closed.
        if (_manager.UpdatePendingRestart is { } staged)
            Announce(staged.Version?.ToString());

        _timer?.Start();
    }

    private async Task CheckAsync()
    {
        if (_manager is null || _busy || _disposed) return;
        _busy = true;

        try
        {
            var available = await _manager.CheckForUpdatesAsync();
            if (available is null) return;

            await _manager.DownloadUpdatesAsync(available);
            Announce(available.TargetFullRelease?.Version?.ToString());
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            // No network, GitHub unreachable, a release without assets, a rate
            // limit. None of it is worth a word to the user: the next check is
            // six hours away and the application works regardless.
        }
        finally
        {
            _busy = false;
        }
    }

    private void Announce(string? version)
    {
        if (_announced || string.IsNullOrWhiteSpace(version)) return;
        _announced = true;
        UpdateReady?.Invoke(this, version);
    }

    /// <summary>
    /// Writes a staged update after this process exits.
    ///
    /// Called on the way out rather than when the download finishes: replacing
    /// files under a running application means restarting it, and restarting it
    /// under somebody who is using it is worse than waiting.
    /// </summary>
    public void ApplyOnExit()
    {
        if (_manager?.UpdatePendingRestart is not { } staged) return;

        try
        {
            _manager.WaitExitThenApplyUpdates(staged, silent: true, restart: false);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            // The application is closing. A failed update is the next run's
            // problem, and it will find the same release waiting.
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _timer?.Stop();
    }
}
