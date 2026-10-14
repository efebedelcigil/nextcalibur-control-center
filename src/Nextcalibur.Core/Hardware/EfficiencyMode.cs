using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Windows' "Efficiency mode" for a process - the one Task Manager offers:
/// EcoQoS (the processor runs it at its most efficient clocks), its timer
/// requests ignored, and the lowest priority. Nothing is ended and nothing
/// lasts: a process that restarts starts without it (Nextcalibur sets it
/// again for the programs on the person's list). For the programs that keep the
/// machine busy while nobody uses it (the owner's ask of 14 October 2026:
/// LG's OnScreen Control was at 2% of a core all day), and only for the ones
/// the person picks.
/// </summary>
public static class EfficiencyMode
{
    /// <summary>Puts a process into Efficiency mode, or takes it out. False when Windows refuses.</summary>
    public static bool Set(int pid, bool on)
    {
        var handle = OpenProcess(ProcessSetInformation | ProcessQueryLimitedInformation, false, pid);
        if (handle == IntPtr.Zero) return false;
        try
        {
            var state = new PowerThrottlingState
            {
                Version = 1,
                ControlMask = ExecutionSpeed | IgnoreTimerResolution,
                StateMask = on ? ExecutionSpeed | IgnoreTimerResolution : 0,
            };
            var ok = SetProcessInformation(handle, ProcessPowerThrottling, ref state, Marshal.SizeOf<PowerThrottlingState>());
            ok &= SetPriorityClass(handle, on ? IdlePriorityClass : NormalPriorityClass);
            return ok;
        }
        finally
        {
            CloseHandle(handle);
        }
    }

    /// <summary>Every running process by this name, into Efficiency mode or out; how many were changed.</summary>
    public static int SetByName(string processName, bool on)
    {
        var changed = 0;
        foreach (var p in Process.GetProcessesByName(processName))
        {
            using (p)
            {
                if (Set(p.Id, on)) changed++;
            }
        }
        return changed;
    }

    private const int ProcessPowerThrottling = 4;
    private const uint ExecutionSpeed = 0x1, IgnoreTimerResolution = 0x4;
    private const uint ProcessSetInformation = 0x0200, ProcessQueryLimitedInformation = 0x1000;
    private const uint IdlePriorityClass = 0x40, NormalPriorityClass = 0x20;

    [StructLayout(LayoutKind.Sequential)]
    private struct PowerThrottlingState
    {
        public uint Version;
        public uint ControlMask;
        public uint StateMask;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint access, bool inherit, int pid);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetProcessInformation(IntPtr process, int infoClass, ref PowerThrottlingState info, int size);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetPriorityClass(IntPtr process, uint priorityClass);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);
}

/// <summary>
/// The automatic side of Efficiency mode: which programs a scan while nobody
/// uses the machine may put into it by itself. Careful by design - a program
/// is picked only when it is a background one (no window of its own: never
/// what the person is using), busy on two scans in a row, never a program the
/// person took out, and at most a few at a time. Windows itself, drivers,
/// security software and input helpers never reach the scan's list at all
/// (see <see cref="TestPreflight"/>).
/// </summary>
public static class IdleLoadPicker
{
    /// <summary>Share of the whole processor (all threads) a program must hold on both scans.</summary>
    public const double AutoCpuPercent = 1.0;

    /// <summary>At most this many programs are added by one scan.</summary>
    public const int MostPerScan = 3;

    /// <summary>The names to put into Efficiency mode now, given this scan and the one before it.</summary>
    public static IReadOnlyList<string> Choose(
        IEnumerable<RunningItem> previous, IEnumerable<RunningItem> now,
        IEnumerable<string> alreadyOn, IEnumerable<string> excluded)
    {
        static IEnumerable<(string Name, double Cpu)> Busy(IEnumerable<RunningItem> items) =>
            items.Where(i => i.Kind == RunningKind.Background && i.CpuPercent >= AutoCpuPercent)
                 .Select(i => (Name: Power.ProgramRules.Normalise(i.Name), Cpu: i.CpuPercent))
                 .Where(i => i.Name is not null)
                 .Select(i => (i.Name!, i.Cpu));

        var skip = new HashSet<string>(alreadyOn.Concat(excluded), StringComparer.OrdinalIgnoreCase);
        var before = new HashSet<string>(Busy(previous).Select(b => b.Name), StringComparer.OrdinalIgnoreCase);
        return Busy(now)
            .Where(b => before.Contains(b.Name) && !skip.Contains(b.Name)
                        && !b.Name.Contains("Nextcalibur", StringComparison.OrdinalIgnoreCase))
            .GroupBy(b => b.Name, StringComparer.OrdinalIgnoreCase)
            .Select(g => (Name: g.Key, Cpu: g.Sum(x => x.Cpu)))
            .OrderByDescending(b => b.Cpu)
            .Take(MostPerScan)
            .Select(b => b.Name)
            .ToList();
    }
}
