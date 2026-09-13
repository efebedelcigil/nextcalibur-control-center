using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// How busy each logical processor is, one core at a time.
///
/// A machine that is idle overall can still have one core pinned at a
/// hundred percent, and that is not idleness - it is something spinning.
/// It is the signature of the faults worth catching: a component stuck in
/// a loop looks exactly like this and nothing else does. The average across
/// all cores hides it completely: one core of sixteen at 100 % reads as
/// 6 % overall, which is a quiet machine.
///
/// The reading is one system call - the same one LibreHardwareMonitor uses
/// for its per-core loads - returning idle, kernel and user ticks for every
/// logical processor. Two calls and the difference between them is the
/// answer; it costs microseconds and touches nothing.
/// </summary>
public sealed class CoreLoad
{
    private long[] _idle = [];
    private long[] _busy = [];

    /// <summary>How many logical processors Windows reports.</summary>
    public int Count { get; private set; }

    /// <summary>
    /// The busy share of each logical processor since the previous call, as
    /// a percentage. Empty on the first call - there is nothing to compare
    /// against yet - and empty when the call fails.
    /// </summary>
    public double[] Read()
    {
        var count = Environment.ProcessorCount;
        var size = Marshal.SizeOf<ProcessorPerformance>();
        var buffer = Marshal.AllocHGlobal(size * count);
        try
        {
            if (NtQuerySystemInformation(SystemProcessorPerformanceInformation, buffer, (uint)(size * count), out var written) != 0)
                return [];

            var returned = (int)(written / size);
            if (returned <= 0) return [];

            var idle = new long[returned];
            var busy = new long[returned];
            for (var i = 0; i < returned; i++)
            {
                var each = Marshal.PtrToStructure<ProcessorPerformance>(buffer + i * size);
                idle[i] = each.IdleTime;
                // Kernel time includes idle time; what is left is real work.
                busy[i] = each.KernelTime + each.UserTime - each.IdleTime;
            }

            var previousIdle = _idle;
            var previousBusy = _busy;
            _idle = idle;
            _busy = busy;
            Count = returned;

            if (previousIdle.Length != returned) return [];

            var loads = new double[returned];
            for (var i = 0; i < returned; i++)
            {
                var idleDelta = idle[i] - previousIdle[i];
                var busyDelta = busy[i] - previousBusy[i];
                var total = idleDelta + busyDelta;
                loads[i] = total > 0 ? Math.Clamp(busyDelta * 100.0 / total, 0, 100) : 0;
            }
            return loads;
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException or OutOfMemoryException)
        {
            return [];
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    /// <summary>Forgets the last reading, so the next one starts a fresh window.</summary>
    public void Reset()
    {
        _idle = [];
        _busy = [];
    }

    /// <summary>
    /// Whether this reading looks like something spinning rather than
    /// something working: at least one core pinned, while the machine as a
    /// whole is quiet. Work that is meant to happen - a build, a game, a
    /// video - lights up several cores, and the average with it.
    /// </summary>
    internal static bool ACoreIsPinned(double[] loads, double pinnedAt, double whileAverageBelow)
    {
        if (loads.Length < 2) return false;

        var pinned = 0;
        var total = 0.0;
        foreach (var load in loads)
        {
            if (load >= pinnedAt) pinned++;
            total += load;
        }

        return pinned >= 1 && total / loads.Length < whileAverageBelow;
    }

    /// <summary>The average load across all logical processors.</summary>
    public static double AverageLoad(double[] loads)
    {
        if (loads.Length == 0) return 0.0;
        var total = 0.0;
        for (var i = 0; i < loads.Length; i++) total += loads[i];
        return total / loads.Length;
    }

    private const int SystemProcessorPerformanceInformation = 8;

    [StructLayout(LayoutKind.Sequential)]
    private struct ProcessorPerformance
    {
        public long IdleTime;
        public long KernelTime;
        public long UserTime;
        public long DpcTime;
        public long InterruptTime;
        public uint InterruptCount;
    }

    [DllImport("ntdll.dll"), DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int NtQuerySystemInformation(int infoClass, IntPtr buffer, uint length, out uint written);
}
