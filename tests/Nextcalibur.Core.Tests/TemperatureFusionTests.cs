using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class TemperatureFusionTests
{
    private static readonly DateTime T0 = new(2026, 10, 14, 14, 32, 30, DateTimeKind.Utc);

    [Fact]
    public void A_climb_shows_before_the_controller_sees_it()
    {
        // The measured case: package 97 °C at once, the controller 56 for 15 seconds.
        var f = new TemperatureFusion();
        for (var s = 0; s < 30; s++) { f.Register(48, T0.AddSeconds(s - 30)); }
        f.Controller(56, T0);
        f.Register(97, T0.AddSeconds(2));
        Assert.True(f.Value > 90, $"fused {f.Value}");
    }

    [Fact]
    public void The_level_settles_on_the_controllers()
    {
        var f = new TemperatureFusion();
        for (var s = 0; s < 120; s++) f.Register(70, T0.AddSeconds(s));
        f.Controller(66, T0.AddSeconds(120));
        Assert.Equal(66, f.Value!.Value, 1);
    }

    [Fact]
    public void Never_below_the_controller()
    {
        var f = new TemperatureFusion();
        for (var s = 0; s < 60; s++) f.Register(80, T0.AddSeconds(s));
        f.Controller(82, T0.AddSeconds(60));
        f.Register(60, T0.AddSeconds(61)); // a sudden drop in the register
        Assert.True(f.Value >= 82);
    }

    [Fact]
    public void Long_disagreement_sets_the_register_aside()
    {
        var f = new TemperatureFusion();
        for (var s = 0; s <= 180; s += 5)
        {
            f.Register(30, T0.AddSeconds(s));
            f.Controller(70, T0.AddSeconds(s));
        }
        Assert.True(f.RegisterSetAside);
        Assert.Equal(70, f.Value);
    }

    [Fact]
    public void Without_the_register_the_controller_alone()
    {
        var f = new TemperatureFusion();
        f.Register(null, T0);
        f.Controller(64, T0);
        Assert.Equal(64, f.Value);
    }
}
