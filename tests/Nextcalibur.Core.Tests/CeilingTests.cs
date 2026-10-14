using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class CeilingTests
{
    private static readonly ModeProfile.Baseline B = ModeProfile.Defaults(true);

    [Theory]
    [InlineData(95, 90)]   // never above the measured upper bound
    [InlineData(80, 80)]
    [InlineData(20, ModeProfile.LowestPercent)]
    [InlineData(null, 90)]
    public void Power_Savers_ceiling_stays_within_its_bounds(int? wanted, int written)
    {
        var office = ModeProfile.For(SystemMode.Office, B, new ModeProfile.Limits(wanted, null));
        Assert.Equal(written, office[PlanTuning.MaxState].Ac);
        Assert.Equal(written, office[PlanTuning.MaxState1].Dc);
    }

    [Fact]
    public void A_boosting_mode_gets_a_clock_limit_and_keeps_its_ceiling()
    {
        var gaming = ModeProfile.For(SystemMode.Gaming, B, new ModeProfile.Limits(null, 3800));
        Assert.Equal(3800, gaming[ModeProfile.FrequencyMax].Ac);
        Assert.Equal(3800, gaming[ModeProfile.FrequencyMax1].Dc);
        Assert.Equal(100, gaming[PlanTuning.MaxState].Ac);
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData(0, 0)]
    [InlineData(900, ModeProfile.LowestFrequencyMhz)]
    [InlineData(9000, ModeProfile.HighestFrequencyMhz)]
    public void A_clock_limit_is_none_or_within_range(int? wanted, int written) =>
        Assert.Equal(written, ModeProfile.ClampFrequency(wanted));

    [Fact]
    public void Power_Saver_has_no_clock_limit_its_percentage_already_stops_the_boost() =>
        Assert.Equal(0, ModeProfile.For(SystemMode.Office, B, new ModeProfile.Limits(80, 3000))[ModeProfile.FrequencyMax].Ac);
}
