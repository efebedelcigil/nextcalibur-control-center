using System.Management;

namespace Nextcalibur.Core.Hardware;

/// <summary>Who set a value: the chip's maker, the laptop's maker, Windows, this application, or the person.</summary>
public enum SetBy
{
    Intel,
    Nvidia,
    LaptopMaker,
    Windows,
    Nextcalibur,
    You,
}

/// <summary>
/// One power value as the machine has it: what is in force, what the
/// component's maker defined for it (when there is a definition), and who
/// set the value in force.
/// </summary>
/// <param name="Key">Stable, for the log and the words: "cpu.base", "cpu.pl1"...</param>
/// <param name="Value">What is in force, in watts or degrees.</param>
/// <param name="Defined">The maker's own definition for it, when there is one.</param>
/// <param name="SetBy">Who set the value in force.</param>
/// <param name="DefinedBy">Whose definition <paramref name="Defined"/> is.</param>
/// <param name="Unit">"W", "°C", "Wh" or "%".</param>
public readonly record struct PowerDefinition(string Key, double Value, double? Defined, SetBy SetBy, SetBy? DefinedBy, string Unit);

/// <summary>
/// The power values each component carries, read from the component -
/// never assumed: the owner's rule of 14 October 2026, after the processor
/// turned out to run at a 65 W limit its maker defines as 45 W. Where the
/// value in force differs from the definition, it was the laptop's maker
/// (its firmware) that set it; where they agree, the definition stands.
/// Anything that cannot be read is left out, never guessed.
///
/// Reads only. The processor's through <see cref="CpuPowerReader"/>'s
/// whitelisted registers; the graphics card's through NVML, and only while
/// it is awake (NVML wakes a sleeping card); the battery's through WMI.
/// </summary>
public static class PowerDefinitions
{
    /// <summary>The processor's: base power, the sustained and short limits, the throttle point.</summary>
    public static IEnumerable<PowerDefinition> Processor(CpuPowerReader msr)
    {
        var unit = msr.ReadDiagnostic(CpuLimits.PowerUnit);
        if (unit is null) yield break;

        double? basePower = null;
        double? maxPower = null;
        if (msr.ReadDiagnostic(CpuLimits.PackagePowerInfo) is { } info)
        {
            var i = CpuLimits.DecodePowerInfo(info, unit.Value);
            if (i.TdpWatts > 0) basePower = i.TdpWatts;
            if (i.MaxWatts > 0) maxPower = i.MaxWatts;
        }
        if (basePower is { } b) yield return new PowerDefinition("cpu.base", b, null, SetBy.Intel, null, "W");

        if (msr.ReadDiagnostic(CpuLimits.PackagePowerLimit) is { } limit)
        {
            var pl = CpuLimits.DecodePowerLimits(limit, unit.Value);
            if (pl.Pl1On && pl.Pl1Watts > 0)
                yield return new PowerDefinition("cpu.pl1", pl.Pl1Watts, basePower, Who(pl.Pl1Watts, basePower), basePower is null ? null : SetBy.Intel, "W");
            if (pl.Pl2On && pl.Pl2Watts > 0)
                yield return new PowerDefinition("cpu.pl2", pl.Pl2Watts, maxPower, Who(pl.Pl2Watts, maxPower), maxPower is null ? null : SetBy.Intel, "W");
        }

        if (msr.ReadDiagnostic(CpuLimits.TemperatureTarget) is { } target)
        {
            var (tj, offset) = CpuLimits.DecodeTarget(target);
            if (tj > 0)
                yield return new PowerDefinition("cpu.throttle", tj - offset, tj, offset == 0 ? SetBy.Intel : SetBy.LaptopMaker, SetBy.Intel, "°C");
        }
    }

    /// <summary>The graphics card's: NVIDIA's default limit and the one in force.</summary>
    public static IEnumerable<PowerDefinition> GraphicsCard(GpuPowerLimits limits)
    {
        yield return new PowerDefinition("gpu.limit", limits.EnforcedWatts, limits.DefaultWatts,
            Who(limits.EnforcedWatts, limits.DefaultWatts, SetBy.Nvidia), SetBy.Nvidia, "W");
        if (limits.MaxWatts is { } max && max > 0)
            yield return new PowerDefinition("gpu.limit.max", max, null, SetBy.LaptopMaker, null, "W");
    }

    /// <summary>The battery's: its design capacity, what it holds now when full, and the health that makes.</summary>
    public static IEnumerable<PowerDefinition> Battery()
    {
        var design = ReadWmi("BatteryStaticData", "DesignedCapacity");
        var full = ReadWmi("BatteryFullChargedCapacity", "FullChargedCapacity");
        if (design is not { } d || d <= 0) yield break;
        yield return new PowerDefinition("battery.design", d / 1000.0, null, SetBy.LaptopMaker, null, "Wh");
        if (full is { } f && f > 0)
        {
            yield return new PowerDefinition("battery.full", f / 1000.0, d / 1000.0, SetBy.Windows, SetBy.LaptopMaker, "Wh");
            yield return new PowerDefinition("battery.health", Math.Round(100.0 * f / d), 100, SetBy.Windows, SetBy.LaptopMaker, "%");
        }
    }

    /// <summary>The definition stands when the value agrees with it; otherwise the laptop's firmware set it.</summary>
    public static SetBy Who(double value, double? defined, SetBy definer = SetBy.Intel) =>
        defined is { } d && Math.Abs(value - d) < 0.5 ? definer : SetBy.LaptopMaker;

    private static double? ReadWmi(string className, string property)
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(@"root\wmi", $"SELECT {property} FROM {className}");
            using var results = searcher.Get();
            foreach (ManagementBaseObject o in results)
            {
                using (o)
                {
                    if (o[property] is uint u) return u;
                }
            }
        }
        catch (Exception ex) when (ex is ManagementException or UnauthorizedAccessException or System.Runtime.InteropServices.COMException)
        {
        }
        return null;
    }
}
