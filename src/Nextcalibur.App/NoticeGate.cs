using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;

namespace Nextcalibur.App;

/// <summary>
/// Nothing on screen while a game or a presentation has it (the owner's rule:
/// nothing about anything comes as a notification during a game, and the
/// application must not disturb the player). Every toast and tray balloon
/// goes through here; while something full-screen is in front - or Windows
/// says the person would rather not be disturbed - it waits, and is shown
/// once they are back on the desktop. The same notice twice is shown once.
/// </summary>
internal static class NoticeGate
{
    private const int MostWaiting = 12;
    private static readonly object Lock = new();
    private static readonly List<(string Key, Action Show)> Waiting = [];

    /// <summary>True while a notice now would land on top of a game or a presentation.</summary>
    public static bool ShouldWait
    {
        get
        {
            try { return UserPresence.ForegroundFullScreenProcessId() is not null || UserPresence.WouldRatherNotBeDisturbed(); }
            catch (Exception ex) when (ex is not OutOfMemoryException) { return false; }
        }
    }

    /// <summary>Holds the notice back when it should wait; false when it can be shown now.</summary>
    public static bool Defer(string title, string body, Action show)
    {
        if (!ShouldWait) return false;
        var key = title + "\n" + body;
        lock (Lock)
        {
            Waiting.RemoveAll(w => w.Key == key);
            if (Waiting.Count >= MostWaiting) Waiting.RemoveAt(0);
            Waiting.Add((key, show));
        }
        Log.Info("notice", "Held back until the screen is free: " + title);
        return true;
    }

    /// <summary>From the slow timer: what waited, shown once nothing full-screen is in front.</summary>
    public static void Flush()
    {
        List<(string Key, Action Show)> due;
        lock (Lock)
        {
            if (Waiting.Count == 0) return;
            if (ShouldWait) return;
            due = [.. Waiting];
            Waiting.Clear();
        }
        foreach (var (_, show) in due)
        {
            try { show(); }
            catch (Exception ex) when (ex is not OutOfMemoryException) { Log.Warn("notice", ex.Message); }
        }
    }

    /// <summary>For the tests.</summary>
    internal static int WaitingCount { get { lock (Lock) return Waiting.Count; } }
}
