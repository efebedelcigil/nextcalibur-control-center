using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class CpuLimitsTests
{
    // A typical unit register: power in 1/8 W, time in 1/1024 s.
    private const ulong Unit = 0x000A0E03;

    [Fact]
    public void Power_limits_read_in_watts_and_seconds()
    {
        // PL1 45 W on, window 2^5 * (1 + 2/4) / 1024 s; PL2 115 W on; locked.
        ulong limit = 360 | (1UL << 15) | (5UL << 17) | (2UL << 22)
                      | (920UL << 32) | (1UL << 47) | (1UL << 63);
        var pl = CpuLimits.DecodePowerLimits(limit, Unit);
        Assert.Equal(45, pl.Pl1Watts);
        Assert.True(pl.Pl1On);
        Assert.Equal(32 * 1.5 / 1024, pl.Pl1WindowSeconds, 6);
        Assert.Equal(115, pl.Pl2Watts);
        Assert.True(pl.Pl2On);
        Assert.True(pl.Locked);
    }

    [Fact]
    public void The_throttle_point_and_its_offset()
    {
        Assert.Equal((100, 5), CpuLimits.DecodeTarget((5UL << 24) | (100UL << 16)));
    }

    [Fact]
    public void Degrees_below_the_throttle_point_need_the_valid_bit()
    {
        Assert.Equal(12, CpuLimits.DegreesBelowTjMax((1UL << 31) | (12UL << 16)));
        Assert.Null(CpuLimits.DegreesBelowTjMax(12UL << 16));
    }

    [Fact]
    public void Limit_reasons_now_and_since_boot_are_told_apart()
    {
        // Now: thermal. Since boot: PROCHOT and PL1.
        var (now, logged) = CpuLimits.DecodeLimitReasons((1UL << 1) | (1UL << 16) | (1UL << 26));
        Assert.Single(now);
        Assert.StartsWith("thermal", now[0]);
        Assert.Equal(2, logged.Count);
        Assert.StartsWith("PROCHOT", logged[0]);
        Assert.StartsWith("PL1", logged[1]);
    }

    [Fact]
    public void Thermal_status_flags()
    {
        var f = CpuLimits.DecodeThermStatus(0b10 | 0b1000 | (1UL << 11));
        Assert.False(f.HotNow);
        Assert.True(f.HotSinceBoot);
        Assert.False(f.ProchotNow);
        Assert.True(f.ProchotSinceBoot);
        Assert.True(f.PowerLimitSinceBoot);
    }

    [Fact]
    public void Intels_own_definition_reads_in_watts()
    {
        // Base 45 W, range 35-115 W, in 1/8 W.
        var info = CpuLimits.DecodePowerInfo(360 | (280UL << 16) | (920UL << 32), Unit);
        Assert.Equal(new CpuLimits.PowerInfo(45, 35, 115), info);
        Assert.Equal((35.0, 21), CpuLimits.DecodeTdpLevel(280 | (21UL << 16), Unit));
        Assert.Equal((0.0, 0), CpuLimits.DecodeTdpLevel(0, Unit));
    }

    [Fact]
    public void Only_the_diagnostic_registers_are_readable()
    {
        Assert.Contains(CpuLimits.PerfLimitReasons, CpuLimits.Readable);
        Assert.DoesNotContain(0x1A0u, CpuLimits.Readable); // IA32_MISC_ENABLE: never
        using var reader = new CpuPowerReader();
        Assert.Throws<ArgumentOutOfRangeException>(() => reader.ReadDiagnostic(0x1A0));
    }
}
