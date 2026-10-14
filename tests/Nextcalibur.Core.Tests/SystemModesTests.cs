using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class SystemModesTests
{
    [Fact]
    public void The_modes_go_from_the_coolest_to_the_fastest() =>
        Assert.Equal([SystemMode.Office, SystemMode.Balanced, SystemMode.Gaming, SystemMode.Performance], SystemModes.InOrder);

    [Fact]
    public void The_next_mode_goes_round()
    {
        Assert.Equal(SystemMode.Balanced, SystemModes.Next(SystemMode.Office));
        Assert.Equal(SystemMode.Office, SystemModes.Next(SystemMode.Performance));
    }

    [Fact]
    public void Balanced_is_added_last_so_a_mode_stored_by_number_reads_the_same() =>
        Assert.Equal([0, 1, 2, 3], new[] { SystemMode.Office, SystemMode.Gaming, SystemMode.Performance, SystemMode.Balanced }.Select(m => (int)m));

    [Fact]
    public void Balanced_runs_on_the_firmwares_Office_profile() =>
        Assert.Equal(ThermalProfile.ValueFor(SystemMode.Office), ThermalProfile.ValueFor(SystemMode.Balanced));

    [Fact]
    public void On_battery_a_mode_as_cool_as_the_batterys_is_left_alone()
    {
        var policy = new BatteryModePolicy { QuietMode = SystemMode.Balanced };
        Assert.Null(policy.OnUnplugged(SystemMode.Office));
        Assert.Null(policy.OnUnplugged(SystemMode.Balanced));
        Assert.Equal(SystemMode.Balanced, policy.OnUnplugged(SystemMode.Gaming));
        Assert.Equal(SystemMode.Gaming, policy.OnPluggedIn(SystemMode.Balanced));
    }
}
