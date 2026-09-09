using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// Lighting state held in memory. Persistence is not covered here: it writes to
/// the real settings directory, and a test that clobbers somebody's keyboard
/// colours to prove it can save them has cost more than it found.
/// </summary>
public class LedStateTests
{
    [Fact]
    public void A_colour_comes_back_as_it_went_in()
    {
        var state = new LedState();
        state.SetColour(LedZone.Middle, 0x12, 0x34, 0x56);

        Assert.Equal((0x12, 0x34, 0x56), state.GetColour(LedZone.Middle));
    }

    [Fact]
    public void Setting_one_zone_leaves_the_others_alone()
    {
        var state = new LedState();
        state.SetColour(LedZone.Left, 255, 0, 0);
        state.SetColour(LedZone.Right, 0, 0, 255);
        state.SetColour(LedZone.Middle, 0, 255, 0);

        Assert.Equal(((byte)255, (byte)0, (byte)0), state.GetColour(LedZone.Left));
        Assert.Equal(((byte)0, (byte)255, (byte)0), state.GetColour(LedZone.Middle));
        Assert.Equal(((byte)0, (byte)0, (byte)255), state.GetColour(LedZone.Right));
    }

    [Theory]
    [InlineData(LedZone.AllKeyboard)]
    [InlineData(LedZone.Everything)]
    public void The_whole_keyboard_sets_all_three_zones(LedZone zone)
    {
        var state = new LedState();
        state.SetColour(LedZone.Left, 1, 1, 1);
        state.SetColour(zone, 9, 8, 7);

        Assert.Equal(((byte)9, (byte)8, (byte)7), state.GetColour(LedZone.Left));
        Assert.Equal(((byte)9, (byte)8, (byte)7), state.GetColour(LedZone.Middle));
        Assert.Equal(((byte)9, (byte)8, (byte)7), state.GetColour(LedZone.Right));
    }

    [Fact]
    public void Black_is_a_colour_and_not_an_absent_one()
    {
        // Zero packs to zero, which is also what a missing entry would look
        // like if the lookup went through a falsy check rather than a key test.
        var state = new LedState();
        state.SetColour(LedZone.Left, 0, 0, 0);

        Assert.Equal(((byte)0, (byte)0, (byte)0), state.GetColour(LedZone.Left));
    }

    [Fact]
    public void Each_profile_keeps_its_own_colours()
    {
        var state = new LedState { ActiveProfile = LedState.Office };
        state.SetColour(LedZone.Left, 255, 0, 0);

        state.ActiveProfile = LedState.Gaming;
        state.SetColour(LedZone.Left, 0, 0, 255);

        state.ActiveProfile = LedState.Office;
        Assert.Equal(((byte)255, (byte)0, (byte)0), state.GetColour(LedZone.Left));
    }

    [Fact]
    public void Effect_and_brightness_follow_the_active_profile()
    {
        var state = new LedState { ActiveProfile = LedState.Office };
        state.Effect = LedEffect.Breathing;
        state.BrightnessPercent = 40;

        state.ActiveProfile = LedState.Performance;
        state.Effect = LedEffect.Static;
        state.BrightnessPercent = 100;

        state.ActiveProfile = LedState.Office;
        Assert.Equal(LedEffect.Breathing, state.Effect);
        Assert.Equal(40, state.BrightnessPercent);
    }

    [Fact]
    public void An_unknown_profile_name_is_created_rather_than_thrown()
    {
        // ActiveProfile is read from a settings file a person can edit.
        var state = new LedState { ActiveProfile = "something-nobody-wrote" };

        var exception = Record.Exception(() => state.SetColour(LedZone.Left, 1, 2, 3));

        Assert.Null(exception);
        Assert.Equal(((byte)1, (byte)2, (byte)3), state.GetColour(LedZone.Left));
    }

    [Fact]
    public void The_four_named_profiles_all_exist_by_default()
    {
        var state = new LedState();

        foreach (var name in LedState.ProfileNames)
            Assert.True(state.Profiles.ContainsKey(name), $"missing profile: {name}");
    }
}
