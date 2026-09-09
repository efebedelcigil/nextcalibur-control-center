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
