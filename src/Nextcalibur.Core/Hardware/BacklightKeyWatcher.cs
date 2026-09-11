using System.Management;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Hears Fn+Space.
///
/// The firmware raises <c>GMC_WMIEvent</c> when the key is pressed, with the
/// new backlight step in the first byte: 0 off, 1 dim, 2 full - the same
/// numbers as <see cref="LedBrightness"/>. The firmware has already applied it
/// by then; this exists so the application knows, and so its own next write
/// carries the level the person chose rather than the one it assumed.
///
/// Event-driven: costs nothing between presses. Needs the event class to be
/// readable by this account, which <see cref="MailboxAccess.Grant()"/> arranges
/// alongside the mailbox itself.
/// </summary>
public sealed class BacklightKeyWatcher : IDisposable
{
    private readonly ManagementEventWatcher _watcher;

    /// <summary>Raised on the watcher's thread with the level the firmware moved to.</summary>
    public event Action<LedBrightness>? LevelChanged;

    /// <exception cref="ManagementException">The class is not readable by this account.</exception>
    /// <exception cref="UnauthorizedAccessException">Same, phrased by a different layer.</exception>
    public BacklightKeyWatcher()
    {
        _watcher = new ManagementEventWatcher(
            new ManagementScope(@"root\WMI"),
            new EventQuery("SELECT * FROM GMC_WMIEvent"));
        _watcher.EventArrived += OnEvent;
        _watcher.Start();
    }

    private void OnEvent(object sender, EventArrivedEventArgs e)
    {
        if (e.NewEvent["EventDetail"] is not byte[] { Length: > 0 } detail) return;
        if (Parse(detail[0]) is { } level) LevelChanged?.Invoke(level);
    }

    /// <summary>The first byte as a level, or null for anything not measured.</summary>
    public static LedBrightness? Parse(byte first) => first switch
    {
        0 => LedBrightness.Off,
        1 => LedBrightness.Half,
        2 => LedBrightness.Full,
        _ => null,
    };

    public void Dispose()
    {
        _watcher.EventArrived -= OnEvent;
        try { _watcher.Stop(); } catch (ManagementException) { }
        _watcher.Dispose();
    }
}
