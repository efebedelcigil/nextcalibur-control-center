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
