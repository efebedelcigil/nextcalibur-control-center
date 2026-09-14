using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// In the "log" collection with <c>LogInjectionTests</c>, and not because
/// it tests logging: the log's folder is a static, and those tests point
/// it at a temporary one while they count the lines they wrote. Anything
/// logging from another class at that moment lands in their file and is
/// counted as theirs. The retirement pass logs.
/// </summary>
[Collection("log")]
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
    public void Rule3_thread_share_calculation_drops_sample_if_thread_cpu_time_regresses()
    {
        var baseThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(100),
        };
        var curThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(50), // Thread CPU went backwards (ID recycled)
        };
        var totalCpuDelta = TimeSpan.FromSeconds(100);

        var (stable, share) = WindowsFaults.CalculateThreadShare(baseThreads, curThreads, totalCpuDelta);

        Assert.False(stable);
        Assert.Equal(0.0, share);
    }

    [Fact]
    public void Rule3_thread_share_calculation_drops_sample_if_thread_delta_exceeds_process_delta()
    {
        var baseThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(100),
        };
        var curThreads = new Dictionary<int, TimeSpan>
        {
            [10] = TimeSpan.FromSeconds(220), // +120s on thread vs +100s on process
        };
        var totalCpuDelta = TimeSpan.FromSeconds(100);

        var (stable, share) = WindowsFaults.CalculateThreadShare(baseThreads, curThreads, totalCpuDelta);

        Assert.False(stable);
        Assert.Equal(0.0, share);
    }

    [Fact]
    public void Rule4_fails_when_working_set_shrinks_by_more_than_1mb()
    {
        var ev = ValidEvidence() with { WorkingSetDeltaBytes = -(1024 * 1024 + 1) };
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

    [Theory]
    [InlineData(0, 0, 18.7, false, true)]
    [InlineData(1, 4, 10.1, false, true)]
    [InlineData(2, 0, 18.7, false, false)]
    [InlineData(8, 0, 5.0, false, false)]
    [InlineData(0, 5, 18.7, false, false)]
    [InlineData(0, 0, 10.0, false, false)]
    [InlineData(0, 0, 9.5, false, false)]
    [InlineData(0, 0, 18.7, true, false)]
    public void Gpu_awake_fault_condition_evaluates_correctly(
        int pState, int util, double watts, bool displayActive, bool expected)
    {
        var actual = GpuClockReader.IsGpuAwakeFaultCondition(pState, util, watts, displayActive);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Gpu_awake_fault_requires_ten_unbroken_minutes()
    {
        using var reader = new GpuClockReader();
        var start = new DateTime(2026, 9, 14, 12, 0, 0, DateTimeKind.Utc);
        string[] procs = ["Brave"];

        // First sample at t = 0
        var f0 = reader.EvaluateAwakeFault(true, start, () => procs, 18.7, 0, 0);
        Assert.Null(f0);

        // Sample at t = 5 min
        var f5 = reader.EvaluateAwakeFault(true, start.AddMinutes(5), () => procs, 18.7, 0, 0);
        Assert.Null(f5);

        // Sample at t = 9.9 min
        var f9 = reader.EvaluateAwakeFault(true, start.AddMinutes(9.9), () => procs, 18.7, 0, 0);
        Assert.Null(f9);

        // Sample at t = 10 min
        var f10 = reader.EvaluateAwakeFault(true, start.AddMinutes(10), () => procs, 18.7, 0, 0);
        Assert.NotNull(f10);
        Assert.Equal(0, f10.PerformanceState);
        Assert.Equal(18.7, f10.Watts);
        Assert.Single(f10.HoldingProcesses);
        Assert.Equal("Brave", f10.HoldingProcesses[0]);
        Assert.Equal(TimeSpan.FromMinutes(10), f10.Duration);
    }

    [Fact]
    public void Gpu_awake_fault_resets_clock_if_condition_breaks()
    {
        using var reader = new GpuClockReader();
        var start = new DateTime(2026, 9, 14, 12, 0, 0, DateTimeKind.Utc);
        string[] procs = ["Brave"];

        // Starts at t = 0
        reader.EvaluateAwakeFault(true, start, () => procs, 18.7, 0, 0);
        // At t = 5 min, still true
        reader.EvaluateAwakeFault(true, start.AddMinutes(5), () => procs, 18.7, 0, 0);

        // At t = 6 min, condition breaks (e.g. card dropped to P8 or power dropped)
        var fBroken = reader.EvaluateAwakeFault(false, start.AddMinutes(6), () => procs, 3.5, 8, 0);
        Assert.Null(fBroken);

        // At t = 7 min, condition becomes true again
        var fNewStart = reader.EvaluateAwakeFault(true, start.AddMinutes(7), () => procs, 18.7, 0, 0);
        Assert.Null(fNewStart);

        // At t = 15 min (8 minutes since restart at t = 7)
        var f8Min = reader.EvaluateAwakeFault(true, start.AddMinutes(15), () => procs, 18.7, 0, 0);
        Assert.Null(f8Min);

        // At t = 17 min (10 minutes since restart at t = 7)
        var f10Min = reader.EvaluateAwakeFault(true, start.AddMinutes(17), () => procs, 18.7, 0, 0);
        Assert.NotNull(f10Min);
    }

    [Fact]
    public void Gpu_awake_fault_describe_formats_holding_processes()
    {
        var faultWithProcs = new GpuAwakeFault(0, 18.7, 0, ["Brave", "Code"], TimeSpan.FromMinutes(10));
        var descWithProcs = faultWithProcs.Describe();
        Assert.Contains("19 W", descWithProcs);
        Assert.Contains("Brave, Code", descWithProcs);

        var faultWithoutProcs = new GpuAwakeFault(0, 15.2, 0, [], TimeSpan.FromMinutes(10));
        var descWithoutProcs = faultWithoutProcs.Describe();
        Assert.Contains("15 W", descWithoutProcs);
        Assert.DoesNotContain("Holding processes", descWithoutProcs);
    }

    [Fact]
    public void Known_faults_contains_required_windows_components()
    {
        var ids = WindowsFaults.Known.Select(k => k.Id).ToList();
        Assert.Contains("input-host", ids);
        Assert.Contains("cross-device", ids);
        Assert.Contains("widgets", ids);
    }

    [Fact]
    public void IsFaultEnabled_filters_faults_properly()
    {
        try
        {
            WindowsFaults.IsFaultEnabled = id => id != "cross-device";
            Assert.True(WindowsFaults.IsFaultEnabled("input-host"));
            Assert.False(WindowsFaults.IsFaultEnabled("cross-device"));
            Assert.True(WindowsFaults.IsFaultEnabled("widgets"));
        }
        finally
        {
            WindowsFaults.IsFaultEnabled = _ => true;
        }
    }

    [Fact]
    public void MemoryTrimmer_handles_nonexistent_process_without_throwing()
    {
        var reclaimed = MemoryTrimmer.TrimIfExceeds("NonExistentProcess_12345", 1024 * 1024);
        Assert.Equal(0, reclaimed);
    }

    [Fact]
    public void MemoryTrimmer_on_current_process_runs_safely()
    {
        var current = System.Diagnostics.Process.GetCurrentProcess();
        // Set threshold higher than current working set to verify threshold check
        var reclaimed = MemoryTrimmer.TrimIfExceeds(current.ProcessName, current.WorkingSet64 + 1024 * 1024 * 100);
        Assert.Equal(0, reclaimed);
    }

    [Fact]
    public void NduFix_is_safe_to_query()
    {
        // Reading registry state should never throw an unhandled exception
        var disabled = NduFix.IsNduDisabled();
        Assert.True(disabled || !disabled);
    }

    [Fact]
    public void MemoryTrimmer_reads_free_physical_memory_percent_safely()
    {
        var freePct = MemoryTrimmer.GetFreePhysicalMemoryPercent();
        if (freePct.HasValue)
        {
            Assert.InRange(freePct.Value, 0.0, 100.0);
        }
    }

    [Fact]
    public void UserPresence_session_unlock_triggers_standdown()
    {
        UserPresence.RecordSessionLock(true);
        Assert.True(UserPresence.NobodyIsWatching());

        UserPresence.RecordSessionLock(false);
        Assert.True(UserPresence.WasRecentlyUnlocked(TimeSpan.FromMinutes(1)));
        Assert.True(UserPresence.WouldRatherNotBeDisturbed());
    }

    [Fact]
    public void AppSettings_kill_list_and_ndu_defaults_are_safe()
    {
        var settings = new AppSettings();
        Assert.True(settings.FixTextInputHost);
        Assert.False(settings.FixCrossDeviceService);
        Assert.False(settings.FixWidgets);
        Assert.False(settings.DisableNdu);
        Assert.Null(settings.OriginalNduStart);
    }

    [Fact]
    public void Footprint_survey_contains_ndu_and_ndu_marker_traces()
    {
        var traces = Footprint.Survey();
        Assert.Contains(traces, t => t.KeepingIsReasonable && t.NeedsElevation && t.Name == Words.Get("S.Core.Trace.Ndu", "The NDU network driver fix"));
        Assert.Contains(traces, t => !t.KeepingIsReasonable && t.NeedsElevation && t.Name == Words.Get("S.Core.Trace.NduMarker", "The NDU backup registry value"));
    }
}
