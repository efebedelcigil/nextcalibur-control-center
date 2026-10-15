using System.Text.RegularExpressions;

namespace Nextcalibur.Core.Power;

/// <summary>"While this program runs, this mode" - one line of the Settings page's list.</summary>
public sealed class ProgramRule
{
    /// <summary>The process name without ".exe", as Task Manager's Details tab shows it.</summary>
    public string Process { get; set; } = "";
    public SystemMode Mode { get; set; } = SystemMode.Office;
    public bool Enabled { get; set; } = true;

    /// <summary>The game's name, for a rule the game scan added; null for one added by hand.</summary>
    public string? Label { get; set; }

    /// <summary>Added by the game scan (<see cref="GameLibrary"/>) rather than by hand.</summary>
    public bool FromScan { get; set; }

    /// <summary>Where the game is installed, for the "open file location" button; null when not known.</summary>
    public string? Folder { get; set; }

    /// <summary>Where the scan found it: Steam, Epic, GOG, Ubisoft, EA, Riot, Xbox, Microsoft Store, Windows.</summary>
    public string? Source { get; set; }

    /// <summary>Whether Windows' Installed apps lists it (null: not known yet).</summary>
    public bool? InInstalledApps { get; set; }
}

/// <summary>
/// Which rule should be in force, from the programs running and when each
/// started - the decision, without the switching, so it can be tested.
///
/// The owner's request of 12 October 2026, after a MATLAB add-on install
/// ran the fans up in Gaming mode: a program that is known to load the
/// machine can have a quieter mode while it runs (or a game a faster one),
/// and the mode goes back when it closes.
///
/// When more than one rule matches, the program started last wins: it is
/// the one the person has just turned to. On battery with "Office on
/// battery" on, a rule asking for more than Office does not apply - the
/// battery's rule was there first and is about the battery.
/// </summary>
public static partial class ProgramRules
{
    [GeneratedRegex(@"^[A-Za-z0-9 ._\-()+'&]{1,64}$")]
    private static partial Regex ProcessNamePattern();

    /// <summary>
    /// A name as it goes into a rule: trimmed, without ".exe", and only the
    /// characters a process name can have - or null. It ends up inside a WMI
    /// query, so nothing else gets through.
    /// </summary>
    public static string? Normalise(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        var trimmed = name.Trim();
        if (trimmed.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) trimmed = trimmed[..^4];
        trimmed = trimmed.Trim();
        return ProcessNamePattern().IsMatch(trimmed) ? trimmed : null;
    }

    /// <summary>The rule in force, or null for none.</summary>
    /// <param name="rules">The rules, in the order they were added.</param>
    /// <param name="running">Running processes named in rules, with when the newest of each started.</param>
    /// <param name="batteryCeiling">On battery with a battery mode set: no rule for a faster mode applies.</param>
    public static ProgramRule? Winning(IEnumerable<ProgramRule> rules, IReadOnlyDictionary<string, DateTime> running,
        SystemMode? batteryCeiling)
    {
        ProgramRule? best = null;
        var bestStart = DateTime.MinValue;
        foreach (var rule in rules)
        {
            if (!rule.Enabled || Normalise(rule.Process) is not { } name) continue;
            if (!running.TryGetValue(name, out var started)) continue;
            if (batteryCeiling is { } ceiling && !SystemModes.AtMost(rule.Mode, ceiling)) continue;
            if (best is null || started > bestStart) { best = rule; bestStart = started; }
        }
        return best;
    }

    /// <summary>
    /// Whether a started or stopped image is one of the rules'. The trace
    /// events carry the kernel's image name, cut at 15 characters
    /// ("AddOnInstaller.exe" arrives as "AddOnInstaller."), so a 15-character
    /// name matches any rule it is the start of; the application checks
    /// again against the full process name.
    /// </summary>
    public static bool IsWatched(IReadOnlySet<string> names, string? image)
    {
        if (string.IsNullOrEmpty(image)) return false;
        var name = image.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) ? image[..^4] : image;
        if (names.Contains(name)) return true;
        return image.Length == 15 && names.Any(n => (n + ".exe").StartsWith(image, StringComparison.OrdinalIgnoreCase));
    }
}
