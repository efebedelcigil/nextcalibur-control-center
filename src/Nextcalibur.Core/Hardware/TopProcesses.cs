using System.Diagnostics;

namespace Nextcalibur.Core.Hardware;

/// <summary>One program's share of the processor over a short window.</summary>
/// <param name="Name">The process name, as Task Manager shows it on the Details tab, without ".exe".</param>
/// <param name="Percent">Share of the whole processor - every core - over the window, 0-100.</param>
public readonly record struct ProcessLoad(string Name, double Percent);

/// <summary>
/// Which programs are using the processor right now - for the overheat
/// warning, where "the processor is at 95 °C" is half an answer and the
/// other half is what is making it hot.
///
/// Two readings of every process's processor time a second apart, the
/// difference shared out the way Task Manager does: as a part of the whole
/// processor, all cores together. Processes with the same name are added up,
/// so a browser's forty helpers are one line. Asked only when a warning is
/// about to be shown; it opens every process twice and is not for a timer.
/// </summary>
public static class TopProcesses
{
    /// <summary>Below this a program is not worth naming in a warning.</summary>
    private const double NoticeablePercent = 1.0;

    /// <summary>Measures over <paramref name="window"/> and returns the busiest few, busiest first.</summary>
    public static IReadOnlyList<ProcessLoad> Measure(int count = 3, TimeSpan? window = null)
    {
        var span = window ?? TimeSpan.FromSeconds(1);
        var before = Snapshot();
        var clock = Stopwatch.StartNew();
        Thread.Sleep(span);
        var after = Snapshot();
        var elapsed = clock.Elapsed.TotalMilliseconds * Environment.ProcessorCount;
        if (elapsed <= 0) return [];

        var byName = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        foreach (var (id, (name, cpu)) in after)
        {
            // A process that started inside the window counts from zero.
            var used = before.TryGetValue(id, out var earlier) && earlier.Name == name ? cpu - earlier.Cpu : cpu;
            if (used <= 0) continue;
            byName[name] = byName.GetValueOrDefault(name) + used / elapsed * 100;
        }

        return byName
            .Where(p => p.Value >= NoticeablePercent)
            .OrderByDescending(p => p.Value)
            .Take(count)
            .Select(p => new ProcessLoad(p.Key, Math.Min(100, p.Value)))
            .ToList();
    }

    /// <summary>"openscad 62%, chrome 9%" - or null when nothing stands out.</summary>
    public static string? Describe(IReadOnlyList<ProcessLoad> loads) =>
        loads.Count == 0 ? null : string.Join(", ", loads.Select(l => $"{l.Name} {l.Percent:0}%"));

    private static Dictionary<int, (string Name, double Cpu)> Snapshot()
    {
        var result = new Dictionary<int, (string, double)>();
        foreach (var process in Process.GetProcesses())
        {
            using (process)
            {
                // The idle "process" is the processor doing nothing.
                if (process.Id == 0) continue;
                try
                {
                    result[process.Id] = (process.ProcessName, process.TotalProcessorTime.TotalMilliseconds);
                }
                catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException
                                               or NotSupportedException or UnauthorizedAccessException)
                {
                    // Protected, or gone between the listing and the question.
                }
            }
        }
        return result;
    }
}
