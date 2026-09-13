using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// The single-instance lock, and what to do when it cannot be had.
    ///
    /// The name lives in the session's own namespace, where anything running
    /// as this account can create it first - with a security descriptor that
    /// refuses everyone, if it likes. Believing that blindly would hand any
    /// unprivileged process a way to stop this application from ever
    /// starting. So a lock that cannot be taken is only believed when there
    /// is another copy of this application actually running; otherwise the
    /// name has been squatted and the start goes ahead without it.
    /// </summary>
    private static Mutex? TakeInstanceLock(out bool isFirst)
    {
        try
        {
            var mutex = new Mutex(initiallyOwned: true, InstanceMutexName, out isFirst);
            if (isFirst) return mutex;
            mutex.Dispose();
            if (AnotherCopyIsRunning()) return null;
            Log.Warn("start", "The single-instance name was already taken, and no other copy is running: starting anyway");
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or WaitHandleCannotBeOpenedException or IOException)
        {
            Log.Warn("start", "The single-instance name is held by something that will not share it: " + ex.Message);
            if (AnotherCopyIsRunning()) { isFirst = false; return null; }
        }

        // Squatted, or held by something that is not us. Start anyway.
        isFirst = true;
        return null;
    }

    private static bool AnotherCopyIsRunning()
    {
        try
        {
            var mine = Environment.ProcessId;
            return Process.GetProcessesByName("Nextcalibur").Any(p => p.Id != mine);
        }
        catch (Exception ex) when (ex is InvalidOperationException or Win32Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Asks this process, elevated, to undo the two machine-wide changes.
    /// Reached only from the uninstall hook, after the person has said yes.
    /// </summary>
    public const string CleanUpArgument = "--remove-system-changes";

    /// <summary>With <see cref="CleanUpArgument"/>: take the tasks and the permission, leave the power repair.</summary>
    public const string KeepRepairArgument = "--keep-repair";

    /// <summary>
    /// With <see cref="CleanUpArgument"/>: also uninstall one dependency, named
    /// by its <see cref="Nextcalibur.Core.Dependencies.Dependency.Id"/> in the
    /// next argument. Repeated once per dependency the person agreed to remove.
    /// </summary>
    public const string RemoveDependencyArgument = "--remove-dependency";

    /// <summary>
    /// Velopack runs this executable with one of its own switches for the
    /// install, update and uninstall hooks. Those runs must not elevate:
    /// the uninstall hook in particular was being handed to the scheduled
    /// task, which started an ordinary copy of the application in the
    /// middle of the uninstall and never ran the hook at all.
    /// </summary>
    private static bool IsVelopackHook(string[] args) =>
        args.Any(a => a.StartsWith("--veloapp-", StringComparison.OrdinalIgnoreCase));

    private static void HardenSearchPaths()
    {
        try { Directory.SetCurrentDirectory(AppContext.BaseDirectory); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException) { }
        // LOAD_LIBRARY_SEARCH_DEFAULT_DIRS: the application's folder, the
        // system folder, and folders added with AddDllDirectory - never the
        // current directory, never PATH.
        try { SetDefaultDllDirectories(0x00001000); }
        catch (Exception ex) when (ex is EntryPointNotFoundException or DllNotFoundException) { }
    }

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    private static extern bool SetDefaultDllDirectories(uint flags);

    [STAThread]
    public static void Main(string[] args)
    {
        // This process runs elevated. Two places Windows would otherwise look
        // in for things this process asks for by name are the account's to
        // fill: the current directory (inherited from whatever unelevated
        // thing started this) and PATH. Neither is searched from here on -
        // libraries come from the application's folder and the system's,
        // and the current directory is the application's own.
        HardenSearchPaths();

        // One taskbar identity, claimed before anything can create a window -
        // Velopack's hooks included, since they may show one.
        AppIdentity.Claim();

        // The application runs elevated. An unelevated start hands over to
        // the scheduled task (no prompt) or to a relaunch (one prompt), and
        // exits. Helper modes below are already elevated when they run.
        var self = Environment.ProcessPath;
        if (self is not null && !args.Contains(CleanUpArgument)
            && !args.Contains(CardSwitchTasks.Argument) && !IsVelopackHook(args)
            && !Elevation.EnsureElevated(args, self))
            return;

        // The words, before anything can show one: the uninstall hook and
        // the first-run repair ask their questions before the window exists.
        // Read before the uninstall removes the settings file.
        Strings.Apply(Strings.Initial(AppSettings.Load()));
        Nextcalibur.Core.Words.Resolver = Strings.TryGet;   // the library's words come from the same dictionaries

        // A short-lived elevated copy of the application doing the uninstall's
        // machine-wide work and exiting. It must not run installer hooks, take
        // the single-instance mutex, or put a second icon in the notification area.
        //
        // Elevated only, and not because it needs the rights: this switch
        // deletes the shortcuts, the registry entry and the install folder,
        // and it asks nobody. Anything running as the account could have
        // started the application with it and taken the installation apart.
        if (args.Contains(CleanUpArgument))
        {
            if (!Elevation.IsElevated()) return;
            Footprint.RemoveMachineTraces(removeRepair: !args.Contains(KeepRepairArgument));
            for (var i = 0; i + 1 < args.Length; i++)
            {
                if (args[i] != RemoveDependencyArgument) continue;
                var id = args[i + 1];
                foreach (var dependency in Nextcalibur.Core.Dependencies.DependencyManager.All)
                    if (dependency.Id == id && dependency.InstalledVersion() is not null) dependency.Uninstall();
            }
            FinishUninstall(self);
            Environment.Exit(0);
            return;
        }

        // Run by the scheduled task, already elevated: switch the card and go.
        // No window, no mutex, no tray icon - this copy lives for a second.
        var cardIndex = Array.IndexOf(args, CardSwitchTasks.Argument);
        if (cardIndex >= 0 && cardIndex + 1 < args.Length)
        {
            if (!Elevation.IsElevated()) return;
            var enable = string.Equals(args[cardIndex + 1], "on", StringComparison.OrdinalIgnoreCase);
            Environment.Exit(GpuModeService.RunPnputil(enable) ? 0 : 1);
            return;
        }

        // Velopack takes over the process during install, update and uninstall
        // hooks, so this must run before any UI is created.
        VelopackApp.Build()
            .OnFirstRun(_ => FirstRun())
            .OnBeforeUninstallFastCallback(_ => CleanUpOnUninstall())
            .Run();

        // One copy at a time. Two would drive the same firmware mailbox and put
        // two icons in the notification area, and the mailbox holds one command
        // at a time — a write from one can land between another's, which is the
        // failure the lighting code takes a lock to avoid within a process.
        _instanceLock = TakeInstanceLock(out var isFirst);
        if (!isFirst)
        {
            // Someone asked for the application while it was already running,
            // most likely from the Start menu with the window in the tray. Ask
            // the copy that is running to show itself, rather than dying
            // silently and looking like nothing happened.
            WakeRunningInstance();
            return;
        }

        Log.Start("Nextcalibur", typeof(App).Assembly.GetName().Version?.ToString(3) ?? "?");

        // Elevated now. The on-demand task is what makes the next start
        // prompt-free; registering it is idempotent and costs a schtasks call.
        // Only a copy under Program Files gets one - and a task an earlier
        // version registered for a copy in the profile is taken away here,
        // because it ran a file the account could replace, as administrator,
        // without asking.
        if (self is not null)
        {
            if (Elevation.RetireTasksNotAllowed(self))
                Log.Info("tasks", "Removed the no-prompt tasks: this copy is not under Program Files, so it is prompted at every start until the installer moves it");
            Elevation.RegisterOpenTask(self);
            StartupRegistration.MigrateRunEntry(self);

            // The card-switch tasks of the unelevated versions ran this
            // executable elevated on demand to enable or disable the card.
            // The elevated application does that itself; two tasks that
            // elevate a file in a user-writable folder are not worth keeping.
            if (CardSwitchTasks.Registered())
            {
                CardSwitchTasks.Unregister();
                Log.Info("tasks", "Removed the card-switch tasks of an earlier version; not needed elevated");
            }

            // An installed copy in the profile (the earlier versions' place)
            // is put out of the account's reach; see InstallFolderGuard. Only
            // an installed copy - a development build's output folder must
            // stay writable to the person building it.
            try
            {
                if (InstallFolderGuard.RootOf(self) is { } root
                    && File.Exists(System.IO.Path.Combine(root, "Update.exe"))
                    && InstallFolderGuard.Harden(root))
                    Log.Info("install", $"Protected the install folder against the account: {root}");
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or IOException or InvalidOperationException or System.Security.SecurityException)
            {
                Log.Warn("install", "Could not protect the install folder: " + ex.Message);
            }
        }

        // Versions before the elevated model widened the firmware interface's
        // permission to this account by name, so an unelevated copy could
        // read it. Elevated, it is not needed, and it lets anything running
        // as the account send firmware commands. Taken back, once, here.
        try
        {
            if (MailboxAccess.WidenedForCurrentAccount())
            {
                MailboxAccess.Revoke();
                Log.Info("access", "Took back the firmware permission an earlier version granted to this account; the elevated application does not need it");
            }
        }
        catch (Exception ex) when (ex is InvalidOperationException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            Log.Warn("access", "Could not take back the widened firmware permission: " + ex.Message);
        }

        // The wizard's install: Velopack's Setup ran silently and never
        // launched us, so its first-run hook never fires. The wizard leaves
        // a marker beside the executable instead; that is the first run.
        if (self is not null && FirstRunMarker(self) is { } marker && File.Exists(marker))
        {
            Log.Info("first-run", "Marker from the installer found");
            FirstRun();
        }
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            Log.Error("crash", "Unhandled exception; the process is ending", e.ExceptionObject as Exception);
        TaskScheduler.UnobservedTaskException += (_, e) =>
        {
            Log.Error("task", "Unobserved task exception", e.Exception);
            e.SetObserved();
        };

        var app = new App();
        app.InitializeComponent();
        var faultsThisMinute = 0;
        var faultMinute = DateTime.MinValue;
        app.DispatcherUnhandledException += (_, e) =>
        {
            // On the interface thread a fault is logged and shown, and the
            // application goes on: one bad click must not take the tray
            // icon and the temperature warning with it.
            //
            // But a timer that faults every tick would raise a dialogue every
            // tick and fill the log for as long as the machine is up. So:
            // the dialogue once a minute, the log line for the first few in a
            // minute and one saying how many were dropped.
            var minute = DateTime.UtcNow.Date.AddMinutes((int)DateTime.UtcNow.TimeOfDay.TotalMinutes);
            if (minute != faultMinute) { faultMinute = minute; faultsThisMinute = 0; }
            faultsThisMinute++;

            if (faultsThisMinute <= 5)
                Log.Error("ui", "Unhandled exception on the interface thread", e.Exception);
            else if (faultsThisMinute == 6)
                Log.Error("ui", "More of the same this minute; not logging each");

            if (faultsThisMinute == 1)
                Dialogs.Warn(Strings.Get("S.Fault.Title"),
                    Strings.Get("S.Fault.Body") + Environment.NewLine + Environment.NewLine + e.Exception.Message);
            e.Handled = true;
        };
        ListenForWakeRequests(app);
        app.Run();

        _instanceLock?.ReleaseMutex();
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
        EventWaitHandle wake;
        try
        {
            wake = new EventWaitHandle(false, EventResetMode.AutoReset, ShowWindowEventName);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or WaitHandleCannotBeOpenedException)
        {
            // Same story as the instance lock: the name is the session's and
            // anything as this account can take it. Without it, opening the
            // window from a second start does not work; everything else does.
            Log.Warn("start", "The wake event could not be created: " + ex.Message);
            return;
        }

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
        Log.Info("uninstall", "Uninstall hook running");
        Footprint.RemoveUserTraces();

        var present = Footprint.Survey().Where(t => t.Present).ToList();
        var ours = present.Where(t => !t.KeepingIsReasonable).ToList();       // tasks, permission: always go
        var keepable = present.Where(t => t.KeepingIsReasonable).ToList();    // plans, the repair: asked

        // The owner's rule: the uninstall leaves nothing of ours behind and
        // does not ask about that; it asks only about what somebody may
        // want to keep - the power plans, which show in Windows' own power
        // options, and the repair, which fixes a Windows fault whether or
        // not this is installed.
        var removeKeepable = keepable.Count > 0 && Dialogs.Ask(Strings.Get("S.Uninstall.KeepTitle"),
            Strings.Get("S.Uninstall.KeepBody", string.Join(Environment.NewLine, keepable.Select(t => "    - " + t.Name))),
            defaultNo: true);

        if (removeKeepable) Footprint.RemoveOwnPowerPlans();

        // The dependencies, a question each: a driver another program may be
        // using is not removed on a nod, and there may be several one day.
        var dependenciesToRemove = new List<Nextcalibur.Core.Dependencies.Dependency>();
        foreach (var dependency in Nextcalibur.Core.Dependencies.DependencyManager.All)
        {
            if (dependency.InstalledVersion() is null) continue;
            if (Dialogs.Ask(Strings.Get("S.Uninstall.DependencyTitle", dependency.Name),
                    Strings.Get("S.Uninstall.DependencyBody", dependency.Name, dependency.Purpose),
                    defaultNo: true))
                dependenciesToRemove.Add(dependency);
        }

        var needsElevation = ours.Any(t => t.NeedsElevation) || (removeKeepable && keepable.Any(t => t.NeedsElevation)) || dependenciesToRemove.Count > 0;
        if (!needsElevation) return;

        if (MailboxAccess.IsElevated())
        {
            Footprint.RemoveMachineTraces(removeRepair: removeKeepable);
            foreach (var dependency in dependenciesToRemove) dependency.Uninstall();
            FinishUninstall(Environment.ProcessPath);
            return;
        }

        try
        {
            var self = Environment.ProcessPath;
            if (self is null) return;
            using var elevated = Process.Start(new ProcessStartInfo
            {
                FileName = self,
                Arguments = string.Join(' ', new[]
                    {
                        CleanUpArgument,
                        removeKeepable ? null : KeepRepairArgument,
                    }
                    .Concat(dependenciesToRemove.Select(d => $"{RemoveDependencyArgument} {d.Id}"))
                    .Where(a => a is not null)),
                UseShellExecute = true,
                Verb = "runas",
            });
            elevated?.WaitForExit(15000);
        }
        catch (Win32Exception)
        {
            Log.Warn("uninstall", "Elevation declined; the scheduled tasks and the permission stay");
        }
    }
    /// <summary>
    /// What Velopack's uninstaller leaves when the copy lived under Program
    /// Files: the folder (it runs as the account and cannot delete there)
    /// and, having stopped at that, the shortcuts. This helper is elevated,
    /// so it takes the shortcuts now and leaves a script to take the folder
    /// once the uninstaller - which runs from inside it - has exited. Seen
    /// in the sandbox 12 September 2026: folder, desktop and Start-menu
    /// shortcuts all still there after "uninstall".
    /// </summary>
    private static void FinishUninstall(string? self)
    {
        if (self is null || InstallFolderGuard.RootOf(self) is not { } root) return;
        if (!File.Exists(System.IO.Path.Combine(root, "Update.exe"))) return;   // not an installed copy

        var shortcuts = AppIdentity.RemoveShortcutsPointingAt(root);
        Log.Info("uninstall", $"Removed {shortcuts} shortcut(s) pointing into {root}");

        try
        {
            // Retries for two minutes: the uninstaller and this process are
            // still running from the folder when the script starts.
            // cmd reads a batch file as it runs it, line by line, for the two
            // minutes this one may take - so it lives where only
            // administrators can reach it, under a name nobody can guess.
            string script;
            using (var file = Nextcalibur.Core.Security.SystemTools.CreateProtectedTemporaryFile(".cmd", out script))
            using (var writer = new StreamWriter(file, new System.Text.UTF8Encoding(false)))
                writer.Write(
                "@echo off\r\n" +
                "set n=0\r\n" +
                ":again\r\n" +
                "timeout /t 3 /nobreak >nul\r\n" +
                $"rmdir /s /q \"{root}\" 2>nul\r\n" +
                $"if not exist \"{root}\" goto done\r\n" +
                "set /a n+=1\r\n" +
                "if %n% lss 40 goto again\r\n" +
                ":done\r\n" +
                "reg delete \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Nextcalibur\" /f >nul 2>&1\r\n" +
                "del \"%~f0\"\r\n");
            Process.Start(new ProcessStartInfo(Nextcalibur.Core.Security.SystemTools.Cmd, $"/c \"{script}\"")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            });
            Log.Info("uninstall", "Folder removal scheduled: " + root);
        }
        catch (Exception ex) when (ex is IOException or Win32Exception or UnauthorizedAccessException)
        {
            Log.Warn("uninstall", "Could not schedule the folder's removal: " + ex.Message);
        }
    }

    /// <summary>
    /// The two things done once after install - and neither on a machine
    /// without the firmware mailbox. Seen in the sandbox, 11 September 2026:
    /// a laptop this application does not understand was handed a power
    /// repair and a Run entry before the window had a chance to lock itself.
    /// "Nothing to click" has to include the installer's own first minute.
    /// </summary>
    private static void FirstRun()
    {
        if (MailboxAccess.Check() == MailboxAvailability.NotSupported)
        {
            // Nothing on a machine this does not understand - not even a
            // start-up task. The marker still goes, so this is not repeated.
            if (Environment.ProcessPath is { } exe && FirstRunMarker(exe) is { } m)
                try { File.Delete(m); } catch (IOException) { }
            return;
        }
        StartWithWindowsOnFirstRun();
        RepairPowerOverlayOnFirstRun();
    }

    /// <summary>The wizard's one-line file beside the install root: {app}irst-run.ini.</summary>
    private static string? FirstRunMarker(string executablePath)
    {
        var root = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(executablePath))
                   ?? System.IO.Path.GetDirectoryName(executablePath);
        return root is null ? null : System.IO.Path.Combine(root, "first-run.ini");
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
            if (Environment.ProcessPath is not { } self) return;

            // The installer's wizard asks; its answer is a one-line file
            // beside the executable, read once and removed. No file - the
            // plain Velopack Setup, which asks nothing - means yes, as before.
            var wanted = true;
            var marker = FirstRunMarker(self);
            var keep = false;
            if (marker is not null && File.Exists(marker))
            {
                var text = File.ReadAllText(marker);
                wanted = !text.Contains("StartWithWindows=0", StringComparison.OrdinalIgnoreCase);
                // A repair: the person's choice stands; only re-register what is there.
                keep = text.Contains("StartWithWindows=keep", StringComparison.OrdinalIgnoreCase);
                try { File.Delete(marker); } catch (IOException) { }
            }

            if (keep) wanted = StartupRegistration.IsEnabled;
            StartupRegistration.Set(wanted, self);
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
                ? Strings.Get("S.FirstRun.Repaired") +
                  Environment.NewLine + Environment.NewLine + string.Join(Environment.NewLine + Environment.NewLine, outcome.Done)
                : Strings.Get("S.FirstRun.Found");

            if (outcome.Blocked is { } blocked)
                body += Environment.NewLine + Environment.NewLine + blocked;
            else
                body += Environment.NewLine + Environment.NewLine + Strings.Get("S.FirstRun.Undo");

            Dialogs.Tell(
                Strings.Get(outcome.Blocked is null ? "S.FirstRun.RepairedTitle" : "S.FirstRun.PartlyTitle"),
                body);
        }
        catch (Exception ex)
        {
            // Never let a failed repair block startup.
            Dialogs.Warn("Nextcalibur",
                Strings.Get("S.FirstRun.Failed") +
                Environment.NewLine + Environment.NewLine + ex.Message +
                Environment.NewLine + Environment.NewLine + Strings.Get("S.FirstRun.Retry"));
        }
    }
}
