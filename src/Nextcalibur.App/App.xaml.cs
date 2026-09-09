using System.Windows;
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

    [STAThread]
    public static void Main(string[] args)
    {
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

            var actions = service.Repair();
            if (actions.Count == 0) return;

            MessageBox.Show(
                "Nextcalibur found and repaired a Windows power configuration fault:\n\n" +
                string.Join("\n\n", actions) +
                "\n\nYour CPU can now idle down properly. You can undo this at any time " +
                "from the Power Mode panel.",
                "Nextcalibur - power fault repaired",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
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
