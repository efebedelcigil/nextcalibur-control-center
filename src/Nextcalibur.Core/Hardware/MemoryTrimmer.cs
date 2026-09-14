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
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public static MEMORYSTATUSEX Create() => new() { dwLength = (uint)Marshal.SizeOf<MEMORYSTATUSEX>() };
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool K32EmptyWorkingSet(IntPtr hProcess);

    internal sealed class TrimRecord
    {
        public DateTime LastTrimTime { get; set; }
        public long WorkingSetAfterTrim { get; set; }
        public DateTime StandDownUntil { get; set; }
    }

    private static readonly Dictionary<string, TrimRecord> History = new(StringComparer.OrdinalIgnoreCase);
    private static readonly object SyncLock = new();

    /// <summary>Returns the current percentage of available physical RAM, or null if unavailable.</summary>
    public static double? GetFreePhysicalMemoryPercent()
    {
        try
        {
            var stat = MEMORYSTATUSEX.Create();
            if (!GlobalMemoryStatusEx(ref stat) || stat.ullTotalPhys == 0) return null;
            return (double)stat.ullAvailPhys / stat.ullTotalPhys * 100.0;
        }
        catch (Exception)
        {
            return null;
        }
    }

    internal static void ResetHistory()
    {
        lock (SyncLock)
        {
            History.Clear();
        }
    }

    internal static TrimRecord? GetRecord(string processName)
    {
        lock (SyncLock)
        {
            return History.TryGetValue(processName, out var r) ? r : null;
        }
    }

    /// <summary>
    /// Trims memory of the specified process name if system memory is under pressure (&lt; 15% free)
    /// or if the process has grown to an extreme unexplainable size.
    /// Stands down for an hour if memory bounces back within 15 minutes of a previous trim.
    /// Returns the bytes reclaimed, or 0 if skipped, below threshold, or call failed.
    /// </summary>
    public static long TrimIfExceeds(string processName, long minWorkingSetBytes, long? extremeWorkingSetBytes = null)
    {
        if (UserPresence.WouldRatherNotBeDisturbed())
        {
            return 0;
        }

        var now = DateTime.UtcNow;
        TrimRecord? record;
        lock (SyncLock)
        {
            if (History.TryGetValue(processName, out record))
            {
                if (now < record.StandDownUntil)
                {
                    return 0;
                }
            }
        }

        long extremeThreshold = extremeWorkingSetBytes ?? processName.ToLowerInvariant() switch
        {
            "dwm" => 2560L * 1024 * 1024,      // 2.5 GB (normal on multi-4K is ~1.5 GB)
            "explorer" => 2048L * 1024 * 1024, // 2.0 GB
            _ => minWorkingSetBytes * 2,
        };

        var freeMemPct = GetFreePhysicalMemoryPercent();
        bool isUnderPressure = freeMemPct.HasValue && freeMemPct.Value < 15.0;

        long reclaimed = 0;
        Process[] processes;
        try
        {
            processes = Process.GetProcessesByName(processName);
        }
        catch
        {
            return 0;
        }

        foreach (var process in processes)
        {
            try
            {
                var before = process.WorkingSet64;
                if (before < minWorkingSetBytes) continue;

                // Rule C.2: If memory returned to bloated size within 15 minutes of previous trim,
                // that memory was actively live and re-faulted. Standing down for 1 hour.
                if (record is not null && record.LastTrimTime != DateTime.MinValue && (now - record.LastTrimTime) <= TimeSpan.FromMinutes(15))
                {
                    lock (SyncLock)
                    {
                        record.StandDownUntil = now.AddHours(1);
                    }
                    Log.Warn("memory", $"{processName} (pid {process.Id}) working set returned to {before / 1024 / 1024} MB within {(now - record.LastTrimTime).TotalMinutes:F1} min of trim; memory is actively live. Standing down for 1 hour.");
                    return 0;
                }

                // Rule C.1: Only trim when system memory is genuinely under pressure (< 15% free)
                // or when the process working set is far past anything explainable (extreme threshold).
                bool isExtreme = before >= extremeThreshold;
                if (!isUnderPressure && !isExtreme)
                {
                    continue;
                }

                if (K32EmptyWorkingSet(process.Handle))
                {
                    process.Refresh();
                    var after = process.WorkingSet64;
                    if (before > after)
                    {
                        var diff = before - after;
                        reclaimed += diff;

                        lock (SyncLock)
                        {
                            if (record is null)
                            {
                                record = new TrimRecord();
                                History[processName] = record;
                            }
                            record.LastTrimTime = now;
                            record.WorkingSetAfterTrim = after;
                            record.StandDownUntil = DateTime.MinValue;
                        }

                        var freePctStr = freeMemPct.HasValue ? $"{freeMemPct.Value:F1}%" : "unknown";
                        Log.Info("memory", $"Trimmed {processName} (pid {process.Id}): {before / 1024 / 1024} MB -> {after / 1024 / 1024} MB (reclaimed {diff / 1024 / 1024} MB, system free RAM: {freePctStr})");
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
