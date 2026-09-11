using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Velopack;

namespace Nextcalibur.App;

public partial class App : Application
{
    /// <summary>
    /// Names for the single-instance handshake. Both are per-session rather than
    /// global: two people signed in at once should each get their own window,
    /// not fight over one.
    /// </summary>
    private const string InstanceMutexName = "Nextcalibur.SingleInstance";
    private const string ShowWindowEventName = "Nextcalibur.ShowWindow";

    private static Mutex? _instanceLock;

    /// <summary>
    /// Asks this process, elevated, to grant the current account access to the
    /// firmware mailbox. Not a mode anybody runs by hand: the window relaunches
    /// itself with this when it finds it cannot read the machine.
    /// </summary>
    public const string GrantAccessArgument = "--grant-sensor-access";

    /// <summary>
    /// Asks this process, elevated, to undo the two machine-wide changes.
    /// Reached only from the uninstall hook, after the person has said yes.
    /// </summary>
    public const string CleanUpArgument = "--remove-system-changes";

    [STAThread]
    public static void Main(string[] args)
    {
        // Before anything else, including Velopack: this is a short-lived
        // elevated copy of the application doing one registry write and exiting.
        // It must not run installer hooks, take the single-instance mutex, or
        // put a second icon in the notification area.
        if (args.Contains(GrantAccessArgument))
        {
            Environment.Exit(GrantSensorAccess());
            return;
        }

        if (args.Contains(CleanUpArgument))
        {
            Footprint.RemoveMachineTraces();
            Environment.Exit(0);
            return;
        }

        // Run by the scheduled task, already elevated: switch the card and go.
        // No window, no mutex, no tray icon - this copy lives for a second.
        var cardIndex = Array.IndexOf(args, CardSwitchTasks.Argument);
        if (cardIndex >= 0 && cardIndex + 1 < args.Length)
        {
            var enable = string.Equals(args[cardIndex + 1], "on", StringComparison.OrdinalIgnoreCase);
            Environment.Exit(GpuModeService.RunPnputil(enable) ? 0 : 1);
            return;
        }

        // Velopack takes over the process during install, update and uninstall
        // hooks, so this must run before any UI is created.
        VelopackApp.Build()
            .OnFirstRun(_ => { StartWithWindowsOnFirstRun(); RepairPowerOverlayOnFirstRun(); })
            .OnBeforeUninstallFastCallback(_ => CleanUpOnUninstall())
            .Run();

        // One copy at a time. Two would drive the same firmware mailbox and put
        // two icons in the notification area, and the mailbox holds one command
        // at a time — a write from one can land between another's, which is the
        // failure the lighting code takes a lock to avoid within a process.
        _instanceLock = new Mutex(initiallyOwned: true, InstanceMutexName, out var isFirst);
        if (!isFirst)
        {
            // Someone asked for the application while it was already running,
            // most likely from the Start menu with the window in the tray. Ask
            // the copy that is running to show itself, rather than dying
            // silently and looking like nothing happened.
            WakeRunningInstance();
            return;
        }

        var app = new App();
        app.InitializeComponent();
        ListenForWakeRequests(app);
        app.Run();

        _instanceLock.ReleaseMutex();
    }

    /// <summary>Signals the running copy to bring its window forward.</summary>
    private static void WakeRunningInstance()
    {
        try
        {
            using var wake = EventWaitHandle.OpenExisting(ShowWindowEventName);
            wake.Set();
        }
        catch (WaitHandleCannotBeOpenedException)
        {
            // The other copy is starting or stopping and has no handle yet.
            // Nothing useful to do; it is about to have a window of its own.
        }
        catch (UnauthorizedAccessException)
        {
            // A copy running as another user. Not ours to talk to.
        }
    }

    /// <summary>
    /// Waits for another copy to ask us to show ourselves.
    ///
    /// A background thread rather than a timer: it costs nothing while nothing
    /// happens, which is the normal case for the whole life of the process.
    /// </summary>
    private static void ListenForWakeRequests(Application app)
    {
        var wake = new EventWaitHandle(false, EventResetMode.AutoReset, ShowWindowEventName);

        var listener = new Thread(() =>
        {
            while (true)
            {
                wake.WaitOne();
                app.Dispatcher.Invoke(() =>
                {
                    if (app.MainWindow is not { } window) return;
                    window.Show();
                    window.WindowState = WindowState.Normal;
                    window.Activate();
                });
            }
        })
        {
            IsBackground = true,
            Name = "Nextcalibur wake listener",
        };

        listener.Start();
    }

    /// <summary>
    /// Takes Nextcalibur's changes back off the machine as it is uninstalled.
    ///
    /// Written against what the vendor's own uninstaller does. That one is
    /// thorough - files, driver, service, scheduled task, power plans, its
    /// registry keys, all gone - and then leaves a single permission behind on
    /// the machine for ever. Weeks later that leftover is what silently stopped
    /// every reading here.
    ///
    /// So: leave nothing. But ask first about anything somebody might want to
    /// keep, because the power-mode repair is a fix to Windows rather than a
    /// part of this application, and quietly undoing it on the way out would
    /// leave a machine worse than it was found.
    ///
    /// Velopack kills this callback after thirty seconds. Everything that needs
    /// no permission is therefore done first and without asking, and the
    /// question that remains has a safe default: no answer means keep, because
    /// an unanswered dialogue must not undo a repair.
    /// </summary>
    private static void CleanUpOnUninstall()
    {
        Footprint.RemoveUserTraces();

        // Everything still standing that is either not ours to assume about, or
        // not ours to remove without a prompt from Windows.
        var remaining = Footprint.Survey()
            .Where(t => t.Present && (t.NeedsElevation || t.KeepingIsReasonable))
            .ToList();
        if (remaining.Count == 0) return;

        var keepable = remaining.Where(t => t.KeepingIsReasonable).Select(t => t.Name).ToList();
        var body =
            "Nextcalibur has removed its own files and settings." +
            Environment.NewLine + Environment.NewLine +
            "These changes to Windows are still in place:" +
            Environment.NewLine +
            string.Join(Environment.NewLine, remaining.Select(t => "    - " + t.Name)) +
            Environment.NewLine + Environment.NewLine +
            (keepable.Count > 0
                ? $"You may want to keep {string.Join(" and ", keepable)}. The power repair fixes a " +
                  "Windows fault and works whether or not Nextcalibur is installed, and the plans are " +
                  "yours to keep using." + Environment.NewLine + Environment.NewLine
                : string.Empty) +
            "Remove them as well? Windows will ask you to confirm.";

        if (!Dialogs.Ask("Nextcalibur - anything else to remove?", body, defaultNo: true)) return;

        // Power plans need no privileges, so they go first and go regardless of
        // whether the prompt below is accepted.
        Footprint.RemoveOwnPowerPlans();

        if (!remaining.Any(t => t.NeedsElevation)) return;

        if (MailboxAccess.IsElevated())
        {
            Footprint.RemoveMachineTraces();
            return;
        }

        try
        {
            var self = Environment.ProcessPath;
            if (self is null) return;

            using var elevated = Process.Start(new ProcessStartInfo
            {
                FileName = self,
                Arguments = CleanUpArgument,
                UseShellExecute = true,
                Verb = "runas",
            });

            // Bounded, because the callback is killed at thirty seconds and a
            // half-finished uninstall is worse than an unanswered prompt.
            elevated?.WaitForExit(15000);
        }
        catch (Win32Exception)
        {
            // The prompt was dismissed. Nothing was undone, which is the safe
            // outcome of the two.
        }
    }

    /// <summary>
    /// Grants access to the firmware mailbox. Runs in the elevated copy.
    /// </summary>
    /// <returns>Zero on success, non-zero for the caller to notice.</returns>
    private static int GrantSensorAccess()
    {
        try
        {
            MailboxAccess.Grant();

            // The one elevation this application ever asks for, so everything
            // that needs elevation is done now. The card-switch tasks are what
            // let UMA and back happen later without a prompt each time.
            if (Environment.ProcessPath is { } self)
                CardSwitchTasks.Register(self);

            return 0;
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or System.Security.SecurityException)
        {
            return 2;
        }
        catch
        {
            return 3;
        }
    }

    /// <summary>
    /// Starts with Windows from the first install onwards, as the vendor's
    /// software does through its logon task. A control centre that is not
    /// running cannot warn about heat or answer the tray. The choice stays
    /// the person's: the tray menu's "Start with Windows" turns it off, and
    /// once they have touched it this is never set again.
    /// </summary>
    private static void StartWithWindowsOnFirstRun()
    {
        try
        {
            if (Environment.ProcessPath is { } self)
                StartupRegistration.Set(true, self);
        }
        catch
        {
            // A Run entry that could not be written is a setting, not a fault;
            // the tray menu shows it unchecked and the person can retry there.
        }
    }

    /// <summary>
    /// Repairs the stuck power-overlay fault the first time the app runs after
    /// installation.
    ///
    /// This is the one fault worth fixing without being asked: on an affected
    /// machine the CPU is pinned at maximum frequency at idle, and nothing in
    /// the vendor software can change it. The repair is reversible and is
    /// reported to the user rather than done silently.
    /// </summary>
    private static void RepairPowerOverlayOnFirstRun()
    {
        try
        {
            var service = new PowerOverlayService();
            if (!service.Diagnose().NeedsRepair) return;

            var outcome = service.Repair();
            if (outcome.Empty) return;

            // Half a repair is still a repair, and saying so is the difference
            // between somebody knowing what state their machine is in and not.
            var body = outcome.Done.Count > 0
                ? "Nextcalibur found and repaired a Windows power configuration fault:" +
                  Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine + Environment.NewLine, outcome.Done)
                : "Nextcalibur found a Windows power configuration fault.";

            if (outcome.Blocked is { } blocked)
                body += Environment.NewLine + Environment.NewLine + blocked;
            else
                body += Environment.NewLine + Environment.NewLine +
                        "Your CPU can now idle down properly. You can undo this at any time " +
                        "from the Power Mode panel.";

            Dialogs.Tell(
                outcome.Blocked is null
                    ? "Nextcalibur - power fault repaired"
                    : "Nextcalibur - power fault partly repaired",
                body);
        }
        catch (Exception ex)
        {
            // Never let a failed repair block startup.
            Dialogs.Warn("Nextcalibur",
                "Nextcalibur could not repair the Windows power overlay automatically:" +
                Environment.NewLine + Environment.NewLine + ex.Message +
                Environment.NewLine + Environment.NewLine + "You can retry from the Power Mode panel.");
        }
    }
}
