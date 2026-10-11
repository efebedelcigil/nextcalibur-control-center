using System.Diagnostics;
using System.Text.Json;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Security;

/// <summary>
/// The record of every change this application makes to the machine that
/// could matter if something went wrong - and of what the person was asked
/// and answered before it. The owner's rule of 11 October 2026: every such
/// choice, with its time, whether a restore point was asked for, whether one
/// was made and its number.
///
/// Kept twice, so that one copy surviving is enough:
/// - <c>audit.jsonl</c> in the protected folder (<see cref="ProtectedStore"/>):
///   only administrators write there, the file's hash is recorded, and a copy
///   changed from outside is noticed and replaced by the last good one;
/// - Windows' Application event log, source "Nextcalibur": outside this
///   application's folders altogether, and kept by Windows after the
///   application is uninstalled.
///
/// One JSON object per line: the time (local, with its offset, and UTC), the
/// account, the application's version, what happened, and its details.
/// </summary>
public static class AuditLog
{
    public const string FileName = "audit.jsonl";
    public const string EventSource = "Nextcalibur";

    /// <summary>The version written into every entry; set once at start.</summary>
    public static string AppVersion { get; set; } = "";

    private static readonly object Gate = new();
    private static readonly JsonSerializerOptions Json = new() { WriteIndented = false };

    /// <summary>Records one entry in both places. Never throws: a record that cannot be kept is said in the ordinary log.</summary>
    public static void Record(string action, IReadOnlyDictionary<string, object?>? details = null)
    {
        var now = DateTimeOffset.Now;
        var entry = new Dictionary<string, object?>
        {
            ["at"] = now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
            ["utc"] = now.UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            ["account"] = Environment.UserDomainName + "\\" + Environment.UserName,
            ["version"] = AppVersion,
            ["action"] = action,
        };
        if (details is not null)
            foreach (var (key, value) in details) entry[key] = value;

        var line = JsonSerializer.Serialize(entry, Json);
        Log.Info("audit", line);

        lock (Gate)
        {
            try
            {
                var existing = ProtectedStore.Read(FileName) ?? string.Empty;
                if (!ProtectedStore.Write(FileName, existing + line + "\n"))
                    Log.Warn("audit", "The audit file could not be written; the event log still has the entry");
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Log.Warn("audit", "The audit file could not be written: " + ex.Message);
            }
        }

        WriteEvent(line);
    }

    /// <summary>The entries kept in the protected folder, oldest first.</summary>
    public static IReadOnlyList<string> Entries() =>
        (ProtectedStore.Read(FileName) ?? string.Empty).Split('\n', StringSplitOptions.RemoveEmptyEntries);

    private static void WriteEvent(string line)
    {
        try
        {
            // Registering the source needs administrator; the application runs
            // elevated. Uninstall removes the registration, not the entries.
            if (!EventLog.SourceExists(EventSource))
                EventLog.CreateEventSource(EventSource, "Application");
            EventLog.WriteEntry(EventSource, line, EventLogEntryType.Information, 1000);
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or InvalidOperationException
                                       or ArgumentException or System.ComponentModel.Win32Exception or IOException)
        {
            Log.Warn("audit", "The event log copy could not be written: " + ex.Message);
        }
    }

    /// <summary>For uninstall: the source's registration goes; what it wrote stays in Windows' log.</summary>
    public static void RemoveEventSource()
    {
        try
        {
            if (EventLog.SourceExists(EventSource)) EventLog.DeleteEventSource(EventSource);
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or InvalidOperationException
                                       or ArgumentException or IOException)
        {
        }
    }
}
