using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class CeilingTestTests
{
    [Fact]
    public void Each_mode_has_its_own_target() =>
        Assert.Equal([65, 75, 85, 90], SystemModes.InOrder.Select(CeilingTest.TargetC));

    [Fact]
    public void Power_Saver_is_tried_from_the_ceiling_in_force_down_never_above_90() =>
        Assert.Equal([90, 85, 80, 75, 70, 65, 60, 55, 50], CeilingTest.Steps(SystemMode.Office, 95, 0));

    [Fact]
    public void A_boosting_mode_is_tried_from_its_limit_down_in_200_MHz()
    {
        Assert.Equal([3000, 2800, 2600, 2400], CeilingTest.Steps(SystemMode.Gaming, 3000, 0));
        // With no limit, the steps below start from the clock reached.
        Assert.Equal([0, 3600, 3400], CeilingTest.Steps(SystemMode.Gaming, 0, 3650).Take(3));
        Assert.Equal([0, 4600, 4400], CeilingTest.Steps(SystemMode.Gaming, 0, 4700).Take(3));
    }

    [Fact]
    public void The_first_step_that_held_is_chosen_the_highest()
    {
        var steps = new[]
        {
            new CeilingStep(90, 70, 72, 22, 2.0),
            new CeilingStep(85, 64, 66, 19, 1.9),
            new CeilingStep(80, 60, 61, 17, 1.8),
        };
        Assert.Equal(85, CeilingTest.Choose(SystemMode.Office, steps));
    }

    [Fact]
    public void A_peak_too_far_over_the_target_does_not_hold() =>
        Assert.False(CeilingTest.Held(SystemMode.Office, new CeilingStep(85, 64, 70, 19, 1.9)));

    [Fact]
    public void Nothing_held_is_no_proposal() =>
        Assert.Null(CeilingTest.Choose(SystemMode.Performance, [new CeilingStep(0, 97, 97, 53, 3.6)]));

    [Theory]
    [InlineData("TiWorker", 5.0, "Windows Update (TiWorker)")]                  // Windows installing updates
    [InlineData("TiWorker", 0.0, null)]                                         // idle: not at work
    [InlineData("DrvInst", 0.0, "a driver or firmware install (DrvInst)")]      // a driver install, at any load
    [InlineData("AFUWINx64", 0.0, "a BIOS or firmware update (AFUWINx64)")]
    [InlineData("msiexec", 2.0, "a program being installed (msiexec)")]
    [InlineData("chrome", 50.0, null)]
    public void Work_that_must_not_be_interrupted(string name, double cpu, string? why) =>
        Assert.Equal(why, TestPreflight.IsCritical(name, null, cpu, 0));
}
