using System.Diagnostics;
using System.Runtime.InteropServices;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Hardware;

/// <summary>What was found, and whether this application did anything about it.</summary>
/// <param name="Id">Short, stable; goes in the log.</param>
/// <param name="What">One sentence for the person, in their language.</param>
/// <param name="Fixed">True when it was put right, false when it is only being reported.</param>
public sealed record FaultFound(string Id, string What, bool Fixed);

/// <summary>Evidence collected across the measurement window for a candidate process.</summary>
public readonly record struct FaultEvidence(
    string ProcessName,
    int Pid,
    bool IsOnAllowList,
    TimeSpan PinnedDuration,
    TimeSpan RequiredWindow,
    bool ThreadSetStable,
    double TopThreadSharePercent,
    ulong IoOperationsDelta,
    uint PageFaultsDelta,
    long WorkingSetDeltaBytes,
    double AverageMachineLoadPercent,
    bool HasVisibleWindow,
    bool IsInCurrentSession,
    bool IsWindowsOwnBinary,
    TimeSpan TimeSinceLastAction,
    int PriorFailures);

/// <summary>The evaluated decision over the seven rules, with individual rule results.</summary>
public readonly record struct FaultEvaluation(
    bool AllowedToAct,
    bool Rule1AllowList,
    bool Rule2Duration,
    bool Rule3Thread,
    bool Rule4Work,
    bool Rule5QuietMachine,
    bool Rule6NoVisibleWindow,
    bool Rule7Locks,
    string LogSummary);

/// <summary>
/// Windows going wrong, watched for and - where it is safe, and only where
/// it is safe - put right.
///
/// A laptop's temperature is the sum of what runs on it, and some of what
/// runs on it is nobody's choice. The case this was written for was found
/// on the owner's machine on 14 September 2026: <c>TextInputHost.exe</c>,
/// the Windows Input Experience, spinning at 100 % of a core since the
/// 11th - 26 hours of one core, in a thin chassis, for nothing at all. The
/// fan was blamed first, as it always is.
///
/// **How it looks for it.** Not by walking every process every few
/// minutes: by reading the per-core loads, which is one system call, and
/// noticing the thing that only a stuck component looks like - one core
/// pinned while the machine as a whole is quiet. On sixteen cores that is
/// invisible in the average (6 %), which is why nobody sees it for days.
/// Only when that shows up does it go looking for who is doing it.
///
/// **What it will do about it, and what it will never do.** This is an
/// application that runs as administrator on somebody else's machine, so
/// the rules are narrow on purpose:
///
/// 1. **It ends nothing that is not on the list below.** The list is
///    specific, named components with a documented habit of getting stuck.
///    Anything else that pins a core is *reported* - named, so the person
///    can decide - and left alone. A browser, a game, a compiler and a
///    virus scanner all pin cores legitimately.
/// 2. **Only components Windows restores by itself.** Nothing is disabled,
///    no service is reconfigured, nothing survives a restart. The single
///    action is ending a process that Windows starts again on demand.
/// 3. **Three locks before ending anything**: it must be on the list, it
///    must be running in this session (never a service, never another
///    account's), and its image must be Windows' own under the system
///    directory - not something that merely shares the name.
/// 4. **Proof over three minutes**, not one sample. Something briefly busy
///    is doing its job.
/// 5. **Once, then not again**: one restart per fault per hour, and after
///    two failures it stops trying and only reports.
/// 6. **It says what it did**, every time, in the log and on screen.
/// 7. **The switch is the person's.** Off, none of this runs at all.
/// </summary>
public static class WindowsFaults
{
    /// <summary>A core at or above this, for a whole window, is not working - it is spinning.</summary>
    public const double PinnedAtPercent = 92;

    /// <summary>...while the machine as a whole is below this. Real work lights up more than one core.</summary>
    public const double QuietMachineBelowPercent = 30;

    /// <summary>A process using at least this share of one core over the window is the one doing it.</summary>
    public const double CulpritAtPercent = 70;

    /// <summary>A single thread must account for at least this share of the process's CPU time.</summary>
    public const double TopThreadSharePercent = 90;

    /// <summary>Maximum I/O operations (read + write + other) allowed across the whole window.</summary>
    public const ulong MaxIoOperations = 100;

    /// <summary>Maximum page faults allowed across the whole window.</summary>
    public const uint MaxPageFaults = 1000;

    /// <summary>Maximum working set movement allowed across the whole window (1 MB).</summary>
    public const long MaxWorkingSetDeltaBytes = 1024 * 1024;

    /// <summary>How long it has to look like that before it is believed (15 minutes unbroken).</summary>
    public static TimeSpan Window { get; set; } = TimeSpan.FromMinutes(15);

    /// <summary>How long before the same fault may be acted on again.</summary>
    public static TimeSpan NotAgainWithin { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// The list. One entry, and the shape is the point: a new fault is a
    /// new line here, with its evidence, not a new heuristic.
    /// </summary>
    public sealed record Watched(string Id, string Process, Func<string> Describe);

    public static readonly List<Watched> Known =
    [
        // TextInputHost.exe: Documented Windows 11 touch keyboard & emoji host fault where a background thread enters an infinite spin loop on 1 core after sleep or session transition.
        new Watched(
            Id: "input-host",
            Process: "TextInputHost",
            Describe: () => Words.Get("S.Core.Fault.InputHost",
                "Windows' input host (TextInputHost) was stuck using a whole processor core. Nextcalibur restarted it; Windows starts it again by itself when the touch keyboard or the emoji panel is needed.")),

        // CrossDeviceService.exe: Documented Phone Link RPC discovery loop bug (Windows 11 22H2/23H2) where background sync spins at 100% of 1 core when phone unlinks.
        new Watched(
            Id: "cross-device",
            Process: "CrossDeviceService",
            Describe: () => Words.Get("S.Core.Fault.CrossDevice",
                "Windows' Phone Link service (CrossDeviceService) was stuck in a background processor loop. Nextcalibur restarted it; Windows starts it again on demand.")),

        // Widgets.exe: Documented Windows 11 Widgets Board bug where background WebView2 renderer enters an unbroken CPU spin loop without user opening the board.
        new Watched(
            Id: "widgets",
            Process: "Widgets",
            Describe: () => Words.Get("S.Core.Fault.Widgets",
                "Windows' Widgets board (Widgets) was stuck in a background processor loop. Nextcalibur restarted it; Windows starts it again when opened.")),
    ];

    /// <summary>
    /// Custom filter to determine if a specific fault ID is enabled by the user's settings.
    /// Default enables all.
    /// </summary>
    public static Func<string, bool> IsFaultEnabled { get; set; } = _ => true;

    private static readonly CoreLoad Cores = new();
    private static readonly Dictionary<int, ProcessMetrics> Before = new();
    private static readonly Dictionary<string, DateTime> ActedAt = new(StringComparer.Ordinal);
    private static readonly Dictionary<string, DateTime> ReportedAt = new(StringComparer.Ordinal);
    private static readonly Dictionary<string, int> Failures = new(StringComparer.Ordinal);
    private static DateTime _pinnedSince = DateTime.MinValue;
    private static DateTime _snapshotAt = DateTime.MinValue;
    private static DateTime _lastSampleAt = DateTime.MinValue;

    /// <summary>Evaluates the seven rules as a pure function over recorded evidence.</summary>
    public static FaultEvaluation Evaluate(FaultEvidence e)
    {
        var r1 = e.IsOnAllowList;
        var r2 = e.PinnedDuration >= e.RequiredWindow;
        var r3 = e.ThreadSetStable && e.TopThreadSharePercent >= TopThreadSharePercent;
        var r4 = e.IoOperationsDelta < MaxIoOperations
                 && e.PageFaultsDelta < MaxPageFaults
                 && Math.Abs(e.WorkingSetDeltaBytes) <= MaxWorkingSetDeltaBytes;
        var r5 = e.AverageMachineLoadPercent < QuietMachineBelowPercent;
        var r6 = !e.HasVisibleWindow;
        var r7 = e.IsInCurrentSession
                 && e.IsWindowsOwnBinary
                 && e.TimeSinceLastAction >= NotAgainWithin
                 && e.PriorFailures < 2;

        var allowed = r1 && r2 && r3 && r4 && r5 && r6 && r7;

        var summary = $"Decision for {e.ProcessName} (pid {e.Pid}): " +
            $"R1(list)={r1}; " +
            $"R2(15m unbroken)={r2} ({e.PinnedDuration.TotalMinutes:N1}m/{e.RequiredWindow.TotalMinutes:N0}m); " +
            $"R3(thread>={TopThreadSharePercent:N0}%)={r3} ({e.TopThreadSharePercent:N1}%, stable={e.ThreadSetStable}); " +
            $"R4(idle: IO<{MaxIoOperations}, flt<{MaxPageFaults}, ws<={MaxWorkingSetDeltaBytes / 1024}kB)={r4} (io={e.IoOperationsDelta}, flt={e.PageFaultsDelta}, wsDelta={e.WorkingSetDeltaBytes / 1024:N0}kB); " +
            $"R5(quiet<{QuietMachineBelowPercent:N0}%)={r5} ({e.AverageMachineLoadPercent:N1}%); " +
            $"R6(no visible win)={r6}; " +
            $"R7(locks: session={e.IsInCurrentSession}, winOwn={e.IsWindowsOwnBinary}, coolDown={e.TimeSinceLastAction.TotalMinutes:N0}m/{(int)NotAgainWithin.TotalMinutes}m, fail={e.PriorFailures}/2)={r7} " +
            $"-> {(allowed ? "ACT" : "REPORT_ONLY")}";

        return new FaultEvaluation(allowed, r1, r2, r3, r4, r5, r6, r7, summary);
    }

    /// <summary>Calculates whether thread IDs remained stable and the share of the top thread.</summary>
    public static (bool Stable, double TopSharePercent) CalculateThreadShare(
        IReadOnlyDictionary<int, TimeSpan> baseThreads,
        IReadOnlyDictionary<int, TimeSpan> currentThreads,
        TimeSpan totalProcessCpuDelta)
    {
        if (baseThreads.Count == 0 || currentThreads.Count == 0) return (false, 0.0);
        if (baseThreads.Count != currentThreads.Count) return (false, 0.0);

        foreach (var id in baseThreads.Keys)
        {
            if (!currentThreads.ContainsKey(id)) return (false, 0.0);
        }

        if (totalProcessCpuDelta <= TimeSpan.Zero) return (false, 0.0);

        var maxThreadDeltaMs = 0.0;
        foreach (var (id, baseTime) in baseThreads)
        {
            var curTime = currentThreads[id];
            var delta = (curTime - baseTime).TotalMilliseconds;
            // Thread CPU time went backwards -> Windows recycled the thread ID for a new thread.
            if (delta < 0) return (false, 0.0);
            // Single thread accrued more CPU time than the entire process did -> inconsistent timing.
            if (delta > totalProcessCpuDelta.TotalMilliseconds * 1.05) return (false, 0.0);
            if (delta > maxThreadDeltaMs) maxThreadDeltaMs = delta;
        }

        var share = (maxThreadDeltaMs / totalProcessCpuDelta.TotalMilliseconds) * 100.0;
        return (true, Math.Clamp(share, 0.0, 100.0));
    }

    /// <summary>
    /// Looks once; call it every few minutes. Cheap unless something is
    /// actually wrong: one system call, and only then a walk of the process
    /// list. Returns what was found, which is almost always nothing.
    /// </summary>
    public static IReadOnlyList<FaultFound> Check(bool mayAct)
    {
        var loads = Cores.Read();
        if (loads.Length == 0) return [];

        var now = DateTime.UtcNow;

        // If more than 2 minutes have elapsed since the last check, the machine was asleep or suspended.
        // Sleep must never quietly count toward the unbroken pinned duration.
        if (_lastSampleAt != DateTime.MinValue && (now - _lastSampleAt) > TimeSpan.FromMinutes(2))
        {
            _pinnedSince = DateTime.MinValue;
            Before.Clear();
        }
        _lastSampleAt = now;

        if (!CoreLoad.ACoreIsPinned(loads, PinnedAtPercent, QuietMachineBelowPercent))
        {
            _pinnedSince = DateTime.MinValue;
            Before.Clear();
            return [];
        }

        var avgLoad = CoreLoad.AverageLoad(loads);

        if (_pinnedSince == DateTime.MinValue)
        {
            _pinnedSince = now;
            Snapshot(now);
            return [];
        }

        var elapsed = now - _snapshotAt;
        var pinnedDuration = now - _pinnedSince;
        if (pinnedDuration < Window || elapsed <= TimeSpan.Zero) return [];

        var culprit = Busiest(elapsed);
        if (culprit is not { } who)
        {
            _pinnedSince = now;
            Snapshot(now);
            return [];
        }

        _pinnedSince = now; // reset the window for next check
        var currentMetrics = ProcessMetrics.Capture(who.Pid);
        Before.TryGetValue(who.Pid, out var baseMetrics);
        Snapshot(now);

        var watched = Known.Find(w => string.Equals(w.Process, who.Name, StringComparison.OrdinalIgnoreCase));
        if (watched is not null && !IsFaultEnabled(watched.Id))
        {
            return [];
        }
        var onList = watched is not null;

        var (threadStable, topShare) = (false, 0.0);
        ulong ioDelta = ulong.MaxValue;
        uint faultDelta = uint.MaxValue;
        long wsDelta = long.MaxValue;

        if (baseMetrics is not null && currentMetrics is not null)
        {
            // Verify it is the exact same process instance across the window
            var sameInstance = (baseMetrics.StartTimeUtc == DateTime.MinValue || baseMetrics.StartTimeUtc == currentMetrics.StartTimeUtc)
                && currentMetrics.TotalProcessorTime >= baseMetrics.TotalProcessorTime;

            if (sameInstance)
            {
                var cpuDelta = currentMetrics.TotalProcessorTime - baseMetrics.TotalProcessorTime;
                (threadStable, topShare) = CalculateThreadShare(baseMetrics.Threads, currentMetrics.Threads, cpuDelta);
                ioDelta = currentMetrics.TotalIoOperations >= baseMetrics.TotalIoOperations
                    ? currentMetrics.TotalIoOperations - baseMetrics.TotalIoOperations
                    : ulong.MaxValue;
                faultDelta = currentMetrics.PageFaultCount >= baseMetrics.PageFaultCount
                    ? currentMetrics.PageFaultCount - baseMetrics.PageFaultCount
                    : uint.MaxValue;
                wsDelta = Math.Abs(currentMetrics.WorkingSet64 - baseMetrics.WorkingSet64);
            }
        }

        var hasWindow = HasVisibleWindow(who.Pid);
        if (string.Equals(who.Name, "CrossDeviceService", StringComparison.OrdinalIgnoreCase))
        {
            // Phone Link: CrossDeviceService has no window, so rule 6 cannot protect it.
            // Stand down if PhoneExperienceHost.exe is running at all (service is in active use).
            hasWindow = hasWindow || PhoneExperienceHostIsRunning();
        }
        var inSession = currentMetrics is not null && currentMetrics.SessionId == CurrentSessionId();
        var winOwn = currentMetrics is not null && IsWindowsOwn(who.Pid);
        var actedAt = watched is not null && ActedAt.TryGetValue(watched.Id, out var lastActed) ? lastActed : DateTime.MinValue;
        var timeSinceLast = actedAt == DateTime.MinValue ? TimeSpan.FromDays(365) : (now - actedAt);
        var failCount = watched is not null && Failures.TryGetValue(watched.Id, out var f) ? f : 0;

        var evidence = new FaultEvidence(
            ProcessName: who.Name,
            Pid: who.Pid,
            IsOnAllowList: onList,
            PinnedDuration: pinnedDuration,
            RequiredWindow: Window,
            ThreadSetStable: threadStable,
            TopThreadSharePercent: topShare,
            IoOperationsDelta: ioDelta,
            PageFaultsDelta: faultDelta,
            WorkingSetDeltaBytes: wsDelta,
            AverageMachineLoadPercent: avgLoad,
            HasVisibleWindow: hasWindow,
            IsInCurrentSession: inSession,
            IsWindowsOwnBinary: winOwn,
            TimeSinceLastAction: timeSinceLast,
            PriorFailures: failCount);

        var eval = Evaluate(evidence);
        Log.Info("windows", eval.LogSummary);

        if (!onList)
        {
            var id = "busy-" + who.Name.ToLowerInvariant();
            if (ReportedAt.TryGetValue(id, out var last) && (now - last) < NotAgainWithin)
                return [];

            ReportedAt[id] = now;
            return [new FaultFound(id,
                Words.Get("S.Core.Fault.Unknown",
                    "{0} has been using a whole processor core for several minutes while nothing else is busy. Nextcalibur has not touched it - it may be doing its job.",
                    who.Name),
                Fixed: false)];
        }

        if (!eval.AllowedToAct || !mayAct)
        {
            if (ReportedAt.TryGetValue(watched!.Id, out var last) && (now - last) < NotAgainWithin)
                return [];

            ReportedAt[watched.Id] = now;
            return [new FaultFound(watched.Id, watched.Describe(), Fixed: false)];
        }

        if (!End(who.Pid, watched!.Process, who.StartTimeUtc))
        {
            Failures[watched.Id] = failCount + 1;
            if (ReportedAt.TryGetValue(watched.Id, out var last) && (now - last) < NotAgainWithin)
                return [];

            ReportedAt[watched.Id] = now;
            return [new FaultFound(watched.Id, watched.Describe(), Fixed: false)];
        }

        ActedAt[watched.Id] = now;
        ReportedAt[watched.Id] = now;
        Failures.Remove(watched.Id);
        Log.Info("windows", $"Ended {watched.Process} (pid {who.Pid}); Windows restarts it on demand");
        return [new FaultFound(watched.Id, watched.Describe(), Fixed: true)];
    }

    /// <summary>Forgets everything; a change of mind in the settings starts the window again.</summary>
    public static void Forget()
    {
        Cores.Reset();
        Before.Clear();
        ActedAt.Clear();
        ReportedAt.Clear();
        Failures.Clear();
        _pinnedSince = DateTime.MinValue;
        _snapshotAt = DateTime.MinValue;
        _lastSampleAt = DateTime.MinValue;
    }

    private static void Snapshot(DateTime at)
    {
        Before.Clear();
        foreach (var process in Process.GetProcesses())
        {
            try
            {
                if (ProcessMetrics.Capture(process) is { } metrics)
                    Before[process.Id] = metrics;
            }
            finally { process.Dispose(); }
        }
        _snapshotAt = at;
    }

    /// <summary>The process that used the most processor since the snapshot, if any used enough to be the one.</summary>
    private static (int Pid, string Name, double Share, DateTime StartTimeUtc)? Busiest(TimeSpan elapsed)
    {
        (int Pid, string Name, double Share, DateTime StartTimeUtc)? worst = null;
        foreach (var process in Process.GetProcesses())
        {
            try
            {
                if (!Before.TryGetValue(process.Id, out var was)) continue;
                // If the process restarted under the same PID, CPU time goes backwards
                if (process.TotalProcessorTime < was.TotalProcessorTime) continue;

                var share = (process.TotalProcessorTime - was.TotalProcessorTime).TotalMilliseconds / elapsed.TotalMilliseconds * 100;
                if (share > (worst?.Share ?? CulpritAtPercent))
                {
                    DateTime start = DateTime.MinValue;
                    try { start = process.StartTime.ToUniversalTime(); } catch { }
                    worst = (process.Id, process.ProcessName, share, start);
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception or NotSupportedException) { }
            finally { process.Dispose(); }
        }
        return worst;
    }

    /// <summary>
    /// Ends it, with the locks that are not the list: it has to be in
    /// this session - a service or another account's process is not ours to
    /// touch - and its image has to be Windows' own, so that something
    /// which merely borrowed the name is left alone.
    /// Re-reads process identity and verifies start time immediately before killing to prevent PID reuse races.
    /// </summary>
    private static bool End(int pid, string expectedName, DateTime expectedStartTimeUtc)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            if (expectedStartTimeUtc != DateTime.MinValue)
            {
                try
                {
                    if (Math.Abs((process.StartTime.ToUniversalTime() - expectedStartTimeUtc).TotalSeconds) > 1.0)
                    {
                        Log.Warn("windows", $"Refusing to end pid {pid}: start time mismatch (expected {expectedStartTimeUtc:O}, got {process.StartTime.ToUniversalTime():O})");
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            if (!string.Equals(process.ProcessName, expectedName, StringComparison.OrdinalIgnoreCase)) return false;
            if (process.SessionId != CurrentSessionId()) return false;
            if (!IsWindowsOwn(process)) return false;

            process.Kill();
            return process.WaitForExit(5000);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or System.ComponentModel.Win32Exception or NotSupportedException)
        {
            Log.Warn("windows", $"Could not end pid {pid}: {ex.Message}");
            return false;
        }
    }

    private static int CurrentSessionId()
    {
        using var self = Process.GetCurrentProcess();
        return self.SessionId;
    }

    /// <summary>Whether the image lives under the Windows directory - Windows' own, not something wearing its name.</summary>
    internal static bool IsWindowsOwn(Process process)
    {
        try
        {
            var path = process.MainModule?.FileName;
            if (path is null) return false;
            var windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows).TrimEnd('\\') + "\\";
            return path.StartsWith(windows, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception or NotSupportedException)
        {
            return false;
        }
    }

    internal static bool IsWindowsOwn(int pid)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
            return IsWindowsOwn(process);
        }
        catch (Exception)
        {
            return false;
        }
    }

    internal static bool HasVisibleWindow(int pid)
    {
        var found = false;
        try
        {
            EnumWindows((hWnd, _) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    GetWindowThreadProcessId(hWnd, out var windowPid);
                    if (windowPid == (uint)pid)
                    {
                        found = true;
                        return false;
                    }
                }
                return true;
            }, IntPtr.Zero);
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return false;
        }
        return found;
    }

    internal static bool PhoneExperienceHostIsRunning()
    {
        try
        {
            return Process.GetProcessesByName("PhoneExperienceHost").Length > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct IO_COUNTERS
    {
        public ulong ReadOperationCount;
        public ulong WriteOperationCount;
        public ulong OtherOperationCount;
        public ulong ReadTransferCount;
        public ulong WriteTransferCount;
        public ulong OtherTransferCount;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetProcessIoCounters(IntPtr hProcess, out IO_COUNTERS lpIoCounters);

    [StructLayout(LayoutKind.Sequential)]
    internal struct PROCESS_MEMORY_COUNTERS
    {
        public uint cb;
        public uint PageFaultCount;
        public UIntPtr PeakWorkingSetSize;
        public UIntPtr WorkingSetSize;
        public UIntPtr QuotaPeakPagedPoolUsage;
        public UIntPtr QuotaPagedPoolUsage;
        public UIntPtr QuotaPeakNonPagedPoolUsage;
        public UIntPtr QuotaNonPagedPoolUsage;
        public UIntPtr PagefileUsage;
        public UIntPtr PeakPagefileUsage;
    }

    [DllImport("psapi.dll", SetLastError = true)]
    private static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS counters, uint cb);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    internal sealed class ProcessMetrics
    {
        public int Pid { get; init; }
        public string Name { get; init; } = "";
        public int SessionId { get; init; }
        public DateTime StartTimeUtc { get; init; }
        public TimeSpan TotalProcessorTime { get; init; }
        public long WorkingSet64 { get; init; }
        public ulong TotalIoOperations { get; init; }
        public uint PageFaultCount { get; init; }
        public Dictionary<int, TimeSpan> Threads { get; init; } = new();

        public static ProcessMetrics? Capture(Process process)
        {
            try
            {
                var pid = process.Id;
                var name = process.ProcessName;
                var session = process.SessionId;
                var cpu = process.TotalProcessorTime;
                var ws = process.WorkingSet64;
                DateTime start = DateTime.MinValue;
                try { start = process.StartTime.ToUniversalTime(); } catch { }

                ulong ioOps = 0;
                try
                {
                    if (GetProcessIoCounters(process.Handle, out var io))
                        ioOps = io.ReadOperationCount + io.WriteOperationCount + io.OtherOperationCount;
                }
                catch (Exception) { }

                uint pageFaults = 0;
                try
                {
                    var memSize = (uint)Marshal.SizeOf<PROCESS_MEMORY_COUNTERS>();
                    if (GetProcessMemoryInfo(process.Handle, out var mem, memSize))
                        pageFaults = mem.PageFaultCount;
                }
                catch (Exception) { }

                var threads = new Dictionary<int, TimeSpan>();
                try
                {
                    foreach (ProcessThread t in process.Threads)
                    {
                        try { threads[t.Id] = t.TotalProcessorTime; } catch { }
                    }
                }
                catch (Exception) { }

                return new ProcessMetrics
                {
                    Pid = pid,
                    Name = name,
                    SessionId = session,
                    StartTimeUtc = start,
                    TotalProcessorTime = cpu,
                    WorkingSet64 = ws,
                    TotalIoOperations = ioOps,
                    PageFaultCount = pageFaults,
                    Threads = threads,
                };
            }
            catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception or NotSupportedException)
            {
                return null;
            }
        }

        public static ProcessMetrics? Capture(int pid)
        {
            try
            {
                using var p = Process.GetProcessById(pid);
                return Capture(p);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
