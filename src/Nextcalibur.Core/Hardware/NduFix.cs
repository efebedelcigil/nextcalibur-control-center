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
    private const string BackupValueName = "NextcaliburOriginalStart";

    /// <summary>Returns true if the NDU driver is currently disabled (Start == 4).</summary>
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

    /// <summary>Returns true if Nextcalibur has modified the NDU driver state (or the backup marker exists).</summary>
    public static bool IsNduModified() => HasBackupMarker();

    /// <summary>Returns true if Nextcalibur's backup marker exists in the registry.</summary>
    public static bool HasBackupMarker()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(KeyPath, false);
            return key?.GetValue(BackupValueName) is not null;
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    /// <summary>Deletes Nextcalibur's backup marker from the registry without altering the Start value.</summary>
    public static bool RemoveBackupMarker()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(KeyPath, true);
            if (key is null) return false;
            key.DeleteValue(BackupValueName, throwOnMissingValue: false);
            Log.Info("registry", "Removed NDU backup marker from registry");
            return true;
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
        {
            Log.Warn("registry", $"Failed to delete NDU backup marker: {ex.Message}");
            return false;
        }
    }

    /// <summary>Reads the current Start value from the registry, or null if unreachable.</summary>
    public static int? ReadCurrentStartValue()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(KeyPath, false);
            return key?.GetValue(ValueName) as int?;
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
        {
            return null;
        }
    }

    /// <summary>
    /// Sets NDU Start to 4 (Disabled) or restores the original value.
    /// Preserves original value in settings and registry backup.
    /// </summary>
    public static bool SetNduDisabled(bool disable, AppSettings? settings = null)
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(KeyPath, true);
            if (key is null) return false;

            if (disable)
            {
                var currentVal = key.GetValue(ValueName) as int? ?? 2;
                var backup = key.GetValue(BackupValueName) as int?;
                var original = backup ?? settings?.OriginalNduStart ?? currentVal;

                if (backup is null)
                {
                    key.SetValue(BackupValueName, original, RegistryValueKind.DWord);
                }

                if (settings is not null && settings.OriginalNduStart is null)
                {
                    settings.OriginalNduStart = original;
                }

                key.SetValue(ValueName, 4, RegistryValueKind.DWord);
                Log.Info("registry", $"NDU Start value set to 4 (Disabled), recorded original: {original}");
            }
            else
            {
                var original = (key.GetValue(BackupValueName) as int?)
                    ?? settings?.OriginalNduStart
                    ?? 2;

                key.SetValue(ValueName, original, RegistryValueKind.DWord);
                key.DeleteValue(BackupValueName, throwOnMissingValue: false);

                if (settings is not null)
                {
                    settings.OriginalNduStart = null;
                }

                Log.Info("registry", $"NDU Start value restored to original ({original})");
            }

            return true;
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
        {
            Log.Warn("registry", $"Failed to update NDU Start value: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Restores the original NDU Start value and removes the backup value.
    ///
    /// Only what we recorded. With no backup there is nothing of ours to undo:
    /// NDU disabled without a marker was disabled by somebody else, and the
    /// guess this used to fall back on - 2, Automatic - turned it back on at
    /// uninstall. The marker is read here, so it must still be there: delete
    /// it after this, never before.
    /// </summary>
    public static bool RestoreOriginal(int? fromSettings = null)
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(KeyPath, true);
            if (key is null) return false;

            var original = fromSettings ?? (key.GetValue(BackupValueName) as int?);
            if (original is null) return false;

            key.SetValue(ValueName, original, RegistryValueKind.DWord);
            key.DeleteValue(BackupValueName, throwOnMissingValue: false);
            Log.Info("registry", $"NDU Start value restored to original ({original}) on uninstall/cleanup");
            return true;
        }
        catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
        {
            Log.Warn("registry", $"Failed to restore NDU Start value: {ex.Message}");
            return false;
        }
    }
}
