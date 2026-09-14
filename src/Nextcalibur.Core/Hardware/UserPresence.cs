using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Whether now is a moment to do anything the person might feel.
///
/// A game running full screen is the case this exists for: a check that
/// downloads, unpacks and asks a question in the middle of a round is worth
/// nothing to anybody, and the tab-out a notification causes is worth less
/// than nothing. Windows already knows - it is the same question its own
/// notifications ask before they appear - so this asks Windows rather than
/// guessing from window titles.
/// </summary>
public static class UserPresence
{
    private static DateTime _lastSessionUnlockUtc = DateTime.MinValue;
    private static bool _isSessionLocked;

    /// <summary>Records the timestamp when the user unlocked their Windows session.</summary>
    public static void RecordSessionUnlock()
    {
        _isSessionLocked = false;
        _lastSessionUnlockUtc = DateTime.UtcNow;
    }

    /// <summary>Records whether the Windows session is currently locked.</summary>
    public static void RecordSessionLock(bool locked)
    {
        _isSessionLocked = locked;
        if (!locked)
        {
            _lastSessionUnlockUtc = DateTime.UtcNow;
        }
    }

    /// <summary>Returns true if the user unlocked their session within the specified window.</summary>
    public static bool WasRecentlyUnlocked(TimeSpan window)
    {
        return DateTime.UtcNow - _lastSessionUnlockUtc < window;
    }

    /// <summary>
    /// True while the person is here and using the machine in a state where
    /// interruptions are unwelcome: a game or presentation has the screen, an
    /// application has asked not to be interrupted, or the session was unlocked
    /// within the last two minutes. Show nothing, cost them nothing.
    /// </summary>
    public static bool WouldRatherNotBeDisturbed()
    {
        if (WasRecentlyUnlocked(TimeSpan.FromMinutes(2))) return true;

        try
        {
            if (SHQueryUserNotificationState(out var state) != 0) return false;
            return state is NotificationState.Busy
                or NotificationState.RunningDirect3dFullScreen
                or NotificationState.PresentationMode;
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return false;
        }
    }

    /// <summary>
    /// True when nobody is at the machine: the session is locked or a screen saver is running.
    /// Quiet work (such as checking for updates or trimming memory under pressure) is welcome,
    /// but notifications and visual reports should stand down.
    /// </summary>
    public static bool NobodyIsWatching()
    {
        if (_isSessionLocked) return true;

        try
        {
            if (SHQueryUserNotificationState(out var state) != 0) return false;
            return state is NotificationState.NotPresent;
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return false;
        }
    }

    /// <summary>What Windows says, for the log and the tests.</summary>
    public static string Describe()
    {
        try
        {
            return SHQueryUserNotificationState(out var state) == 0 ? state.ToString() : "unknown";
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return "unknown";
        }
    }

    private enum NotificationState
    {
        NotPresent = 1,
        Busy = 2,
        RunningDirect3dFullScreen = 3,
        PresentationMode = 4,
        AcceptsNotifications = 5,
        QuietTime = 6,
        RunningWindowsStoreApp = 7,
    }

    [DllImport("shell32.dll")]
    private static extern int SHQueryUserNotificationState(out NotificationState state);
}
