using System.Diagnostics;
using System.Text;

namespace Nextcalibur.Core.Configuration;

/// <summary>
/// The application's own record of what it did and what went wrong.
///
/// Set by the owner on 12 September 2026, after a crash that only the Windows
/// event log had seen. One file a day under the settings folder, each line
/// stamped to the millisecond with the thread and a category; seven days
/// kept, the rest deleted at start. Events, not readings: a mode change is a
/// line, a temperature sample is not - the log must stay small enough to
/// read and cheap enough to never matter.
///
/// Writes are synchronous under a lock, because a log that loses its last
/// lines in a crash is no use, and the lines are few and short.
///
/// The line (the owner's ask of 14 October 2026, "standartlara uyalım"):
/// <c>2026-10-14T20:30:01.123+03:00 INFO  [pid:tid] category: message key=value ...</c>
/// - an ISO 8601 local time with its offset, a five-letter level (DEBUG,
/// INFO, WARN, ERROR, FATAL), the process and thread, a category, the
/// message and any fields. An error carries every inner exception with its
/// type, HRESULT and stack. A day's file stops taking anything below ERROR
/// past 20 MB, so a fault in a loop cannot fill the disk.
/// </summary>
public static class Log
{
    private static readonly object Gate = new();
    private static readonly int KeepDays = 7;
    private const long MostBytesPerDay = 20L * 1024 * 1024;
    private static bool _capTold;
    private static bool _folderReady;
    private static string? _currentFile;

    /// <summary>
    /// Where the files live: the account's protected folder (see
    /// Security.ProtectedStore), so nothing unelevated can write lines into
    /// the record of what happened. The account may read it.
    /// </summary>
    public static string Folder { get; private set; } = Path.Combine(Security.ProtectedStore.Folder, "logs");

    /// <summary>
    /// The tests write into a folder of their own: proving something
    /// about the log by writing into the log somebody attaches to a bug
    /// report would be a poor trade.
    /// </summary>
    internal static void WriteTo(string folder)
    {
        Folder = folder;
        _folderReady = false;
        _currentFile = null;
    }

    /// <summary>Called once at start: opens today's file and prunes old ones.</summary>
    public static void Start(string application, string version)
    {
        try
        {
            if (EnsureFolder()) Prune();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            _folderReady = false;
        }

        Info("start", $"{application} {version}; pid {Environment.ProcessId}; elevated {Elevation.IsElevated()}; args: {string.Join(' ', Environment.GetCommandLineArgs().Skip(1))}");
        // What a bug report needs to know about where it ran - never who.
        Info("start", Fields("environment",
            ("os", Environment.OSVersion.VersionString), ("build", WindowsBuild()), ("dotnet", Environment.Version),
            ("x64", Environment.Is64BitProcess), ("cpus", Environment.ProcessorCount),
            ("culture", System.Globalization.CultureInfo.CurrentCulture.Name), ("ui", System.Globalization.CultureInfo.CurrentUICulture.Name),
            ("uptime", TimeSpan.FromMilliseconds(Environment.TickCount64).ToString(@"d\.hh\:mm\:ss")),
            ("timezone", TimeZoneInfo.Local.Id)));
    }

    public static void Debug(string category, string message) => Write("DEBUG", category, message);
    public static void Info(string category, string message) => Write("INFO ", category, message);
    public static void Warn(string category, string message) => Write("WARN ", category, message);

    /// <summary>A line with fields after the message: <c>key=value</c>, a value with spaces in quotes.</summary>
    public static void Info(string category, string message, params (string Key, object? Value)[] fields) => Write("INFO ", category, Fields(message, fields));

    /// <summary>A warning with fields.</summary>
    public static void Warn(string category, string message, params (string Key, object? Value)[] fields) => Write("WARN ", category, Fields(message, fields));

    /// <summary>
    /// An error, with the exception's type, HRESULT, message and stack, and
    /// the same for every inner exception. The stack is the one place a line
    /// break is wanted, so it is written after the line rather than inside it.
    /// </summary>
    public static void Error(string category, string message, Exception? ex = null) => Exceptional("ERROR", category, message, ex);

    /// <summary>The process cannot go on: the last lines it writes.</summary>
    public static void Fatal(string category, string message, Exception? ex = null) => Exceptional("FATAL", category, message, ex);

    private static void Exceptional(string level, string category, string message, Exception? ex)
    {
        if (ex is null) { Write(level, category, message); return; }
        var depth = 0;
        for (var e = ex; e is not null && depth < 8; e = e.InnerException, depth++)
        {
            var lead = depth == 0 ? message + ": " : "  caused by ";
            Write(level, category, $"{lead}{e.GetType().FullName} (0x{e.HResult:X8}): {e.Message}");
            foreach (var frame in (e.StackTrace ?? string.Empty).Split('\n'))
                if (frame.Trim().Length > 0) Write(level, category, "    " + frame.Trim());
            if (e is AggregateException aggregate && aggregate.InnerExceptions.Count > 1)
                foreach (var each in aggregate.InnerExceptions.Skip(1).Take(4))
                    Write(level, category, $"  also {each.GetType().FullName} (0x{each.HResult:X8}): {each.Message}");
        }
    }

    /// <summary>A message with <c>key=value</c> fields after it.</summary>
    public static string Fields(string message, params (string Key, object? Value)[] fields)
    {
        if (fields.Length == 0) return message;
        var text = new StringBuilder(message);
        foreach (var (key, value) in fields)
        {
            var shown = value switch
            {
                null => "-",
                IFormattable f => f.ToString(null, System.Globalization.CultureInfo.InvariantCulture),
                _ => value.ToString() ?? "-",
            };
            if (shown.Contains(' ') || shown.Length == 0) shown = "\"" + shown.Replace("\"", "'") + "\"";
            text.Append(' ').Append(key).Append('=').Append(shown);
        }
        return text.ToString();
    }

    private static string WindowsBuild()
    {
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            return $"{key?.GetValue("CurrentBuild")}.{key?.GetValue("UBR")} {key?.GetValue("DisplayVersion")}".Trim();
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException or IOException)
        {
            return "?";
        }
    }

    /// <summary>Deletes files older than the kept window. At start, and again each time the day turns.</summary>
    private static void Prune()
    {
        try
        {
            foreach (var old in Directory.EnumerateFiles(Folder, "nextcalibur-*.log"))
            {
                if (File.GetLastWriteTime(old) < DateTime.Now.AddDays(-KeepDays))
                    try { File.Delete(old); } catch (IOException) { }
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
        }
    }

    /// <summary>
    /// One line stays one line. Messages carry things from outside - an
    /// exception's text, a server's answer, a path - and a newline in one of
    /// them would let it write log entries of its own, with any timestamp and
    /// any wording it liked. Every control character becomes a space; a very
    /// long message is cut.
    /// </summary>
    private static string OneLine(string text)
    {
        if (text.Length > 4000) text = text[..4000] + "...";
        Span<char> buffer = text.Length <= 512 ? stackalloc char[text.Length] : new char[text.Length];
        for (var i = 0; i < text.Length; i++)
            buffer[i] = char.IsControl(text[i]) ? ' ' : text[i];
        return new string(buffer);
    }

    /// <summary>
    /// The folder, made once and checked once. An elevated process appending
    /// to a folder the account owns must not follow a link somebody put
    /// there, so a folder that is one is not written to at all.
    /// </summary>
    private static bool EnsureFolder()
    {
        if (_folderReady) return true;
        try
        {
            _folderReady = Security.ProfileFiles.EnsureOrdinaryFolder(Folder);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            _folderReady = false;
        }
        return _folderReady;
    }

    private static void Write(string level, string category, string message)
    {
        // Invariant: the format follows the machine's calendar otherwise -
        // a Windows set to the Hijri or Buddhist calendar would date these
        // lines, and name the files, in a year nobody reading a bug report
        // would recognise, and the day would turn in the wrong place.
        var line = string.Create(System.Globalization.CultureInfo.InvariantCulture,
            $"{DateTimeOffset.Now:yyyy-MM-dd'T'HH:mm:ss.fffzzz} {level} [{Environment.ProcessId}:{Environment.CurrentManagedThreadId,3}] {OneLine(category)}: {OneLine(message)}");
        System.Diagnostics.Debug.WriteLine(line);
        // Not only after Start: the first things worth logging - a name held
        // by something else, a folder that turned out to be a link - happen
        // before anything has called it, and they were being dropped.
        if (!EnsureFolder()) return;

        lock (Gate)
        {
            try
            {
                // Named per write, not once at start: a process that runs
                // across midnight starts the next day's file rather than
                // growing yesterday's.
                var file = Path.Combine(Folder, string.Create(System.Globalization.CultureInfo.InvariantCulture, $"nextcalibur-{DateTime.Now:yyyyMMdd}.log"));
                if (file != _currentFile)
                {
                    // A process that runs for weeks would otherwise keep every
                    // day's file until its next start.
                    if (_currentFile is not null) Prune();
                    _currentFile = file;
                }
                if (!Security.ProfileFiles.IsOrdinaryFileOrAbsent(file)) return;
                // Past the day's cap only errors get in, and the cap is said once.
                var info = new FileInfo(file);
                if (info.Exists && info.Length > MostBytesPerDay && level is not ("ERROR" or "FATAL"))
                {
                    if (_capTold) return;
                    _capTold = true;
                    line = string.Create(System.Globalization.CultureInfo.InvariantCulture,
                        $"{DateTimeOffset.Now:yyyy-MM-dd'T'HH:mm:ss.fffzzz} WARN  [{Environment.ProcessId}:{Environment.CurrentManagedThreadId,3}] log: Today's file passed {MostBytesPerDay / 1048576} MB; only errors are written until tomorrow");
                }
                else if (!info.Exists) _capTold = false;
                using var writer = new StreamWriter(file, append: true, Encoding.UTF8);
                writer.WriteLine(line);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                // A log that cannot be written is not worth failing anything over.
            }
        }
    }

    /// <summary>Removes every log file; part of the uninstall.</summary>
    public static void Remove()
    {
        try
        {
            if (Directory.Exists(Folder)) Directory.Delete(Folder, recursive: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
        }
    }
}
