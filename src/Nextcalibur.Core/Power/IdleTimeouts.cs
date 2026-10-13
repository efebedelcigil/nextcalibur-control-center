namespace Nextcalibur.Core.Power;

/// <summary>"Turn off the display after" and "sleep after", plugged in and on battery.</summary>
public enum IdleSetting
{
    DisplayOff,
    Sleep,
}

/// <summary>One plan's timeouts, in seconds; 0 is never.</summary>
public readonly record struct Timeouts(PlanTuning.AcDc Display, PlanTuning.AcDc Sleep)
{
    public PlanTuning.AcDc this[IdleSetting setting] => setting == IdleSetting.DisplayOff ? Display : Sleep;

    public Timeouts With(IdleSetting setting, PlanTuning.AcDc value) =>
        setting == IdleSetting.DisplayOff ? this with { Display = value } : this with { Sleep = value };
}

/// <summary>
/// The display and sleep timeouts of each mode's plan.
///
/// The owner's request of 14 October 2026: set them per mode, plugged in and
/// on battery, in step with Windows - each mode is its own plan, so the page
/// and the Control Panel's plan settings are the same values, and a change
/// in either shows in the other; each mode keeps its own.
///
/// Nothing here writes on its own: a value is written when it is chosen on
/// the Settings page, and only that value.
/// </summary>
public static class IdleTimeouts
{
    /// <summary>The sleep group, and its "sleep after" (STANDBYIDLE, seconds).</summary>
    public static readonly Guid SubSleep = new("238c9fa8-0aad-41ed-83f4-97be242c8f20");
    public static readonly Guid StandbyIdle = new("29f6c1db-86da-48c5-9fdb-f2b67b1f44da");

    public static Guid SettingOf(IdleSetting setting) =>
        setting == IdleSetting.DisplayOff ? PlanTuning.VideoIdle : StandbyIdle;

    public static Timeouts? Read(Guid plan) =>
        PlanTuning.Read(plan, PlanTuning.VideoIdle) is { } display && PlanTuning.Read(plan, StandbyIdle) is { } sleep
            ? new Timeouts(display, sleep)
            : null;

    public static bool Write(Guid plan, IdleSetting setting, PlanTuning.AcDc value) =>
        value.Ac >= 0 && value.Dc >= 0 && PlanTuning.Write(plan, SettingOf(setting), value);

    /// <summary>A plan's value with one half - plugged in or on battery - replaced.</summary>
    public static PlanTuning.AcDc Carry(PlanTuning.AcDc theirs, bool pluggedIn, int seconds) =>
        pluggedIn ? theirs with { Ac = seconds } : theirs with { Dc = seconds };

    /// <summary>The choices offered, Windows' own list, with the current value added when it is not on it; "never" last.</summary>
    public static IReadOnlyList<int> Choices(int current)
    {
        int[] minutes = [1, 2, 3, 5, 10, 15, 20, 25, 30, 45, 60, 120, 180, 240, 300];
        var list = minutes.Select(m => m * 60).ToList();
        if (current > 0 && !list.Contains(current)) list.Add(current);
        list.Sort();
        list.Add(0);
        return list;
    }
}
