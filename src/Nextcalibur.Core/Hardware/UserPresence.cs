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
    /// <summary>
    /// True while a game or a presentation has the screen, or an
    /// application has asked not to be interrupted. Nothing that costs
    /// network, disk or attention should happen while this is true; the
    /// readings carry on, because they are local and the tray tooltip is
    /// what somebody alt-tabs to see.
    /// </summary>
    public static bool WouldRatherNotBeDisturbed()
    {
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
