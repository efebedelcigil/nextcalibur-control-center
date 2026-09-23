using System.Text.Json;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The two files in the profile are the account's to write, and the elevated
/// application turns what they say into firmware commands and a driver's
/// start type. What comes out of loading them has to be something that is
/// safe to send, whatever went in.
/// </summary>
public class HostileSettingsTests
{
    [Theory]
    [InlineData("15")]
    [InlineData("255")]
    [InlineData("\"NotAnEffect\"")]
    public void An_unknown_lighting_effect_is_not_sent_to_the_firmware(string effect)
    {
        var json = "{\"ActiveProfile\":\"UserDefine\",\"Profiles\":{\"UserDefine\":{\"Effect\":" + effect + ",\"BrightnessPercent\":50}}}";
        LedState? loaded;
        try { loaded = JsonSerializer.Deserialize<LedState>(json); }
        catch (JsonException) { return; }   // refused outright is safe too

        var state = LedState.Sanitised(loaded!);
        Assert.True(Enum.IsDefined(state.Effect));
    }

    [Fact]
    public void Nulls_and_out_of_range_values_come_back_usable()
    {
        var json = """{"ActiveProfile":null,"Profiles":{"UserDefine":{"Effect":"Static","BrightnessPercent":900,"Colours":null},"Office":null}}""";
        var state = LedState.Sanitised(JsonSerializer.Deserialize<LedState>(json)!);

        Assert.Equal(LedState.UserDefine, state.ActiveProfile);
        Assert.Equal(100, state.BrightnessPercent);
        Assert.Equal(((byte)0xFF, (byte)0xFF, (byte)0xFF), state.GetColour(LedZone.Left));
        foreach (var name in LedState.ProfileNames)
            Assert.NotNull(state.Profiles[name]);
    }

    [Fact]
    public void A_colour_wider_than_24_bits_is_cut_to_24()
    {
        var json = """{"Profiles":{"UserDefine":{"Colours":{"Left":4294967295}}}}""";
        var state = LedState.Sanitised(JsonSerializer.Deserialize<LedState>(json)!);
        Assert.Equal(0xFFFFFFu, state.Current.Colours["Left"]);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(0, null)]      // boot start - never for NDU
    [InlineData(1, null)]      // system start
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(4, 4)]
    [InlineData(5, null)]
    [InlineData(-1, null)]
    public void Only_start_types_a_network_driver_may_have_are_restored(int? given, int? expected)
    {
        Assert.Equal(expected, NduFix.Valid(given));
    }

    [Fact]
    public void A_settings_file_cannot_carry_a_start_type_into_the_registry()
    {
        var settings = AppSettings.Migrate(JsonSerializer.Deserialize<AppSettings>("""{"OriginalNduStart":0}"""));
        Assert.Null(settings.OriginalNduStart);
    }
}
