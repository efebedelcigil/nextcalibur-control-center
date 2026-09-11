using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class ThermalProfileTests
{
    [Theory]
    [InlineData(SystemMode.Performance, 0u)]
    [InlineData(SystemMode.Gaming, 1u)]
    [InlineData(SystemMode.Office, 2u)]
    public void Values_are_the_ones_measured_from_the_vendor(SystemMode mode, uint value)
    {
        Assert.Equal(value, ThermalProfile.ValueFor(mode));
        Assert.Equal(mode, ThermalProfile.ModeFor(value));
    }

    [Fact]
    public void An_unseen_value_is_not_a_mode() => Assert.Null(ThermalProfile.ModeFor(3));

    [Fact]
    public void The_register_is_the_one_the_vendor_writes_at_startup() =>
        Assert.Equal(0x0300, (ushort)SmiSubsystem.Profile);
}
