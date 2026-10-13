using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Nextcalibur.App;

/// <summary>
/// Windows' screen saver: on or off and after how long. One value for the
/// account - Windows keeps no plugged-in and battery halves for it - read
/// and written the way the Control Panel's own dialogue does.
/// </summary>
internal static class ScreenSaver
{
    private const uint GetActive = 0x10, SetActive = 0x11, GetTimeout = 0x0E, SetTimeout = 0x0F;
    private const uint UpdateIniFile = 0x01, SendChange = 0x02;
    private const string DesktopKey = @"Control Panel\Desktop";

    /// <summary>The seconds after which it starts; 0 when it is off or none is chosen.</summary>
    public static int Current()
    {
        var active = 0;
        var seconds = 0;
        if (!SystemParametersInfo(GetActive, 0, ref active, 0) || active == 0) return 0;
        if (Chosen() is null) return 0;
        return SystemParametersInfo(GetTimeout, 0, ref seconds, 0) ? Math.Max(seconds, 0) : 0;
    }

    /// <summary>
    /// Starts it after <paramref name="seconds"/>, or switches it off for 0.
    /// With none chosen, Windows' blank one is: switched on, it has to show
    /// something, and blank is what "none" looked like before.
    /// </summary>
    public static bool Set(int seconds)
    {
        var flags = UpdateIniFile | SendChange;
        if (seconds <= 0) return SystemParametersInfo(SetActive, 0, IntPtr.Zero, flags);

        if (Chosen() is null)
        {
            var blank = System.IO.Path.Combine(Environment.SystemDirectory, "scrnsave.scr");
            if (!File.Exists(blank)) return false;
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(DesktopKey, writable: true);
                key?.SetValue("SCRNSAVE.EXE", blank, RegistryValueKind.String);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException)
            {
                return false;
            }
        }
        return SystemParametersInfo(SetTimeout, (uint)seconds, IntPtr.Zero, flags)
               && SystemParametersInfo(SetActive, 1, IntPtr.Zero, flags);
    }

    private static string? Chosen()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(DesktopKey);
            return key?.GetValue("SCRNSAVE.EXE") is string path && !string.IsNullOrWhiteSpace(path) ? path : null;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException or IOException)
        {
            return null;
        }
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(uint action, uint param, ref int value, uint flags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(uint action, uint param, IntPtr value, uint flags);
}
