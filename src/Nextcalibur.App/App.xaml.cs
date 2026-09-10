using System.Windows;
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

        // Velopack takes over the process during install, update and uninstall
        // hooks, so this must run before any UI is created.
        VelopackApp.Build()
            .OnFirstRun(_ => RepairPowerOverlayOnFirstRun())
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
    /// Grants access to the firmware mailbox. Runs in the elevated copy.
    /// </summary>
    /// <returns>Zero on success, non-zero for the caller to notice.</returns>
    private static int GrantSensorAccess()
    {
        try
        {
            MailboxAccess.Grant();
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

            MessageBox.Show(
                body,
                outcome.Blocked is null
                    ? "Nextcalibur - power fault repaired"
                    : "Nextcalibur - power fault partly repaired",
                MessageBoxButton.OK,
                outcome.Blocked is null ? MessageBoxImage.Information : MessageBoxImage.Warning);
        }
        catch (Exception ex)
        {
            // Never let a failed repair block startup.
            MessageBox.Show(
                "Nextcalibur could not repair the Windows power overlay automatically:\n\n" +
                ex.Message + "\n\nYou can retry from the Power Mode panel.",
                "Nextcalibur",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}
