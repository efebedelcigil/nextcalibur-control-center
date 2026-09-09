using System.Text.Json;
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

    /// <summary>Warn when the CPU exceeds this temperature, in °C. Zero disables.</summary>
    public int CpuWarningTemperatureC { get; set; } = 90;

    private static string Path => System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Nextcalibur", "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(Path))
                return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(Path)) ?? new AppSettings();
        }
        catch
        {
            // A corrupt or unreadable settings file is not worth failing startup over.
        }
        return new AppSettings();
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
    public static void Set(bool enabled, string executablePath)
    {
        using var key = Registry.CurrentUser.CreateSubKey(RunKey, writable: true)
            ?? throw new InvalidOperationException("Could not open the Run key.");

        if (enabled)
            key.SetValue(ValueName, $"\"{executablePath}\" --tray", RegistryValueKind.String);
        else
            key.DeleteValue(ValueName, throwOnMissingValue: false);
    }
}
