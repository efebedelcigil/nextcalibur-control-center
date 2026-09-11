using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Win32;

namespace Nextcalibur.Core.Configuration;

/// <summary>User preferences, persisted per user.</summary>
public sealed class AppSettings
{
    /// <summary>How often to sample sensors while the window is visible, in milliseconds.</summary>
    public int PollIntervalMs { get; set; } = 2000;

    /// <summary>Keep running in the notification area when the window is closed.</summary>
    public bool MinimiseToTray { get; set; } = true;

    /// <summary>Start hidden in the notification area.</summary>
    public bool StartMinimised { get; set; }

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

    /// <summary>True when a reading at or above the threshold should warn.</summary>
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

    /// <summary>Dark, light, or follow Windows.</summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ThemePreference Theme { get; set; } = ThemePreference.System;

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

        return settings;
    }

    public void Save()
    {
        try
        {
            var dir = System.IO.Path.GetDirectoryName(Path)!;
            Directory.CreateDirectory(dir);
            File.WriteAllText(Path, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch
        {
            // Losing preferences is preferable to crashing on shutdown.
        }
    }
}

/// <summary>Registers the application to start with Windows, for the current user.</summary>
public static class StartupRegistration
{
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string ValueName = "Nextcalibur";

    public static bool IsEnabled
    {
        get
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey);
            return key?.GetValue(ValueName) is not null;
        }
    }

    /// <summary>
    /// Enables or disables launch at sign-in. Uses HKCU so no elevation is
    /// needed and the setting stays scoped to this user.
    /// </summary>
    /// <returns>False when the registry already said what we were about to say.</returns>
    public static bool Set(bool enabled, string executablePath)
    {
        var wanted = enabled ? $"\"{executablePath}\" --tray" : null;

        using var read = Registry.CurrentUser.OpenSubKey(RunKey);
        var current = read?.GetValue(ValueName) as string;
        if (current == wanted) return false;

        using var key = Registry.CurrentUser.CreateSubKey(RunKey, writable: true)
            ?? throw new InvalidOperationException("Could not open the Run key.");

        if (wanted is not null)
            key.SetValue(ValueName, wanted, RegistryValueKind.String);
        else
            key.DeleteValue(ValueName, throwOnMissingValue: false);

        return true;
    }
}
