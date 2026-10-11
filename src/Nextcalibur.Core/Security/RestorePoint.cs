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

    /// <summary>Asks for a restore point and says what came of it. Slow - seconds to a minute; never on the interface thread.</summary>
    public static RestorePointOutcome Create(string description)
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

        var after = Newest();
        if (after is { } made && (before is null || made.Sequence > before.Value.Sequence))
            return new RestorePointOutcome(true, made.Sequence, made.CreatedAt, status.Status, null);

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
