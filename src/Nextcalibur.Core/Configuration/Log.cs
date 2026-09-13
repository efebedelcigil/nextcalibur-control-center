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
/// </summary>
public static class Log
{
    private static readonly object Gate = new();
    private static readonly int KeepDays = 7;
    private static bool _folderReady;
    private static string? _currentFile;

    /// <summary>Where the files live: %AppData%\Nextcalibur\logs.</summary>
    public static string Folder { get; private set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nextcalibur", "logs");

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
    }

    public static void Info(string category, string message) => Write("INFO ", category, message);
    public static void Warn(string category, string message) => Write("WARN ", category, message);
    /// <summary>
    /// An error, with the exception's type, message and stack. The stack is
    /// the one place a line break is wanted, so it is written after the line
    /// rather than inside it.
    /// </summary>
    public static void Error(string category, string message, Exception? ex = null)
    {
        if (ex is null) { Write("ERROR", category, message); return; }
        Write("ERROR", category, $"{message}: {ex.GetType().Name}: {ex.Message}");
        foreach (var frame in (ex.StackTrace ?? string.Empty).Split('\n'))
            if (frame.Trim().Length > 0) Write("ERROR", category, "  " + frame.Trim());
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
        var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {level} [{Environment.CurrentManagedThreadId,3}] {OneLine(category)}: {OneLine(message)}";
        Debug.WriteLine(line);
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
                var file = Path.Combine(Folder, $"nextcalibur-{DateTime.Now:yyyyMMdd}.log");
                if (file != _currentFile)
                {
                    // A process that runs for weeks would otherwise keep every
                    // day's file until its next start.
                    if (_currentFile is not null) Prune();
                    _currentFile = file;
                }
                if (!Security.ProfileFiles.IsOrdinaryFileOrAbsent(file)) return;
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
