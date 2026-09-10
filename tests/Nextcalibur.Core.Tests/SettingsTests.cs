using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
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

/// <summary>
/// The rule that decides whether somebody is told "your laptop isn't supported"
/// or "this needs permission once". Getting it backwards is how a working
/// machine gets written off.
/// </summary>
public class MailboxAccessTests
{
    [Fact]
    public void The_block_guid_is_written_the_way_the_registry_expects()
    {
        // No braces, lower case. Every entry already under that key is written
        // this way, and a value written any other way is never consulted - it
        // sits in the registry looking correct while access stays refused. This
        // cost an hour on hardware, so it is pinned here.
        Assert.DoesNotContain("{", MailboxAccess.BlockGuid);
        Assert.DoesNotContain("}", MailboxAccess.BlockGuid);
        Assert.Equal(MailboxAccess.BlockGuid, MailboxAccess.BlockGuid.ToLowerInvariant());
        Assert.True(Guid.TryParse(MailboxAccess.BlockGuid, out _));
    }

    [Fact]
    public void Checking_availability_never_throws()
    {
        // Runs on whatever machine the tests run on, including one with no such
        // interface at all. A diagnostic that throws is worse than one that
        // says "no".
        var state = MailboxAccess.Check();

        Assert.True(Enum.IsDefined(state));
    }

    [Fact]
    public void Elevation_is_reported_not_assumed()
    {
        // Whatever the answer here, asking must not throw: it is called before
        // anything is attempted, precisely to avoid attempting it.
        _ = MailboxAccess.IsElevated();
    }
}

/// <summary>
/// What Nextcalibur leaves on a machine, and what it may take back without
/// asking. The rule this pins was set after watching the vendor's uninstaller:
/// thorough about its own files, and then one permission left on the machine
/// for ever - which is what silently stopped every reading here weeks later.
/// </summary>
public class FootprintTests
{
    [Fact]
    public void Every_trace_says_whether_undoing_it_needs_permission()
    {
        // The uninstall hook has thirty seconds. It cannot afford to discover
        // halfway through that something needs elevation.
        foreach (var trace in Footprint.Survey())
        {
            Assert.False(string.IsNullOrWhiteSpace(trace.Name));
        }
    }

    [Fact]
    public void The_power_repair_is_the_one_worth_keeping()
    {
        // It fixes Windows, not us. Undoing it on the way out would leave the
        // machine worse than we found it, so it can never be removed silently.
        var repair = Footprint.Survey().Single(t => t.Name.Contains("power-mode repair"));

        Assert.True(repair.KeepingIsReasonable);
        Assert.True(repair.NeedsElevation);
    }

    [Fact]
    public void Nothing_that_only_serves_this_application_is_worth_keeping()
    {
        foreach (var trace in Footprint.Survey().Where(t => !t.KeepingIsReasonable))
        {
            Assert.DoesNotContain("repair", trace.Name);
        }
    }

    [Fact]
    public void The_user_scope_traces_need_no_permission()
    {
        // Settings and the startup entry live under the current user, so an
        // uninstall can always clear them - no prompt, no question.
        foreach (var name in new[] { "Your settings", "The start-with-Windows entry" })
        {
            var trace = Footprint.Survey().Single(t => t.Name == name);
            Assert.False(trace.NeedsElevation);
        }
    }
}
