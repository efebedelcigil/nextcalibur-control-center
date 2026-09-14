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

    private static readonly CoreLoad CpuLoad = new();
    private static DateTime _lastCpuLoadSample = DateTime.MinValue;
    private static bool _lastCpuWasHeavy;

    /// <summary>
    /// Returns true if the processor is under heavy multi-threaded or multi-core workload
    /// (e.g. 3D rendering, video encoding, compilation, heavy gaming).
    /// </summary>
    public static bool IsHeavyCpuLoad()
    {
        var now = DateTime.UtcNow;
        if (now - _lastCpuLoadSample < TimeSpan.FromSeconds(2))
        {
            return _lastCpuWasHeavy;
        }

        _lastCpuLoadSample = now;
        try
        {
            var loads = CpuLoad.Read();
            if (loads.Length == 0) return _lastCpuWasHeavy = false;

            var avg = CoreLoad.AverageLoad(loads);
            if (avg >= 35.0) return _lastCpuWasHeavy = true;

            var busyCores = 0;
            for (var i = 0; i < loads.Length; i++)
            {
                if (loads[i] >= 75.0) busyCores++;
            }

            return _lastCpuWasHeavy = (busyCores >= 2 && avg >= 20.0);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            return _lastCpuWasHeavy = false;
        }
    }

    /// <summary>
    /// Returns true if the foreground application is running fullscreen or borderless fullscreen
    /// covering an entire display monitor (such as a game, video player, or presentation).
    /// </summary>
    public static bool IsForegroundFullScreenOrBorderless()
    {
        try
        {
            var hwnd = GetForegroundWindow();
            if (hwnd == IntPtr.Zero || hwnd == GetDesktopWindow() || hwnd == GetShellWindow()) return false;

            var sb = new System.Text.StringBuilder(256);
            if (GetClassName(hwnd, sb, sb.Capacity) > 0)
            {
                var cls = sb.ToString();
                if (cls is "Progman" or "WorkerW" or "Shell_TrayWnd" or "Windows.UI.Core.CoreWindow")
                {
                    return false;
                }
            }

            if (!GetWindowRect(hwnd, out var rect)) return false;

            var hMon = MonitorFromWindow(hwnd, MonitorDefaultToNearest);
            if (hMon == IntPtr.Zero) return false;

            var mi = new MonitorInfo { CbSize = Marshal.SizeOf<MonitorInfo>() };
            if (!GetMonitorInfo(hMon, ref mi)) return false;

            return rect.Left <= mi.RcMonitor.Left
                && rect.Top <= mi.RcMonitor.Top
                && rect.Right >= mi.RcMonitor.Right
                && rect.Bottom >= mi.RcMonitor.Bottom;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            return false;
        }
    }

    /// <summary>
    /// True when the user is playing a game (fullscreen or borderless windowed), running a presentation,
    /// or when the system is under heavy computational load (rendering, compiling, encoding).
    /// Nextcalibur should stand down and cause ZERO bottlenecks or interruptions in this state.
    /// </summary>
    public static bool IsGamingOrHeavyLoad()
    {
        return WouldRatherNotBeDisturbed()
            || IsForegroundFullScreenOrBorderless()
            || IsHeavyCpuLoad();
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

    private const uint MonitorDefaultToNearest = 2;

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MonitorInfo
    {
        public int CbSize;
        public Rect RcMonitor;
        public Rect RcWork;
        public uint DwFlags;
    }

    [DllImport("shell32.dll")]
    private static extern int SHQueryUserNotificationState(out NotificationState state);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetShellWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);
}
