using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// Decisions the project has settled, pinned so that changing them has to be
/// deliberate. None of these would break a build if they drifted; all of them
/// would change how the application behaves on somebody's machine.
/// </summary>
public class CoexistencePolicyTests
{
    [Fact]
    public void Sharing_the_mailbox_means_asking_for_less_not_more()
    {
        // Both applications drive one mailbox and it holds one command at a
        // time. Neither can lock the other out, so the only way to be a good
        // neighbour is to poll further apart and give up sooner. A change that
        // inverted either of these would turn a collision into a stall for
        // both, which is the failure this policy exists to prevent.
        Assert.True(VendorSoftware.PolitePollMs > VendorSoftware.NormalPollMs);
        Assert.True(VendorSoftware.PoliteAttempts < VendorSoftware.NormalAttempts);
    }

    [Fact]
    public void The_polite_interval_clears_the_vendor_six_second_rhythm()
    {
        // Documented in PROTOCOL.md: the vendor software reads the thermal
        // block about every six seconds. Sitting on top of that is worse than
        // sampling less often.
        Assert.True(VendorSoftware.PolitePollMs > 6000);
    }

    [Fact]
    public void A_requested_interval_is_never_shortened()
    {
        // PollIntervalMs may only slow sampling down. Whatever it decides about
        // the vendor software, asking for 10 seconds must not produce 7.
        Assert.Equal(10000, VendorSoftware.PollIntervalMs(10000));
    }
}

public class PowerOverlayTests
{
    [Fact]
    public void The_four_windows_power_modes_are_all_present_and_distinct()
    {
        var all = PowerOverlays.All;

        Assert.Equal(4, all.Count);
        Assert.Equal(4, all.Select(o => o.Overlay).Distinct().Count());
        Assert.Contains(all, o => o.Overlay == PowerOverlays.MaxPerformance);
        Assert.All(all, o => Assert.False(string.IsNullOrWhiteSpace(o.Name)));
        Assert.All(all, o => Assert.False(string.IsNullOrWhiteSpace(o.Description)));
    }
}

/// <summary>
/// The clean-machine case: a laptop that has been formatted and has never had
/// the vendor software on it. Nothing here may assume a previous installation,
/// and nothing may leave the machine in the state this project exists to fix.
/// </summary>
public class CleanMachineTests
{
    private static OverlayDiagnosis Unguarded => new(
        PowerOverlays.None, MinProcessorStateOverride: null, ProvisionedMinProcessorState: 100);

    private static OverlayDiagnosis Guarded => new(
        PowerOverlays.None,
        MinProcessorStateOverride: PowerOverlayService.SafeMinProcessorState,
        ProvisionedMinProcessorState: 100);

    [Fact]
    public void Performance_is_refused_until_the_guard_exists()
    {
        // The mode activates the overlay that pins the CPU. Offering it before
        // the guard is written would have the application create the fault it
        // was written to repair - which is exactly the state a freshly
        // installed machine is in.
        Assert.False(PowerOverlayService.PerformanceModeIsSafe(Unguarded));
        Assert.True(PowerOverlayService.PerformanceModeIsSafe(Guarded));
    }

    [Fact]
    public void A_provisioned_hundred_is_not_a_guard()
    {
        // The OEM value is what pins the CPU in the first place. Only the
        // override counts.
        Assert.True(Unguarded.GuardMissing);
    }

    [Fact]
    public void A_repair_that_did_half_the_work_reports_both_halves()
    {
        var outcome = new RepairOutcome(["cleared the overlay"], "the rest needs administrator");

        Assert.False(outcome.Empty);
        Assert.Single(outcome.Done);
        Assert.NotNull(outcome.Blocked);
    }

    [Fact]
    public void A_repair_with_nothing_to_do_is_empty()
    {
        Assert.True(new RepairOutcome([], null).Empty);
    }
}
