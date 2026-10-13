using Nextcalibur.Core.Power;
using Xunit;
using AcDc = Nextcalibur.Core.Power.PlanTuning.AcDc;

namespace Nextcalibur.Core.Tests;

public class IdleTimeoutsTests
{
    private static Timeouts T(int dAc, int dDc, int sAc, int sDc) => new(new AcDc(dAc, dDc), new AcDc(sAc, sDc));

    [Fact]
    public void Replacing_one_half_keeps_the_other()
    {
        Assert.Equal(new AcDc(900, 600), IdleTimeouts.Carry(new AcDc(0, 600), pluggedIn: true, 900));
        Assert.Equal(new AcDc(0, 60), IdleTimeouts.Carry(new AcDc(0, 600), pluggedIn: false, 60));
    }

    [Fact]
    public void Choices_hold_the_current_value_and_end_with_never()
    {
        var choices = IdleTimeouts.Choices(420);
        Assert.Contains(420, choices);
        Assert.Equal(0, choices[^1]);
        Assert.Equal(choices.Take(choices.Count - 1).OrderBy(x => x), choices.Take(choices.Count - 1));
        Assert.Single(IdleTimeouts.Choices(0), 0);
    }

    [Fact]
    public void A_timeout_reads_by_setting()
    {
        var t = T(300, 180, 1800, 600);
        Assert.Equal(new AcDc(1800, 600), t[IdleSetting.Sleep]);
        Assert.Equal(T(300, 180, 60, 600), t.With(IdleSetting.Sleep, new AcDc(60, 600)));
    }
}
