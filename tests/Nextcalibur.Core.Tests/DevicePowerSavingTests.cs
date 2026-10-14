using Nextcalibur.Core.Power;
using Xunit;
using AcDc = Nextcalibur.Core.Power.PlanTuning.AcDc;

namespace Nextcalibur.Core.Tests;

public class DevicePowerSavingTests
{
    [Fact]
    public void On_everywhere_off_everywhere_or_mixed()
    {
        Assert.True(DevicePowerSaving.State([new AcDc(1, 1), new AcDc(0, 2)]));
        Assert.False(DevicePowerSaving.State([new AcDc(0, 0), new AcDc(0, 0)]));
        Assert.Null(DevicePowerSaving.State([new AcDc(1, 1), new AcDc(0, 0)]));
        Assert.Null(DevicePowerSaving.State([]));
    }

    [Fact]
    public void On_is_what_Windows_Balanced_has_here_when_it_saves()
    {
        Assert.Equal(new AcDc(0, 3), DevicePowerSaving.On(DeviceSaving.Wifi, new AcDc(0, 3)));
        // A Balanced that saves nothing gives Windows' documented values instead.
        Assert.Equal(new AcDc(0, 2), DevicePowerSaving.On(DeviceSaving.Wifi, new AcDc(0, 0)));
        Assert.Equal(new AcDc(1, 1), DevicePowerSaving.On(DeviceSaving.Usb, null));
    }

    [Fact]
    public void Off_saves_nothing() => Assert.False(DevicePowerSaving.Saving(DevicePowerSaving.Off));
}
