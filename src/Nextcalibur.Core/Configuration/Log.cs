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

    /// <summary>Where the files live: %AppData%\Nextcalibur\logs.</summary>
    public static string Folder { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Nextcalibur", "logs");

    /// <summary>Called once at start: opens today's file and prunes old ones.</summary>
    public static void Start(string application, string version)
    {
        try
        {
            Directory.CreateDirectory(Folder);
            _folderReady = true;
            foreach (var old in Directory.EnumerateFiles(Folder, "nextcalibur-*.log"))
            {
                if (File.GetLastWriteTime(old) < DateTime.Now.AddDays(-KeepDays))
                    try { File.Delete(old); } catch (IOException) { }
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            _folderReady = false;
        }

        Info("start", $"{application} {version}; pid {Environment.ProcessId}; elevated {Elevation.IsElevated()}; args: {string.Join(' ', Environment.GetCommandLineArgs().Skip(1))}");
    }

    public static void Info(string category, string message) => Write("INFO ", category, message);
    public static void Warn(string category, string message) => Write("WARN ", category, message);
    public static void Error(string category, string message, Exception? ex = null) =>
        Write("ERROR", category, ex is null ? message : $"{message}: {ex.GetType().Name}: {ex.Message}{Environment.NewLine}{ex.StackTrace}");

    private static void Write(string level, string category, string message)
    {
        var line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {level} [{Environment.CurrentManagedThreadId,3}] {category}: {message}";
        Debug.WriteLine(line);
        if (!_folderReady) return;

        lock (Gate)
        {
            try
            {
                // Named per write, not once at start: a process that runs
                // across midnight starts the next day's file rather than
                // growing yesterday's.
                var file = Path.Combine(Folder, $"nextcalibur-{DateTime.Now:yyyyMMdd}.log");
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
