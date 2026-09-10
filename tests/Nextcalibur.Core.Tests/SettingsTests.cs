using Nextcalibur.Core.Configuration;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The overheat warning is the reason this application stays resident, so the
/// settings that govern it get held to a higher standard than the rest: there
/// is exactly one way to switch it off, and switching it off must not lose what
/// it was set to.
/// </summary>
public class SettingsTests
{
    [Fact]
    public void A_new_installation_watches_the_temperature()
    {
        var settings = new AppSettings();

        Assert.True(settings.WarnsAboutHeat);
        Assert.Equal(90, settings.CpuWarningTemperatureC);
    }

    [Fact]
    public void Switching_the_warning_off_keeps_the_threshold()
    {
        var settings = new AppSettings { CpuWarningTemperatureC = 85 };

        settings.OverheatWarningEnabled = false;

        Assert.False(settings.WarnsAboutHeat);
        Assert.Equal(85, settings.CpuWarningTemperatureC);

        settings.OverheatWarningEnabled = true;
        Assert.True(settings.WarnsAboutHeat);
        Assert.Equal(85, settings.CpuWarningTemperatureC);
    }

    [Fact]
    public void A_zero_threshold_can_never_mean_warn_at_zero()
    {
        // Whatever route produces it, a zero threshold must not turn every
        // reading into a notification.
        var settings = new AppSettings { CpuWarningTemperatureC = 0 };

        Assert.False(settings.WarnsAboutHeat);
    }

    [Fact]
    public void An_older_settings_file_that_used_zero_to_mean_off_still_means_off()
    {
        // How the warning was switched off before there was a switch.
        var migrated = AppSettings.Migrate(new AppSettings { CpuWarningTemperatureC = 0 });

        Assert.False(migrated.OverheatWarningEnabled);
        Assert.False(migrated.WarnsAboutHeat);

        // And the threshold comes back usable, so turning it on later does not
        // land on nothing.
        Assert.True(migrated.CpuWarningTemperatureC > 0);
    }

    [Fact]
    public void An_older_settings_file_with_a_threshold_is_left_alone()
    {
        var migrated = AppSettings.Migrate(new AppSettings { CpuWarningTemperatureC = 80 });

        Assert.True(migrated.WarnsAboutHeat);
        Assert.Equal(80, migrated.CpuWarningTemperatureC);
    }

    [Fact]
    public void A_missing_settings_file_gives_the_defaults()
    {
        var migrated = AppSettings.Migrate(null);

        Assert.True(migrated.WarnsAboutHeat);
    }
}
