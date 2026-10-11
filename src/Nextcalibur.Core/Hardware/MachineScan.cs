using System.Text.Json;
using Microsoft.Win32;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;

namespace Nextcalibur.Core.Hardware;

/// <summary>What one scan of the machine found, as kept on disk.</summary>
public sealed class MachineScanRecord
{
    public DateTime ScannedAtUtc { get; set; }
    public string Version { get; set; } = "";
    public string Trigger { get; set; } = "";
    public SupportLevel Level { get; set; }
    public List<string> Reasons { get; set; } = new();

    /// <summary>The vendor's own power plans - Office, Gaming, High performance - present by name.</summary>
    public bool VendorPlansPresent { get; set; }

    /// <summary>The BIOS's identity as Windows reads it - kept for comparing one scan with the next, never sent anywhere.</summary>
    public string? BiosVendor { get; set; }
    public string? BiosVersion { get; set; }
    public string? BoardProduct { get; set; }

    /// <summary>When the level last changed, and from what.</summary>
    public SupportLevel? PreviousLevel { get; set; }
    public DateTime? LevelChangedAtUtc { get; set; }
}

/// <summary>
/// Which parts of this application belong to this machine - the owner's rule
/// of 11 October 2026: what is almost certainly Casper's (the firmware
/// mailbox, the vendor's power plans and modes, the fan turbo, the keyboard
/// light, the graphics switch, the Fn keys standing in for them) is only
/// offered where the machine has shown itself to be one; anywhere else those
/// controls cannot be clicked and Windows keeps its own defaults, so
/// nobody's system is changed by a guess.
///
/// The machine's identity is <see cref="HardwareSupport"/>'s: whether the
/// mailbox exists and answers the way this protocol does - the vendor never
/// filled in the model name, so the answers are the only identity there is.
/// This runs it, adds what else can be seen (the vendor's plans, the BIOS's
/// name), and keeps the result where only an administrator can write it -
/// once by the installer (<c>--scan</c>) and again at every start, with a
/// change of verdict between two scans written to the log.
/// </summary>
public static class MachineScan
{
    public static string RecordPath => Path.Combine(Security.ProtectedStore.Root, "machine-scan.json");

    private static readonly JsonSerializerOptions Json = new() { WriteIndented = true };

    /// <summary>Scans, compares with the last record, keeps the new one. Returns the verdict to act on.</summary>
    public static SupportVerdict RunAndRecord(string trigger, string version)
    {
        var verdict = HardwareSupport.Check();
        var previous = Load();

        var record = new MachineScanRecord
        {
            ScannedAtUtc = DateTime.UtcNow,
            Version = version,
            Trigger = trigger,
            Level = verdict.Level,
            Reasons = verdict.Reasons.ToList(),
            VendorPlansPresent = VendorPlansPresent(),
            PreviousLevel = previous?.PreviousLevel,
            LevelChangedAtUtc = previous?.LevelChangedAtUtc,
        };
        ReadBios(record);

        if (previous is not null && previous.Level != verdict.Level)
        {
            record.PreviousLevel = previous.Level;
            record.LevelChangedAtUtc = record.ScannedAtUtc;
            Log.Warn("support", $"The machine's verdict changed from {previous.Level} to {verdict.Level} since {previous.ScannedAtUtc:u} ({previous.Trigger})");
        }
        if (previous is not null && (previous.BiosVersion != record.BiosVersion || previous.BoardProduct != record.BoardProduct))
            Log.Info("support", $"BIOS or board differs from the last scan: {previous.BiosVersion} -> {record.BiosVersion}");

        Save(record);
        return verdict;
    }

    public static MachineScanRecord? Load()
    {
        try
        {
            return File.Exists(RecordPath)
                ? JsonSerializer.Deserialize<MachineScanRecord>(File.ReadAllText(RecordPath), Json)
                : null;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            return null;
        }
    }

    private static void Save(MachineScanRecord record)
    {
        try
        {
            Directory.CreateDirectory(Security.ProtectedStore.Root);
            var temp = RecordPath + ".tmp";
            File.WriteAllText(temp, JsonSerializer.Serialize(record, Json));
            File.Move(temp, RecordPath, overwrite: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Not elevated, or the folder is not ours yet: the verdict still
            // stands for this run; only the record is missing.
            Log.Warn("support", "Could not keep the machine scan: " + ex.Message);
        }
    }

    private static bool VendorPlansPresent()
    {
        var plans = SystemModeService.EnumeratePlans();
        return plans.ContainsKey("Office") && plans.ContainsKey("Gaming");
    }

    private static void ReadBios(MachineScanRecord record)
    {
        try
        {
            using var bios = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS");
            record.BiosVendor = bios?.GetValue("BIOSVendor") as string;
            record.BiosVersion = bios?.GetValue("BIOSVersion") as string;
            record.BoardProduct = bios?.GetValue("BaseBoardProduct") as string;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException)
        {
        }
    }
}
