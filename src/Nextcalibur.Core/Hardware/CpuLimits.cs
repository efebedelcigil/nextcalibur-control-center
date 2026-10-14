namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Intel's registers on why the processor is held back - its power limits,
/// its throttle temperature, and the reasons it is running below what it
/// asked for - turned into words. Read-only diagnostics, for the question
/// "why is it this hot" (14 October 2026: 94 °C after a cleaning and new
/// paste); see <see cref="CpuPowerReader.ReadDiagnostic"/>.
/// </summary>
public static class CpuLimits
{
    public const uint PowerUnit = 0x606;        // MSR_RAPL_POWER_UNIT
    public const uint PackagePowerLimit = 0x610; // MSR_PKG_POWER_LIMIT
    public const uint CoreThermStatus = 0x19C;   // IA32_THERM_STATUS
    public const uint TemperatureTarget = 0x1A2; // MSR_TEMPERATURE_TARGET
    public const uint PackageThermStatus = 0x1B1; // IA32_PACKAGE_THERM_STATUS
    public const uint PerfLimitReasons = 0x64F;  // MSR_CORE_PERF_LIMIT_REASONS

    /// <summary>The registers <see cref="CpuPowerReader.ReadDiagnostic"/> will read; nothing else, and nothing is ever written.</summary>
    public static readonly IReadOnlySet<uint> Readable = new HashSet<uint>
    {
        PowerUnit, PackagePowerLimit, CoreThermStatus, TemperatureTarget, PackageThermStatus, PerfLimitReasons,
    };

    /// <summary>The package power limits: PL1 (sustained) with its time window, PL2 (short), and whether the firmware locked them.</summary>
    public readonly record struct PowerLimits(double Pl1Watts, bool Pl1On, double Pl1WindowSeconds, double Pl2Watts, bool Pl2On, bool Locked);

    public static PowerLimits DecodePowerLimits(ulong limit, ulong unit)
    {
        var watt = 1.0 / (1L << (int)(unit & 0xF));
        var second = 1.0 / (1L << (int)((unit >> 16) & 0xF));
        var y = (int)((limit >> 17) & 0x1F);
        var z = (int)((limit >> 22) & 0x3);
        return new PowerLimits(
            (limit & 0x7FFF) * watt, (limit & (1UL << 15)) != 0,
            (1L << y) * (1 + z / 4.0) * second,
            ((limit >> 32) & 0x7FFF) * watt, (limit & (1UL << 47)) != 0,
            (limit & (1UL << 63)) != 0);
    }

    /// <summary>The temperature the processor throttles at, and the offset below it the firmware may have set.</summary>
    public static (int TjMax, int Offset) DecodeTarget(ulong target) =>
        ((int)((target >> 16) & 0xFF), (int)((target >> 24) & 0x3F));

    /// <summary>Degrees below the throttle point, from a thermal status register; null when not valid.</summary>
    public static int? DegreesBelowTjMax(ulong status) =>
        (status & (1UL << 31)) != 0 ? (int)((status >> 16) & 0x7F) : null;

    private static readonly (int Bit, string Name)[] Reasons =
    [
        (0, "PROCHOT (an outside signal: the embedded controller, the VRM, a hot GPU)"),
        (1, "thermal (the processor at its throttle temperature)"),
        (4, "residency state regulation"),
        (5, "running average thermal limit"),
        (6, "voltage regulator thermal alert"),
        (7, "voltage regulator current limit (TDC)"),
        (8, "other"),
        (10, "PL1 (sustained power limit)"),
        (11, "PL2 (short power limit)"),
        (12, "max turbo limit (more cores busy, lower turbo)"),
        (13, "turbo transition attenuation"),
    ];

    /// <summary>What is holding the cores back now, and what has since the log was last cleared (since boot).</summary>
    public static (IReadOnlyList<string> Now, IReadOnlyList<string> SinceBoot) DecodeLimitReasons(ulong value) =>
        (Reasons.Where(r => (value & (1UL << r.Bit)) != 0).Select(r => r.Name).ToList(),
         Reasons.Where(r => (value & (1UL << (r.Bit + 16))) != 0).Select(r => r.Name).ToList());

    /// <summary>The thermal status flags: throttling now or since boot, PROCHOT now or since boot, a power-limit notification since boot.</summary>
    public readonly record struct ThermFlags(bool HotNow, bool HotSinceBoot, bool ProchotNow, bool ProchotSinceBoot, bool PowerLimitSinceBoot);

    public static ThermFlags DecodeThermStatus(ulong status) => new(
        (status & 1) != 0, (status & 2) != 0, (status & 4) != 0, (status & 8) != 0, (status & (1UL << 11)) != 0);
}
