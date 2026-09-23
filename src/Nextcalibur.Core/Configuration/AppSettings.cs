using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Win32;

namespace Nextcalibur.Core.Configuration;

/// <summary>User preferences, persisted per user.</summary>
public sealed class AppSettings
{
    /// <summary>How often to sample sensors while the window is visible, in milliseconds.</summary>
    public int PollIntervalMs { get; set; } = 2000;

    /// <summary>Start hidden in the notification area.</summary>
    public bool StartMinimised { get; set; } = true;

    /// <summary>Show a Windows notification when the CPU gets too hot.</summary>
    public bool OverheatWarningEnabled { get; set; } = true;

    /// <summary>
    /// Drop to Office when the charger comes out and put the previous mode
    /// back when it returns. On by default because the vendor does it.
    /// </summary>
    public bool QuietOnBattery { get; set; } = true;

    /// <summary>
    /// Look for a new release on GitHub: once a minute after start, then every
    /// six hours - one small request each time, nothing between. Off means no
    /// request at all; the tray still offers a check by hand.
    /// </summary>
    public bool AutoCheckForUpdates { get; set; } = true;

    /// <summary>
    /// Go further than checking: when a release is found, download it,
    /// verify it and restart into it without asking. Off by default - the
    /// ask-first flow is the default - and it never runs while a graphics
    /// change is waiting for a restart, since that restart is the person's
    /// to time. Meaningless with <see cref="AutoCheckForUpdates"/> off.
    /// </summary>
    public bool AutoInstallUpdates { get; set; }

    /// <summary>
    /// The language of the interface: English or Turkish, switched live.
    /// Null until chosen, meaning "whatever Windows is set to" - Turkish on
    /// a Turkish Windows, English on every other.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
    public UiLanguage? Language { get; set; }

    /// <summary>
    /// The mode the person last chose, put back at the next start. Windows
    /// comes up on Balanced after a restart whatever plan was active before,
    /// so without this a chosen mode lasted one session.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Nextcalibur.Core.Power.SystemMode? LastSystemMode { get; set; }

    /// <summary>
    /// Settings this version does not know, kept and written back untouched.
    /// Seen 11 September 2026: 0.4.0 ran once between two 0.5.0 sessions,
    /// loaded a file with <c>LastSystemMode</c> in it, and saved it without -
    /// the chosen mode was gone. From this version on, an older copy that
    /// happens to run cannot lose what a newer one wrote.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Unknown { get; set; }

    /// <summary>
    /// Warn when the CPU exceeds this temperature, in °C.
    ///
    /// Turning the warning off is <see cref="OverheatWarningEnabled"/>, not a
    /// zero here. Two ways to express "off" drift apart: switch it off from one
    /// place, back on from the other, and the machine is left unwatched while
    /// the interface says otherwise. This value only ever means "how hot is too
    /// hot", and it survives the warning being switched off and on again.
    /// </summary>
    public int CpuWarningTemperatureC { get; set; } = 90;

    /// <summary>
    /// The GPU's own threshold, °C. Lower by default: the card's driver
    /// throttles it in the high eighties, so ninety on the GPU is a heatsink
    /// that has already lost.
    /// </summary>
    public int GpuWarningTemperatureC { get; set; } = 85;

    /// <summary>The range either threshold may be set to.</summary>
    public const int MinWarningTemperatureC = 60;
    public const int MaxWarningTemperatureC = 105;

    /// <summary>True when a reading at or above a threshold should warn.</summary>
    [JsonIgnore]
    public bool WarnsAboutHeat => OverheatWarningEnabled && CpuWarningTemperatureC > 0;

    /// <summary>
    /// The person has been told the vendor's Control Center is installed
    /// alongside this, and has chosen to keep it.
    ///
    /// Recorded so the recommendation is made once rather than every time the
    /// window opens. It is their machine: the two applications share one
    /// firmware mailbox and will get in each other's way, and saying so once is
    /// the whole of this project's business in the matter.
    /// </summary>
    public bool AcceptedVendorSoftware { get; set; }

    /// <summary>
    /// Whether to watch for the handful of known Windows faults that cost a
    /// laptop heat and battery, and put right the ones that can be put
    /// right (see <see cref="Hardware.WindowsFaults"/>). On by default: the
    /// one thing it does is restart a component Windows starts again by
    /// itself, and it says so afterwards. Off, it watches nothing.
    /// </summary>
    public bool CompensateWindowsFaults { get; set; } = true;

    /// <summary>Watches and restarts TextInputHost when stuck spinning on a core unbroken.</summary>
    public bool FixTextInputHost { get; set; } = true;

    /// <summary>Watches and restarts CrossDeviceService (Phone Link) when stuck in a background CPU loop. Off by default until observed on hardware.</summary>
    public bool FixCrossDeviceService { get; set; } = false;

    /// <summary>Watches and restarts Widgets.exe when stuck spinning in background with no window. Off by default until observed on hardware.</summary>
    public bool FixWidgets { get; set; } = false;

    /// <summary>Watches discrete GPU staying awake at full clocks with no display.</summary>
    public bool WatchGpuAwake { get; set; } = true;

    /// <summary>Safely trims bloated DWM memory via Windows working set trim under memory pressure.</summary>
    public bool TrimDwmMemory { get; set; } = true;

    /// <summary>Safely trims bloated Explorer thumbnail/COM cache under memory pressure.</summary>
    public bool TrimExplorerMemory { get; set; } = true;

    /// <summary>Disables Windows Network Data Usage (NDU) driver to prevent gigabyte-scale non-paged pool RAM leak.</summary>
    public bool DisableNdu { get; set; } = false;

    /// <summary>The original Ndu Start value before Nextcalibur modified it, or null if untouched.</summary>
    public int? OriginalNduStart { get; set; }

    /// <summary>Dark, light, or follow Windows.</summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ThemePreference Theme { get; set; } = ThemePreference.System;

    /// <summary>
    /// Details of a pending restart across the application, or null when none is pending.
    /// </summary>
    public PendingRestartInfo? PendingRestart { get; set; }

    /// <summary>True when a restart is currently pending for this boot session.</summary>
    [JsonIgnore]
    public bool HasPendingRestart => PendingRestart is not null && PendingRestart.Reasons.Count > 0
        && IsRestartPending(PendingRestart.BootTimeUtc, CurrentBootTimeUtc());

    /// <summary>
    /// Computes the machine's boot time in UTC, rounded to the minute.
    /// </summary>
    public static DateTime CurrentBootTimeUtc()
    {
        var boot = DateTime.UtcNow - TimeSpan.FromMilliseconds(Environment.TickCount64);
        return new DateTime(boot.Year, boot.Month, boot.Day, boot.Hour, boot.Minute, 0, DateTimeKind.Utc);
    }

    /// <summary>
    /// Checks whether a stored boot time belongs to the current boot session.
    /// Returns false if storedBootUtc is null, earlier than current boot, or significantly different.
    /// </summary>
    public static bool IsRestartPending(DateTime? storedBootUtc, DateTime currentBootUtc)
    {
        if (storedBootUtc is null) return false;
        return Math.Abs((currentBootUtc - storedBootUtc.Value).TotalMinutes) <= 2.0;
    }

    /// <summary>Records a reason requiring a restart, binding it to the current boot time.</summary>
    public void RequestRestart(string reasonId, string? argument = null)
    {
        var currentBoot = CurrentBootTimeUtc();
        if (PendingRestart is null || !IsRestartPending(PendingRestart.BootTimeUtc, currentBoot))
        {
            PendingRestart = new PendingRestartInfo
            {
                BootTimeUtc = currentBoot,
            };
        }

        if (!PendingRestart.Reasons.Contains(reasonId, StringComparer.OrdinalIgnoreCase))
            PendingRestart.Reasons.Add(reasonId);

        if (argument is not null)
            PendingRestart.ReasonArguments[reasonId] = argument;
    }

    /// <summary>Clears a specific reason (e.g. if a setting was toggled back off before rebooting).</summary>
    public void ClearRestartReason(string reasonId)
    {
        if (PendingRestart is null) return;
        PendingRestart.Reasons.RemoveAll(r => string.Equals(r, reasonId, StringComparison.OrdinalIgnoreCase));
        PendingRestart.ReasonArguments.Remove(reasonId);
        if (PendingRestart.Reasons.Count == 0)
            PendingRestart = null;
    }

    /// <summary>Clears all pending restart reasons.</summary>
    public void ClearAllRestartReasons()
    {
        PendingRestart = null;
    }

    private static string Path => System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Nextcalibur", "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(Path))
                return Migrate(JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(Path)));
        }
        catch
        {
            // A corrupt or unreadable settings file is not worth failing startup over.
        }
        return new AppSettings();
    }

    /// <summary>
    /// Brings a settings file written by an older version up to date.
    ///
    /// Before <see cref="OverheatWarningEnabled"/> existed, a zero threshold was
    /// how the warning was switched off. Reading that as "warn at 0 °C" would
    /// turn a deliberate silence into a notification on every reading, so the
    /// old meaning is honoured and the threshold restored to its default - which
    /// is what the person gets back when they switch the warning on again.
    /// </summary>
    internal static AppSettings Migrate(AppSettings? loaded)
    {
        var settings = loaded ?? new AppSettings();

        if (settings.CpuWarningTemperatureC <= 0)
        {
            settings.OverheatWarningEnabled = false;
            settings.CpuWarningTemperatureC = new AppSettings().CpuWarningTemperatureC;
        }

        settings.CpuWarningTemperatureC = Math.Clamp(settings.CpuWarningTemperatureC, MinWarningTemperatureC, MaxWarningTemperatureC);
        settings.GpuWarningTemperatureC = Math.Clamp(settings.GpuWarningTemperatureC, MinWarningTemperatureC, MaxWarningTemperatureC);
        // A file edited by hand, or by something else: an interval of zero
        // would spin, a negative one would throw at the timer.
        if (settings.PollIntervalMs is < 500 or > 60000) settings.PollIntervalMs = new AppSettings().PollIntervalMs;

        // Written into a driver's key by an elevated process: only what that
        // key may hold. See NduFix.Valid.
        settings.OriginalNduStart = Hardware.NduFix.Valid(settings.OriginalNduStart);

        if (settings.PendingRestart is not null)
        {
            settings.PendingRestart.Reasons ??= new();
            settings.PendingRestart.ReasonArguments ??= new(StringComparer.OrdinalIgnoreCase);
            if (!IsRestartPending(settings.PendingRestart.BootTimeUtc, CurrentBootTimeUtc())
                || settings.PendingRestart.Reasons.Count == 0)
            {
                settings.PendingRestart = null;
            }
        }

        return settings;
    }

    private static readonly object SaveLock = new();

    public void Save()
    {
        lock (SaveLock)
        {
            try
            {
                var dir = System.IO.Path.GetDirectoryName(Path)!;
                // Not through a link: this process is elevated and the folder is the account's.
                if (!Security.ProfileFiles.EnsureOrdinaryFolder(dir)) return;
                // Written beside and moved into place, so a write cut short - the
                // battery giving out, a forced power-off - leaves the previous file
                // whole rather than a truncated one that loads as defaults.
                var temporary = Path + ".tmp";
                if (!Security.ProfileFiles.IsOrdinaryFileOrAbsent(temporary)) return;
                File.WriteAllText(temporary, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
                File.Move(temporary, Path, overwrite: true);
            }
            catch
            {
                // Losing preferences is preferable to crashing on shutdown.
            }
        }
    }
}

/// <summary>
/// Start with Windows. Since the application runs elevated (12 September
/// 2026), this is the logon task in <see cref="Elevation"/> rather than a
/// <c>Run</c> entry: Windows will not start an elevated program from
/// <c>Run</c>. The old entry, from earlier versions, is removed whenever
/// this is set.
/// </summary>
public static class StartupRegistration
{
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string ValueName = "Nextcalibur";

    public static bool IsEnabled =>
        Environment.ProcessPath is { } self && Elevation.StartsWithWindows(self);

    /// <summary>Enables or disables launch at sign-in. Elevated only.</summary>
    /// <returns>False when nothing needed changing.</returns>
    public static bool Set(bool enabled, string executablePath)
    {
        RemoveOldRunEntry();
        return Elevation.SetStartWithWindows(enabled, executablePath);
    }

    /// <summary>
    /// A copy updated from a version that started through the Run entry:
    /// that entry cannot start an elevated program, so the choice it
    /// recorded is carried over to the logon task and the entry removed.
    /// Nothing happens when there is no entry.
    /// </summary>
    public static void MigrateRunEntry(string executablePath)
    {
        try
        {
            using var run = Registry.CurrentUser.OpenSubKey(RunKey);
            // The value is the account's to write, and this turns into a
            // task that starts something at logon. So it is migrated only
            // when it is what an earlier version of this application left:
            // a command naming this executable. Anything else is somebody
            // else's entry that happens to share the name, and is left
            // alone rather than acted on.
            if (run?.GetValue(ValueName) is not string command) return;
            if (!command.Contains(System.IO.Path.GetFileName(executablePath), StringComparison.OrdinalIgnoreCase)) return;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            return;
        }
        Set(true, executablePath);
    }

    private static void RemoveOldRunEntry()
    {
        try
        {
            using var run = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            run?.DeleteValue(ValueName, throwOnMissingValue: false);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
        }
    }
}

/// <summary>The two languages the interface speaks.</summary>
public enum UiLanguage
{
    English,
    Turkish,
}

/// <summary>
/// Details of a pending restart: when it was requested and for what reasons.
/// </summary>
public sealed class PendingRestartInfo
{
    /// <summary>The UTC boot time of the machine when this restart was requested.</summary>
    public DateTime BootTimeUtc { get; set; }

    /// <summary>List of reason identifiers: "ndu", "installer", "pawnio", "gpu".</summary>
    public List<string> Reasons { get; set; } = new();

    /// <summary>Optional arguments for reasons, e.g. installer dependency name or gpu target mode.</summary>
    public Dictionary<string, string> ReasonArguments { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}
