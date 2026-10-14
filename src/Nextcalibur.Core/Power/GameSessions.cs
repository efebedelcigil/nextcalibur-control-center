namespace Nextcalibur.Core.Power;

/// <summary>
/// What a program full screen in front was, and whether to ask about it once
/// it has closed - the decisions, without the windows, so they can be tested.
///
/// The owner's request of 14 October 2026: a game should not run in Office
/// by accident, without adding every game to the program rules by hand -
/// but a full-screen browser is not a game, and a simple game may be meant
/// for Office. So nothing switches by itself: when a program that held the
/// screen for a while in Office closes, the application asks, in its own
/// window, whether that program should have a mode from now on; the answer
/// is a program rule, or a note not to ask again. Nothing is asked while
/// the game runs - Windows holds notifications back then, and a question
/// over a game is a tab-out - and nothing about games comes as a
/// notification at all.
/// </summary>
public static class GameSessions
{
    /// <summary>A session shorter than this was a glance, not a game.</summary>
    public static readonly TimeSpan Shortest = TimeSpan.FromMinutes(3);

    // Programs that cover the screen and are not games: browsers, players,
    // presentations and calls, remote desktops, the shell. By process name,
    // without ".exe".
    private static readonly HashSet<string> NotGames = new(StringComparer.OrdinalIgnoreCase)
    {
        "brave", "chrome", "msedge", "firefox", "opera", "opera_gx", "vivaldi", "arc", "iexplore", "librewolf", "waterfox", "zen",
        "vlc", "mpc-hc", "mpc-hc64", "mpc-be", "mpc-be64", "mpv", "PotPlayer", "PotPlayer64", "PotPlayerMini", "PotPlayerMini64",
        "wmplayer", "Video.UI", "Microsoft.Media.Player", "kodi", "stremio", "Spotify", "Netflix", "plex", "Plex Media Player",
        "POWERPNT", "soffice", "soffice.bin", "Acrobat", "AcroRd32", "SumatraPDF",
        "Teams", "ms-teams", "Zoom", "Discord", "slack", "obs64", "obs32",
        "mstsc", "msrdc", "vmconnect", "vmware", "VirtualBoxVM", "parsecd", "AnyDesk", "TeamViewer",
        "explorer", "ApplicationFrameHost", "ShellExperienceHost", "SearchHost", "StartMenuExperienceHost", "LockApp",
        "Nextcalibur", "Taskmgr", "mmc", "ScreenClippingHost", "SnippingTool",
    };

    /// <summary>Whether a program that covers the screen is known not to be a game.</summary>
    public static bool IsNotAGame(string processName) =>
        NotGames.Contains(processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ? processName[..^4] : processName);

    /// <summary>The mode the session spent most of its looks in; null with none.</summary>
    public static SystemMode? MostlyIn(IReadOnlyDictionary<SystemMode, int> looks) =>
        looks.Where(l => l.Value > 0).OrderByDescending(l => l.Value).Select(l => (SystemMode?)l.Key).FirstOrDefault();

    /// <summary>
    /// Whether to ask about a program once it closes: it held the screen long
    /// enough, mostly in Office, has no rule yet, and was not answered before.
    /// </summary>
    public static bool ShouldAsk(string processName, TimeSpan held, SystemMode? mostlyIn,
        IEnumerable<ProgramRule> rules, IEnumerable<string> answered)
    {
        // In a quiet mode - Power Saver or Balanced - by accident, most likely.
        if (held < Shortest || mostlyIn is not (SystemMode.Office or SystemMode.Balanced)) return false;
        if (ProgramRules.Normalise(processName) is not { } name || IsNotAGame(name)) return false;
        if (rules.Any(r => string.Equals(ProgramRules.Normalise(r.Process), name, StringComparison.OrdinalIgnoreCase))) return false;
        return !answered.Any(a => string.Equals(a, name, StringComparison.OrdinalIgnoreCase));
    }
}
