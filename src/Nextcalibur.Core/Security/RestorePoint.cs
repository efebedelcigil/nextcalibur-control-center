using System.Management;
using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Security;

/// <summary>What asking Windows for a restore point came to.</summary>
/// <param name="Created">A new restore point exists because of this request.</param>
/// <param name="Sequence">Its number in Windows' list - what System Restore shows and restores by. For one not created, the newest there is.</param>
/// <param name="CreatedAt">When Windows says it was made.</param>
/// <param name="Status">The status Windows answered, 0 for success.</param>
/// <param name="Reason">Why there is no new one, in words, when there is not.</param>
public readonly record struct RestorePointOutcome(bool Created, long? Sequence, DateTime? CreatedAt, int Status, string? Reason);

/// <summary>
/// A Windows restore point before a change that could need undoing - the
/// owner's rule of 11 October 2026, offered with every such change and
/// ticked by default.
///
/// Made the way installers make them, through <c>SRSetRestorePointW</c>: a
/// "begin" that takes the snapshot, an "end" that closes it. Windows may say
/// yes and make nothing - it allows one a day by default, and System
/// Protection can be off for the system drive - so the result is checked
/// against Windows' own list of restore points, before and after, rather
/// than taken from the call.
/// </summary>
public static class RestorePoint
{
    private const int BeginSystemChange = 100;
    private const int EndSystemChange = 101;
    private const int ModifySettings = 12;
    private const int ErrorServiceDisabled = 1058;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct RestorePointInfo
    {
        public int EventType;
        public int RestorePtType;
        public long SequenceNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Description;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct StateManagerStatus
    {
        public int Status;
        public long SequenceNumber;
    }

    [DllImport("srclient.dll", CharSet = CharSet.Unicode)]
    private static extern bool SRSetRestorePointW(ref RestorePointInfo info, out StateManagerStatus status);

    private const string SystemRestoreKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore";
    private const string FrequencyValue = "SystemRestorePointCreationFrequency";

    /// <summary>
    /// Asks for a restore point and says what came of it. Slow - seconds to a
    /// minute; never on the interface thread.
    ///
    /// Windows makes at most one a day unless told otherwise - the
    /// SystemRestorePointCreationFrequency value, in minutes, 1440 when absent.
    /// The way Microsoft documents around it is that value set to 0: so it is,
    /// for the length of this one request, and put back exactly as it was -
    /// a value restored, or no value - straight after. Both are recorded in
    /// the audit log.
    /// </summary>
    public static RestorePointOutcome Create(string description)
    {
        var original = ReadFrequency();

        // The original first, beside the value, so a crash or a power cut in
        // the seconds between lifting and putting back is undone at the next
        // start (RecoverLimit) rather than left as a machine without a limit.
        if (!WriteMarker(original)) return CreateOnce(description);
        var lifted = SetFrequency(0);
        AuditLog.Record("restore-point-limit", new Dictionary<string, object?>
        {
            ["step"] = "lifted", ["original"] = original?.ToString() ?? "absent (Windows default: 1440 minutes)", ["ok"] = lifted,
        });
        try
        {
            return CreateOnce(description);
        }
        finally
        {
            var restored = original is { } value ? SetFrequency(value) : DeleteFrequency();
            if (restored) DeleteMarker();
            AuditLog.Record("restore-point-limit", new Dictionary<string, object?>
            {
                ["step"] = "put back", ["to"] = original?.ToString() ?? "absent", ["ok"] = restored,
            });
        }
    }

    private const string MarkerValue = "NextcaliburOriginalFrequency";

    /// <summary>
    /// At start and at uninstall: a limit lifted by a run that never put it
    /// back is put back now, from the marker written before lifting it.
    /// </summary>
    public static void RecoverLimit()
    {
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SystemRestoreKey);
            if (key?.GetValue(MarkerValue) is not string marker) return;
            var ok = int.TryParse(marker, out var minutes) ? SetFrequency(minutes) : DeleteFrequency();
            if (ok) DeleteMarker();
            AuditLog.Record("restore-point-limit", new Dictionary<string, object?>
            {
                ["step"] = "put back after an interrupted run", ["to"] = marker, ["ok"] = ok,
            });
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { }
    }

    private static bool WriteMarker(int? original)
    {
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(SystemRestoreKey, writable: true);
            key.SetValue(MarkerValue, original?.ToString() ?? "absent", Microsoft.Win32.RegistryValueKind.String);
            return true;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { return false; }
    }

    private static void DeleteMarker()
    {
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SystemRestoreKey, writable: true);
            key?.DeleteValue(MarkerValue, throwOnMissingValue: false);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { }
    }

    private static int? ReadFrequency()
    {
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SystemRestoreKey);
            return key?.GetValue(FrequencyValue) is int minutes ? minutes : null;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { return null; }
    }

    private static bool SetFrequency(int minutes)
    {
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(SystemRestoreKey, writable: true);
            key.SetValue(FrequencyValue, minutes, Microsoft.Win32.RegistryValueKind.DWord);
            return true;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { return false; }
    }

    private static bool DeleteFrequency()
    {
        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SystemRestoreKey, writable: true);
            key?.DeleteValue(FrequencyValue, throwOnMissingValue: false);
            return true;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException) { return false; }
    }

    private static RestorePointOutcome CreateOnce(string description)
    {
        var before = Newest();
        StateManagerStatus status;
        try
        {
            var info = new RestorePointInfo
            {
                EventType = BeginSystemChange,
                RestorePtType = ModifySettings,
                Description = description.Length > 255 ? description[..255] : description,
            };
            if (!SRSetRestorePointW(ref info, out status))
            {
                return new RestorePointOutcome(false, before?.Sequence, before?.CreatedAt, status.Status,
                    status.Status == ErrorServiceDisabled
                        ? Words.Get("S.Core.Restore.Disabled", "System Protection is off for the system drive, so Windows makes no restore points.")
                        : Words.Get("S.Core.Restore.Refused", "Windows refused (status {0}).", status.Status));
            }

            var end = new RestorePointInfo { EventType = EndSystemChange, RestorePtType = ModifySettings, SequenceNumber = status.SequenceNumber, Description = "" };
            SRSetRestorePointW(ref end, out _);
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return new RestorePointOutcome(false, null, null, -1, Words.Get("S.Core.Restore.Missing", "System Restore is not available on this Windows."));
        }

        // Windows lists the new point only once it has finished writing it -
        // seconds later, not when the call returns. Looked for by the number
        // the call gave, for up to a minute: looking at once said "none made"
        // for two points Windows had made (tour, 12 October 2026).
        var wanted = status.SequenceNumber;
        var deadline = DateTime.UtcNow.AddSeconds(60);
        while (true)
        {
            var after = Newest();
            if (after is { } made && (made.Sequence >= wanted && wanted > 0 || before is null || made.Sequence > before.Value.Sequence))
                return new RestorePointOutcome(true, made.Sequence, made.CreatedAt, status.Status, null);
            if (DateTime.UtcNow >= deadline) break;
            Thread.Sleep(1000);
        }

        // Not listed within the minute, but Windows said it was made: that is
        // what it said, with the number it gave.
        if (status.Status == 0 && wanted > 0 && (before is null || wanted > before.Value.Sequence))
            return new RestorePointOutcome(true, wanted, null, status.Status, null);

        // Said yes, made nothing: Windows' once-a-day limit.
        return new RestorePointOutcome(false, before?.Sequence, before?.CreatedAt, status.Status,
            before is { } last
                ? Words.Get("S.Core.Restore.Daily", "Windows made no new one: it allows one a day, and #{0} was made at {1}.", last.Sequence, last.CreatedAt.ToString("yyyy-MM-dd HH:mm"))
                : Words.Get("S.Core.Restore.None", "Windows made no restore point."));
    }

    /// <summary>The newest restore point Windows lists, or null when there are none or they cannot be read.</summary>
    public static (long Sequence, DateTime CreatedAt)? Newest()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(@"root\default", "SELECT SequenceNumber, CreationTime FROM SystemRestore");
            using var results = searcher.Get();
            (long, DateTime)? newest = null;
            foreach (ManagementObject point in results)
            {
                using (point)
                {
                    var sequence = Convert.ToInt64(point["SequenceNumber"]);
                    var created = point["CreationTime"] is string text ? ManagementDateTimeConverter.ToDateTime(text) : DateTime.MinValue;
                    if (newest is null || sequence > newest.Value.Item1) newest = (sequence, created);
                }
            }
            return newest;
        }
        catch (Exception ex) when (ex is ManagementException or UnauthorizedAccessException or COMException or FormatException or InvalidCastException)
        {
            return null;
        }
    }
}
