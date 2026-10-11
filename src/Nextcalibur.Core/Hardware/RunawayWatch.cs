using System.Diagnostics;

namespace Nextcalibur.Core.Hardware;

/// <summary>A program found heating the machine for nothing, and what to say about it.</summary>
/// <param name="Name">The process, as Task Manager names it.</param>
/// <param name="Cores">How much of the processor it has used, in cores; 0 for the graphics card.</param>
/// <param name="For">How long it has kept it up.</param>
/// <param name="OnGpu">Whether it is the graphics card it is keeping busy.</param>
public readonly record struct Runaway(string Name, double Cores, TimeSpan For, bool OnGpu);

/// <summary>
/// Programs stuck in a loop, burning the processor or the graphics card in
/// the background for nothing - the owner's request of 11 October 2026,
/// after the touch-keyboard host spent 26 hours on one core (see
/// <see cref="WindowsFaults"/>, which catches that one kind: a single core
/// pinned while the rest of the machine is quiet, and only then).
///
/// This one looks wider and only ever warns. A program is named when, for
/// fifteen minutes without a break:
/// - it has used at least <see cref="BusyCores"/> of a core every minute -
///   one spinning thread or several;
/// - it has done so <em>steadily</em>: its quietest minute at least
///   <see cref="SteadyRatio"/> of its busiest. A loop runs flat; real work -
///   a build, a render, an export - rises and falls with its stages;
/// - and it was not the program in front of the person, which is what they
///   are using and know about.
/// The graphics card the same way: busy above <see cref="GpuBusyPercent"/>
/// for fifteen minutes with nothing full-screen and the program on the card
/// not the one in front.
///
/// Nothing is ended - the program may be doing its job. Each is named at
/// most once every six hours. Readings are a minute or two apart, taken by
/// the caller; a longer gap is sleep and starts every count again.
/// </summary>
public sealed class RunawayWatch
{
    public static readonly TimeSpan Window = TimeSpan.FromMinutes(15);
    public const double BusyCores = 0.85;
    public const double SteadyRatio = 0.6;
    public const int GpuBusyPercent = 40;
    private static readonly TimeSpan Gap = TimeSpan.FromMinutes(3.5);
    private static readonly TimeSpan NotAgainWithin = TimeSpan.FromHours(6);

    private sealed class Track
    {
        public required string Name { get; init; }
        public DateTime Since { get; init; }
        public double Min { get; set; } = double.MaxValue;
        public double Max { get; set; }
        public double Sum { get; set; }
        public int Samples { get; set; }
    }

    private readonly Dictionary<(int Pid, long Start), double> _lastCpu = new();
    private readonly Dictionary<(int Pid, long Start), Track> _tracks = new();
    private readonly Dictionary<string, DateTime> _reported = new(StringComparer.OrdinalIgnoreCase);
    private DateTime _last = DateTime.MinValue;
    private DateTime _gpuBusySince = DateTime.MinValue;

    /// <summary>
    /// One reading of every process's processor time, in milliseconds, keyed
    /// by process and start time so a reused id is a new process.
    /// </summary>
    public IReadOnlyList<Runaway> SampleCpu(
        IReadOnlyDictionary<(int Pid, long Start), (string Name, double CpuMs)> now,
        DateTime at, int foregroundPid, ISet<string> handledElsewhere)
    {
        var found = new List<Runaway>();
        var elapsed = _last == DateTime.MinValue ? TimeSpan.Zero : at - _last;
        var asleep = elapsed > Gap || elapsed < TimeSpan.Zero;
        if (asleep) { _tracks.Clear(); _gpuBusySince = DateTime.MinValue; }

        if (_last != DateTime.MinValue && !asleep && elapsed > TimeSpan.Zero)
        {
            foreach (var (key, (name, cpu)) in now)
            {
                if (!_lastCpu.TryGetValue(key, out var before) || cpu < before) { _tracks.Remove(key); continue; }
                var cores = (cpu - before) / elapsed.TotalMilliseconds;

                if (cores < BusyCores || key.Pid == foregroundPid || key.Pid == Environment.ProcessId
                    || handledElsewhere.Contains(name))
                {
                    _tracks.Remove(key);
                    continue;
                }

                if (!_tracks.TryGetValue(key, out var track))
                    _tracks[key] = track = new Track { Name = name, Since = _last };
                track.Min = Math.Min(track.Min, cores);
                track.Max = Math.Max(track.Max, cores);
                track.Sum += cores;
                track.Samples++;

                if (at - track.Since < Window || track.Min < track.Max * SteadyRatio) continue;
                if (_reported.TryGetValue(name, out var last) && at - last < NotAgainWithin) continue;

                _reported[name] = at;
                found.Add(new Runaway(name, track.Sum / track.Samples, at - track.Since, OnGpu: false));
                _tracks.Remove(key);
            }

            foreach (var gone in _tracks.Keys.Where(k => !now.ContainsKey(k)).ToList()) _tracks.Remove(gone);
        }

        _lastCpu.Clear();
        foreach (var (key, value) in now) _lastCpu[key] = value.CpuMs;
        _last = at;
        return found;
    }

    /// <summary>
    /// One reading of the graphics card, taken by the caller only while the
    /// card is awake anyway. <paramref name="onCard"/> is asked only when
    /// there is something to report: it wakes nothing, but it is not free.
    /// </summary>
    public IReadOnlyList<Runaway> SampleGpu(int utilisationPercent, bool fullScreen, string? foregroundName,
        DateTime at, Func<IReadOnlyList<string>> onCard)
    {
        if (utilisationPercent < GpuBusyPercent || fullScreen)
        {
            _gpuBusySince = DateTime.MinValue;
            return [];
        }

        if (_gpuBusySince == DateTime.MinValue) { _gpuBusySince = at; return []; }
        if (at - _gpuBusySince < Window) return [];

        var names = onCard();

        // The program in front is the one being used; a card busy for it is
        // a card doing its job, whatever else is also on it.
        if (foregroundName is not null && names.Contains(foregroundName, StringComparer.OrdinalIgnoreCase))
        {
            _gpuBusySince = at;
            return [];
        }

        var lasted = at - _gpuBusySince;
        _gpuBusySince = DateTime.MinValue;
        var found = new List<Runaway>();
        var self = Path.GetFileNameWithoutExtension(Environment.ProcessPath) ?? "";
        foreach (var name in names.Where(n => !string.Equals(n, self, StringComparison.OrdinalIgnoreCase)).Take(3))
        {
            if (_reported.TryGetValue("gpu:" + name, out var last) && at - last < NotAgainWithin) continue;
            _reported["gpu:" + name] = at;
            found.Add(new Runaway(name, 0, lasted, OnGpu: true));
        }
        return found;
    }

    /// <summary>Every process's processor time now, for <see cref="SampleCpu"/>.</summary>
    public static Dictionary<(int Pid, long Start), (string Name, double CpuMs)> ReadProcesses()
    {
        var result = new Dictionary<(int, long), (string, double)>();
        foreach (var process in Process.GetProcesses())
        {
            using (process)
            {
                if (process.Id is 0 or 4) continue; // the idle loop, and the kernel - which nothing here could name usefully
                try
                {
                    long start;
                    try { start = process.StartTime.ToUniversalTime().Ticks; }
                    catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException) { start = 0; }
                    result[(process.Id, start)] = (process.ProcessName, process.TotalProcessorTime.TotalMilliseconds);
                }
                catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException
                                               or NotSupportedException or UnauthorizedAccessException)
                {
                }
            }
        }
        return result;
    }
}
