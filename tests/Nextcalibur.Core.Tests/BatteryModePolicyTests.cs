using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class BatteryModePolicyTests
{
    [Fact]
    public void Unplugging_from_gaming_drops_to_office_and_remembers_gaming()
    {
        var policy = new BatteryModePolicy();
        Assert.Equal(SystemMode.Office, policy.OnUnplugged(SystemMode.Gaming));
        Assert.Equal(SystemMode.Gaming, policy.Remembered);
    }

    [Fact]
    public void Plugging_back_in_restores_what_the_person_had()
    {
        var policy = new BatteryModePolicy();
        policy.OnUnplugged(SystemMode.Performance);
        Assert.Equal(SystemMode.Performance, policy.OnPluggedIn(SystemMode.Office));
        Assert.Null(policy.Remembered);
    }

    [Fact]
    public void Already_quiet_or_no_mode_means_nothing_to_do()
    {
        var policy = new BatteryModePolicy();
        Assert.Null(policy.OnUnplugged(SystemMode.Office));
        Assert.Null(policy.OnPluggedIn(SystemMode.Office));
        Assert.Null(policy.OnUnplugged(null));
        Assert.Null(policy.OnPluggedIn(null));
    }

    [Fact]
    public void A_mode_changed_by_hand_on_battery_is_not_undone()
    {
        var policy = new BatteryModePolicy();
        policy.OnUnplugged(SystemMode.Gaming);
        policy.UserChose(SystemMode.Performance, onBattery: true);
        Assert.Null(policy.OnPluggedIn(SystemMode.Performance));
    }

    [Fact]
    public void A_mode_found_changed_at_plug_in_is_left_alone_even_without_notice()
    {
        // The window was closed while they changed it: the mode at plug-in is
        // not the quiet one we set, so it is theirs.
        var policy = new BatteryModePolicy();
        policy.OnUnplugged(SystemMode.Gaming);
        Assert.Null(policy.OnPluggedIn(SystemMode.Performance));
    }

    [Fact]
    public void Remembers_the_persons_choice_not_ours()
    {
        var policy = new BatteryModePolicy();
        policy.OnUnplugged(SystemMode.Gaming);
        // A second unplug notification while already on battery and quiet must
        // not overwrite Gaming with Office.
        policy.OnUnplugged(SystemMode.Office);
        Assert.Equal(SystemMode.Gaming, policy.Remembered);
    }
}
