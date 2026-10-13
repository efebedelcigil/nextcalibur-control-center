namespace Nextcalibur.Core.Power;

/// <summary>
/// The processor settings each mode's plan is given, beyond the floor and
/// the ceiling.
///
/// The vendor's Office and Gaming plans are Balanced with a different floor
/// or ceiling and nothing else (read 14 October 2026): Office boosted as
/// hard as Gaming, put its threads on the performance cores first, and asked
/// the processor for the same energy preference. The owner chose this
/// profile that day, after 94 °C in Office with little running:
///
/// - <b>Office</b> - quiet first: boost only when it pays, an energy
///   preference toward efficiency, work on the efficient cores first, and
///   slowing down before the fans when it warms (passive cooling).
/// - <b>Gaming</b> - Balanced's own behaviour with a floor that lets the
///   processor rest, and a little more eagerness plugged in.
/// - <b>Performance</b> - eager: a low energy preference and the
///   performance cores first.
///
/// On battery each mode keeps a step toward efficiency, as Windows does.
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

    // Their values, as Windows numbers them.
    private const int BoostAggressive = 2, BoostEfficient = 3;
    private const int PreferPerformant = 2, PreferEfficient = 4, Automatic = 5;
    private const int Passive = 0, Active = 1;

    private static PlanTuning.AcDc Both(int value) => new(value, value);

    /// <summary>The settings this profile touches, every one of them in the processor group.</summary>
    public static IReadOnlyList<Guid> Settings { get; } =
    [
        PlanTuning.MinState, PlanTuning.MaxState, PlanTuning.MaxState1,
        BoostMode, EnergyPreference, EnergyPreference1, Scheduling, ShortScheduling, CoolingPolicy,
    ];

    /// <summary>What a mode's plan is given, setting by setting.</summary>
    public static IReadOnlyDictionary<Guid, PlanTuning.AcDc> For(SystemMode mode) => mode switch
    {
        SystemMode.Office => new Dictionary<Guid, PlanTuning.AcDc>
        {
            [PlanTuning.MinState] = Both(PlanTuning.SafeFloor),
            [PlanTuning.MaxState] = Both(PlanTuning.CoolCeiling),
            [PlanTuning.MaxState1] = Both(PlanTuning.CoolCeiling),
            [BoostMode] = Both(BoostEfficient),
            [EnergyPreference] = new(80, 90),
            [EnergyPreference1] = new(80, 90),
            [Scheduling] = Both(PreferEfficient),
            [ShortScheduling] = Both(PreferEfficient),
            [CoolingPolicy] = Both(Passive),
        },
        SystemMode.Gaming => new Dictionary<Guid, PlanTuning.AcDc>
        {
            [PlanTuning.MinState] = Both(PlanTuning.SafeFloor),
            [PlanTuning.MaxState] = Both(100),
            [PlanTuning.MaxState1] = Both(100),
            [BoostMode] = Both(BoostAggressive),
            [EnergyPreference] = new(33, 50),
            [EnergyPreference1] = new(33, 50),
            [Scheduling] = Both(Automatic),
            [ShortScheduling] = Both(Automatic),
            [CoolingPolicy] = new(Active, Passive),
        },
        SystemMode.Performance => new Dictionary<Guid, PlanTuning.AcDc>
        {
            [PlanTuning.MinState] = Both(PlanTuning.SafeFloor),
            [PlanTuning.MaxState] = Both(100),
            [PlanTuning.MaxState1] = Both(100),
            [BoostMode] = Both(BoostAggressive),
            [EnergyPreference] = new(15, 33),
            [EnergyPreference1] = new(15, 33),
            [Scheduling] = Both(PreferPerformant),
            [ShortScheduling] = Both(PreferPerformant),
            [CoolingPolicy] = new(Active, Passive),
        },
        _ => new Dictionary<Guid, PlanTuning.AcDc>(),
    };
}
