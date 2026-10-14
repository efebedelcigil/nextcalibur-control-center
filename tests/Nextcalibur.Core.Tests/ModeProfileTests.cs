using Nextcalibur.Core.Power;
using Xunit;
using AcDc = Nextcalibur.Core.Power.PlanTuning.AcDc;

namespace Nextcalibur.Core.Tests;

public class ModeProfileTests
{
    public static TheoryData<SystemMode, bool> Modes => new()
    {
        { SystemMode.Office, true }, { SystemMode.Balanced, true }, { SystemMode.Gaming, true }, { SystemMode.Performance, true },
        { SystemMode.Office, false }, { SystemMode.Balanced, false }, { SystemMode.Gaming, false }, { SystemMode.Performance, false },
    };

    [Theory]
    [MemberData(nameof(Modes))]
    public void Every_mode_sets_every_setting_and_nothing_else(SystemMode mode, bool hybrid) =>
        Assert.Equal(ModeProfile.Settings.OrderBy(g => g), ModeProfile.For(mode, ModeProfile.Defaults(hybrid)).Keys.OrderBy(g => g));

    [Theory]
    [MemberData(nameof(Modes))]
    public void Values_fit_the_original_markers(SystemMode mode, bool hybrid)
    {
        // Originals are kept as "ac,dc"; every value written must read back.
        foreach (var v in ModeProfile.For(mode, ModeProfile.Defaults(hybrid)).Values)
            Assert.Equal(v, PlanTuning.ParseMarker($"{v.Ac},{v.Dc}"));
    }

    [Theory]
    [MemberData(nameof(Modes))]
    public void No_mode_pins_the_processor(SystemMode mode, bool hybrid)
    {
        // High performance's own floor is 100% plugged in: never taken.
        var floor = ModeProfile.For(mode, ModeProfile.Defaults(hybrid))[PlanTuning.MinState];
        Assert.True(floor.Ac < PlanTuning.PinnedFloor && floor.Dc < PlanTuning.PinnedFloor);
    }

    [Fact]
    public void Balanced_is_what_Windows_chose_for_Balanced_here()
    {
        // A machine whose Balanced differs from the defaults: the mode follows it.
        var b = ModeProfile.Defaults(true);
        var balanced = b.Balanced.ToDictionary(x => x.Key, x => x.Value);
        balanced[ModeProfile.EnergyPreference] = new AcDc(38, 55);
        balanced[ModeProfile.BoostMode] = new AcDc(1, 1);
        var mode = ModeProfile.For(SystemMode.Balanced, b with { Balanced = balanced });
        Assert.Equal(new AcDc(38, 55), mode[ModeProfile.EnergyPreference]);
        Assert.Equal(new AcDc(1, 1), mode[ModeProfile.BoostMode]);
    }

    [Fact]
    public void Gaming_sits_between_Balanced_and_High_performance()
    {
        var b = ModeProfile.Defaults(true);
        var gaming = ModeProfile.For(SystemMode.Gaming, b);
        // Plugged in: no higher than the top of Intel's performance band; on battery: Balanced's.
        Assert.Equal(ModeProfile.PerformanceBandTop, gaming[ModeProfile.EnergyPreference].Ac);
        Assert.Equal(b.Balanced[ModeProfile.EnergyPreference].Dc, gaming[ModeProfile.EnergyPreference].Dc);
        Assert.True(gaming[ModeProfile.BoostMode].Ac >= 2);
        // A Balanced already eager stays where it is.
        var balanced = b.Balanced.ToDictionary(x => x.Key, x => x.Value);
        balanced[ModeProfile.EnergyPreference] = new AcDc(10, 50);
        Assert.Equal(10, ModeProfile.For(SystemMode.Gaming, b with { Balanced = balanced })[ModeProfile.EnergyPreference].Ac);
    }

    [Fact]
    public void Office_caps_both_processor_classes_with_an_efficient_boost()
    {
        var office = ModeProfile.For(SystemMode.Office, ModeProfile.Defaults(true));
        Assert.Equal(PlanTuning.CoolCeiling, office[PlanTuning.MaxState].Ac);
        Assert.Equal(PlanTuning.CoolCeiling, office[PlanTuning.MaxState1].Ac);
        Assert.Equal(4, office[ModeProfile.BoostMode].Ac); // power saver's aggressive -> efficient aggressive
    }

    [Fact]
    public void Office_is_never_less_efficient_than_Balanced()
    {
        var b = ModeProfile.Defaults(false);
        var saver = b.PowerSaver.ToDictionary(x => x.Key, x => x.Value);
        saver[ModeProfile.EnergyPreference] = new AcDc(20, 70); // a power saver set oddly
        var office = ModeProfile.For(SystemMode.Office, b with { PowerSaver = saver });
        Assert.Equal(new AcDc(45, 70), office[ModeProfile.EnergyPreference]);
    }

    [Fact]
    public void Core_preference_only_on_a_processor_with_two_kinds_of_core()
    {
        var hybrid = ModeProfile.For(SystemMode.Office, ModeProfile.Defaults(true));
        var plain = ModeProfile.For(SystemMode.Office, ModeProfile.Defaults(false));
        Assert.Equal(4, hybrid[ModeProfile.Scheduling].Ac);
        Assert.Equal(5, plain[ModeProfile.Scheduling].Ac);
        Assert.Equal(2, ModeProfile.For(SystemMode.Performance, ModeProfile.Defaults(true))[ModeProfile.Scheduling].Ac);
    }

    [Fact]
    public void The_modes_lean_from_efficiency_to_performance()
    {
        int Epp(SystemMode m) => ModeProfile.For(m, ModeProfile.Defaults(true))[ModeProfile.EnergyPreference].Ac;
        Assert.True(Epp(SystemMode.Office) > Epp(SystemMode.Balanced));
        Assert.True(Epp(SystemMode.Balanced) > Epp(SystemMode.Gaming));
        Assert.True(Epp(SystemMode.Gaming) > Epp(SystemMode.Performance));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(2, 4)]
    [InlineData(5, 4)]
    public void Boost_modes_have_an_efficient_variant(int boost, int efficient) =>
        Assert.Equal(efficient, ModeProfile.Efficient(boost));

    [Fact]
    public void This_machine_reads_a_baseline()
    {
        // Reads only; on any Windows the built-in plans answer.
        var b = ModeProfile.ReadBaseline();
        Assert.Equal(ModeProfile.Settings.Count, b.Balanced.Count);
    }
}
