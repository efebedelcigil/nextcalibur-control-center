using Microsoft.Win32;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.Core.Configuration;

/// <summary>One thing this application changed on the machine.</summary>
/// <param name="Name">What to call it when asking about it.</param>
/// <param name="Present">It is currently there.</param>
/// <param name="NeedsElevation">Undoing it needs administrator.</param>
/// <param name="KeepingIsReasonable">
/// Somebody might want this left behind after uninstalling. True for repairs,
/// false for things that only exist to serve this application.
/// </param>
public readonly record struct Trace(
    string Name,
    bool Present,
    bool NeedsElevation,
    bool KeepingIsReasonable);

/// <summary>
/// A trace left by a past or current feature outside the application's own folder,
/// and how to detect and remove it once the feature is dropped.
/// </summary>
/// <param name="Description">What it is, in one line.</param>
/// <param name="CreatedIn">Which version introduced it.</param>
/// <param name="RetiredIn">
/// The version that dropped the feature, or null if the feature is still active.
/// The pass acts on an entry only when the running version is at or past it.
/// </param>
/// <param name="Detect">Returns true if the trace is currently present on the machine.</param>
/// <param name="Remove">Removes the trace.</param>
/// <param name="NeedsAsking">
/// True for fixes to Windows that a person may want to keep; false for things that only served this application.
/// </param>
public sealed record RetirementEntry(
    string Description,
    Version CreatedIn,
    Version? RetiredIn,
    Func<bool> Detect,
    Func<bool> Remove,
    bool NeedsAsking);

/// <summary>
/// Everything Nextcalibur leaves on a machine, and how to take it back off.
///
/// Written after watching what the vendor's software leaves behind: its
/// uninstaller is thorough about files, services, tasks and its own registry
/// keys, and then leaves one permission on the machine for ever. That single
/// leftover is what silently broke every reading here weeks later.
///
/// So the rule for this project is stricter, and it has two halves. Leave
/// nothing behind - and **ask before undoing anything somebody might want to
/// keep**. The power-overlay guard is the case in point: it is a repair to
/// Windows, not part of this application, and removing it on the way out puts
/// the machine back where its processor can be pinned at full speed by a
/// setting nobody chose.
/// </summary>
public static class Footprint
{
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string RunValue = "Nextcalibur";

    /// <summary>
    /// The retirement list. Anything this application writes outside its own folder
    /// must be listed here before its code can be removed.
    ///
    /// Entries that have NeedsAsking = true are guarded: during startup (where askUser
    /// is null), they are not removed without explicit permission. The asking happens
    /// during uninstallation in RemoveMachineTraces(bool removeRepair), where the user
    /// is asked whether to keep Windows repairs and that decision is passed to RetireOldVersions.
    /// </summary>
    public static IReadOnlyList<RetirementEntry> Retirements(string? executablePath = null)
    {
        var self = executablePath ?? Environment.ProcessPath;
        return new List<RetirementEntry>
        {
            new(
                Description: "The card-switch scheduled tasks of the unelevated versions",
                CreatedIn: new Version(0, 5, 0),
                RetiredIn: new Version(0, 5, 4),
                Detect: () => CardSwitchTasks.Registered(),
                Remove: () =>
                {
                    try { CardSwitchTasks.Unregister(); return true; }
                    catch (Exception ex) when (ex is not OutOfMemoryException) { return false; }
                },
                NeedsAsking: false),

            new(
                Description: "The widened firmware mailbox permission granted to this account",
                CreatedIn: new Version(0, 5, 0),
                RetiredIn: new Version(0, 5, 4),
                Detect: () =>
                {
                    try { return MailboxAccess.WidenedForCurrentAccount(); }
                    catch { return false; }
                },
                Remove: () =>
                {
                    try { MailboxAccess.Revoke(); return true; }
                    catch (Exception ex) when (ex is not OutOfMemoryException) { return false; }
                },
                NeedsAsking: false),

            new(
                Description: "The HKCU Run registry value migrated to the logon task",
                CreatedIn: new Version(0, 5, 0),
                RetiredIn: new Version(0, 5, 4),
                Detect: () =>
                {
                    try
                    {
                        using var run = Registry.CurrentUser.OpenSubKey(RunKey);
                        if (run?.GetValue(RunValue) is not string command) return false;
                        return self is null || command.Contains(Path.GetFileName(self), StringComparison.OrdinalIgnoreCase);
                    }
                    catch { return false; }
                },
                Remove: () =>
                {
                    if (self is not null)
                    {
                        StartupRegistration.MigrateRunEntry(self);
                        return true;
                    }
                    else
                    {
                        try
                        {
                            using var run = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
                            if (run?.GetValue(RunValue) is not null)
                            {
                                run.DeleteValue(RunValue, throwOnMissingValue: false);
                                return true;
                            }
                            return false;
                        }
                        catch { return false; }
                    }
                },
                NeedsAsking: false),

            new(
                Description: "The no-prompt scheduled tasks belonging to a copy not under Program Files",
                CreatedIn: new Version(0, 5, 0),
                RetiredIn: new Version(0, 5, 4),
                Detect: () =>
                {
                    if (self is null || Elevation.MayStartWithoutPrompt(self)) return false;
                    return Elevation.StartsWithWindows(self) || Elevation.OpenTaskPointsAt(self);
                },
                Remove: () =>
                {
                    if (self is not null)
                    {
                        return Elevation.RetireTasksNotAllowed(self);
                    }
                    return false;
                },
                NeedsAsking: false),

            new(
                Description: "The NDU network driver fix and backup registry value",
                CreatedIn: new Version(0, 5, 4),
                RetiredIn: null, // Still active in 0.5.4; listed now so retiring the feature later only needs filling in a version.
                Detect: () =>
                {
                    try { return NduFix.IsNduDisabled() || NduFix.HasBackupMarker(); }
                    catch { return false; }
                },
                Remove: () =>
                {
                    var acted = false;
                    try { if (NduFix.HasBackupMarker()) { NduFix.RemoveBackupMarker(); acted = true; } }
                    catch (Exception ex) when (ex is not OutOfMemoryException) { }

                    try { if (NduFix.IsNduDisabled()) { NduFix.RestoreOriginal(); acted = true; } }
                    catch (Exception ex) when (ex is not OutOfMemoryException) { }
                    return acted;
                },
                NeedsAsking: true),
        };
    }

    /// <summary>
    /// Checks the retirement list and cleans up traces of features that have been dropped
    /// in this or earlier versions. Called once at startup when elevated.
    /// Safe to run multiple times.
    /// </summary>
    public static IReadOnlyList<RetirementEntry> RetireOldVersions(
        Version? currentVersion = null,
        string? executablePath = null,
        Func<RetirementEntry, bool>? askUser = null,
        IEnumerable<RetirementEntry>? entries = null)
    {
        var running = currentVersion ?? typeof(Footprint).Assembly.GetName().Version ?? new Version(0, 5, 4);
        var removed = new List<RetirementEntry>();

        foreach (var entry in entries ?? Retirements(executablePath))
        {
            if (entry.RetiredIn is null || running < entry.RetiredIn)
                continue;

            try
            {
                if (!entry.Detect())
                    continue;

                if (entry.NeedsAsking && (askUser is null || !askUser(entry)))
                    continue;

                if (!entry.Remove())
                    continue;

                removed.Add(entry);
                Log.Info("retire", $"Retired old trace ({entry.CreatedIn} -> {entry.RetiredIn}): {entry.Description}");
            }
            catch (Exception ex) when (ex is not OutOfMemoryException)
            {
                Log.Warn("retire", $"Could not retire trace '{entry.Description}': {ex.Message}");
            }
        }

        return removed;
    }

    /// <summary>What is currently on this machine because of Nextcalibur.</summary>
    public static IReadOnlyList<Trace> Survey()
    {
        var traces = new List<Trace>
        {
            new(Words.Get("S.Core.Trace.Settings", "Your settings"), SettingsExist(), NeedsElevation: false, KeepingIsReasonable: false),
            new(Words.Get("S.Core.Trace.StartupTask", "The start-with-Windows task"), StartupRegistration.IsEnabled, NeedsElevation: true, KeepingIsReasonable: false),
            new(Words.Get("S.Core.Trace.OpenTask", "The task that starts Nextcalibur without a prompt"),
                Environment.ProcessPath is { } self && CardSwitchTasks.SchtasksOutput($"/query /tn \"{Elevation.OpenTask}\"") is not null,
                NeedsElevation: true,
                KeepingIsReasonable: false),
            new(Words.Get("S.Core.Trace.Permission", "Permission to read the sensors"),
                MailboxAccess.Check() == MailboxAvailability.Available,
                NeedsElevation: true,
                KeepingIsReasonable: false),
            new(Words.Get("S.Core.Trace.CardTasks", "The tasks that switch the graphics card without a prompt"),
                CardSwitchTasks.Registered(),
                NeedsElevation: true,
                KeepingIsReasonable: false),
            new(Words.Get("S.Core.Trace.FolderGuard", "The install folder's protection against the account"),
                Environment.ProcessPath is { } exe && InstallFolderGuard.RootOf(exe) is { } root
                    && (InstallFolderGuard.IsUnderProgramFiles(root) || InstallFolderGuard.IsHardened(root)),
                NeedsElevation: true,
                KeepingIsReasonable: false),
            new(Words.Get("S.Core.Trace.PowerPlans", "The power plans it created"),
                SystemModeService.EnumeratePlans().Keys.Any(n => n.StartsWith("Nextcalibur ", StringComparison.Ordinal)),
                NeedsElevation: false,
                // Ours, but somebody may have tuned them and want them left -
                // and unlike settings they are visible in Windows' own power
                // options, so removing them silently would be a surprise.
                KeepingIsReasonable: true),
            new(Words.Get("S.Core.Trace.Repair", "The Windows power-mode repair"),
                !new PowerOverlayService().Diagnose().GuardMissing,
                NeedsElevation: true,
                // A repair to Windows rather than a part of this application.
                // Undoing it silently would leave somebody worse off than
                // before they ever installed us.
                KeepingIsReasonable: true),
            new(Words.Get("S.Core.Trace.Ndu", "The NDU network driver fix"),
                NduFix.IsNduDisabled(),
                NeedsElevation: true,
                // A fix to Windows network driver rather than part of this application.
                // Asked before undoing so the user can choose to keep it.
                KeepingIsReasonable: true),
            new(Words.Get("S.Core.Trace.NduMarker", "The NDU backup registry value"),
                NduFix.HasBackupMarker(),
                NeedsElevation: true,
                // Our marker in a Windows key. Deleted at uninstall either way.
                KeepingIsReasonable: false),
        };

        return traces;
    }

    /// <summary>
    /// Removes what belongs to this application and needs no privileges.
    /// Safe to call at any time, including from an uninstall hook.
    /// </summary>
    public static void RemoveUserTraces()
    {
        Log.Remove();

        try
        {
            var directory = Path.GetDirectoryName(SettingsPath);
            if (directory is not null && Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // A settings file that will not delete is not worth failing an
            // uninstall over; Windows will not miss the kilobyte.
        }

        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            key?.DeleteValue(RunValue, throwOnMissingValue: false);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
        }
    }

    /// <summary>
    /// Deletes the power plans this application created. Needs no privileges -
    /// creating, renaming and deleting a plan all work as an ordinary user,
    /// verified on hardware - but it is still asked about, because they are
    /// visible in Windows' own power options and somebody may have tuned them.
    /// </summary>
    public static void RemoveOwnPowerPlans()
    {
        // Windows refuses to delete the active plan; step off it first.
        var plans = SystemModeService.EnumeratePlans();
        if (plans.TryGetValue("Balanced", out var balanced)) SystemModeService.TryActivate(balanced);
        foreach (var plan in plans)
        {
            if (!plan.Key.StartsWith("Nextcalibur ", StringComparison.Ordinal)) continue;
            SystemModeService.DeletePlan(plan.Value);
        }
    }

    /// <summary>
    /// Removes the two machine-wide changes. Needs administrator, and is only
    /// ever called after the person has been asked.
    /// </summary>
    public static void RemoveMachineTraces(bool removeRepair = true)
    {
        try { MailboxAccess.Revoke(); }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException) { }

        CardSwitchTasks.Unregister();
        Elevation.RemoveTasks();

        // So the uninstaller, which runs as the account, can delete the files.
        try
        {
            if (Environment.ProcessPath is { } exe && InstallFolderGuard.RootOf(exe) is { } root)
                InstallFolderGuard.Release(root);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or InvalidOperationException or System.Security.SecurityException) { }

        // The marker we leave in Windows' own key is ours and must be deleted either way.
        try { NduFix.RemoveBackupMarker(); }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException) { }

        // Retire old versions with the user's repair-retention decision
        try { RetireOldVersions(askUser: _ => removeRepair); }
        catch (Exception ex) when (ex is not OutOfMemoryException) { }

        if (!removeRepair) return;
        try { new PowerOverlayService().RemoveGuard(); }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException) { }

        try { NduFix.RestoreOriginal(); }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException) { }
    }

    private static string SettingsPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Nextcalibur", "settings.json");

    private static bool SettingsExist() => File.Exists(SettingsPath);
}
