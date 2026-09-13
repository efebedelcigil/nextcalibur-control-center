using System.Diagnostics;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Hardware;

/// <summary>What was found, and whether this application did anything about it.</summary>
/// <param name="Id">Short, stable; goes in the log.</param>
/// <param name="What">One sentence for the person, in their language.</param>
/// <param name="Fixed">True when it was put right, false when it is only being reported.</param>
public sealed record FaultFound(string Id, string What, bool Fixed);

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
    private const double PinnedAtPercent = 92;

    /// <summary>...while the machine as a whole is below this. Real work lights up more than one core.</summary>
    private const double QuietMachineBelowPercent = 30;

    /// <summary>A process using at least this share of one core over the window is the one doing it.</summary>
    private const double CulpritAtPercent = 70;

    /// <summary>How long it has to look like that before it is believed.</summary>
    public static readonly TimeSpan Window = TimeSpan.FromMinutes(3);

    /// <summary>How long before the same fault may be acted on again.</summary>
    private static readonly TimeSpan NotAgainWithin = TimeSpan.FromHours(1);

    /// <summary>
    /// The list. One entry, and the shape is the point: a new fault is a
    /// new line here, with its evidence, not a new heuristic.
    /// </summary>
    private static readonly Watched[] Known =
    [
        new Watched(
            Id: "input-host",
            Process: "TextInputHost",
            Describe: () => Words.Get("S.Core.Fault.InputHost",
                "Windows' input host (TextInputHost) was stuck using a whole processor core. Nextcalibur restarted it; Windows starts it again by itself when the touch keyboard or the emoji panel is needed.")),
    ];

    private sealed record Watched(string Id, string Process, Func<string> Describe);

    private static readonly CoreLoad Cores = new();
    private static readonly Dictionary<int, TimeSpan> Before = new();
    private static readonly Dictionary<string, DateTime> ActedAt = new(StringComparer.Ordinal);
    private static readonly Dictionary<string, int> Failures = new(StringComparer.Ordinal);
    private static DateTime _pinnedSince = DateTime.MinValue;
    private static DateTime _snapshotAt = DateTime.MinValue;

    /// <summary>
    /// Looks once; call it every few minutes. Cheap unless something is
    /// actually wrong: one system call, and only then a walk of the process
    /// list. Returns what was found, which is almost always nothing.
    /// </summary>
    public static IReadOnlyList<FaultFound> Check(bool mayAct)
    {
        var loads = Cores.Read();
        if (loads.Length == 0) return [];   // the first look; nothing to compare against yet

        if (!CoreLoad.ACoreIsPinned(loads, PinnedAtPercent, QuietMachineBelowPercent))
        {
            _pinnedSince = DateTime.MinValue;
            Before.Clear();
            return [];
        }

        var now = DateTime.UtcNow;
        if (_pinnedSince == DateTime.MinValue)
        {
            // First sight of it. Remember who was using what, and wait.
            _pinnedSince = now;
            Snapshot(now);
            return [];
        }

        var elapsed = now - _snapshotAt;
        if (now - _pinnedSince < Window || elapsed <= TimeSpan.Zero) return [];

        var culprit = Busiest(elapsed);
        Snapshot(now);
        if (culprit is not { } who) return [];

        Log.Warn("windows", $"A core has been pinned for {(now - _pinnedSince).TotalMinutes:N0} minutes; {who.Name} (pid {who.Pid}) used {who.Share:N0}% of a core");
        _pinnedSince = now;   // whatever happens next, the window starts again

        var watched = Array.Find(Known, w => string.Equals(w.Process, who.Name, StringComparison.OrdinalIgnoreCase));
        if (watched is null)
            return [new FaultFound("busy-process",
                Words.Get("S.Core.Fault.Unknown",
                    "{0} has been using a whole processor core for several minutes while nothing else is busy. Nextcalibur has not touched it - it may be doing its job.",
                    who.Name),
                Fixed: false)];

        if (!mayAct) return [new FaultFound(watched.Id, watched.Describe(), Fixed: false)];
        if (ActedAt.TryGetValue(watched.Id, out var last) && now - last < NotAgainWithin) return [];
        if (Failures.TryGetValue(watched.Id, out var failed) && failed >= 2)
            return [new FaultFound(watched.Id, watched.Describe(), Fixed: false)];

        if (!End(who.Pid, watched.Process))
        {
            Failures[watched.Id] = failed + 1;
            return [new FaultFound(watched.Id, watched.Describe(), Fixed: false)];
        }

        ActedAt[watched.Id] = now;
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
        Failures.Clear();
        _pinnedSince = DateTime.MinValue;
    }

    private static void Snapshot(DateTime at)
    {
        Before.Clear();
        foreach (var process in Process.GetProcesses())
        {
            try { Before[process.Id] = process.TotalProcessorTime; }
            catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception or NotSupportedException) { }
            finally { process.Dispose(); }
        }
        _snapshotAt = at;
    }

    /// <summary>The process that used the most processor since the snapshot, if any used enough to be the one.</summary>
    private static (int Pid, string Name, double Share)? Busiest(TimeSpan elapsed)
    {
        (int Pid, string Name, double Share)? worst = null;
        foreach (var process in Process.GetProcesses())
        {
            try
            {
                if (!Before.TryGetValue(process.Id, out var was)) continue;
                var share = (process.TotalProcessorTime - was).TotalMilliseconds / elapsed.TotalMilliseconds * 100;
                if (share > (worst?.Share ?? CulpritAtPercent)) worst = (process.Id, process.ProcessName, share);
            }
            catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception or NotSupportedException) { }
            finally { process.Dispose(); }
        }
        return worst;
    }

    /// <summary>
    /// Ends it, with the two locks that are not the list: it has to be in
    /// this session - a service or another account's process is not ours to
    /// touch - and its image has to be Windows' own, so that something
    /// which merely borrowed the name is left alone.
    /// </summary>
    private static bool End(int pid, string expectedName)
    {
        try
        {
            using var process = Process.GetProcessById(pid);
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
    private static bool IsWindowsOwn(Process process)
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
}
