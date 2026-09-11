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

    /// <summary>What is currently on this machine because of Nextcalibur.</summary>
    public static IReadOnlyList<Trace> Survey()
    {
        var traces = new List<Trace>
        {
            new("Your settings", SettingsExist(), NeedsElevation: false, KeepingIsReasonable: false),
            new("The start-with-Windows task", StartupRegistration.IsEnabled, NeedsElevation: false, KeepingIsReasonable: false),
            new("Permission to read the sensors",
                MailboxAccess.Check() == MailboxAvailability.Available,
                NeedsElevation: true,
                KeepingIsReasonable: false),
            new("The tasks that switch the graphics card without a prompt",
                CardSwitchTasks.Registered(),
                NeedsElevation: true,
                KeepingIsReasonable: false),
            new("The power plans it created",
                SystemModeService.EnumeratePlans().Keys.Any(n => n.StartsWith("Nextcalibur ", StringComparison.Ordinal)),
                NeedsElevation: false,
                // Ours, but somebody may have tuned them and want them left -
                // and unlike settings they are visible in Windows' own power
                // options, so removing them silently would be a surprise.
                KeepingIsReasonable: true),
            new("The Windows power-mode repair",
                !new PowerOverlayService().Diagnose().GuardMissing,
                NeedsElevation: true,
                // A repair to Windows rather than a part of this application.
                // Undoing it silently would leave somebody worse off than
                // before they ever installed us.
                KeepingIsReasonable: true),
        };

        return traces;
    }

    /// <summary>
    /// Removes what belongs to this application and needs no privileges.
    /// Safe to call at any time, including from an uninstall hook.
    /// </summary>
    public static void RemoveUserTraces()
    {
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
            // The logon task, and the Run entry earlier versions wrote.
            StartupRegistration.Set(false, string.Empty);
        }
        catch (InvalidOperationException)
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
        foreach (var plan in SystemModeService.EnumeratePlans())
        {
            if (!plan.Key.StartsWith("Nextcalibur ", StringComparison.Ordinal)) continue;
            SystemModeService.DeletePlan(plan.Value);
        }
    }

    /// <summary>
    /// Removes the two machine-wide changes. Needs administrator, and is only
    /// ever called after the person has been asked.
    /// </summary>
    public static void RemoveMachineTraces()
    {
        try { MailboxAccess.Revoke(); }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException) { }

        CardSwitchTasks.Unregister();

        try { new PowerOverlayService().RemoveGuard(); }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException) { }
    }

    private static string SettingsPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Nextcalibur", "settings.json");

    private static bool SettingsExist() => File.Exists(SettingsPath);
}
