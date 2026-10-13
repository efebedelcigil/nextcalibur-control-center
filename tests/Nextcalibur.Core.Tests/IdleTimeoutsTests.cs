using Nextcalibur.Core.Power;
using Xunit;
using AcDc = Nextcalibur.Core.Power.PlanTuning.AcDc;

namespace Nextcalibur.Core.Tests;

public class IdleTimeoutsTests
{
    private static readonly Guid Office = Guid.NewGuid(), Gaming = Guid.NewGuid(), Performance = Guid.NewGuid();

    private static Timeouts T(int dAc, int dDc, int sAc, int sDc) => new(new AcDc(dAc, dDc), new AcDc(sAc, sDc));

    [Fact]
    public void Nothing_changed_is_nothing()
    {
        var known = new Dictionary<Guid, Timeouts> { [Office] = T(300, 180, 1800, 600) };
        Assert.Null(IdleTimeouts.ChangedOutside(known, new Dictionary<Guid, Timeouts>(known), Office));
    }

    [Fact]
    public void A_plan_never_seen_before_is_not_a_change()
    {
        var known = new Dictionary<Guid, Timeouts>();
        var now = new Dictionary<Guid, Timeouts> { [Office] = T(300, 180, 1800, 600) };
        Assert.Null(IdleTimeouts.ChangedOutside(known, now, Office));
    }

    [Fact]
    public void The_active_plan_is_looked_at_first()
    {
        var known = new Dictionary<Guid, Timeouts> { [Office] = T(300, 180, 0, 0), [Gaming] = T(0, 180, 0, 0) };
        var now = new Dictionary<Guid, Timeouts> { [Office] = T(600, 180, 0, 0), [Gaming] = T(60, 180, 0, 0) };
        Assert.Equal(Gaming, IdleTimeouts.ChangedOutside(known, now, Gaming));
    }

    [Fact]
    public void Only_the_changed_half_is_reported()
    {
        var d = IdleTimeouts.Differences(T(300, 180, 1800, 600), T(300, 120, 1800, 600));
        Assert.Equal([(IdleSetting.DisplayOff, false, 120)], d);
    }

    [Fact]
    public void Carrying_keeps_the_other_mode_s_other_half()
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
