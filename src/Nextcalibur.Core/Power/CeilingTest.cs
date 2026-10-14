namespace Nextcalibur.Core.Power;

/// <summary>One step of a ceiling test: the ceiling tried, and what the processor did under full load at it.</summary>
/// <param name="Value">Percent for Power Saver, MHz for the boosting modes (0 = no limit).</param>
/// <param name="HoldC">The package temperature over the step's second half, the average.</param>
/// <param name="PeakC">The highest package temperature in the step.</param>
/// <param name="Watts">The package power over the second half, the average.</param>
/// <param name="Ghz">The clock over the second half, the average.</param>
public readonly record struct CeilingStep(int Value, double HoldC, double PeakC, double Watts, double Ghz);

/// <summary>
/// The ceiling test's decisions, without the load or the readings (the
/// owner's design of 14 October 2026): for a mode, the ceilings to try from
/// the highest allowed down, the temperature the mode should hold at full
/// load, and the highest ceiling that held it. Never above the mode's upper
/// bound, never above the ceiling in force - a test only ever keeps or
/// lowers it, for a machine whose cooling has got worse (dust) until it is
/// cleaned.
/// </summary>
public static class CeilingTest
{
    /// <summary>Full load per step, and the longest cool-down between steps.</summary>
    public static readonly TimeSpan StepLength = TimeSpan.FromSeconds(45), CoolDown = TimeSpan.FromSeconds(45);

    /// <summary>A peak may run over the target by this much; the hold may not.</summary>
    public const double PeakAllowanceC = 3;

    /// <summary>What each mode should hold at full load (the owner's choice): the quieter the mode, the cooler.</summary>
    public static int TargetC(SystemMode mode) => mode switch
    {
        SystemMode.Office => 65,
        SystemMode.Balanced => 75,
        SystemMode.Gaming => 85,
        _ => 90,
    };

    /// <summary>
    /// The ceilings to try, from the one in force down: Power Saver in five
    /// points to the lowest offered, the boosting modes in 200 MHz from the
    /// limit in force (or the highest clock seen, with no limit) to 2.4 GHz.
    /// </summary>
    /// <param name="mode">The mode tested.</param>
    /// <param name="current">The ceiling in force: percent, or MHz (0 = no limit).</param>
    /// <param name="topClockMhz">With no limit, the highest clock the processor reached - where the steps start below.</param>
    public static IReadOnlyList<int> Steps(SystemMode mode, int current, int topClockMhz)
    {
        var steps = new List<int>();
        if (mode == SystemMode.Office)
        {
            for (var p = Math.Min(current, ModeProfile.UpperPercent(mode)); p >= ModeProfile.LowestPercent; p -= 5) steps.Add(p);
            return steps;
        }

        steps.Add(current);
        var start = current > 0 ? current : (topClockMhz / 200) * 200;
        for (var f = start - 200; f >= 2400; f -= 200) steps.Add(f);
        return steps;
    }

    /// <summary>Whether a step held the mode's target.</summary>
    public static bool Held(SystemMode mode, CeilingStep step) =>
        step.HoldC <= TargetC(mode) && step.PeakC <= TargetC(mode) + PeakAllowanceC;

    /// <summary>The highest ceiling that held, or null when none did. The steps are tried from the highest down, so the first that held.</summary>
    public static int? Choose(SystemMode mode, IEnumerable<CeilingStep> steps) =>
        steps.Where(s => Held(mode, s)).Select(s => (int?)s.Value).FirstOrDefault();

    /// <summary>Whether a value is no higher than another, for this mode's units: no limit (0) is the highest clock.</summary>
    public static bool NotAbove(SystemMode mode, int value, int than) =>
        mode == SystemMode.Office ? value <= than : than == 0 || (value != 0 && value <= than);
}
