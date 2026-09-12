using System.Diagnostics;
using Microsoft.Win32;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Detects the vendor's own Control Center, and says how hard we should be
/// polling while it is there.
///
/// Both applications drive the same firmware mailbox, and it holds one command
/// at a time. Neither can lock the other out, so the only way to be a good
/// neighbour is to ask for less: poll further apart and give up sooner, leaving
/// the mailbox free between our reads. Sensor readings are worth a second of
/// staleness; making the other application's readings stall is not.
/// </summary>
public static class VendorSoftware
{
    private static readonly string[] ProcessNames = ["ControlCenter", "ControlCenterDaemon"];

    /// <summary>How often to sample when we have the mailbox to ourselves.</summary>
    public const int NormalPollMs = 2000;

    /// <summary>
    /// How often to sample while the vendor software is running. It polls the
    /// thermal block about every six seconds, so this stays clear of its rhythm
    /// rather than sitting on top of it.
    /// </summary>
    public const int PolitePollMs = 7000;

    /// <summary>Retry budget when we have the mailbox to ourselves.</summary>
    public const int NormalAttempts = 8;

    /// <summary>
    /// Retry budget while sharing. Retrying hard is precisely what turns a
    /// collision into a stall for both applications.
    /// </summary>
    public const int PoliteAttempts = 3;

    /// <summary>
    /// How long a detection result is reused.
    ///
    /// This matters more than it looks. Answering the question costs a full
    /// process enumeration, and the answer is needed on every sensor read — so
    /// without a cache the hot path walked every process on the machine twice a
    /// second. That leaked handles at roughly four a second and showed up as
    /// steadily climbing memory.
    /// </summary>
    private static readonly TimeSpan CacheFor = TimeSpan.FromSeconds(4);

    private static readonly object Gate = new();
    private static DateTime _checkedAt = DateTime.MinValue;
    private static bool _running;

    /// <summary>True when the vendor Control Center or its daemon is running.</summary>
    public static bool IsRunning()
    {
        lock (Gate)
        {
            var now = DateTime.UtcNow;
            if (now - _checkedAt < CacheFor) return _running;

            _running = Detect();
            _checkedAt = now;
            return _running;
        }
    }

    /// <summary>Forces the next <see cref="IsRunning"/> call to look again.</summary>
    public static void Invalidate()
    {
        lock (Gate) _checkedAt = DateTime.MinValue;
    }

    /// <summary>
    /// True when the vendor Control Center is installed, whether or not it is
    /// running at this moment.
    ///
    /// A different question from <see cref="IsRunning"/> and it deserves a
    /// different answer. Running means the two are colliding now; installed
    /// means they will, the next time it starts - which on this software is at
    /// every logon, through a scheduled task.
    ///
    /// Checked by its uninstall entry rather than by looking for files: that is
    /// the record Windows keeps, it survives the application being moved, and
    /// it is what Programs and Features acts on.
    /// </summary>
    public static bool IsInstalled() => FindInstallation() is not null;

    /// <summary>
    /// What Windows records about the installed vendor software: its displayed
    /// name and how it would be uninstalled.
    ///
    /// The uninstall command is offered so somebody can act on the
    /// recommendation without hunting through Settings. Nextcalibur never runs
    /// it: removing another application is not a decision this one gets to make.
    /// </summary>
    public static (string Name, string? UninstallCommand)? FindInstallation()
    {
        foreach (var root in new[]
                 {
                     @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall",
                     @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall",
                 })
        {
            using var key = Registry.LocalMachine.OpenSubKey(root);
            if (key is null) continue;

            foreach (var name in key.GetSubKeyNames())
            {
                using var entry = key.OpenSubKey(name);
                if (entry?.GetValue("DisplayName") is not string display) continue;
                if (!display.Contains("EXCALIBUR CONTROL CENTER", StringComparison.OrdinalIgnoreCase)) continue;

                return (display, entry.GetValue("UninstallString") as string);
            }
        }

        return null;
    }

    // One instance for the life of the process: it remembers which ids it
    // has looked at, which is what makes a look cheap (see ProcessPresence).
    private static readonly ProcessPresence Presence =
        new(ProcessNames.Select(n => n + ".exe").ToArray());

    private static bool Detect() => Presence.AnyRunning();

    /// <summary>The polling interval to use right now, in milliseconds.</summary>
    public static int PollIntervalMs(int preferred) =>
        IsRunning() ? Math.Max(preferred, PolitePollMs) : preferred;

    /// <summary>The retry budget to use right now.</summary>
    public static int Attempts() => IsRunning() ? PoliteAttempts : NormalAttempts;
}
