using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class IdleRecommendedTests
{
    [Theory]
    [InlineData(SystemMode.Office)]
    [InlineData(SystemMode.Balanced)]
    [InlineData(SystemMode.Gaming)]
    [InlineData(SystemMode.Performance)]
    public void Battery_is_never_longer_than_plugged_in_and_nothing_is_never(SystemMode mode)
    {
        var t = IdleTimeouts.Recommended(mode);
        Assert.True(t.Display.Dc > 0 && t.Display.Dc <= t.Display.Ac);
        Assert.True(t.Sleep.Dc > 0 && t.Sleep.Dc <= t.Sleep.Ac);
        Assert.True(t.Display.Ac < t.Sleep.Ac && t.Display.Dc < t.Sleep.Dc);   // the screen goes before the machine sleeps
    }

    [Fact]
    public void The_quiet_modes_are_never_longer_than_the_fast_ones()
    {
        var saver = IdleTimeouts.Recommended(SystemMode.Office);
        var gaming = IdleTimeouts.Recommended(SystemMode.Gaming);
        Assert.True(saver.Display.Ac <= gaming.Display.Ac && saver.Sleep.Ac <= gaming.Sleep.Ac);
    }
}
