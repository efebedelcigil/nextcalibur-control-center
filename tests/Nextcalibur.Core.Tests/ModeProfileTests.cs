using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class ModeProfileTests
{
    public static TheoryData<SystemMode> Modes => new() { SystemMode.Office, SystemMode.Gaming, SystemMode.Performance };

    [Theory]
    [MemberData(nameof(Modes))]
    public void Every_mode_sets_every_setting_and_nothing_else(SystemMode mode) =>
        Assert.Equal(ModeProfile.Settings.OrderBy(g => g), ModeProfile.For(mode).Keys.OrderBy(g => g));

    [Theory]
    [MemberData(nameof(Modes))]
    public void Values_fit_the_original_markers(SystemMode mode)
    {
        // Originals are kept as "ac,dc" percentages; every value written must read back.
        foreach (var v in ModeProfile.For(mode).Values)
            Assert.Equal(v, PlanTuning.ParseMarker($"{v.Ac},{v.Dc}"));
    }

    [Theory]
    [MemberData(nameof(Modes))]
    public void No_mode_pins_the_processor(SystemMode mode)
    {
        var floor = ModeProfile.For(mode)[PlanTuning.MinState];
        Assert.True(floor.Ac < PlanTuning.PinnedFloor && floor.Dc < PlanTuning.PinnedFloor);
    }

    [Fact]
    public void Office_caps_both_processor_classes()
    {
        var office = ModeProfile.For(SystemMode.Office);
        Assert.Equal(PlanTuning.CoolCeiling, office[PlanTuning.MaxState].Ac);
        Assert.Equal(PlanTuning.CoolCeiling, office[PlanTuning.MaxState1].Ac);
    }

    [Fact]
    public void Office_leans_further_toward_efficiency_than_gaming_and_gaming_than_performance()
    {
        int Epp(SystemMode m) => ModeProfile.For(m)[ModeProfile.EnergyPreference].Ac;
        Assert.True(Epp(SystemMode.Office) > Epp(SystemMode.Gaming));
        Assert.True(Epp(SystemMode.Gaming) > Epp(SystemMode.Performance));
    }
}
