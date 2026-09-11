using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Nextcalibur.App.Controls;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.App;

public partial class MainWindow : Window
{
    private readonly PowerOverlayService _power = new();
    private readonly SystemModeService _modes = new();

    /// <summary>What to do with the mode when the charger comes and goes.</summary>
    private readonly BatteryModePolicy _battery = new();

    /// <summary>
    /// The last power source seen. Windows raises one notification for the
    /// charger and for every battery-level change alike, so a transition has
    /// to be found by comparing.
    /// </summary>
    private bool _onBattery = PowerSource.OnBattery();

    /// <summary>
    /// The mode as last known here. Kept rather than re-detected at the
    /// moment the charger moves, because Windows keeps one overlay for AC and
    /// another for battery: the instant after unplugging, the effective
    /// overlay is already the battery one and detection sees no mode at all.
    /// </summary>
    private SystemMode? _currentMode;
    private readonly GpuModeService _gpu = new();
    private readonly ThemeService _theme = new();
    private readonly CpuClockReader _cpuClock = new();
    private readonly GpuClockReader _gpuClock = new();
    private readonly AppSettings _settings = AppSettings.Load();
    private readonly UpdateService _updates = new();
    private readonly DispatcherTimer _timer = new();

    /// <summary>Slow-timer cadence while the window is on screen.</summary>
    private static readonly TimeSpan VisibleSlowInterval = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Slow-timer cadence while hidden in the notification area. Only the
    /// overheat check runs at this point, and a fault that has been building for
    /// half a minute is not less of a fault for being noticed a few seconds
    /// later.
    /// </summary>
    private static readonly TimeSpan HiddenSlowInterval = TimeSpan.FromSeconds(30);

    /// <summary>
    /// How many hidden ticks pass between idle reclamations — ten of them, so
    /// once every five minutes. See <see cref="ReclaimWhileIdle"/>.
    /// </summary>
    private const int HiddenTicksPerReclaim = 10;

    /// <summary>Hidden slow-timer ticks since the last reclamation.</summary>
    private int _hiddenTicks;

    private EcMailbox? _mailbox;
    private ThermalReader? _thermal;
    private LedController? _led;

    /// <summary>Hears Fn+Space so the next lighting write keeps the level it set.</summary>
    private BacklightKeyWatcher? _backlightKey;
    private TrayPresence? _tray;

    private int _consecutiveFailures;
    private bool _exiting;

    /// <summary>
    /// True while a firmware read is in flight. The read now happens off the
    /// user-interface thread, so a slow one must not have a second started on
    /// top of it — the mailbox holds one command at a time.
    /// </summary>
    private bool _sampling;

    /// <summary>
    /// The most recent mailbox reading, kept so the Display page can say what
    /// the card is costing without paying for its own measurement.
    /// </summary>
    private ThermalSample? _lastThermal;

    private bool _overheatNotified;
    private bool _mailboxFailed;

    /// <summary>What this machine has been judged able to use. See <see cref="HardwareSupport"/>.</summary>
    private SupportVerdict _support;

    /// <summary>
    /// Whether the firmware interface was found at startup.
    ///
    /// Cached deliberately. Asking WMI costs a fresh scope connection and a
    /// query, and the banner asked every five seconds — which leaked kernel
    /// handles at roughly four a second and was the application's only leak.
    /// The answer cannot change while the process runs: the interface is a
    /// property of the machine, not of anything we do.
    /// </summary>
    private bool _mailboxSupported;

    /// <summary>
    /// Suppresses hardware writes while the lighting controls are being filled
    /// in from stored state. Assigning IsChecked and Value raises the same
    /// events a click does.
    /// </summary>
    private bool _ledUiReady;

    /// <summary>
    /// Same purpose as <see cref="_ledUiReady"/>, for the mode tabs: assigning
    /// IsChecked while showing the machine's current state must not be mistaken
    /// for the user asking to change it.
    /// </summary>
    private bool _modeUiReady;

    /// <summary>Same guard again, for the theme buttons.</summary>
    private bool _themeUiReady;

    public MainWindow()
    {
        InitializeComponent();

        // Every dialogue the application raises is owned by this window from
        // here on, which is what makes it modal: nothing else in the window
        // responds until it is answered.
        Dialogs.Owner = this;

        ApplyPollInterval();
        Loaded += OnLoaded;
        StateChanged += OnStateChanged;

        // Not StateChanged: closing to the tray hides the window rather than
        // minimising it, so the state never changes and the handler never runs.
        IsVisibleChanged += (_, e) => { if (e.NewValue is true) RefreshLightingFromHardware(); };
        Closing += OnClosing;
        Application.Current.SessionEnding += (_, _) => _sessionEnding = true;
    }

    /// <summary>
    /// Blanks every figure the markup starts with, before any reading arrives.
    ///
    /// The markup carries numbers so the page has a shape at design time -
    /// 46.8% of memory, 733 of 1396 GB - and on a machine where the readings
    /// never come, those numbers stay on screen looking like readings. They
    /// were noticed on an unsupported machine, where the page is locked and
    /// nothing should be shown at all. The rule this project keeps is that
    /// nothing on screen is invented; a dash is the honest value until the
    /// real one is known.
    /// </summary>
    private void ClearInventedValues()
    {
        RamGauge.Value = 0;
        RamPercent.Text = "--";
        RamDetail.Text = "--";
        SsdGauge.Value = 0;
        SsdPercent.Text = "--";
        SsdDetail.Text = "--";
        CpuFan.Text = "-- rpm";
        GpuFan.Text = "-- rpm";
    }

    // ---------------------------------------------------------------- startup

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ClearInventedValues();
        LoadTheme();

        _tray = new TrayPresence(this, _settings);
        _tray.ExitRequested += (_, _) => Exit();

        // Said once, through the tray, because that is where this application
        // lives when it has something to say and no window on screen.
        _updates.UpdateReady += (_, version) => _tray?.ShowMessage(
            $"Nextcalibur {version} is ready",
            "It will be installed the next time you close the application from the tray menu.");
        _updates.AutomaticChecksEnabled = () => _settings.AutoCheckForUpdates;

        // The window's switch and the tray's menu are two faces of one setting.
        OverheatWarningToggle.IsChecked = _settings.OverheatWarningEnabled;
        OverheatWarningToggle.Checked += (_, _) => SetOverheatWarning(true);
        OverheatWarningToggle.Unchecked += (_, _) => SetOverheatWarning(false);
        _tray.OverheatSettingChanged += (_, _) => Dispatcher.BeginInvoke(() =>
            OverheatWarningToggle.IsChecked = _settings.OverheatWarningEnabled);
        LoadOverheatThresholds();
        VersionText.Text = RunningVersion();
        _updates.CheckedByHand += (_, what) => Dialogs.Tell("Nextcalibur - updates", what);
        _tray.Updates = _updates;
        _updates.Start();

        if (_settings.StartMinimised &&
            Environment.GetCommandLineArgs().Contains("--tray", StringComparer.OrdinalIgnoreCase))
        {
            Hide();
        }

        // Said before anything is read, because it explains readings that are
        // about to look unreliable.
        RecommendRemovingVendorSoftwareOnce();

        // Before deciding the machine is unsupported, rule out the far more
        // likely explanation: this account has not been allowed to use the
        // interface yet.
        AskForMailboxAccessIfNeeded();

        // The gate. Everything below is allowed or not by what the machine has
        // just said about itself, and an unsupported one is locked before a
        // single control is wired.
        _support = HardwareSupport.Check();
        ApplySupportVerdict();
        if (_support.Level == SupportLevel.Unsupported) return;

        _mailboxSupported = EcMailbox.IsSupported();

        // The Office, Gaming and High performance plans this used to select are
        // the vendor's, created by its installer. On a machine that never had
        // that software every mode falls back to Balanced and the three become
        // one. Making our own costs nothing and needs no administrator.
        SystemModeService.EnsurePlansExist();

        RefreshOverlay();
        LoadPowerModes();
        LoadDeviceNames();
        LoadGpuMode();
        RefreshStorage();
        RefreshBanner();
        StartSlowTimer();
        Microsoft.Win32.SystemEvents.PowerModeChanged += OnPowerSourceMayHaveChanged;

        if (!_mailboxSupported)
        {
            NavLighting.IsEnabled = false;
            return;
        }

        if (!ConnectToMailbox()) return;

        LoadSystemMode();
        RestoreModeAtStartup();
        LoadLightingUi();

        _timer.Tick += (_, _) => Sample();
        Sample();
        RefreshClocks();
        if (IsVisible) _timer.Start();
    }

    /// <summary>Opens the mailbox and everything that reads through it.</summary>
    private bool ConnectToMailbox()
    {
        try
        {
            _backlightKey?.Dispose();
            _mailbox?.Dispose();
            _mailbox = new EcMailbox();
            _thermal = new ThermalReader(_mailbox);
            _led = new LedController(_mailbox);
            ListenForTheBacklightKey();
            _mailboxFailed = false;
            RefreshBanner();
            return true;
        }
        catch (EcMailboxUnavailableException)
        {
            _mailboxFailed = true;
            RefreshBanner();
            NavLighting.IsEnabled = false;
            return false;
        }
    }

    /// <summary>Whether the vendor's software was installed at the last look.</summary>
    private bool? _vendorWasInstalled;

    /// <summary>
    /// Notices the vendor's software being removed while this runs, because
    /// its uninstaller does two things to this application on the way out,
    /// both measured 11 September 2026: it narrows the firmware permission
    /// back to administrators, which stops every reading here; and it puts
    /// Windows on Balanced, which drops the mode. The first needs the person
    /// (one prompt), the second does not.
    /// </summary>
    private void WatchTheVendorComingAndGoing()
    {
        var installed = VendorSoftware.IsInstalled();
        var removed = _vendorWasInstalled == true && !installed;
        _vendorWasInstalled = installed;
        if (!removed) return;

        if (MailboxAccess.Check() == MailboxAvailability.AccessNotGranted)
        {
            var granted = AskForMailboxAccessIfNeeded(
                "Casper's Control Center has just been removed, and its uninstaller took " +
                "Nextcalibur's access to the firmware with it.");
            if (granted)
            {
                _support = HardwareSupport.Check();
                ApplySupportVerdict();
                if (ConnectToMailbox()) { Sample(); LoadLightingUi(); }
            }
        }

        if (_currentMode is { } mode)
        {
            try
            {
                ApplyMode(mode);
                LoadSystemMode();
                RefreshOverlay();
            }
            catch (Exception)
            {
                // The tabs show what is; the person picks again.
            }
        }
    }

    /// <summary>
    /// Says once that the vendor's Control Center is installed alongside this,
    /// and leaves the decision where it belongs.
    ///
    /// The two drive the same firmware mailbox, which holds one command at a
    /// time, so readings stall and lighting changes fail to stick while both are
    /// about. That is worth saying. It is not worth refusing to run over, and it
    /// is certainly not worth removing somebody else's software over - so this
    /// recommends, records the answer, and never asks again.
    ///
    /// Checked whenever the window opens rather than only at install, because
    /// the order is not fixed: somebody may install that software afterwards, or
    /// put it back later.
    /// </summary>
    /// <summary>
    /// Locks the window to what the machine has been judged able to use.
    ///
    /// Unsupported means nothing: every page's navigation off, a banner that
    /// says so, and an offer to remove the application. Read-only means the
    /// two pages that write - lighting and the graphics switch - are off and
    /// the rest stays. Supported changes nothing.
    ///
    /// The owner's rule, set once the graphics switch went in: the repository
    /// is public, this writes to firmware, and a laptop it was not built
    /// against must be given nothing to click. The power page would work
    /// anywhere - it is Windows, not firmware - and is locked anyway, because
    /// a rule with no exceptions is easier to trust.
    /// </summary>
    private void ApplySupportVerdict()
    {
        switch (_support.Level)
        {
            case SupportLevel.Supported:
                return;

            case SupportLevel.ReadOnly:
                NavLighting.IsEnabled = false;
                foreach (var button in new[] { ModeDiscrete, ModeHybrid, ModeUma })
                    button.IsEnabled = false;
                ShowBanner(
                    "Readings only on this laptop",
                    "The firmware interface is there, but it did not answer the way the machine this was " +
                    "built against does, so nothing that writes to it is offered: " + string.Join(" ", _support.Reasons),
                    (SolidColorBrush)FindResource("Warn"));
                return;

            case SupportLevel.Unsupported:
                foreach (var nav in new[] { NavSystem, NavPower, NavDisplay, NavLighting })
                    nav.IsEnabled = false;
                foreach (var page in new[] { PageSystem, PagePower, PageDisplay, PageLighting })
                    page.IsEnabled = false;
                ShowBanner(
                    "This laptop isn't supported",
                    "Nextcalibur was built for a specific firmware interface and this machine does not have it. " +
                    "Nothing has been changed, and nothing here will do anything. " + string.Join(" ", _support.Reasons),
                    (SolidColorBrush)FindResource("Bad"));
                OfferRemovalOnUnsupportedMachine();
                return;
        }
    }

    private void OfferRemovalOnUnsupportedMachine()
    {
        var remove = Dialogs.Ask("Nextcalibur - not supported here",
            "This laptop does not have the firmware interface Nextcalibur was built for." +
            Environment.NewLine + Environment.NewLine +
            "Nothing has been changed on your machine, and to keep it that way, nothing in this " +
            "window will respond. There is no reason to keep it installed." +
            Environment.NewLine + Environment.NewLine +
            "Open Windows' list of installed apps so you can remove it?",
            defaultNo: false);

        if (!remove) return;

        try
        {
            Process.Start(new ProcessStartInfo("ms-settings:appsfeatures") { UseShellExecute = true });
        }
        catch (Exception ex) when (ex is Win32Exception or InvalidOperationException)
        {
        }
    }

    /// <summary>Whether the recommendation has been put this session; once is enough.</summary>
    private bool _vendorRecommended;

    private void RecommendRemovingVendorSoftwareOnce()
    {
        if (_vendorRecommended) return;

        if (VendorSoftware.FindInstallation() is not { } vendor)
        {
            // Gone. "Keep it" was an answer about the copy that was here; a
            // copy installed later is a new question.
            if (_settings.AcceptedVendorSoftware)
            {
                _settings.AcceptedVendorSoftware = false;
                _settings.Save();
            }
            return;
        }

        if (_settings.AcceptedVendorSoftware) return;
        _vendorRecommended = true;

        var body =
            $"{vendor.Name} is installed on this machine." +
            Environment.NewLine + Environment.NewLine +
            "Both it and Nextcalibur talk to the same interface in your laptop's firmware, " +
            "and it can only handle one request at a time. With both installed, readings " +
            "stall and lighting changes sometimes fail to apply - in either application." +
            Environment.NewLine + Environment.NewLine +
            "Removing it is recommended. Nextcalibur will not do that for you, and it works " +
            "either way." +
            Environment.NewLine + Environment.NewLine +
            "Open Windows' list of installed apps now?" +
            Environment.NewLine + Environment.NewLine +
            "Choosing No keeps it, and this message will not come back.";

        var open = Dialogs.Ask("Nextcalibur - two applications, one interface", body, defaultNo: false);

        if (open)
        {
            try
            {
                Process.Start(new ProcessStartInfo("ms-settings:appsfeatures") { UseShellExecute = true });
            }
            catch (Exception ex) when (ex is Win32Exception or InvalidOperationException)
            {
                // Nothing worth reporting: they know where their settings are.
            }

            // Not recorded as accepted. They may not go through with it, and
            // being asked again next time is the right outcome if they do not.
            return;
        }

        _settings.AcceptedVendorSoftware = true;
        _settings.Save();
    }

    /// <summary>
    /// Offers to grant this account access to the firmware mailbox, once.
    ///
    /// Every reading comes through one ACPI data block, and a kernel-WMI block
    /// grants the administrators group alone until somebody widens it. The
    /// vendor's software widens it at install time; on a machine that never had
    /// it - or that has had it removed - an ordinary account sees nothing at
    /// all, which used to surface as "readings have stalled" and a suggestion to
    /// close software that is not running.
    ///
    /// Asked only while access is missing. Once granted, the application never
    /// needs elevation again, which is the difference between this and the
    /// vendor software prompting at every startup.
    /// </summary>
    /// <returns>True when a grant was made just now.</returns>
    private bool AskForMailboxAccessIfNeeded(string? because = null)
    {
        var access = MailboxAccess.Check();
        if (access == MailboxAvailability.NotSupported) return false;

        // Two permissions, one prompt. On a fresh machine neither is there. On
        // one that already had the sensors - a copy from before Fn+Space was
        // heard - only the second is missing, and the wording says so.
        var needsSensors = access == MailboxAvailability.AccessNotGranted;
        var needsKey = !needsSensors && !MailboxAccess.CanHearEvents();
        if (!needsSensors && !needsKey) return false;

        var proceed = Dialogs.Confirm("Nextcalibur - one-time permission",
            (because is null ? string.Empty : because + Environment.NewLine + Environment.NewLine) +
            (needsSensors
                ? "Nextcalibur needs permission to read this machine's sensors." +
                  Environment.NewLine + Environment.NewLine +
                  "Temperatures, fan speeds and the keyboard lighting all come from one " +
                  "interface built into your laptop's firmware, and Windows keeps it closed " +
                  "to ordinary accounts until it is opened once."
                : "Nextcalibur needs permission to hear the keyboard's backlight key." +
                  Environment.NewLine + Environment.NewLine +
                  "Fn+Space changes the backlight in firmware. Without this, the next " +
                  "lighting change from here would put the backlight back to full.") +
            Environment.NewLine + Environment.NewLine +
            "Windows will ask you to confirm. This happens once - afterwards " +
            "Nextcalibur runs without any special privileges.");

        if (!proceed) return false;

        try
        {
            var self = Environment.ProcessPath;
            if (self is null) return false;

            // A second, short-lived copy of this same executable: it writes one
            // registry value and exits. Shipping a script instead would mean
            // depending on the execution policy of a machine we have just
            // established we know nothing about.
            using var elevated = Process.Start(new ProcessStartInfo
            {
                FileName = self,
                Arguments = App.GrantAccessArgument,
                UseShellExecute = true,
                Verb = "runas",
            });

            elevated?.WaitForExit();
            return elevated?.ExitCode == 0;
        }
        catch (Win32Exception)
        {
            // The user dismissed the prompt. Nothing was changed and nothing
            // more needs saying: the banner already explains what is missing.
            return false;
        }
    }

    /// <summary>Exit from the tray menu: the same question, then the same close.</summary>
    private void Exit()
    {
        if (!Dialogs.Ask("Exit Nextcalibur?",
                "This closes Nextcalibur completely: no temperature warning, no mode switch " +
                "when the charger moves, until it is started again.",
                defaultNo: true))
            return;
        _exiting = true;
        Close();
    }

    /// <summary>
    /// Takes the backlight step off a thermal sample. The event says when the
    /// key moves; this says where it is, including before this ran at all.
    /// Same slider update as the event path, so the page agrees with the keys.
    /// </summary>
    private void CarryTheBacklightLevel(ThermalSample s)
    {
        if (_led is not { } led) return;
        if (BacklightKeyWatcher.Parse((byte)s.BacklightLevel) is not { } level) return;
        if (led.HardwareLevel == level) return;

        led.HardwareLevel = level;
        var wasReady = _ledUiReady;
        _ledUiReady = false;
        BrightnessSlider.Value = led.EffectiveBrightnessPercent;
        BrightnessValue.Text = $"{led.EffectiveBrightnessPercent}%";
        _ledUiReady = wasReady;
    }

    /// <summary>
    /// Subscribes to the firmware's Fn+Space event. Without it every lighting
    /// write said "full brightness" and undid the key; with it the write
    /// carries the level the key chose. Quietly absent when the event class
    /// is not readable - the permission dialogue is where that is fixed.
    /// </summary>
    private void ListenForTheBacklightKey()
    {
        if (_led is null) return;

        try
        {
            _backlightKey = new BacklightKeyWatcher();
            _backlightKey.LevelChanged += level =>
            {
                if (_led is not { } led) return;
                led.HardwareLevel = level;

                // The slider shows what the keyboard shows. Set without
                // writing: the firmware has already done this one.
                Dispatcher.BeginInvoke(() =>
                {
                    var wasReady = _ledUiReady;
                    _ledUiReady = false;
                    BrightnessSlider.Value = led.EffectiveBrightnessPercent;
                    BrightnessValue.Text = $"{led.EffectiveBrightnessPercent}%";
                    _ledUiReady = wasReady;
                });
            };
        }
        catch (Exception ex) when (ex is System.Management.ManagementException or UnauthorizedAccessException)
        {
            _backlightKey = null;
        }
    }

    private void OnStateChanged(object? sender, EventArgs e)
    {
        // Sampling costs a firmware round trip. There is nothing to update while
        // the window is not on screen, so stop rather than burn the mailbox.
        if (WindowState == WindowState.Minimized)
        {
            _timer.Stop();
            TrimWorkingSet();
        }
        else if (_thermal is not null)
        {
            _timer.Start();
        }
    }

    /// <summary>
    /// Re-reads the lighting the machine is actually showing.
    ///
    /// Fn+Space changes the keyboard backlight without going through this
    /// application at all - the firmware reports the new level on its event
    /// class and applies it itself, measured on hardware. So anything set from
    /// that key while the window was away leaves the controls describing a state
    /// the keyboard is no longer in.
    ///
    /// Deliberately a re-read on becoming visible rather than a subscription to
    /// that event class. Subscribing would mean asking for administrator on a
    /// second GUID - the event class has no security descriptor of its own, so
    /// it is administrators-only - and a second permission prompt is a poor
    /// trade for keeping a panel in step that nobody is looking at anyway.
    /// </summary>
    private void RefreshLightingFromHardware()
    {
        if (_led is null || !_mailboxSupported) return;

        try
        {
            LoadLightingUi();
        }
        catch (EcMailboxUnavailableException)
        {
            // A miss here costs nothing: the controls keep the values they had,
            // and the next time the window is opened it tries again.
        }
    }

    /// <summary>
    /// Close means exit, after a question; the tray has its own button now.
    /// The question is skipped when Windows is the one closing us - a sign-out
    /// or shutdown must not hang on a dialogue - and when the answer was
    /// already given from the tray menu.
    /// </summary>
    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (!_exiting && !_sessionEnding)
        {
            if (!Dialogs.Ask("Exit Nextcalibur?",
                    "This closes Nextcalibur completely: no temperature warning, no mode switch " +
                    "when the charger moves, until it is started again." +
                    Environment.NewLine + Environment.NewLine +
                    "To keep it running out of the way, use the tray button instead.",
                    defaultNo: true))
            {
                e.Cancel = true;
                return;
            }
            _exiting = true;
        }

        {
            _timer.Stop();
            Microsoft.Win32.SystemEvents.PowerModeChanged -= OnPowerSourceMayHaveChanged;
            _tray?.Dispose();
            _backlightKey?.Dispose();
            _mailbox?.Dispose();
            _cpuClock.Dispose();
            _gpuClock.Dispose();
            _settings.Save();

            // Last, and after everything else is released: this hands a staged
            // release to an installer that waits for this process to go away.
            _updates.ApplyOnExit();
            _updates.Dispose();
        }
    }

    /// <summary>Set when Windows is ending the session, so closing asks nothing.</summary>
    private bool _sessionEnding;

    /// <summary>
    /// The tray button: the window goes away, the application stays. This is
    /// what the close button used to do, and the only way the temperature
    /// warning and the charger rule are of any use with no window on screen.
    /// </summary>
    private void OnMinimiseToTrayClick(object sender, RoutedEventArgs e) => HideToTray();

    private void HideToTray()
    {
        if (_tray is null) { WindowState = WindowState.Minimized; return; }
        Hide();
        _timer.Stop();
        TrimWorkingSet();
    }

    // ------------------------------------------------------------ window chrome

    // The native title bar is switched off, so dragging and the caption buttons
    // are ours to provide.

    private void OnTitleBarDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed) DragMove();
    }

    private void OnMinimiseClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    [System.Runtime.InteropServices.DllImport("psapi.dll")]
    private static extern bool EmptyWorkingSet(IntPtr process);

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentProcess();

    /// <summary>
    /// Asks Windows to page out what the window was using.
    ///
    /// This does not free memory — the pages are still committed and come back
    /// when needed. It hands back the resident set of a window nobody is
    /// looking at, which is what a tray application should do rather than
    /// holding a hundred-odd megabytes of rendering state on screen-less watch.
    /// </summary>
    /// <summary>
    /// Reclaims what the tray watch leaves behind, and hands back the pages.
    ///
    /// Each firmware read leaves a few objects whose handles are only released
    /// when a finaliser runs — measured at about five and a half per read. With
    /// the window on screen this never shows, because drawing allocates enough
    /// to keep collections coming. Hidden, the application allocates almost
    /// nothing, so no collection happens and nothing runs those finalisers: the
    /// handle count climbed at 0.18 a second, about fifteen thousand a day, and
    /// the working set crept from 13 to 34 MB over twelve minutes.
    ///
    /// Forcing a collection is normally the wrong instinct, because the runtime
    /// schedules them better than a guess does. The exception is an application
    /// that has gone idle, where the heuristics have nothing left to work from —
    /// which is exactly this, and is the same reasoning that already justifies
    /// <see cref="TrimWorkingSet"/> on the same transition. It runs every tenth
    /// hidden tick, so once every five minutes, and only while nobody is
    /// looking at the window.
    /// </summary>
    private static void ReclaimWhileIdle()
    {
        // The first pass queues the finalisers, the wait runs them — which is
        // what actually closes the handles — and the second reclaims what they
        // released. Blocking here is safe: the window is hidden, so the thread
        // this runs on has nothing to draw.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        TrimWorkingSet();
    }

    private static void TrimWorkingSet()
    {
        try
        {
            // The kernel32 call returns a pseudo-handle that needs no cleanup.
            // Process.GetCurrentProcess() would hand back an object holding a
            // real handle, which is the sort of thing this method exists to
            // avoid accumulating.
            EmptyWorkingSet(GetCurrentProcess());
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            // A nicety; never worth failing over.
        }
    }

    // Close hides to the tray; OnClosing decides. Exit is in the tray menu.
    private void OnCloseClick(object sender, RoutedEventArgs e) => Close();

    // ------------------------------------------------------------- navigation

    private void OnNavChanged(object sender, RoutedEventArgs e)
    {
        if (PageSystem is null) return;   // fires once before the tree is built

        PageSystem.Visibility = NavSystem.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        PagePower.Visibility = NavPower.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        PageDisplay.Visibility = NavDisplay.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        PageLighting.Visibility = NavLighting.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;

        // Both pages describe state that other software can change while we are
        // running, so re-read it when the page comes into view rather than
        // showing whatever was true at startup.
        if (NavPower.IsChecked == true) RefreshOverlay();
        if (NavDisplay.IsChecked == true) LoadGpuMode();
    }

    // ----------------------------------------------------------- system mode

    private void LoadSystemMode()
    {
        _modeUiReady = false;
        var current = _modes.DetectCurrent();
        if (current is { } mode)
            ModeButtonFor(mode).IsChecked = true;
        else
            foreach (var button in new[] { ModeOffice, ModeGaming, ModePerformance })
                button.IsChecked = false;
        _currentMode = current;
        _modeUiReady = true;
        RefreshModeStatus(current);
    }

    /// <summary>
    /// Puts the person's mode back after a restart. Windows comes up on
    /// Balanced regardless of what was active at shutdown, so the mode they
    /// chose is remembered here and re-applied: as it was, on the charger;
    /// the quiet one, on battery, with theirs restored when the charger
    /// returns. Nothing is written when the machine is already there.
    /// </summary>
    private void RestoreModeAtStartup()
    {
        if (_settings.LastSystemMode is not { } last || !_support.AllowsReads) return;

        var target = _onBattery && _settings.QuietOnBattery
            ? _battery.OnUnplugged(last) ?? BatteryModePolicy.QuietMode
            : last;
        if (_currentMode == target) return;

        try
        {
            ApplyMode(target);
            LoadSystemMode();
            RefreshOverlay();
        }
        catch (Exception)
        {
            // Performance without the guard, or a plan gone missing: the tabs
            // show what is, and the person picks again.
        }
    }

    /// <summary>
    /// A mode is the Windows plan and the firmware profile together. The plan
    /// first: it needs no hardware and is the part that can fail loudly. The
    /// profile is best effort - a mailbox that will not take it leaves the
    /// fans on whatever curve they had, which is the state the machine was
    /// in a moment ago.
    /// </summary>
    private void ApplyMode(SystemMode mode)
    {
        _modes.Apply(mode);

        if (_mailbox is null || !_support.AllowsWrites) return;
        try
        {
            ThermalProfile.Write(_mailbox, mode);
        }
        catch (EcMailboxUnavailableException)
        {
            // Said above: the plan is applied, the fans keep their curve.
        }
    }

    /// <summary>The mode and power-source half of the subtitle, e.g. "Gaming mode, on AC power".</summary>
    private string _modeStatus = string.Empty;

    /// <summary>When the readings were last taken, for the other half.</summary>
    private DateTimeOffset? _lastSampleAt;

    /// <summary>
    /// Says which mode is on and where the power is coming from. Six
    /// variations for the three modes, plus a seventh when Windows is on none
    /// of them - a fresh machine sits on Balanced, and blank tabs said nothing
    /// about that. Composed with the reading time rather than fighting it for
    /// the one line.
    /// </summary>
    private void RefreshModeStatus(SystemMode? current)
    {
        var source = _onBattery ? "on battery" : "on AC power";
        _modeStatus = current is { } mode
            ? $"{mode} mode, {source}"
            : $"No mode active ({SystemModeService.DescribeCurrent()}), {source}";
        ShowSubtitle();
    }

    private void ShowSubtitle() =>
        SubtitleText.Text = _lastSampleAt is { } at
            ? $"{_modeStatus}  ·  Updated {at:HH:mm:ss}"
            : _modeStatus;

    private RadioButton ModeButtonFor(SystemMode mode) => mode switch
    {
        SystemMode.Gaming => ModeGaming,
        SystemMode.Performance => ModePerformance,
        _ => ModeOffice,
    };

    private void OnSystemModeChanged(object sender, RoutedEventArgs e)
    {
        if (!_modeUiReady) return;
        if (sender is not RadioButton button || button.Tag is not string tag) return;
        if (!Enum.TryParse<SystemMode>(tag, out var mode)) return;

        try
        {
            ApplyMode(mode);
            _currentMode = mode;
            _battery.UserChose(mode, _onBattery);
            _settings.LastSystemMode = mode;
            _settings.Save();
            RefreshModeStatus(mode);
            RefreshOverlay();
        }
        catch (Exception ex)
        {
            Dialogs.Warn("Could not change mode", ex.Message);
            LoadSystemMode();
        }
    }

    /// <summary>
    /// The vendor's behaviour when the charger comes out: quiet mode on
    /// battery, and the person's own mode back when the charger returns. The
    /// rules are in <see cref="BatteryModePolicy"/>; this only notices the
    /// change and applies what it says. Costs nothing between changes - it is
    /// a Windows notification, not a poll.
    /// </summary>
    private void OnPowerSourceMayHaveChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
    {
        if (e.Mode != Microsoft.Win32.PowerModes.StatusChange) return;

        var onBattery = PowerSource.OnBattery();
        if (onBattery == _onBattery) return;
        _onBattery = onBattery;

        Dispatcher.BeginInvoke(() =>
        {
            try
            {
                if (!_settings.QuietOnBattery || !_support.AllowsReads)
                {
                    RefreshModeStatus(_currentMode);
                    return;
                }

                var next = onBattery ? _battery.OnUnplugged(_currentMode) : _battery.OnPluggedIn(_currentMode);
                if (next is { } mode) ApplyMode(mode);

                LoadSystemMode();
                RefreshOverlay();
            }
            catch (Exception)
            {
                // A mode that could not be switched on unplug is the state the
                // machine was already in. Nothing to undo, nothing to say.
            }
        });
    }

    // ------------------------------------------------------------ graphics

    private void LoadGpuMode()
    {
        var config = _gpu.Detect();
        GpuModeDetail.Text = GpuModeService.Describe(config, IdleWatts(config), _lastThermal);

        // The transitions the firmware refuses, said on the card rather than
        // on click: UMA is a switched-off card and Discrete hands it the
        // panel, so neither is reached from the other without Hybrid between.
        ModeUmaNote.Visibility = config.Mode == GpuMode.Discrete ? Visibility.Visible : Visibility.Collapsed;
        ModeDiscreteNote.Visibility = config.Mode == GpuMode.Uma ? Visibility.Visible : Visibility.Collapsed;
        ModeHybridNote.Visibility = Visibility.Collapsed;

        // A firmware write that has not been restarted into yet. The register
        // reads the running mode, so "pending" is the mode the person chose
        // and confirmed this session, not something read back.
        GpuRestartPending.Text = _gpuPendingRestart is { } pending
            ? $"Restart to switch to {pending}. Until then the machine runs as shown."
            : string.Empty;
        GpuRestartPending.Visibility = _gpuPendingRestart is null ? Visibility.Collapsed : Visibility.Visible;

        if (config.Mode is not { } mode) return;
        GpuButtonFor(mode).IsChecked = true;
    }

    /// <summary>
    /// A dialogue drawn inside the window. Modal the old-fashioned way: a
    /// nested dispatcher frame runs until a button is pressed, so the caller
    /// gets an answer back synchronously, as it did from MessageBox. The
    /// overlay covers the whole window and takes every click; Escape gives
    /// the fallback, which every caller has chosen as the safe answer.
    /// </summary>
    public MessageBoxResult ShowOverlayDialog(string title, string body, MessageBoxButton buttons, MessageBoxResult fallback)
    {
        var (primary, secondary, primaryText, secondaryText) = buttons switch
        {
            MessageBoxButton.YesNo => (MessageBoxResult.Yes, MessageBoxResult.No, "Yes", "No"),
            MessageBoxButton.OKCancel => (MessageBoxResult.OK, MessageBoxResult.Cancel, "OK", "Cancel"),
            _ => (MessageBoxResult.OK, (MessageBoxResult?)null, "OK", string.Empty),
        };

        DialogTitleText.Text = title;
        DialogBodyText.Text = body;
        DialogButtonPrimary.Content = primaryText;
        DialogButtonSecondary.Content = secondaryText;
        DialogButtonSecondary.Visibility = secondary is null ? Visibility.Collapsed : Visibility.Visible;

        var result = fallback;
        var frame = new DispatcherFrame();

        void Primary(object s, RoutedEventArgs e) { result = primary; frame.Continue = false; }
        void Secondary(object s, RoutedEventArgs e) { result = secondary ?? fallback; frame.Continue = false; }
        void Key(object s, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape) { result = fallback; frame.Continue = false; e.Handled = true; }
            else if (e.Key == System.Windows.Input.Key.Enter) { result = fallback == primary || secondary is null ? primary : fallback; frame.Continue = false; e.Handled = true; }
        }

        DialogButtonPrimary.Click += Primary;
        DialogButtonSecondary.Click += Secondary;
        ModalDialogOverlay.PreviewKeyDown += Key;
        ModalDialogOverlay.Visibility = Visibility.Visible;
        (fallback == primary || secondary is null ? DialogButtonPrimary : DialogButtonSecondary).Focus();

        try
        {
            Dispatcher.PushFrame(frame);
        }
        finally
        {
            DialogButtonPrimary.Click -= Primary;
            DialogButtonSecondary.Click -= Secondary;
            ModalDialogOverlay.PreviewKeyDown -= Key;
            ModalDialogOverlay.Visibility = Visibility.Collapsed;
        }

        return result;
    }

    private void SetOverheatWarning(bool on)
    {
        CpuWarnSlider.IsEnabled = GpuWarnSlider.IsEnabled = on;
        if (_settings.OverheatWarningEnabled == on) return;
        _settings.OverheatWarningEnabled = on;
        _settings.Save();
        _tray?.SyncOverheatMenu();
    }

    /// <summary>The version of this copy, as the package carries it - "0.5.1", without the commit hash.</summary>
    private static string RunningVersion()
    {
        var info = typeof(MainWindow).Assembly
            .GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
            .OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
            .FirstOrDefault()?.InformationalVersion;
        var version = info?.Split('+')[0] ?? typeof(MainWindow).Assembly.GetName().Version?.ToString(3) ?? "?";
        return "v" + version;
    }

    private bool _thresholdsReady;

    /// <summary>One slider per chip; the number beside each is the setting.</summary>
    private void LoadOverheatThresholds()
    {
        _thresholdsReady = false;
        CpuWarnSlider.Value = _settings.CpuWarningTemperatureC;
        GpuWarnSlider.Value = _settings.GpuWarningTemperatureC;
        CpuWarnValue.Text = $"{_settings.CpuWarningTemperatureC} °C";
        GpuWarnValue.Text = $"{_settings.GpuWarningTemperatureC} °C";
        CpuWarnSlider.IsEnabled = GpuWarnSlider.IsEnabled = _settings.OverheatWarningEnabled;
        _thresholdsReady = true;

        CpuWarnSlider.ValueChanged += (_, e) => OnThresholdMoved(e.NewValue, CpuWarnValue, v => _settings.CpuWarningTemperatureC = v);
        GpuWarnSlider.ValueChanged += (_, e) => OnThresholdMoved(e.NewValue, GpuWarnValue, v => _settings.GpuWarningTemperatureC = v);

        // The number is typeable too. Unit stripped while editing; on Enter
        // or leaving the box the text is either a whole number inside the
        // range, or it is refused and the slider's value comes back.
        WireTypedThreshold(CpuWarnValue, CpuWarnSlider);
        WireTypedThreshold(GpuWarnValue, GpuWarnSlider);

        OverheatResetButton.Click += (_, _) =>
        {
            var defaults = new AppSettings();
            CpuWarnSlider.Value = defaults.CpuWarningTemperatureC;
            GpuWarnSlider.Value = defaults.GpuWarningTemperatureC;
        };
    }

    private void OnThresholdMoved(double value, System.Windows.Controls.TextBox readout, Action<int> store)
    {
        var celsius = (int)Math.Round(value);
        if (!readout.IsKeyboardFocused) readout.Text = $"{celsius} °C";
        if (!_thresholdsReady) return;
        store(celsius);
        _settings.Save();
        // A new limit is a new question: let the next hot reading warn again.
        _overheatNotified = false;
    }

    private static void WireTypedThreshold(System.Windows.Controls.TextBox box, Slider slider)
    {
        box.GotKeyboardFocus += (_, _) =>
        {
            box.Text = ((int)Math.Round(slider.Value)).ToString();
            box.SelectAll();
        };

        void Commit()
        {
            var text = box.Text.Replace("°C", string.Empty).Trim();
            var accepted = int.TryParse(text, out var typed)
                && typed >= AppSettings.MinWarningTemperatureC
                && typed <= AppSettings.MaxWarningTemperatureC;
            if (accepted) slider.Value = typed;
            // Either way the box shows what the setting now is, and nothing else.
            box.Text = $"{(int)Math.Round(slider.Value)} °C";
        }

        box.LostKeyboardFocus += (_, _) => Commit();
        box.PreviewKeyDown += (_, e) =>
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Commit();
                System.Windows.Input.Keyboard.ClearFocus();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                box.Text = $"{(int)Math.Round(slider.Value)} °C";
                System.Windows.Input.Keyboard.ClearFocus();
                e.Handled = true;
            }
        };
    }

    /// <summary>The mode written to firmware this session and not yet restarted into.</summary>
    private GpuMode? _gpuPendingRestart;

    /// <summary>
    /// Power draw, but only where asking for it is free.
    ///
    /// NVML wakes the GPU. In Discrete the card is already awake - it is
    /// drawing the desktop - so the reading costs nothing and is worth having.
    /// In Hybrid and UMA it would be this application waking the card the mode
    /// exists to keep asleep, every time somebody opened the page, and then
    /// reporting the draw it had just caused. Temperature and fan speed come
    /// from the mailbox in every mode and wake nothing.
    /// </summary>
    private GpuLoad? IdleWatts(GpuConfiguration config) =>
        config.Mode == GpuMode.Discrete ? _gpuClock.ReadLoad() : null;

    private RadioButton GpuButtonFor(GpuMode mode) => mode switch
    {
        GpuMode.Hybrid => ModeHybrid,
        GpuMode.Uma => ModeUma,
        _ => ModeDiscrete,
    };

    /// <summary>
    /// How long the mode buttons stay locked after a switch.
    ///
    /// A device switch is not instantaneous: pnputil returns, the driver stops
    /// or starts over the next second or two, WMI catches up after that, and
    /// the sensor timer needs a couple of ticks on a fresh NVML handle before
    /// anything about the card can be trusted again. A second click inside
    /// that window would act on a state that is still settling. Four ticks of
    /// the two-second timer, with a margin.
    /// </summary>
    private static readonly TimeSpan GpuSwitchCooldown = TimeSpan.FromSeconds(10);

    private DispatcherTimer? _gpuCooldown;

    /// <summary>
    /// Locks the mode buttons and unlocks them when the machine has settled,
    /// counting down on screen so the lock reads as deliberate rather than as
    /// the page having stopped working.
    /// </summary>
    private void LockGpuButtonsWhileSettling()
    {
        foreach (var button in new[] { ModeDiscrete, ModeHybrid, ModeUma })
            button.IsEnabled = false;

        var remaining = (int)GpuSwitchCooldown.TotalSeconds;
        GpuSettling.Text = SettlingText(remaining);
        GpuSettling.Visibility = Visibility.Visible;

        _gpuCooldown?.Stop();
        _gpuCooldown = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _gpuCooldown.Tick += (_, _) =>
        {
            remaining--;
            if (remaining > 0)
            {
                GpuSettling.Text = SettlingText(remaining);
                return;
            }

            _gpuCooldown!.Stop();
            GpuSettling.Visibility = Visibility.Collapsed;
            foreach (var button in new[] { ModeDiscrete, ModeHybrid, ModeUma })
                button.IsEnabled = true;
            LoadGpuMode();
        };
        _gpuCooldown.Start();
    }

    private static string SettlingText(int seconds) =>
        $"Letting the change settle - the modes unlock in {seconds} second{(seconds == 1 ? "" : "s")}.";

    private void OnGpuModeChanged(object sender, RoutedEventArgs e)
    {
        var config = _gpu.Detect();
        GpuModeDetail.Text = GpuModeService.Describe(config, IdleWatts(config), _lastThermal);

        if (config.Mode is not { } current) return;
        if (sender is not RadioButton button || ReferenceEquals(button, GpuButtonFor(current))) return;
        if (button.Tag is not string tag || !Enum.TryParse<GpuMode>(tag, out var target)) return;

        if (_mailbox is null || !_support.AllowsWrites)
        {
            Dialogs.Warn("Graphics mode",
                _support.AllowsWrites
                    ? "The firmware interface is not available, so the mode cannot be changed from here."
                    : "This laptop has not shown it speaks the protocol Nextcalibur was built against, " +
                      "so nothing that writes to it is offered. Readings only.");
            GpuButtonFor(current).IsChecked = true;
            return;
        }

        // UMA and back are a device switch: immediate, no restart, and nothing
        // the TPM measures. The firmware modes are the ones that cost a PIN.
        var touchesFirmware = target != GpuMode.Uma && !(current == GpuMode.Uma && target == GpuMode.Hybrid);

        if (touchesFirmware && !ConfirmFirmwareSwitch(target))
        {
            GpuButtonFor(current).IsChecked = true;
            return;
        }

        // The NVML handle names a device that is about to be switched off or
        // on. Using it afterwards does not fail, it kills the process inside
        // nvml.dll - which is how the first attempt at this ended, and how the
        // vendor's own software ends four seconds after its UMA button.
        _gpuClock.Reset();

        try
        {
            var outcome = _gpu.Switch(_mailbox, target);

            if (!outcome.Changed)
            {
                Dialogs.Warn("Graphics mode", outcome.Summary);
                LoadGpuMode();
                return;
            }

            LockGpuButtonsWhileSettling();

            if (outcome.RestartNeeded)
            {
                _gpuPendingRestart = target;
                OfferRestart(outcome.Summary);
            }
            else
            {
                // Immediate changes deserve a word too. The first version said
                // nothing after switching the card off, and the only sign that
                // anything had happened was Windows' own elevation prompt.
                Dialogs.Tell("Graphics mode", outcome.Summary);
            }

            LoadGpuMode();
        }
        catch (OperationCanceledException)
        {
            // They dismissed the elevation prompt. Nothing changed; nothing to say.
            GpuButtonFor(current).IsChecked = true;
        }
        catch (InvalidOperationException ex)
        {
            // A transition the rules refuse: the card is driving the screen, or
            // it is switched off. Said as it is, and the selection put back.
            Dialogs.Warn("Not from here", ex.Message);
            GpuButtonFor(current).IsChecked = true;
        }
        catch (Exception ex) when (ex is EcMailboxUnavailableException or Win32Exception)
        {
            Dialogs.Warn("Could not change the mode", ex.Message);
            GpuButtonFor(current).IsChecked = true;
        }
        finally
        {
            // Whatever happened to the device, the next clock read starts from
            // a fresh enumeration rather than a handle that may name nothing.
            _gpuClock.Reset();
        }
    }

    /// <summary>
    /// The conversation the vendor's software never has. Its dialogue asks
    /// "restart?" and mentions neither the PIN nor BitLocker; both were learned
    /// here by losing the PIN, four times in one night.
    /// </summary>
    private bool ConfirmFirmwareSwitch(GpuMode target)
    {
        var answer = Dialogs.Ask("Graphics mode",
            $"Switch to {target}?" +
            Environment.NewLine + Environment.NewLine +
            "This changes which chip drives your screen, and takes effect at the next restart." +
            Environment.NewLine + Environment.NewLine +
            "Two things happen because of it, and Casper's own software warns about neither:" +
            Environment.NewLine +
            "    - Your Windows PIN will stop working and need setting up again." +
            Environment.NewLine +
            "    - If BitLocker is on, the next boot can ask for your 48-digit recovery key. " +
            "Without it the drive does not open. Find it first - it is in your Microsoft " +
            "account at aka.ms/myrecoverykey, or wherever you saved it." +
            Environment.NewLine + Environment.NewLine +
            "Both happen because the change alters what the TPM measures when the machine starts." +
            Environment.NewLine + Environment.NewLine +
            "Go ahead?",
            defaultNo: true);

        return answer;
    }

    private void OfferRestart(string summary)
    {
        var restart = Dialogs.Ask("Graphics mode",
            summary + Environment.NewLine + Environment.NewLine + "Restart now?", defaultNo: true);

        if (!restart) return;

        try
        {
            // /t 0: any longer delay makes Windows raise its own "your session
            // will end" notice on top of the question just answered. The person
            // has said yes twice by now; the third notice was noise.
            Process.Start(new ProcessStartInfo("shutdown.exe", "/r /t 0 /d p:2:4 /c \"Nextcalibur: applying the graphics mode change.\"")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
            });
        }
        catch (Win32Exception)
        {
            Dialogs.Warn("Graphics mode", "Could not start the restart. Restart the machine yourself to apply the change.");
        }
    }

    // --------------------------------------------------------------- power mode

    private void LoadPowerModes()
    {
        _modeUiReady = false;

        var active = PowerOverlayService.GetActiveOverlay();
        foreach (var (option, button, text) in PowerModeControls())
        {
            text.Text = option.Description;
            if (option.Overlay == active) button.IsChecked = true;
        }

        _modeUiReady = true;
    }

    private IEnumerable<(PowerModeOption Option, RadioButton Button, TextBlock Text)> PowerModeControls()
    {
        var buttons = new[] { PowerModeEfficiency, PowerModeBalanced, PowerModeBetter, PowerModeBest };
        var texts = new[] { PowerModeEfficiencyText, PowerModeBalancedText, PowerModeBetterText, PowerModeBestText };

        // PowerOverlays.All is ordered coolest to fastest, matching the cards.
        for (var i = 0; i < PowerOverlays.All.Count && i < buttons.Length; i++)
            yield return (PowerOverlays.All[i], buttons[i], texts[i]);
    }

    private void OnPowerModeChanged(object sender, RoutedEventArgs e)
    {
        if (!_modeUiReady) return;
        if (sender is not RadioButton button) return;

        var chosen = PowerModeControls().FirstOrDefault(x => ReferenceEquals(x.Button, button));
        if (chosen.Button is null) return;

        try
        {
            PowerOverlayService.SetActiveOverlay(chosen.Option.Overlay);
            RefreshOverlay();

            // Changing the overlay can move the machine out of whichever system
            // mode it matched, so that page has to be re-read too.
            LoadSystemMode();
        }
        catch (Exception ex)
        {
            Dialogs.Warn("Could not change mode", ex.Message);
            LoadPowerModes();
        }
    }

    // -------------------------------------------------------------------- theme

    private void LoadTheme()
    {
        _theme.Preference = _settings.Theme;
        ApplyTheme();

        _themeUiReady = false;
        (_settings.Theme switch
        {
            ThemePreference.Dark => ThemeDark,
            ThemePreference.Light => ThemeLight,
            _ => ThemeSystem,
        }).IsChecked = true;
        _themeUiReady = true;
    }

    private void OnThemeChanged(object sender, RoutedEventArgs e)
    {
        if (!_themeUiReady) return;
        if (sender is not RadioButton button || button.Tag is not string tag) return;
        if (!Enum.TryParse<ThemePreference>(tag, out var preference)) return;

        _theme.Preference = preference;
        _settings.Theme = preference;
        _settings.Save();
        ApplyTheme();
    }

    /// <summary>
    /// Swaps the palette dictionary. Everything else in the application
    /// references brushes with DynamicResource, so replacing the entry is all it
    /// takes — no reload, no restart.
    /// </summary>
    private void ApplyTheme()
    {
        var wanted = _theme.Resolved == Theme.Light
            ? "Themes/Palette.Light.xaml"
            : "Themes/Palette.Dark.xaml";

        var merged = Application.Current.Resources.MergedDictionaries;
        for (var i = 0; i < merged.Count; i++)
        {
            var source = merged[i].Source?.OriginalString;
            if (source is null || !source.Contains("Palette.", StringComparison.OrdinalIgnoreCase)) continue;
            if (source.EndsWith(wanted, StringComparison.OrdinalIgnoreCase)) return;

            merged[i] = new ResourceDictionary { Source = new Uri(wanted, UriKind.Relative) };
            _theme.MarkApplied();

            // The zone previews carry brushes assigned in code rather than
            // resource references — the selection ring is one colour or
            // transparent, which a DynamicResource cannot express. Local values
            // do not follow a palette swap, so they are re-applied here. Without
            // this the selected zone keeps the previous theme's accent until the
            // next time the user touches the lighting controls.
            RefreshPreview();
            return;
        }
    }

    // ------------------------------------------------------------ device names

    /// <summary>
    /// Asks the machine what its processor and graphics card are called. Read
    /// once at startup — these do not change while the application runs, and no
    /// model name is ever written into the source.
    /// </summary>
    private void LoadDeviceNames()
    {
        var cpu = SystemInfo.ProcessorName();
        var gpu = SystemInfo.GraphicsName();

        CpuName.Text = cpu ?? string.Empty;
        GpuName.Text = gpu ?? string.Empty;
        CpuName.ToolTip = cpu;
        GpuName.ToolTip = gpu;
    }

    // ------------------------------------------------------- memory and disk

    private void RefreshStorage()
    {
        var memory = SystemInfo.Memory();
        RamGauge.Value = memory.Percent;
        RamPercent.Text = $"{memory.Percent:N1}%";
        RamDetail.Text = memory.Describe();

        var disk = SystemInfo.SystemDrive();
        SsdGauge.Value = disk.Percent;
        SsdPercent.Text = $"{disk.Percent:N1}%";
        SsdDetail.Text = disk.Describe();
    }

    // ---------------------------------------------------------------- sensors

    /// <summary>
    /// Takes one reading and puts it on screen.
    ///
    /// The firmware read happens on a thread-pool thread, and that is not a
    /// performance flourish — it is the fix for a handle leak. The mailbox goes
    /// through <c>System.Management</c>, which requires an MTA thread; called
    /// from the single-threaded user-interface thread every call is marshalled
    /// across, and each marshalling leaves a kernel event behind that lives
    /// until the garbage collector finalises it. Measured at 2.4 handles a
    /// second, climbing past a thousand between collections. Thread-pool threads
    /// are already MTA, so the marshalling — and the leak — simply stops.
    /// Taking a firmware round trip off the UI thread is the smaller benefit.
    /// </summary>
    private async void Sample()
    {
        if (_thermal is null || _sampling) return;

        ThermalSample s;
        var reader = _thermal;

        _sampling = true;
        try
        {
            var reading = await Task.Run(
                () => reader.TryRead(out var value) ? value : (ThermalSample?)null);

            if (reading is null)
            {
                // A single miss is normal when something else touches the mailbox.
                if (++_consecutiveFailures >= 5)
                {
                    SubtitleText.Text = "Readings have stalled. Close Casper's Control Center and reopen this window.";
                    _timer.Stop();
                }
                return;
            }

            s = reading.Value;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            // This runs as async void off a timer: an escaping exception would
            // take the process down rather than surface anywhere useful.
            _consecutiveFailures++;
            return;
        }
        finally
        {
            _sampling = false;
        }

        _consecutiveFailures = 0;
        _lastThermal = s;
        CarryTheBacklightLevel(s);

        CpuTemp.Text = $"{s.CpuTemperatureC} °C";
        GpuTemp.Text = $"{s.GpuTemperatureC} °C";
        CpuFan.Text = $"{s.CpuFanRpm} rpm";
        GpuFan.Text = $"{s.GpuFanRpm} rpm";

        ColourByTemperature(CpuTemp, s.CpuTemperatureC);
        ColourByTemperature(GpuTemp, s.GpuTemperatureC);

        RefreshClocks();
        _lastSampleAt = s.Timestamp;
        ShowSubtitle();
    }

    /// <summary>
    /// Shows the frequencies the parts are actually running at. Either can be
    /// unreadable — no NVIDIA card, an unavailable counter — and then the
    /// reading is left blank rather than filled with a guess.
    /// </summary>
    private void RefreshClocks()
    {
        CpuClock.Text = _cpuClock.ReadGhz() is { } cpu ? $"{cpu:N2} GHz" : string.Empty;
        GpuClock.Text = _gpuClock.ReadGhz() is { } gpu ? $"{gpu:N2} GHz" : string.Empty;
    }

    /// <summary>
    /// Colours a reading by how hot it is.
    ///
    /// This attaches a resource reference rather than assigning a brush.
    /// Assigning one sets a local value, which outranks the style and does not
    /// follow a palette swap: the reading would keep the previous theme's
    /// colour until the next timer tick, and often past it.
    /// </summary>
    private static void ColourByTemperature(TextBlock reading, int celsius)
    {
        var key = celsius switch
        {
            >= 90 => "Bad",
            >= 80 => "Warn",
            _ => "Ink",
        };
        reading.SetResourceReference(ForegroundProperty, key);
    }

    /// <summary>Whether the discrete adapter was enabled the last time anybody looked.</summary>
    private bool? _discreteWasEnabled;

    /// <summary>
    /// Notices the discrete adapter being switched off or on by something
    /// other than this window - Device Manager, the vendor's software - and
    /// drops the NVML handle before it can be used against a device that is
    /// no longer there.
    ///
    /// Our own switches reset the handle directly. This is for everyone else's,
    /// and it has a window of one slow tick in which a stale handle could still
    /// be read; that is the best available without a device-change
    /// notification, and far better than the process ending.
    ///
    /// A WMI query, so off the interface thread: from a single-threaded
    /// apartment every call leaks a kernel handle.
    /// </summary>
    /// <summary>
    /// Re-selects the current mode when its plan has been deleted from under
    /// it. Windows drops to Balanced when the active plan goes; re-applying
    /// makes a Nextcalibur plan and selects it, so the mode survives the
    /// vendor software being removed while this is running.
    /// </summary>
    private void KeepTheModesPlanAlive()
    {
        if (_currentMode is not { } mode || !_support.AllowsReads) return;
        if (SystemModeService.HasPlanFor(mode)) return;

        try
        {
            ApplyMode(mode);
            LoadSystemMode();
            RefreshOverlay();
        }
        catch (Exception)
        {
            // Next tick, or the person picks again; the tabs show what is.
        }
    }

    private async Task WatchForTheCardBeingSwitched()
    {
        bool enabled;
        try
        {
            enabled = await Task.Run(GpuModeService.DiscreteAdapterEnabled);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            return;
        }

        if (_discreteWasEnabled is { } was && was != enabled)
        {
            _gpuClock.Reset();
            LoadGpuMode();
        }

        _discreteWasEnabled = enabled;
    }

    private void StartSlowTimer()
    {
        var slow = new DispatcherTimer { Interval = VisibleSlowInterval };
        slow.Tick += async (_, _) =>
        {
            // Everything below the guard exists to keep the window truthful.
            // While it is in the notification area there is nothing to keep
            // truthful, so none of it runs and the timer itself slows down.
            var onScreen = IsVisible && WindowState != WindowState.Minimized;
            slow.Interval = onScreen ? VisibleSlowInterval : HiddenSlowInterval;

            if (onScreen)
            {
                _hiddenTicks = 0;
                RefreshBanner();
                ApplyPollInterval();
                RefreshStorage();
                if (_theme.PollForChange()) ApplyTheme();
                await WatchForTheCardBeingSwitched();
            }

            // Hidden or not: a registry read, no firmware. The vendor's
            // uninstaller takes its plans with it; the mode this borrowed one
            // for must not stay gone until the next start.
            KeepTheModesPlanAlive();

            // Same registry, other direction: the vendor can be installed
            // while this is running, and the recommendation is meant for the
            // moment it appears, not the next start.
            if (_support.Level != SupportLevel.Unsupported)
            {
                RecommendRemovingVendorSoftwareOnce();
                WatchTheVendorComingAndGoing();
            }

            // The overheat warning is the one thing worth a firmware read while
            // hidden — it is the reason the application stays resident at all.
            if (!_settings.WarnsAboutHeat && !onScreen) return;

            if (_thermal is null) return;

            // Off the user-interface thread for the same reason as Sample: see
            // the note there. This read is the one that keeps the tray tooltip
            // and the overheat warning alive while the window is put away.
            var reader = _thermal;
            ThermalSample s;
            try
            {
                var reading = await Task.Run(
                    () => reader.TryRead(out var value) ? value : (ThermalSample?)null);
                if (reading is null) return;
                s = reading.Value;
            }
            catch (Exception ex) when (ex is not OutOfMemoryException)
            {
                return;
            }

            _lastThermal = s;
            _tray?.UpdateStatus(s.CpuTemperatureC, s.GpuTemperatureC, s.CpuFanRpm);

            // Done after the read, so this tick's own leavings are included.
            if (!onScreen && ++_hiddenTicks >= HiddenTicksPerReclaim)
            {
                _hiddenTicks = 0;
                ReclaimWhileIdle();
            }

            // Each chip against its own threshold, one balloon at a time.
            var cpuLimit = _settings.CpuWarningTemperatureC;
            var gpuLimit = _settings.GpuWarningTemperatureC;
            var hot = s.CpuTemperatureC >= cpuLimit ? $"CPU at {s.CpuTemperatureC} °C"
                    : s.GpuTemperatureC >= gpuLimit ? $"GPU at {s.GpuTemperatureC} °C"
                    : null;
            if (_settings.WarnsAboutHeat && hot is not null)
            {
                if (!_overheatNotified)
                {
                    _overheatNotified = true;
                    _tray?.ShowMessage(hot,
                        "Sustained temperatures this high usually mean the heatsink needs cleaning.");
                }
            }
            else if (s.CpuTemperatureC < cpuLimit - 8 && s.GpuTemperatureC < gpuLimit - 8)
            {
                // Re-arm only after a clear drop, so the balloon cannot flap.
                _overheatNotified = false;
            }
        };
        slow.Start();
    }

    // ------------------------------------------------------------------ power

    private void RefreshOverlay()
    {
        var d = _power.Diagnose();
        OverlayState.Text = PowerOverlays.Describe(d.ActiveOverlay);

        if (!d.NeedsRepair)
        {
            OverlayDetail.Text =
                "Your chosen mode is in charge, and Nextcalibur has made sure the fastest " +
                "mode can no longer hold the processor at full speed while the laptop is idle.";
            OverlayState.Foreground = (SolidColorBrush)FindResource("Good");
            FixButton.Visibility = Visibility.Collapsed;
            return;
        }

        var problems = new List<string>();
        if (d.OverlayIsStuck)
            problems.Add(
                "Windows is set to Best performance, and it is holding your processor at full speed " +
                "even when the laptop is doing nothing. That is why changing modes seems to have no effect.");
        if (d.GuardMissing)
            problems.Add(
                "Nothing is stopping another application from switching to that mode and holding the " +
                "processor at full speed again.");

        OverlayDetail.Text = string.Join(" ", problems);
        OverlayState.Foreground = (SolidColorBrush)FindResource("Warn");
        FixButton.Visibility = Visibility.Visible;
    }

    private void OnRepairClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var outcome = _power.Repair();
            RefreshOverlay();

            var body = outcome.Done.Count > 0
                ? string.Join(Environment.NewLine + Environment.NewLine, outcome.Done)
                : "Nothing needed changing.";
            if (outcome.Blocked is { } blocked)
                body += Environment.NewLine + Environment.NewLine + blocked;

            Dialogs.Tell(outcome.Blocked is null ? "Fixed" : "Partly fixed", body);
        }
        catch (Exception ex)
        {
            Dialogs.Warn("Could not fix it", ex.Message);
        }
    }

    // --------------------------------------------------------------- lighting

    private LedZone SelectedZone =>
        TabZoneB.IsChecked == true ? LedZone.Middle :
        TabZoneC.IsChecked == true ? LedZone.Right :
        LedZone.Left;

    private void LoadLightingUi()
    {
        if (_led is null) return;

        _ledUiReady = false;

        LedPower.IsChecked = _led.State.Enabled;
        EffectFor(_led.State.Effect).IsChecked = true;
        BrightnessSlider.Value = _led.EffectiveBrightnessPercent;
        BrightnessValue.Text = $"{_led.EffectiveBrightnessPercent}%";
        ProfileFor(_led.State.ActiveProfile).IsChecked = true;

        RefreshPreview();
        ApplyEnabledState();

        _ledUiReady = true;
    }

    private RadioButton EffectFor(LedEffect effect) => effect switch
    {
        LedEffect.Breathing => FxBreathing,
        LedEffect.Blink => FxBlink,
        LedEffect.Heartbeat => FxHeartbeat,
        LedEffect.ColourCycle => FxCycle,
        LedEffect.Wave => FxWave,
        _ => FxStatic,
    };

    private LedEffect SelectedEffect =>
        FxBreathing.IsChecked == true ? LedEffect.Breathing :
        FxBlink.IsChecked == true ? LedEffect.Blink :
        FxHeartbeat.IsChecked == true ? LedEffect.Heartbeat :
        FxCycle.IsChecked == true ? LedEffect.ColourCycle :
        FxWave.IsChecked == true ? LedEffect.Wave :
        LedEffect.Static;

    private RadioButton ProfileFor(string name) => name switch
    {
        LedState.Office => ProfOffice,
        LedState.Gaming => ProfGaming,
        LedState.Performance => ProfPerformance,
        _ => ProfUser,
    };

    /// <summary>
    /// Colour cycle and wave generate their own colours in firmware, so the
    /// wheel would be a control with no effect. Switching the lighting off
    /// disables everything, as the vendor software does.
    /// </summary>
    private void ApplyEnabledState()
    {
        var on = LedPower.IsChecked == true;
        var wheelApplies = on && SelectedEffect is not (LedEffect.ColourCycle or LedEffect.Wave);

        foreach (var control in new UIElement[]
                 {
                     TabZoneA, TabZoneB, TabZoneC, SelectAll, ReloadButton,
                     EffectPanel, BrightnessSlider, ProfileRow,
                 })
        {
            control.IsEnabled = on;
        }

        Wheel.IsEnabled = wheelApplies;
        WheelHint.Visibility = on && !wheelApplies ? Visibility.Visible : Visibility.Collapsed;
        BrightnessValue.Opacity = on ? 1.0 : 0.35;
    }

    private void RefreshPreview()
    {
        if (_led is null) return;

        var selected = SelectedZone;
        var all = SelectAll.IsChecked == true;
        var accent = (SolidColorBrush)FindResource("Accent");

        foreach (var (border, zone) in new[]
                 {
                     (PreviewA, LedZone.Left), (PreviewB, LedZone.Middle), (PreviewC, LedZone.Right),
                 })
        {
            var (r, g, b) = _led.State.GetColour(zone);

            // Frozen: this runs on every lighting interaction, and an unfrozen
            // brush keeps a change handler alive on the visual it is attached to.
            var colour = new SolidColorBrush(Color.FromRgb(r, g, b));
            colour.Freeze();

            border.Background = colour;
            border.BorderBrush = all || zone == selected ? accent : Brushes.Transparent;
        }

        Wheel.SelectedColour = ColourOf(selected);
    }

    private Color ColourOf(LedZone zone)
    {
        var (r, g, b) = _led!.State.GetColour(zone);
        return Color.FromRgb(r, g, b);
    }

    /// <summary>
    /// Picks a zone to edit.
    ///
    /// Choosing one clears Select all, because otherwise the tabs do nothing
    /// anyone can see: with every zone selected they are all drawn at full
    /// strength and all lifted, so pressing Zone A, B or C leaves the keyboard
    /// pixel for pixel identical. Clicking a zone tab says "this is the one I
    /// want to edit", and the interface should agree rather than silently
    /// ignore it.
    /// </summary>
    private void OnZoneTabChanged(object sender, RoutedEventArgs e)
    {
        if (!_ledUiReady) return;

        // Assigning IsChecked does not raise Click, which is what
        // OnSelectAllToggled is wired to, so the refresh has to happen here.
        // The markup's own triggers follow the property change by themselves.
        if (SelectAll.IsChecked == true) SelectAll.IsChecked = false;

        RefreshPreview();
    }

    private void OnSelectAllToggled(object sender, RoutedEventArgs e)
    {
        if (!_ledUiReady) return;
        RefreshPreview();
    }

    private void OnColourPicked(object? sender, Color colour)
    {
        if (!_ledUiReady || _led is null) return;

        var target = SelectAll.IsChecked == true ? LedZone.AllKeyboard : SelectedZone;
        RunLighting(() => _led.SetColour(target, colour.R, colour.G, colour.B));
        RefreshPreview();
    }

    private void OnEffectChecked(object sender, RoutedEventArgs e)
    {
        if (!_ledUiReady || _led is null) return;

        var effect = SelectedEffect;
        ApplyEnabledState();
        RunLighting(() => _led.SetEffect(effect));
    }

    private void OnBrightnessChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        var percent = (int)Math.Round(e.NewValue);
        if (BrightnessValue is not null) BrightnessValue.Text = $"{percent}%";
        if (!_ledUiReady || _led is null) return;
        RunLighting(() => _led.SetBrightness(percent));
    }

    private void OnLedPowerToggled(object sender, RoutedEventArgs e)
    {
        ApplyEnabledState();
        if (!_ledUiReady || _led is null) return;

        var on = LedPower.IsChecked == true;
        RunLighting(() => _led.SetEnabled(on));
    }

    private void OnProfileChecked(object sender, RoutedEventArgs e)
    {
        if (!_ledUiReady || _led is null) return;
        if (sender is not RadioButton button || button.Tag is not string name) return;

        RunLighting(() => _led.SetProfile(name));
        LoadDeviceNames();
        LoadSystemMode();
        LoadGpuMode();
        LoadLightingUi();
    }

    private void OnReloadClick(object sender, RoutedEventArgs e)
    {
        if (_led is null) return;
        RunLighting(_led.Apply);
    }

    /// <summary>
    /// Runs a lighting change with sensor sampling paused. Both share the
    /// firmware mailbox, and a sample landing between zone writes makes the
    /// hardware drop them.
    /// </summary>
    private void RunLighting(Action action)
    {
        var wasRunning = _timer.IsEnabled;
        _timer.Stop();

        try
        {
            var mailbox = _mailbox;

            // The write goes to a thread-pool thread for the same reason the
            // sensor read does: System.Management needs an MTA thread, and
            // every call made from this one leaves a kernel handle behind. A
            // colour-wheel drag was measured pushing the count to 1183 before a
            // collection took it back.
            //
            // The hold is taken inside the lambda, on the thread that does the
            // writing. Monitor is thread-affine: holding it here and writing
            // there would block that thread against a lock this one owns, and
            // this one is waiting for it. Stopping the timer keeps a new sample
            // from starting; the hold is what keeps a multi-zone write together
            // against one already in flight.
            //
            // This still blocks the interface thread, exactly as before. That
            // is deliberate — it preserves the ordering every caller here
            // assumes, and the throttling that keeps a drag from queueing
            // hundreds of writes the hardware would lag behind. Only the
            // apartment changes.
            Task.Run(() =>
            {
                using (mailbox?.Hold()) action();
            }).GetAwaiter().GetResult();
        }
        catch (EcMailboxUnavailableException)
        {
            // The message deliberately names the likely cause rather than
            // repeating the exception, which talks about mailboxes and retries.
            Dialogs.Warn("Lighting",
                "The keyboard lighting did not respond. If Casper's Control Center is open, " +
                "close it and try again.");
        }
        finally
        {
            if (wasRunning) _timer.Start();
        }
    }

    // ----------------------------------------------------------------- shared

    /// <summary>
    /// Decides what the banner should say right now, and hides it when there is
    /// nothing to say.
    ///
    /// This is re-evaluated on a timer rather than only at startup: the most
    /// common reason for the banner to appear is the vendor software running,
    /// and the user's natural response is to close it. A warning that stays up
    /// after the problem is gone teaches people to ignore warnings.
    /// </summary>
    /// <summary>
    /// Sets the sampling interval for present conditions. Sharing the mailbox
    /// with the vendor software means backing off rather than competing.
    /// </summary>
    private void ApplyPollInterval()
    {
        var ms = VendorSoftware.PollIntervalMs(_settings.PollIntervalMs);
        if (Math.Abs(_timer.Interval.TotalMilliseconds - ms) < 1) return;
        _timer.Interval = TimeSpan.FromMilliseconds(ms);
    }

    private void RefreshBanner()
    {
        if (_mailboxFailed || !_mailboxSupported)
        {
            // Two very different situations used to share one message. Telling
            // somebody their laptop is unsupported when the real answer is
            // "you declined a permission prompt a moment ago" is worse than
            // saying nothing: it reads as final, and it is wrong.
            if (MailboxAccess.Check() == MailboxAvailability.AccessNotGranted)
            {
                ShowBanner(
                    "Sensors need permission",
                    "This laptop has the interface Nextcalibur reads, but this account has not " +
                    "been allowed to use it yet. Reopen Nextcalibur to be asked again - it takes " +
                    "one confirmation, once.",
                    (SolidColorBrush)FindResource("Warn"));
                return;
            }

            ShowBanner(
                "This laptop isn't supported",
                "Nextcalibur can't find the sensors and lighting on this machine, so those " +
                "pages won't work. Power settings still do.",
                (SolidColorBrush)FindResource("Bad"));
            return;
        }

        if (VendorSoftware.IsRunning())
        {
            ShowBanner(
                "Casper's Control Center is open",
                "Both apps are talking to the same hardware, so readings may stall and " +
                "lighting changes may not stick. Close it and this message will clear itself.",
                (SolidColorBrush)FindResource("Warn"));
            return;
        }

        Banner.Visibility = Visibility.Collapsed;
    }

    private void ShowBanner(string title, string body, SolidColorBrush colour)
    {
        BannerTitle.Text = title;
        BannerTitle.Foreground = colour;
        Banner.BorderBrush = colour;
        BannerBody.Text = body;
        Banner.Visibility = Visibility.Visible;
    }

}

