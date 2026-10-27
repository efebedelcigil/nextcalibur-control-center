using System.Text.Json;

namespace Nextcalibur.Core.Configuration;

/// <summary>
/// Protection against crashes (the owner's ask of 14 October 2026).
///
/// A marker in the protected folder says a run is in progress; a clean exit
/// removes it. Found at start, it means the last run ended without one - a
/// crash, a kill, a power cut - and that is logged with when it started and
/// when it was last seen alive. When the runs keep ending soon after they
/// start (<see cref="LoopCount"/> in <see cref="LoopWindow"/>), the start is
/// a safe one: the things done to the hardware at start - the lighting put
/// back, the firmware's profile put back - are left out, since one of them
/// may be what brings it down. Everything else, the temperature warning
/// first, runs as usual. A clean run clears the history.
/// </summary>
public static class CrashGuard
{
    /// <summary>A run that ends within this of its start counts towards a crash loop.</summary>
    public static readonly TimeSpan EarlyEnd = TimeSpan.FromMinutes(3);

    /// <summary>This many early ends ...</summary>
    public const int LoopCount = 2;

    /// <summary>... within this long make the next start a safe one.</summary>
    public static readonly TimeSpan LoopWindow = TimeSpan.FromMinutes(15);

    private static string _marker = Path.Combine(Security.ProtectedStore.Folder, "session.json");
    private static Session? _current;
    private static readonly object Gate = new();

    /// <summary>True when this start is a safe one; see the class notes.</summary>
    public static bool SafeMode { get; private set; }

    /// <summary>The run before this one, when it ended without a clean exit; null otherwise.</summary>
    public static Session? UncleanPrevious { get; private set; }

    /// <summary>What the marker holds.</summary>
    public sealed class Session
    {
        public DateTimeOffset Started { get; set; }
        public DateTimeOffset LastAlive { get; set; }
        public int Pid { get; set; }
        public string Version { get; set; } = "";
        public string? LastFault { get; set; }
        /// <summary>When earlier runs ended unclean soon after they started (the newest last).</summary>
        public List<DateTimeOffset> EarlyEnds { get; set; } = [];
    }

    /// <summary>For the tests: a marker somewhere else.</summary>
    internal static void UseMarker(string path)
    {
        _marker = path;
        _current = null;
        SafeMode = false;
        UncleanPrevious = null;
    }

    /// <summary>Called once at start, after the log: reads the last run's marker and writes this run's.</summary>
    public static void Begin(string version, DateTimeOffset? now = null)
    {
        var at = now ?? DateTimeOffset.Now;
        var previous = Read();
        var earlyEnds = new List<DateTimeOffset>();
        if (previous is not null)
        {
            UncleanPrevious = previous;
            earlyEnds.AddRange(previous.EarlyEnds.Where(e => at - e <= LoopWindow));
            var ranFor = previous.LastAlive - previous.Started;
            if (ranFor <= EarlyEnd) earlyEnds.Add(previous.LastAlive);
            Log.Warn("crash-guard", "The last run did not end cleanly",
                ("started", previous.Started.ToString("o")), ("lastAlive", previous.LastAlive.ToString("o")),
                ("ranFor", ranFor.ToString(@"hh\:mm\:ss")), ("pid", previous.Pid), ("version", previous.Version),
                ("lastFault", previous.LastFault), ("earlyEndsRecently", earlyEnds.Count));
        }

        SafeMode = earlyEnds.Count >= LoopCount;
        if (SafeMode)
            Log.Warn("crash-guard", "Safe start: the last runs ended soon after starting; the lighting and the firmware profile are not put back at this start",
                ("earlyEnds", earlyEnds.Count), ("window", LoopWindow.TotalMinutes + " min"));

        _current = new Session { Started = at, LastAlive = at, Pid = Environment.ProcessId, Version = version, EarlyEnds = earlyEnds };
        Write(_current);
    }

    /// <summary>From the slow timer: still alive. Cheap - one small file now and then.</summary>
    public static void Heartbeat(DateTimeOffset? now = null)
    {
        lock (Gate)
        {
            if (_current is null) return;
            var at = now ?? DateTimeOffset.Now;
            // Past the early window once, the history of early ends has served: this run is a good one.
            if (_current.EarlyEnds.Count > 0 && at - _current.Started > EarlyEnd) _current.EarlyEnds.Clear();
            _current.LastAlive = at;
            Write(_current);
        }
    }

    /// <summary>A fault the process is about to die of: kept in the marker for the next start to report.</summary>
    public static void RecordFault(Exception? ex)
    {
        lock (Gate)
        {
            if (_current is null) return;
            _current.LastFault = ex is null ? "unknown" : $"{ex.GetType().FullName}: {ex.Message}";
            _current.LastAlive = DateTimeOffset.Now;
            Write(_current);
        }
    }

    /// <summary>A clean exit: the marker goes.</summary>
    public static void End()
    {
        bool wasRunning;
        lock (Gate)
        {
            // Once: a session end calls it, then the exit after it.
            wasRunning = _current is not null;
            _current = null;
            try { if (File.Exists(_marker)) File.Delete(_marker); }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { }
        }
        if (wasRunning) Log.Info("crash-guard", "Clean exit");
    }

    private static Session? Read()
    {
        try
        {
            if (!File.Exists(_marker) || !Security.ProfileFiles.IsOrdinaryFileOrAbsent(_marker)) return null;
            return JsonSerializer.Deserialize<Session>(File.ReadAllText(_marker));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException or NotSupportedException)
        {
            return null;
        }
    }

    private static void Write(Session session)
    {
        try
        {
            var folder = Path.GetDirectoryName(_marker);
            if (folder is not null) Directory.CreateDirectory(folder);
            if (!Security.ProfileFiles.IsOrdinaryFileOrAbsent(_marker)) return;
            File.WriteAllText(_marker, JsonSerializer.Serialize(session));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
        }
    }
}
