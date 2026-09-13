using System.Security;
using Microsoft.Win32;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Manages the Windows Network Data Usage (NDU) driver state in the registry
/// to prevent the well-known non-paged pool kernel memory leak on network adapters.
/// </summary>
public static class NduFix
{
    private const string KeyPath = @"SYSTEM\CurrentControlSet\Services\Ndu";
    private const string ValueName = "Start";

    /// <summary>Returns true if the NDU driver is disabled (Start == 4).</summary>
    public static bool IsNduDisabled()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(KeyPath, false);
            return key?.GetValue(ValueName) is int val && val == 4;
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    /// <summary>Sets NDU Start to 4 (Disabled) or 2 (Automatic/Enabled).</summary>
    public static bool SetNduDisabled(bool disable)
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(KeyPath, true);
            if (key is null) return false;
            key.SetValue(ValueName, disable ? 4 : 2, RegistryValueKind.DWord);
            Log.Info("registry", $"NDU Start value set to {(disable ? 4 : 2)}");
            return true;
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
        {
            Log.Warn("registry", $"Failed to update NDU Start value: {ex.Message}");
            return false;
        }
    }
}
