using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Safely trims bloated working sets of core Windows processes (DWM, Explorer)
/// using Windows' official K32EmptyWorkingSet API.
///
/// This does NOT kill or terminate processes. It flushes unreferenced cached pages
/// back to the OS standby/free list without any screen flicker or application interruption.
/// </summary>
public static class MemoryTrimmer
{
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool K32EmptyWorkingSet(IntPtr hProcess);

    /// <summary>
    /// Trims memory of the specified process name if its working set exceeds the threshold.
    /// Returns the bytes reclaimed, or 0 if below threshold or call failed.
    /// </summary>
    public static long TrimIfExceeds(string processName, long minWorkingSetBytes)
    {
        long reclaimed = 0;
        foreach (var process in Process.GetProcessesByName(processName))
        {
            try
            {
                var before = process.WorkingSet64;
                if (before >= minWorkingSetBytes)
                {
                    if (K32EmptyWorkingSet(process.Handle))
                    {
                        process.Refresh();
                        var after = process.WorkingSet64;
                        if (before > after)
                        {
                            var diff = before - after;
                            reclaimed += diff;
                            Log.Info("memory", $"Trimmed {processName} (pid {process.Id}): {before / 1024 / 1024} MB -> {after / 1024 / 1024} MB (reclaimed {diff / 1024 / 1024} MB)");
                        }
                    }
                }
            }
            catch (Exception ex) when (ex is Win32Exception or InvalidOperationException or NotSupportedException)
            {
                // Process may be protected or access denied; fail quietly.
            }
            finally
            {
                process.Dispose();
            }
        }
        return reclaimed;
    }
}
