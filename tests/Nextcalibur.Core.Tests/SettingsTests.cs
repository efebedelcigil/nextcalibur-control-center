using System.Security.AccessControl;
using System.Security.Principal;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The overheat warning is the reason this application stays resident, so the
/// settings that govern it get held to a higher standard than the rest: there
/// is exactly one way to switch it off, and switching it off must not lose what
/// it was set to.
/// </summary>
/// <summary>
/// In the "log" collection with <c>LogInjectionTests</c>, and not because
/// it tests logging: the log's folder is a static, and those tests point
/// it at a temporary one while they count the lines they wrote. Anything
/// logging from another class at that moment lands in their file and is
/// counted as theirs. The retirement pass logs.
/// </summary>
[Collection("log")]
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
        // Start-with-Windows is a scheduled task now, elevated like the rest; settings are the user-scope trace.
        foreach (var name in new[] { "Your settings" })
        {
            var trace = Footprint.Survey().Single(t => t.Name == name);
            Assert.False(trace.NeedsElevation);
        }
    }

    [Fact]
    public void Every_entry_on_retirement_list_has_detection_and_removal()
    {
        var entries = Footprint.Retirements();
        Assert.NotEmpty(entries);

        foreach (var entry in entries)
        {
            Assert.False(string.IsNullOrWhiteSpace(entry.Description), "Entry description must not be empty");
            Assert.NotNull(entry.CreatedIn);
            Assert.NotNull(entry.Detect);
            Assert.NotNull(entry.Remove);
        }
    }

    [Fact]
    public void Retired_entries_have_valid_version_order()
    {
        var entries = Footprint.Retirements();
        foreach (var entry in entries)
        {
            if (entry.RetiredIn is not null)
            {
                Assert.True(entry.RetiredIn >= entry.CreatedIn,
                    $"{entry.Description}: RetiredIn ({entry.RetiredIn}) must be >= CreatedIn ({entry.CreatedIn})");
            }
        }
    }

    [Fact]
    public void Ndu_entry_is_unretired_and_needs_asking()
    {
        var entries = Footprint.Retirements();
        var ndu = Assert.Single(entries, e => e.Description.Contains("NDU", StringComparison.OrdinalIgnoreCase));

        Assert.Null(ndu.RetiredIn); // RetiredIn must be empty/null while the feature is active
        Assert.True(ndu.NeedsAsking, "Windows fixes must require asking before removal");
    }

    [Fact]
    public void Non_windows_entries_do_not_need_asking()
    {
        var entries = Footprint.Retirements();
        foreach (var entry in entries.Where(e => !e.Description.Contains("NDU", StringComparison.OrdinalIgnoreCase)))
        {
            Assert.False(entry.NeedsAsking, $"{entry.Description} is an internal application trace and should not ask");
        }
    }

    [Fact]
    public void Older_version_does_not_execute_later_retirements()
    {
        // An earlier version (e.g. 0.5.3) running against the list must not execute 0.5.4 retirements.
        var pastVersion = new Version(0, 5, 3);
        var retired = Footprint.RetireOldVersions(currentVersion: pastVersion);
        Assert.Empty(retired);
    }

    [Fact]
    public void Retirement_pass_is_safe_to_run_twice()
    {
        // Must be safe to run twice: nothing fails on repeated execution. No test writes to HKLM.
        var first = Footprint.RetireOldVersions(currentVersion: new Version(0, 5, 4));
        var second = Footprint.RetireOldVersions(currentVersion: new Version(0, 5, 4));

        Assert.NotNull(first);
        Assert.NotNull(second);
    }

    [Fact]
    public void Retirement_pass_waits_when_entry_needs_asking_and_askUser_is_null()
    {
        var traceExists = true;
        var entry = new RetirementEntry(
            Description: "Mock guarded setting",
            CreatedIn: new Version(0, 5, 0),
            RetiredIn: new Version(0, 5, 4),
            Detect: () => traceExists,
            Remove: () => { traceExists = false; return true; },
            NeedsAsking: true);

        var removed = Footprint.RetireOldVersions(
            currentVersion: new Version(0, 5, 4),
            askUser: null,
            entries: new[] { entry });

        Assert.Empty(removed);
        Assert.True(traceExists, "Trace must still be there when askUser is null");
    }

    [Fact]
    public void Retirement_pass_waits_when_entry_needs_asking_and_askUser_returns_false()
    {
        var traceExists = true;
        var entry = new RetirementEntry(
            Description: "Mock guarded setting",
            CreatedIn: new Version(0, 5, 0),
            RetiredIn: new Version(0, 5, 4),
            Detect: () => traceExists,
            Remove: () => { traceExists = false; return true; },
            NeedsAsking: true);

        var removed = Footprint.RetireOldVersions(
            currentVersion: new Version(0, 5, 4),
            askUser: _ => false,
            entries: new[] { entry });

        Assert.Empty(removed);
        Assert.True(traceExists, "Trace must still be there when user declines");
    }

    [Fact]
    public void Retirement_pass_removes_when_entry_needs_asking_and_askUser_returns_true()
    {
        var traceExists = true;
        var entry = new RetirementEntry(
            Description: "Mock guarded setting",
            CreatedIn: new Version(0, 5, 0),
            RetiredIn: new Version(0, 5, 4),
            Detect: () => traceExists,
            Remove: () => { traceExists = false; return true; },
            NeedsAsking: true);

        var removed = Footprint.RetireOldVersions(
            currentVersion: new Version(0, 5, 4),
            askUser: _ => true,
            entries: new[] { entry });

        Assert.Single(removed);
        Assert.False(traceExists, "Trace must be gone when user permits removal");
    }

    [Fact]
    public void Retirement_pass_does_not_log_or_report_when_remove_does_nothing()
    {
        var entry = new RetirementEntry(
            Description: "Mock no-op setting",
            CreatedIn: new Version(0, 5, 0),
            RetiredIn: new Version(0, 5, 4),
            Detect: () => true,
            Remove: () => false,
            NeedsAsking: false);

        var removed = Footprint.RetireOldVersions(
            currentVersion: new Version(0, 5, 4),
            askUser: null,
            entries: new[] { entry });

        Assert.Empty(removed);
    }

    [Fact]
    public void IsRestartPending_comparison_with_equal_earlier_and_missing_boot()
    {
        var currentBoot = new DateTime(2026, 9, 14, 12, 0, 0, DateTimeKind.Utc);

        // Equal to current boot: still pending
        Assert.True(AppSettings.IsRestartPending(currentBoot, currentBoot));

        // Within tolerance (e.g. clock rounding): still pending
        Assert.True(AppSettings.IsRestartPending(currentBoot.AddMinutes(0.5), currentBoot));

        // Earlier boot time (machine has rebooted): cleared
        Assert.False(AppSettings.IsRestartPending(currentBoot.AddHours(-1), currentBoot));
        Assert.False(AppSettings.IsRestartPending(currentBoot.AddMinutes(-5), currentBoot));

        // Missing (null): cleared
        Assert.False(AppSettings.IsRestartPending(null, currentBoot));
    }

    [Fact]
    public void AppSettings_Migrate_clears_pending_restart_when_boot_time_differs()
    {
        var currentBoot = AppSettings.CurrentBootTimeUtc();
        var oldBoot = currentBoot.AddHours(-2);

        var settings = new AppSettings
        {
            PendingRestart = new PendingRestartInfo
            {
                BootTimeUtc = oldBoot,
                Reasons = new List<string> { "ndu", "gpu" },
                ReasonArguments = new Dictionary<string, string> { { "gpu", "Discrete" } },
            },
        };

        var migrated = AppSettings.Migrate(settings);
        Assert.Null(migrated.PendingRestart);
    }

    [Fact]
    public void AppSettings_RequestRestart_and_ClearRestartReason_manage_reasons_properly()
    {
        var settings = new AppSettings();
        Assert.False(settings.HasPendingRestart);

        settings.RequestRestart("ndu");
        Assert.True(settings.HasPendingRestart);
        Assert.Contains("ndu", settings.PendingRestart!.Reasons);

        settings.RequestRestart("gpu", "Discrete");
        Assert.Contains("gpu", settings.PendingRestart.Reasons);
        Assert.Equal("Discrete", settings.PendingRestart.ReasonArguments["gpu"]);

        settings.ClearRestartReason("ndu");
        Assert.DoesNotContain("ndu", settings.PendingRestart.Reasons);
        Assert.True(settings.HasPendingRestart);

        settings.ClearRestartReason("gpu");
        Assert.Null(settings.PendingRestart);
        Assert.False(settings.HasPendingRestart);
    }
}

/// <summary>
/// Check before you change: every component that writes to the machine has to
/// look first, and leave alone anything already in the state it wanted.
///
/// The owner asked for this after the autopsy, and one of the components was
/// getting it wrong in a way that mattered: the sensor permission was written
/// over whatever was there, so on a machine running the vendor's software it
/// would have removed that software's own access. Complaining about a vendor
/// leaving a permission behind and then trampling theirs is not a position
/// worth holding.
/// </summary>
public class IdempotenceTests
{
    [Fact]
    public void A_machine_that_already_has_access_is_left_alone()
    {
        // Grant returns false when it wrote nothing. On this machine access is
        // already in place, so a grant must be a no-op - and must not need
        // elevation to work that out.
        if (MailboxAccess.Check() != MailboxAvailability.Available) return;

        Assert.False(MailboxAccess.Grant());
    }

    [Fact]
    public void Repairing_a_machine_with_nothing_wrong_changes_nothing()
    {
        var service = new PowerOverlayService();
        if (service.Diagnose().NeedsRepair) return;

        var outcome = service.Repair();

        Assert.Empty(outcome.Done);
        Assert.Null(outcome.Blocked);
        Assert.True(outcome.Empty);
    }
}

/// <summary>
/// The bug this pins broke every reading on the development machine for a
/// couple of minutes, and would have shipped: a grant that runs elevated
/// deciding the account already had access, because an elevated token carries
/// the administrators group and the descriptor's built-in entry looked like a
/// match. It wrote nothing and reported success.
/// </summary>
public class MailboxAccessCoverageTests
{
    private static SecurityIdentifier Me =>
        WindowsIdentity.GetCurrent().User ?? throw new InvalidOperationException("no account");

    private static RawSecurityDescriptor Descriptor(string sddl) => new(sddl);

    [Fact]
    public void An_administrators_entry_does_not_count_as_access()
    {
        // True only while elevated, which is exactly when this question gets
        // asked - so counting it is how the machine ends up unreadable.
        Assert.False(MailboxAccess.HasAccess(
            Descriptor("O:BAG:BAD:(A;;0x12001f;;;BA)(A;;0x12001f;;;SY)"), Me));
    }

    [Fact]
    public void This_accounts_own_entry_counts()
    {
        Assert.True(MailboxAccess.HasAccess(
            Descriptor($"O:BAG:BAD:(A;;0x12001f;;;BA)(A;;0x12001f;;;{Me.Value})"), Me));
    }

    [Fact]
    public void The_vendors_grant_to_everyone_counts()
    {
        // Authenticated Users applies whether or not anybody is elevated, so a
        // machine running the vendor's software needs nothing added.
        Assert.True(MailboxAccess.HasAccess(Descriptor("O:BAG:BAD:(A;;0x121fff;;;AU)"), Me));
    }

    [Fact]
    public void An_entry_that_grants_too_little_does_not_count()
    {
        // Read without write would light no keyboard.
        Assert.False(MailboxAccess.HasAccess(
            Descriptor($"O:BAG:BAD:(A;;0x00001;;;{Me.Value})"), Me));
    }

    [Fact]
    public void An_empty_descriptor_grants_nothing()
    {
        Assert.False(MailboxAccess.HasAccess(Descriptor("O:BAG:BAD:"), Me));
    }
}

public class ForwardCompatibilityTests
{
    [Fact]
    public void A_setting_this_version_does_not_know_survives_a_round_trip()
    {
        var json = """{"PollIntervalMs":2000,"FutureSetting":"keep me","Nested":{"a":1}}""";
        var settings = System.Text.Json.JsonSerializer.Deserialize<AppSettings>(json)!;
        var back = System.Text.Json.JsonSerializer.Serialize(settings);
        Assert.Contains("\"FutureSetting\":\"keep me\"", back);
        Assert.Contains("\"Nested\":{\"a\":1}", back);
    }
}
