using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class WindowsFaultsTests
{
    private static FaultEvidence ValidEvidence() => new(
        ProcessName: "TextInputHost",
        Pid: 1234,
        IsOnAllowList: true,
        PinnedDuration: TimeSpan.FromMinutes(16),
        RequiredWindow: TimeSpan.FromMinutes(15),
        ThreadSetStable: true,
        TopThreadSharePercent: 98.5,
        IoOperationsDelta: 5,
        PageFaultsDelta: 50,
        WorkingSetDeltaBytes: 1024 * 200, // 200 kB
        AverageMachineLoadPercent: 12.0,
        HasVisibleWindow: false,
        IsInCurrentSession: true,
        IsWindowsOwnBinary: true,
        TimeSinceLastAction: TimeSpan.FromHours(2),
        PriorFailures: 0);

    [Fact]
    public void All_seven_rules_pass_allows_acting()
    {
        var ev = ValidEvidence();
        var result = WindowsFaults.Evaluate(ev);

        Assert.True(result.AllowedToAct);
        Assert.True(result.Rule1AllowList);
        Assert.True(result.Rule2Duration);
        Assert.True(result.Rule3Thread);
        Assert.True(result.Rule4Work);
        Assert.True(result.Rule5QuietMachine);
        Assert.True(result.Rule6NoVisibleWindow);
        Assert.True(result.Rule7Locks);
        Assert.Contains("-> ACT", result.LogSummary);
    }

    [Fact]
    public void Rule1_fails_when_process_is_not_on_allow_list()
    {
        var ev = ValidEvidence() with { IsOnAllowList = false, ProcessName = "UnknownProcess" };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule1AllowList);
        Assert.False(result.AllowedToAct);
        Assert.Contains("-> REPORT_ONLY", result.LogSummary);
    }

    [Fact]
    public void Rule2_fails_when_pinned_duration_under_required_window()
    {
        var ev = ValidEvidence() with { PinnedDuration = TimeSpan.FromMinutes(14.9) };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule2Duration);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule2_passes_when_pinned_duration_meets_required_window()
    {
        var ev = ValidEvidence() with { PinnedDuration = TimeSpan.FromMinutes(15) };
        var result = WindowsFaults.Evaluate(ev);

        Assert.True(result.Rule2Duration);
        Assert.True(result.AllowedToAct);
    }

    [Fact]
    public void Rule3_fails_when_thread_set_is_not_stable()
    {
        var ev = ValidEvidence() with { ThreadSetStable = false };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule3Thread);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule3_fails_when_top_thread_share_is_under_90_percent()
    {
        var ev = ValidEvidence() with { TopThreadSharePercent = 89.9 };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule3Thread);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule3_thread_share_calculation_identifies_pinning_thread()
    {
        var baseThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(100),
            [20] = TimeSpan.FromSeconds(50),
        };
        var curThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(195), // +95 s
            [20] = TimeSpan.FromSeconds(55),  // +5 s
        };
        var totalCpuDelta = TimeSpan.FromSeconds(100);

        var (stable, share) = WindowsFaults.CalculateThreadShare(baseThreads, curThreads, totalCpuDelta);

        Assert.True(stable);
        Assert.Equal(95.0, share, precision: 1);
    }

    [Fact]
    public void Rule3_thread_share_calculation_drops_sample_if_thread_set_changed()
    {
        var baseThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(100),
        };
        var curThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(195),
            [20] = TimeSpan.FromSeconds(5), // new thread spawned
        };
        var totalCpuDelta = TimeSpan.FromSeconds(100);

        var (stable, share) = WindowsFaults.CalculateThreadShare(baseThreads, curThreads, totalCpuDelta);

        Assert.False(stable);
        Assert.Equal(0.0, share);
    }

    [Fact]
    public void Rule4_fails_when_io_operations_exceed_limit()
    {
        var ev = ValidEvidence() with { IoOperationsDelta = 100 };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule4Work);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule4_fails_when_page_faults_exceed_limit()
    {
        var ev = ValidEvidence() with { PageFaultsDelta = 1000 };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule4Work);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule4_fails_when_working_set_delta_exceeds_1mb()
    {
        var ev = ValidEvidence() with { WorkingSetDeltaBytes = 1024 * 1024 + 1 };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule4Work);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule4_passes_with_negative_working_set_delta_within_1mb()
    {
        var ev = ValidEvidence() with { WorkingSetDeltaBytes = -500 * 1024 };
        var result = WindowsFaults.Evaluate(ev);

        Assert.True(result.Rule4Work);
        Assert.True(result.AllowedToAct);
    }

    [Fact]
    public void Rule5_fails_when_machine_average_load_exceeds_30_percent()
    {
        var ev = ValidEvidence() with { AverageMachineLoadPercent = 30.0 };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule5QuietMachine);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule6_fails_when_process_has_visible_window()
    {
        var ev = ValidEvidence() with { HasVisibleWindow = true };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule6NoVisibleWindow);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule7_fails_when_process_is_not_in_current_session()
    {
        var ev = ValidEvidence() with { IsInCurrentSession = false };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule7Locks);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule7_fails_when_process_is_not_windows_own_binary()
    {
        var ev = ValidEvidence() with { IsWindowsOwnBinary = false };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule7Locks);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule7_fails_when_acted_within_last_hour()
    {
        var ev = ValidEvidence() with { TimeSinceLastAction = TimeSpan.FromMinutes(59) };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule7Locks);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void Rule7_fails_when_prior_failures_equal_or_exceed_two()
    {
        var ev = ValidEvidence() with { PriorFailures = 2 };
        var result = WindowsFaults.Evaluate(ev);

        Assert.False(result.Rule7Locks);
        Assert.False(result.AllowedToAct);
    }

    [Fact]
    public void CoreLoad_ACoreIsPinned_identifies_single_pinned_core_on_quiet_machine()
    {
        double[] loads = [95.0, 5.0, 4.0, 6.0, 5.0, 3.0, 4.0, 5.0];
        Assert.True(CoreLoad.ACoreIsPinned(loads, 92, 30));
    }

    [Fact]
    public void CoreLoad_ACoreIsPinned_rejects_heavy_multicore_work()
    {
        double[] loads = [95.0, 92.0, 85.0, 70.0, 60.0, 50.0, 40.0, 30.0];
        // Average is 65.25%, which is > 30%
        Assert.False(CoreLoad.ACoreIsPinned(loads, 92, 30));
    }

    [Fact]
    public void CoreLoad_AverageLoad_calculates_arithmetic_mean()
    {
        double[] loads = [10.0, 20.0, 30.0, 40.0];
        Assert.Equal(25.0, CoreLoad.AverageLoad(loads));
    }
}
