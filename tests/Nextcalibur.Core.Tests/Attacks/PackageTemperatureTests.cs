using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// The processor's own thermal register, decoded. This reading decides
/// whether the firmware is asked for a temperature at all, so a wrong
/// answer here is either a warning that never comes or an interrupt that
/// need not have happened.
/// </summary>
public class PackageTemperatureTests
{
    /// <summary>Bit 31 valid, the reading in bits 22:16 as degrees below the throttle point.</summary>
    private static ulong Status(int degreesBelow, bool valid = true) =>
        (valid ? 1UL << 31 : 0UL) | ((ulong)(degreesBelow & 0x7F) << 16);

    private static ulong Target(int tjMax) => (ulong)(tjMax & 0xFF) << 16;

    [Theory]
    [InlineData(100, 0, 100)]     // at the throttle point
    [InlineData(100, 40, 60)]
    [InlineData(105, 62, 43)]
    [InlineData(90, 45, 45)]
    public void The_temperature_is_the_throttle_point_less_the_reading(int tjMax, int below, int expected)
    {
        Assert.Equal(expected, CpuPowerReader.PackageTemperature(Status(below), Target(tjMax)));
    }

    [Fact]
    public void A_reading_the_processor_calls_invalid_is_no_reading()
    {
        Assert.Null(CpuPowerReader.PackageTemperature(Status(40, valid: false), Target(100)));
    }

    /// <summary>
    /// A throttle point no processor has means the register is not what
    /// this code thinks it is - another vendor's map, a module that did not
    /// load. Better no number than a number from the wrong register.
    /// </summary>
    [Theory]
    [InlineData(0)]
    [InlineData(30)]
    [InlineData(200)]
    public void A_throttle_point_that_is_not_one_is_refused(int tjMax)
    {
        Assert.Null(CpuPowerReader.PackageTemperature(Status(40), Target(tjMax)));
    }

    [Fact]
    public void A_reading_further_below_than_the_throttle_point_is_refused()
    {
        // 127 is the widest the field goes; against a 100 °C point that is
        // -27 °C, which is not a temperature this laptop is at.
        Assert.Null(CpuPowerReader.PackageTemperature(Status(127), Target(100)));
    }

    [Fact]
    public void Zero_registers_are_no_reading()
    {
        Assert.Null(CpuPowerReader.PackageTemperature(0, 0));
    }
}
