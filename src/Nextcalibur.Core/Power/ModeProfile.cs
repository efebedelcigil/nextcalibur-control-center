using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Power;

/// <summary>
/// The processor settings each mode's plan is given, beyond the floor and
/// the ceiling - taken from what Windows itself chose for this machine.
///
/// The vendor's Office and Gaming plans are Balanced with a different floor
/// or ceiling and nothing else (read 14 October 2026): Office boosted as
/// hard as Gaming and put its threads on the performance cores first. The
/// first profile (0.6.2) gave every mode fixed numbers; the owner's rule
/// the same day was that the values must come from the machine, since they
/// differ from one to the next. So each mode starts from one of Windows' own
/// built-in plans, read at every start - they exist, hidden or not, on every
/// Windows - and only what the mode is for is added:
///
/// - <b>Balanced</b> is Windows' Balanced, as Windows set it here, with a
///   floor that never pins the processor.
/// - <b>Gaming</b> sits between Balanced and High performance (the owner's
///   rule, 14 October 2026): Balanced, with an energy preference plugged in
///   no higher than the top of Intel's "performance" band - 64 of 255, 25%
///   (intel_pstate's bands; Intel moved Alder Lake mobile's balanced
///   point to 102) - and an aggressive boost.
/// - <b>Performance</b> is High performance, but with Balanced's floor -
///   High performance holds the processor at 100% plugged in.
/// - <b>Office</b> is Power saver, never less efficient than Balanced, with
///   the efficient variant of its boost mode (measured: the average clock
///   at idle 4.3 -> 3.4 GHz), both core types held to 90% - the boost off (see PlanTuning.CoolCeiling), and - on a
///   processor that has two kinds of core - work on the efficient ones first.
/// </summary>
public static class ModeProfile
{
    // The settings, by the names powercfg's aliases give them.
    public static readonly Guid BoostMode = new("be337238-0d82-4146-a960-4f3749d470c7");         // PERFBOOSTMODE
    public static readonly Guid EnergyPreference = new("36687f9e-e3a5-4dbf-b1dc-15eb381c6863");  // PERFEPP
    public static readonly Guid EnergyPreference1 = new("36687f9e-e3a5-4dbf-b1dc-15eb381c6864"); // PERFEPP1
    public static readonly Guid Scheduling = new("93b8b6dc-0698-4d1c-9ee4-0644e900c85d");        // SCHEDPOLICY
    public static readonly Guid ShortScheduling = new("bae08b81-2d5e-4688-ad6a-13243356654b");   // SHORTSCHEDPOLICY
    public static readonly Guid CoolingPolicy = new("94d3a615-a899-4ac5-ae2b-e4d8f634367f");     // SYSCOOLPOL
    public static readonly Guid FrequencyMax = new("75b0ae3f-bce0-45a7-8c89-c9611c25e100");      // PROCFREQMAX, MHz, 0 = none
    public static readonly Guid FrequencyMax1 = new("75b0ae3f-bce0-45a7-8c89-c9611c25e101");     // PROCFREQMAX1

    // Windows' built-in plans: the same identifiers on every installation.
    public static readonly Guid PowerSaverPlan = new("a1841308-3541-4fab-bc81-f71556f20b4a");
    public static readonly Guid BalancedPlan = new("381b4222-f694-41f0-9685-ff5bb260df2e");
    public static readonly Guid HighPerformancePlan = new("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

    // Values, as Windows numbers them.
    private const int PreferPerformant = 2, PreferEfficient = 4;
    private const int BoostAggressive = 2;

    /// <summary>The top of Intel's "performance" energy-preference band, as Windows' percentage: 64 of 255.</summary>
    public const int PerformanceBandTop = 25;

    private static PlanTuning.AcDc Both(int value) => new(value, value);

    /// <summary>The settings this profile touches, every one of them in the processor group.</summary>
    public static IReadOnlyList<Guid> Settings { get; } =
    [
        PlanTuning.MinState, PlanTuning.MaxState, PlanTuning.MaxState1,
        BoostMode, EnergyPreference, EnergyPreference1, Scheduling, ShortScheduling, CoolingPolicy,
        FrequencyMax, FrequencyMax1,
    ];

    /// <summary>
    /// The person's ceilings for a mode, within the measured upper bounds
    /// (the owner's rule, 14 October 2026): Power Saver as a percentage - any
    /// value under 100 turns the boost off - and the boosting modes as a
    /// clock limit in MHz, the boost kept up to it. Null means the mode's own.
    /// </summary>
    public sealed record Limits(int? PercentCeiling, int? FrequencyMhz)
    {
        public static Limits None { get; } = new(null, null);
    }

    /// <summary>The highest ceiling a mode may have, in percent: Power Saver's measured 90, the others the processor's own.</summary>
    public static int UpperPercent(SystemMode mode) => mode == SystemMode.Office ? PlanTuning.CoolCeiling : 100;

    /// <summary>The lowest percentage offered: below it the machine is too slow to be worth it.</summary>
    public const int LowestPercent = 50;

    /// <summary>The range a clock limit may be set in, in MHz; 0 is no limit.</summary>
    public const int LowestFrequencyMhz = 1500, HighestFrequencyMhz = 6000;

    /// <summary>A percentage ceiling as it may be written: within the lowest and the mode's upper bound.</summary>
    public static int ClampPercent(SystemMode mode, int? wanted) =>
        Math.Clamp(wanted ?? UpperPercent(mode), LowestPercent, UpperPercent(mode));

    /// <summary>A clock limit as it may be written: 0 (none) or within the range.</summary>
    public static int ClampFrequency(int? wanted) =>
        wanted is not { } f || f <= 0 ? 0 : Math.Clamp(f, LowestFrequencyMhz, HighestFrequencyMhz);

    /// <summary>
    /// What Windows chose on this machine: its three built-in plans' values
    /// for <see cref="Settings"/>, and whether the processor has two kinds
    /// of core.
    /// </summary>
    public sealed record Baseline(
        IReadOnlyDictionary<Guid, PlanTuning.AcDc> PowerSaver,
        IReadOnlyDictionary<Guid, PlanTuning.AcDc> Balanced,
        IReadOnlyDictionary<Guid, PlanTuning.AcDc> HighPerformance,
        bool Hybrid);

    // Windows' documented defaults, for a value a plan does not give back -
    // a stripped installation, a setting the build does not have.
    private static readonly Dictionary<Guid, PlanTuning.AcDc> SaverDefaults = new()
    {
        [PlanTuning.MinState] = Both(5), [PlanTuning.MaxState] = Both(100), [PlanTuning.MaxState1] = Both(100),
        [BoostMode] = Both(2), [EnergyPreference] = Both(60), [EnergyPreference1] = Both(60),
        [Scheduling] = Both(5), [ShortScheduling] = Both(5), [CoolingPolicy] = Both(0),
        [FrequencyMax] = Both(0), [FrequencyMax1] = Both(0),
    };

    private static readonly Dictionary<Guid, PlanTuning.AcDc> BalancedDefaults = new()
    {
        [PlanTuning.MinState] = Both(5), [PlanTuning.MaxState] = Both(100), [PlanTuning.MaxState1] = Both(100),
        [BoostMode] = Both(2), [EnergyPreference] = new(45, 50), [EnergyPreference1] = new(45, 50),
        [Scheduling] = Both(5), [ShortScheduling] = Both(5), [CoolingPolicy] = new(1, 0),
        [FrequencyMax] = Both(0), [FrequencyMax1] = Both(0),
    };

    private static readonly Dictionary<Guid, PlanTuning.AcDc> HighDefaults = new()
    {
        [PlanTuning.MinState] = new(100, 5), [PlanTuning.MaxState] = Both(100), [PlanTuning.MaxState1] = Both(100),
        [BoostMode] = Both(5), [EnergyPreference] = Both(0), [EnergyPreference1] = Both(0),
        [Scheduling] = Both(5), [ShortScheduling] = Both(5), [CoolingPolicy] = Both(1),
        [FrequencyMax] = Both(0), [FrequencyMax1] = Both(0),
    };

    /// <summary>Reads the baseline from this machine; a value that cannot be read is Windows' documented default.</summary>
    public static Baseline ReadBaseline() => new(
        ReadPlan(PowerSaverPlan, SaverDefaults), ReadPlan(BalancedPlan, BalancedDefaults),
        ReadPlan(HighPerformancePlan, HighDefaults), IsHybrid());

    private static Dictionary<Guid, PlanTuning.AcDc> ReadPlan(Guid plan, Dictionary<Guid, PlanTuning.AcDc> defaults) =>
        Settings.ToDictionary(s => s, s => PlanTuning.Read(plan, s) ?? defaults[s]);

    /// <summary>The baseline with Windows' documented defaults throughout, for tests and as a last resort.</summary>
    public static Baseline Defaults(bool hybrid) => new(SaverDefaults, BalancedDefaults, HighDefaults, hybrid);

    /// <summary>What a mode's plan is given, setting by setting, from the baseline and the person's ceilings.</summary>
    public static IReadOnlyDictionary<Guid, PlanTuning.AcDc> For(SystemMode mode, Baseline b, Limits? limits)
    {
        var values = For(mode, b).ToDictionary(kv => kv.Key, kv => kv.Value);
        if (values.Count == 0) return values;
        limits ??= Limits.None;

        if (mode == SystemMode.Office)
        {
            var percent = ClampPercent(mode, limits.PercentCeiling);
            values[PlanTuning.MaxState] = values[PlanTuning.MaxState1] = Both(percent);
            values[FrequencyMax] = values[FrequencyMax1] = Both(0);
        }
        else
        {
            var mhz = ClampFrequency(limits.FrequencyMhz);
            values[FrequencyMax] = values[FrequencyMax1] = Both(mhz);
        }
        return values;
    }

    /// <summary>What a mode's plan is given, setting by setting, from the baseline.</summary>
    public static IReadOnlyDictionary<Guid, PlanTuning.AcDc> For(SystemMode mode, Baseline b)
    {
        var floor = Floor(b.Balanced[PlanTuning.MinState]);
        switch (mode)
        {
            case SystemMode.Balanced:
                var balanced = Settings.ToDictionary(s => s, s => b.Balanced[s]);
                balanced[PlanTuning.MinState] = floor;
                return balanced;

            case SystemMode.Gaming:
                var gaming = Settings.ToDictionary(s => s, s => b.Balanced[s]);
                gaming[PlanTuning.MinState] = floor;
                gaming[EnergyPreference] = gaming[EnergyPreference] with { Ac = Math.Min(b.Balanced[EnergyPreference].Ac, PerformanceBandTop) };
                gaming[EnergyPreference1] = gaming[EnergyPreference1] with { Ac = Math.Min(b.Balanced[EnergyPreference1].Ac, PerformanceBandTop) };
                gaming[BoostMode] = gaming[BoostMode] with { Ac = Math.Max(b.Balanced[BoostMode].Ac, BoostAggressive) };
                return gaming;

            case SystemMode.Performance:
                var high = Settings.ToDictionary(s => s, s => b.HighPerformance[s]);
                high[PlanTuning.MinState] = floor;
                if (b.Hybrid) high[Scheduling] = high[ShortScheduling] = Both(PreferPerformant);
                return high;

            case SystemMode.Office:
                var office = Settings.ToDictionary(s => s, s => b.PowerSaver[s]);
                office[PlanTuning.MinState] = floor;
                office[PlanTuning.MaxState] = office[PlanTuning.MaxState1] = Both(PlanTuning.CoolCeiling);
                office[BoostMode] = new(Efficient(b.PowerSaver[BoostMode].Ac), Efficient(b.PowerSaver[BoostMode].Dc));
                office[EnergyPreference] = AtLeast(b.PowerSaver[EnergyPreference], b.Balanced[EnergyPreference]);
                office[EnergyPreference1] = AtLeast(b.PowerSaver[EnergyPreference1], b.Balanced[EnergyPreference1]);
                if (b.Hybrid) office[Scheduling] = office[ShortScheduling] = Both(PreferEfficient);
                return office;

            default:
                return new Dictionary<Guid, PlanTuning.AcDc>();
        }
    }

    /// <summary>A floor that never pins the processor: the given one, or Windows' safe floor when it would.</summary>
    private static PlanTuning.AcDc Floor(PlanTuning.AcDc f) => new(
        f.Ac >= PlanTuning.PinnedFloor ? PlanTuning.SafeFloor : f.Ac,
        f.Dc >= PlanTuning.PinnedFloor ? PlanTuning.SafeFloor : f.Dc);

    /// <summary>The higher energy preference - toward efficiency - of the two, plugged in and on battery each.</summary>
    private static PlanTuning.AcDc AtLeast(PlanTuning.AcDc a, PlanTuning.AcDc b) => new(Math.Max(a.Ac, b.Ac), Math.Max(a.Dc, b.Dc));

    /// <summary>
    /// The efficient variant of a boost mode: enabled -> efficient enabled,
    /// any aggressive one -> efficient aggressive; off stays off.
    /// </summary>
    public static int Efficient(int boost) => boost switch
    {
        0 => 0,
        1 or 3 => 3,
        _ => 4,
    };

    /// <summary>Whether the processor has two kinds of core (Windows' efficiency classes), as hybrid Intel processors do.</summary>
    public static bool IsHybrid()
    {
        try
        {
            uint length = 0;
            GetLogicalProcessorInformationEx(RelationProcessorCore, IntPtr.Zero, ref length);
            if (length == 0) return false;
            var buffer = Marshal.AllocHGlobal((int)length);
            try
            {
                if (!GetLogicalProcessorInformationEx(RelationProcessorCore, buffer, ref length)) return false;
                var classes = new HashSet<byte>();
                for (var offset = 0; offset + 10 <= length;)
                {
                    var size = Marshal.ReadInt32(buffer, offset + 4);
                    if (size <= 0) break;
                    classes.Add(Marshal.ReadByte(buffer, offset + 9)); // PROCESSOR_RELATIONSHIP.EfficiencyClass
                    offset += size;
                }
                return classes.Count > 1;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return false;
        }
    }

    private const int RelationProcessorCore = 0;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetLogicalProcessorInformationEx(int relationship, IntPtr buffer, ref uint length);
}
