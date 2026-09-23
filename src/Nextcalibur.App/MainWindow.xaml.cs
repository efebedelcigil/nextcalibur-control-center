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

    /// <summary>Package power through PawnIO; reads nothing when it cannot.</summary>
    private readonly CpuPowerReader _cpuPower = new();

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
    private DispatcherTimer? _slowTimer;

    /// <summary>Slow-timer cadence while the window is on screen.</summary>
    private static readonly TimeSpan VisibleSlowInterval = TimeSpan.FromSeconds(5);

    /// <summary>
    /// The hidden interval, once every minute. Hidden, the only reason to
    /// touch the firmware at all is the overheat warning, and nothing this
    /// application can say about a temperature is worth saying twice a
    /// minute.
    /// </summary>
    private static readonly TimeSpan HiddenInterval = TimeSpan.FromMinutes(1);

    /// <summary>
    /// And once every two minutes while the machine is over its threshold
    /// and has been told so.
    ///
    /// The first shape of this had it the other way round - thirty seconds
    /// while hot, a minute while cool - which is precisely backwards. A
    /// laptop that is overheating is a laptop that cannot spare the work,
    /// and every read costs this process, the WMI host that serves it, and
    /// a slice of whatever is making the heat. Reading more often at that
    /// moment adds to the problem it is there to report.
    ///
    /// Nothing is lost by backing off: the warning has already been given,
    /// it is given once, and what the reads are watching for afterwards is
    /// the machine cooling down - which nobody needs to hear about within
    /// thirty seconds.
    /// </summary>
    private static readonly TimeSpan HiddenHotInterval = TimeSpan.FromMinutes(2);

    /// <summary>
    /// How many hidden ticks pass between idle reclamations. A hidden tick
    /// is a minute now (two while the machine is hot), so five of them is
    /// the five minutes this used to mean. See <see cref="ReclaimWhileIdle"/>.
    /// </summary>
    private const int HiddenTicksPerReclaim = 5;

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
    private string? _exitRoute;   // for the one exit line in the log, whichever way it was asked

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
    private sealed record DeferredOverheat(bool IsCpu, int MaxTemperatureC, DateTime TimestampUtc);
    private DeferredOverheat? _deferredOverheat;
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
        // The words first, so the markup's DynamicResources find them at
        // the first render; the choice is remembered, or Windows' language
        // the first time.
        Strings.Apply(Strings.Initial(_settings));
        Strings.Changed += OnLanguageChanged;
        InitializeComponent();

        // Every dialogue the application raises is owned by this window from
        // here on, which is what makes it modal: nothing else in the window
        // responds until it is answered.
        Dialogs.Owner = this;

        ApplyPollInterval();
        WindowsFaults.IsFaultEnabled = id => id switch
        {
            "input-host" => _settings.FixTextInputHost,
            "cross-device" => _settings.FixCrossDeviceService,
            "widgets" => _settings.FixWidgets,
            _ => true
        };
        Loaded += OnLoaded;
        ContentRendered += (_, _) => HasRendered = true;
        StateChanged += OnStateChanged;
        Activated += (_, _) =>
        {
            SetProcessPriority(PriorityNormal);
            // Only when it differs: setting a DispatcherTimer's interval
            // restarts its countdown, and somebody switching windows every
            // few seconds would otherwise hold the slow tick - and with it
            // the overheat check - off for as long as they kept doing it.
            if (_slowTimer is not null && IsVisible && WindowState != WindowState.Minimized
                && _slowTimer.Interval != VisibleSlowInterval)
            {
                _slowTimer.Interval = VisibleSlowInterval;
            }
            SyncSamplingToScreen();
        };
        Deactivated += (_, _) =>
        {
            SetProcessPriority(PriorityBelowNormal);
            SyncSamplingToScreen();
        };

        // Not StateChanged: closing to the tray hides the window rather than
        // minimising it, so the state never changes and the handler never runs.
        IsVisibleChanged += (_, e) =>
        {
            if (e.NewValue is true)
            {
                RefreshLightingFromHardware();
                TryShowDeferredOverheat();
            }
            // Hide-to-tray stops the sampling; Show from the tray leaves the
            // window state where it was, so StateChanged never fires and the
            // readings stayed frozen until the next minimise (found 12
            // September 2026). Visibility is the other half of the same rule.
            SyncSamplingToScreen();
        };
        Closing += OnClosing;
        DriveList.ItemsSource = _drives;
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
        _updates.UpdateFound += (_, version) => OnUpdateFound(version);
        _updates.DependencyFound += (_, status) => AnnounceDependency(status);
        _tray.BalloonClicked += (_, _) =>
        {
            if (_pendingDependency is { } dependency) { _pendingDependency = null; OfferDependency(dependency); }
            else if (_updates.Available is not null) OfferUpdate();
        };
        _tray.CheckForUpdatesRequested += (_, _) => CheckForUpdatesByHand();
        _updates.AutomaticChecksEnabled = () => _settings.AutoCheckForUpdates;

        // The window's switch and the tray's menu are two faces of one setting.
        OverheatWarningToggle.IsChecked = _settings.OverheatWarningEnabled;
        OverheatWarningToggle.Checked += (_, _) => SetOverheatWarning(true);
        OverheatWarningToggle.Unchecked += (_, _) => SetOverheatWarning(false);
        _tray.OverheatSettingChanged += (_, _) => Dispatcher.BeginInvoke(() =>
            OverheatWarningToggle.IsChecked = _settings.OverheatWarningEnabled);
        LoadOverheatThresholds();
        WireSettingsPage();
        VersionText.Text = RunningVersion();
        _updates.CheckedByHand += (_, what) => Dialogs.Tell(Strings.Get("S.Update.Title"), what);
        _updates.Start();

        if (_settings.StartMinimised &&
            Environment.GetCommandLineArgs().Contains("--tray", StringComparer.OrdinalIgnoreCase))
        {
            Hide();
        }

        // Said before anything is read, because it explains readings that are
        // about to look unreliable.
        RecommendRemovingVendorSoftwareOnce();

        // The gate. Everything below is allowed or not by what the machine has
        // just said about itself, and an unsupported one is locked before a
        // single control is wired.
        _support = HardwareSupport.Check();
        Log.Info("support", $"{_support.Level}: {string.Join(" | ", _support.Reasons)}");
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
        WatchTheEssentialDrivers();
        RefreshBanner();
        StartSlowTimer();
        Microsoft.Win32.SystemEvents.PowerModeChanged += OnPowerSourceMayHaveChanged;
        Microsoft.Win32.SystemEvents.SessionSwitch += OnSessionSwitch;

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
        Log.Info("vendor", "The vendor's software was removed while running");

        // Its uninstaller narrows the firmware permission back to
        // administrators, which an elevated process does not notice; only the
        // dropped mode needs putting back.
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
                    Strings.Get("S.Banner.ReadOnlyTitle"),
                    Strings.Get("S.Banner.ReadOnlyBody") + " " + string.Join(" ", _support.Reasons),
                    (SolidColorBrush)FindResource("Warn"));
                return;

            case SupportLevel.Unsupported:
                foreach (var nav in new[] { NavSystem, NavPower, NavDisplay, NavLighting })
                    nav.IsEnabled = false;
                foreach (var page in new[] { PageSystem, PagePower, PageDisplay, PageLighting })
                    page.IsEnabled = false;
                LockSettingsPage();
                // The readings panel sits outside the pages since 0.5.1, so
                // its controls need locking on their own - and its numbers
                // are settings for a warning that will never fire here.
                foreach (var control in new UIElement[] { OverheatWarningToggle, CpuWarnSlider, GpuWarnSlider, CpuWarnValue, GpuWarnValue, OverheatResetButton, UpdateNowButton, OpenLogButton })
                    control.IsEnabled = false;
                OverheatWarningToggle.IsChecked = false;
                CpuWarnValue.Text = GpuWarnValue.Text = "--";
                ShowBanner(
                    Strings.Get("S.Banner.UnsupportedTitle"),
                    Strings.Get("S.Banner.UnsupportedBody") + " " + string.Join(" ", _support.Reasons),
                    (SolidColorBrush)FindResource("Bad"));
                OfferRemovalOnUnsupportedMachine();
                return;
        }
    }

    private void OfferRemovalOnUnsupportedMachine()
    {
        var remove = Dialogs.Ask(Strings.Get("S.Unsupported.Title"), Strings.Get("S.Unsupported.Body"), defaultNo: false);

        if (!remove) return;

        try
        {
            Unelevated.Open("ms-settings:appsfeatures");
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

        var open = Dialogs.Ask(Strings.Get("S.Vendor.Title"), Strings.Get("S.Vendor.Body", vendor.Name), defaultNo: false);

        if (open)
        {
            try
            {
                Unelevated.Open("ms-settings:appsfeatures");
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

    /// <summary>Exit from the tray menu: the same question, then the same close.</summary>
    private void Exit()
    {
        if (_dialogOpen) return;
        // The question is the window's own dialogue; bring the window up to ask it.
        if (!IsVisible || WindowState == WindowState.Minimized)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        var pending = _gpuPendingRestart is { } mode
            ? Strings.Get("S.Exit.PendingGraphics", mode) + Environment.NewLine + Environment.NewLine
            : string.Empty;
        if (!Dialogs.Ask(Strings.Get("S.Exit.Title"),
                pending + Strings.Get("S.Exit.Body"),
                defaultNo: true))
        {
            _exitRoute = null;
            return;
        }
        _exiting = true;
        _exitRoute ??= "the tray menu";
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
        KeepMinimiseBox();
        if (WindowState != WindowState.Minimized)
        {
            TryShowDeferredOverheat();
        }
        else
        {
            SetProcessPriority(PriorityBelowNormal);
        }
        SyncSamplingToScreen();
    }

    /// <summary>
    /// Sampling costs a firmware round trip and there is nothing to update
    /// while the window is not on screen, so it runs exactly when the window
    /// is visible and not minimised - whichever of the two just changed.
    /// </summary>
    private void SyncSamplingToScreen()
    {
        if (!ReadingsAreOnScreen())
        {
            _timer.Stop();
            TrimWorkingSet();
        }
        else if (_thermal is not null && !_timer.IsEnabled)
        {
            _timer.Start();
            Sample();   // now, not two seconds from now
        }
    }

    /// <summary>
    /// Whether anything on screen is showing a reading.
    ///
    /// The readings panel is on every page but Lighting and Settings, and
    /// the fast timer exists to keep it truthful. On those two pages it was
    /// still reading the firmware every couple of seconds for a panel that
    /// is not there - and every read is a write to the mailbox, which
    /// raises a system-management interrupt that stops all cores. Somebody
    /// choosing a colour was paying thirty of those a minute for nothing.
    ///
    /// The slow timer carries on either way: the tray tooltip and the
    /// overheat warning are not on any page.
    /// </summary>
    private bool ReadingsAreOnScreen()
    {
        if (!IsVisible || WindowState == WindowState.Minimized) return false;
        if (!IsActive && (Nextcalibur.Core.Hardware.UserPresence.WouldRatherNotBeDisturbed() || Nextcalibur.Core.Hardware.UserPresence.NobodyIsWatching() || Nextcalibur.Core.Hardware.UserPresence.IsGamingOrHeavyLoad())) return false;
        if (PageLighting is { Visibility: Visibility.Visible }) return false;
        if (PageSettings is { Visibility: Visibility.Visible }) return false;
        return true;
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
            if (_dialogOpen)
            {
                e.Cancel = true;
                return;
            }

            // The question is the window's own dialogue, so the window has
            // to be on screen to ask it: a close from the taskbar's menu
            // arrives with the window minimised, and Windows' box is not
            // the look this application has.
            // ...but a window cannot be shown while it is closing (WPF throws,
            // and the taskbar's "Close window" found that out). So: refuse
            // this close, bring the window up, and ask from there - Exit()
            // asks and then closes for real.
            if (!IsVisible || WindowState == WindowState.Minimized)
            {
                e.Cancel = true;
                _exitRoute ??= "the taskbar";
                Dispatcher.BeginInvoke(Exit);
                return;
            }

            if (!Dialogs.Ask(Strings.Get("S.Exit.Title"), Strings.Get("S.Exit.BodyFromClose"), defaultNo: true))
            {
                e.Cancel = true;
                return;
            }
            _exiting = true;
            _exitRoute ??= "the close button";
        }

        if (_settingsSaveDelay is { IsEnabled: true }) { _settingsSaveDelay.Stop(); _settings.Save(); }
        Log.Info("exit", _sessionEnding ? "Windows is ending the session" : $"Exit confirmed from {_exitRoute ?? "the update restart"}");

        {
            _timer.Stop();
            _slowTimer?.Stop();
            _settingsSaveDelay?.Stop();
            _gpuCooldown?.Stop();
            _lightingThrottle?.Stop();
            if (_tourActive) EndTour();
            Microsoft.Win32.SystemEvents.PowerModeChanged -= OnPowerSourceMayHaveChanged;
            Microsoft.Win32.SystemEvents.SessionSwitch -= OnSessionSwitch;
            Strings.Changed -= OnLanguageChanged;
            WindowsFaults.IsFaultEnabled = _ => true;
            Dialogs.Owner = null;
            _tray?.Dispose();
            _backlightKey?.Dispose();
            _mailbox?.Dispose();
            _cpuClock.Dispose();
            _gpuClock.Dispose();
            _cpuPower.Dispose();
            _settings.Save();

            // Last, and after everything else is released: this hands a staged
            // release to an installer that waits for this process to go away.
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
        Hide();   // IsVisibleChanged stops the sampling and trims
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

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
    private static extern bool SetPriorityClass(IntPtr hProcess, uint dwPriorityClass);

    private const uint PriorityNormal = 0x00000020;
    private const uint PriorityBelowNormal = 0x00004000;
    private static uint _currentPriority = PriorityNormal;

    private static void SetProcessPriority(uint priority)
    {
        if (_currentPriority == priority) return;
        try
        {
            if (SetPriorityClass(GetCurrentProcess(), priority))
            {
                _currentPriority = priority;
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
        }
    }

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
        // The first pass queues the finalisers, the wait runs them - which is
        // what actually closes the handles - and the second reclaims what they
        // released. Blocking here is safe: the window is hidden, so the thread
        // this runs on has nothing to draw.
        //
        // Generation one, and no compacting. What needs collecting is a
        // handful of wrappers left by a firmware read a minute or two ago;
        // hidden, the application allocates almost nothing, so nothing has
        // aged out of the young generations and nothing older needs
        // touching. Collecting the whole 230 MB heap instead - which is
        // what GC.Collect() means - turned out to cost more processor than
        // every firmware read between two reclamations put together
        // (measured 13 September 2026: it was most of the hidden cost, not
        // the reads it cleans up after).
        GC.Collect(1, GCCollectionMode.Forced, blocking: true, compacting: false);
        GC.WaitForPendingFinalizers();
        GC.Collect(1, GCCollectionMode.Forced, blocking: true, compacting: false);

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
        ShowSettingsPageIfChosen();

        // Both pages describe state that other software can change while we are
        // running, so re-read it when the page comes into view rather than
        // showing whatever was true at startup.
        if (NavPower.IsChecked == true) { RefreshOverlay(); LoadPowerModes(); }
        if (NavDisplay.IsChecked == true) LoadGpuMode();

        // Lighting and Settings have no readings panel, so nothing needs the
        // firmware every two seconds while one of them is open.
        SyncSamplingToScreen();
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

        // The Power Mode cards follow the mode: what is allowed changed.
        if (PowerModeEfficiency is not null) LoadPowerModes();
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
        if (_currentMode == target)
        {
            // Windows already has the plan and the overlay; the firmware may
            // not have the profile - a cold boot can leave the controller on
            // its own default while Windows restores the plan. One read, and
            // a write only when they disagree.
            EnsureFirmwareProfile(target);
            return;
        }

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
    private void EnsureFirmwareProfile(SystemMode mode)
    {
        if (_mailbox is null || !_support.AllowsWrites) return;
        try
        {
            if (ThermalProfile.Read(_mailbox) == mode) return;
            ThermalProfile.Write(_mailbox, mode);
            Log.Info("mode", $"Firmware profile put back to {mode} at start");
        }
        catch (EcMailboxUnavailableException)
        {
        }
    }

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
        var source = Strings.Get(_onBattery ? "S.Status.OnBattery" : "S.Status.OnAc");
        _lastModeShown = current;
        _modeStatus = current is { } mode
            ? Strings.Get("S.Status.Mode", mode, source)
            : Strings.Get("S.Status.NoMode", SystemModeService.DescribeCurrent(), source);
        ShowSubtitle();
    }

    private void ShowSubtitle() =>
        SubtitleText.Text = _lastSampleAt is { } at
            ? $"{_modeStatus}  ·  {Strings.Get("S.Status.Updated", at.ToString("HH:mm:ss"))}"
            : _modeStatus;

    /// <summary>The mode the status line last described, so it can be re-said in a new language.</summary>
    private SystemMode? _lastModeShown;

    /// <summary>
    /// Everything the code-behind put on screen in words, said again in the
    /// language just chosen. The markup's own texts follow the dictionary
    /// swap by themselves.
    /// </summary>
    private void OnLanguageChanged(object? sender = null, EventArgs? e = null)
    {
        if (PageSystem is null) return;   // before the tree is built
        RefreshModeStatus(_lastModeShown);
        UpdateNowButton.Content = _updates.Available is not null && _updates.AvailableVersion is { } v
            ? Strings.Get("S.Corner.UpdateTo", v)
            : Strings.Get("S.Corner.UpToDate");
        OpenLogButton.Content = Strings.Get("S.Corner.OpenLog");
        _tray?.RebuildMenu();
        LoadSettingsPage();
        if (_tourActive) PlaceTourStep();   // the card's words, in the new language
        Log.Info("language", $"Switched to {Strings.Current}");
    }

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
            Log.Info("mode", $"Chosen: {mode}");
            ApplyMode(mode);
            _currentMode = mode;
            _battery.UserChose(mode, _onBattery);
            _settings.LastSystemMode = mode;
            _settings.Save();
            RefreshModeStatus(mode);
            RefreshOverlay();
            // The Power Mode cards' range follows the mode; without this the
            // page kept the previous mode's cards until the next full reload.
            LoadPowerModes();
        }
        catch (Exception ex)
        {
            Dialogs.Warn(Strings.Get("S.Mode.ChangeFailed"), ex.Message);
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
        if (e.Mode is Microsoft.Win32.PowerModes.Resume or Microsoft.Win32.PowerModes.Suspend)
        {
            Nextcalibur.Core.Hardware.WindowsFaults.Forget();
            _gpuClock.ResetAwakeFault();
            _currentGpuFaultText = null;
            if (e.Mode == Microsoft.Win32.PowerModes.Resume)
            {
                Dispatcher.BeginInvoke(() => _led?.Apply());
            }
            return;
        }

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
                Log.Info("power", $"{(onBattery ? "On battery" : "Charger back")}; mode {_currentMode} -> {(next?.ToString() ?? "unchanged")}");
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

    private void OnSessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
    {
        if (e.Reason is Microsoft.Win32.SessionSwitchReason.SessionLock)
        {
            UserPresence.RecordSessionLock(true);
        }
        else if (e.Reason is Microsoft.Win32.SessionSwitchReason.SessionUnlock)
        {
            UserPresence.RecordSessionLock(false);
            Nextcalibur.Core.Hardware.WindowsFaults.Forget();
            Dispatcher.InvokeAsync(TryShowDeferredOverheat);
        }
    }

    // ------------------------------------------------------------ graphics

    /// <summary>
    /// Detection is a WMI query of the video controllers, and that takes
    /// long enough to be felt as a stall when it runs on the interface
    /// thread - it did, every time the Display page was opened, and it
    /// leaked a handle a call from the STA thread besides. So it runs on
    /// the pool and the page is filled in when it answers; an answer
    /// overtaken by a newer request is dropped.
    /// </summary>
    private int _gpuDetectSequence;

    private void LoadGpuMode()
    {
        var sequence = ++_gpuDetectSequence;
        Task.Run(() =>
        {
            var config = _gpu.Detect();
            return (config, load: IdleWatts(config));
        }).ContinueWith(t =>
        {
            if (t.IsFaulted || sequence != _gpuDetectSequence) return;
            ApplyGpuMode(t.Result.config, t.Result.load);
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void ApplyGpuMode(GpuConfiguration config, GpuLoad? load)
    {
        _currentGpuMode = config.Mode;
        var detail = GpuModeService.Describe(config, load, _lastThermal);
        if (_currentGpuFaultText is not null)
            detail += "\n\n" + _currentGpuFaultText;
        GpuModeDetail.Text = detail;

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
            ? Strings.Get("S.Gpu.RestartPending", pending)
            : string.Empty;
        GpuRestartPending.Visibility = _gpuPendingRestart is null ? Visibility.Collapsed : Visibility.Visible;

        if (config.Mode is not { } mode) return;

        // Showing the machine's state is not the person asking to change it;
        // the Checked handler must not take it for a click (the same guard
        // the mode tabs and the lighting page have).
        _gpuUiReady = false;
        GpuButtonFor(mode).IsChecked = true;
        _gpuUiReady = true;
    }

    /// <summary>False while the graphics buttons are being set from a reading rather than by a click.</summary>
    private bool _gpuUiReady = true;

    // ------------------------------------------------------------- updates

    /// <summary>
    /// A release was found. Say so through the tray and light the corner
    /// button; the offer itself waits for a click, because a question that
    /// pops up on its own is the thing the person asked not to have.
    ///
    /// Said once per process. A "no" is remembered for the rest of the
    /// session - the six-hourly checks find the same release and say
    /// nothing more - and forgotten at the next start, where the first
    /// check says it again, once. The owner's rule, 12 September 2026: remind
    /// at startup, never nag in between.
    /// </summary>
    private void OnUpdateFound(string version)
    {
        UpdateNowButton.IsEnabled = true;
        UpdateNowButton.Content = Strings.Get("S.Corner.UpdateTo", version);
        if (_checkingByHand) return;

        // Installing without asking is a setting of its own, off by default,
        // and it steps aside while a graphics change waits for a restart:
        // that restart is the person's to time.
        if (_settings.AutoInstallUpdates && _gpuPendingRestart is null)
        {
            Log.Info("update", $"{version} found; installing automatically (the setting is on)");
            _tray?.ShowMessage(Strings.Get("S.Update.AutoTitle", version), Strings.Get("S.Update.AutoBody"));
            _ = InstallUpdateAsync(version);
            return;
        }

        // A toast with an "Update now" button; the balloon when a toast
        // cannot be shown. Either way a click brings the window and the
        // question, and the answer is the person's.
        if (!Toasts.TryShow(Strings.Get("S.Update.AvailableTitle", version), Strings.Get("S.Update.AvailableToast"), Strings.Get("S.Update.Now"),
                () => { _tray?.ShowWindowFromOutside(); OfferUpdate(); }))
            _tray?.ShowMessage(Strings.Get("S.Update.AvailableTitle", version), Strings.Get("S.Update.AvailableBalloon"));
    }

    /// <summary>Set while a check the person asked for runs, so the offer comes as a dialogue rather than a balloon.</summary>
    private bool _checkingByHand;

    /// <summary>A check the person asked for, from the tray or the Settings page: the answer is a dialogue either way.</summary>
    private async void CheckForUpdatesByHand()
    {
        if (_checkingByHand) return;
        _checkingByHand = true;
        try
        {
            await _updates.CheckNowAsync();
            if (_updates.Available is not null) OfferUpdate();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Error("updates", "Manual update check failed", ex);
        }
        finally
        {
            _checkingByHand = false;
        }
    }

    private void OnUpdateNowClick(object sender, RoutedEventArgs e) => OfferUpdate();

    private void OnOpenLogClick(object sender, RoutedEventArgs e)
    {
        try { Unelevated.Open(Log.Folder); }
        catch (Exception ex) when (ex is Win32Exception or InvalidOperationException) { }
    }

    private bool _updating;

    /// <summary>The question, then the work: download in front of them, restart into the new version.</summary>
    private async void OfferUpdate()
    {
        if (_updating || _updates.Available is null) return;

        try
        {
            var version = _updates.AvailableVersion ?? Strings.Get("S.Update.ANewVersion");
            var pending = _gpuPendingRestart is { } mode
                ? Strings.Get("S.Update.PendingGraphics", mode) + Environment.NewLine + Environment.NewLine
                : string.Empty;
            if (!Dialogs.Ask(Strings.Get("S.Update.OfferTitle", version),
                    pending + Strings.Get("S.Update.OfferBody", version),
                    defaultNo: _gpuPendingRestart is not null))
            {
                // Declined: the corner button stays, nothing else will ask.
                return;
            }

            Log.Info("update", $"Accepted {version}; downloading");
            await InstallUpdateAsync(version);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Error("updates", "Offer update failed", ex);
        }
    }

    /// <summary>The work, after the question or instead of it: download in front of them, restart into the new version.</summary>
    private async Task InstallUpdateAsync(string version)
    {
        if (_updating) return;
        _updating = true;
        try
        {
            ShowProgress(Strings.Get("S.Update.Progress", version), Strings.Get("S.Progress.Downloading"));
            var progress = new Progress<int>(p =>
            {
                DialogProgress.Value = p;
                DialogBodyText.Text = p < 100 ? Strings.Get("S.Progress.DownloadingPercent", p) : Strings.Get("S.Update.InstallingRestarting");
            });
            _exiting = true;
            _settings.Save();
            await _updates.DownloadAndRestartAsync(progress);
            // Only reached if the restart did not happen.
            _exiting = false;
            HideProgress();
            Dialogs.Warn(Strings.Get("S.Update.Title"), Strings.Get("S.Update.NoRestart"));
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            _exiting = false;
            HideProgress();
            Dialogs.Warn(Strings.Get("S.Update.Title"), Strings.Get("S.Update.Failed") + " " + ex.Message);
        }
        finally
        {
            _updating = false;
        }
    }

    /// <summary>
    /// A dependency is missing or behind: the question, then the work, in
    /// the window's own dialogue. No downloads the "no"; the next start asks
    /// again, the checks in between do not.
    /// </summary>
    /// <summary>A dependency found by the timer while the window is away, waiting for the click that brings it back.</summary>
    private Nextcalibur.Core.Dependencies.DependencyStatus? _pendingDependency;

    /// <summary>
    /// A dependency is missing or behind. With the window on screen, the
    /// question is asked there; with the window away, a question would be
    /// Windows' own box floating out of nowhere (the tray's Exit had the
    /// same fault), so it is a notification instead, and the click brings
    /// the window and the question together.
    /// </summary>
    private void AnnounceDependency(Nextcalibur.Core.Dependencies.DependencyStatus status)
    {
        if (IsVisible && WindowState != WindowState.Minimized && HasRendered)
        {
            OfferDependency(status);
            return;
        }

        var install = status.Missing;
        _pendingDependency = status;
        var title = Strings.Get(install ? "S.Dependency.InstallAvailable" : "S.Dependency.UpdateAvailable", status.Dependency.Name);
        if (!Toasts.TryShow(title, status.Describe(), Strings.Get(install ? "S.Dependency.Install" : "S.Dependency.Update"),
                () => { _tray?.ShowWindowFromOutside(); _pendingDependency = null; OfferDependency(status); }))
            _tray?.ShowMessage(title, Strings.Get(install ? "S.Dependency.ClickToInstall" : "S.Dependency.ClickToUpdate"));
    }

    /// <summary>
    /// Offers waiting for the one in progress. Two dependencies can need
    /// something at the same moment - a fresh machine needs the runtime and
    /// PawnIO both - and the check raises them one after the other, faster
    /// than a person can answer. They used to be dropped: the second was
    /// never mentioned, and the reading it serves showed "--" with nothing
    /// said about why.
    /// </summary>
    private readonly Queue<Nextcalibur.Core.Dependencies.DependencyStatus> _dependenciesWaiting = new();

    private async void OfferDependency(Nextcalibur.Core.Dependencies.DependencyStatus status)
    {
        if (_updating)
        {
            if (!_dependenciesWaiting.Any(waiting => waiting.Dependency.Id == status.Dependency.Id))
                _dependenciesWaiting.Enqueue(status);
            return;
        }

        try
        {
            var install = status.Missing;
            if (!Dialogs.Ask(Strings.Get(install ? "S.Dependency.InstallTitle" : "S.Dependency.UpdateTitle", status.Dependency.Name),
                    status.Describe() + Environment.NewLine + Environment.NewLine +
                    Strings.Get("S.Dependency.How") +
                    Environment.NewLine + Environment.NewLine + Strings.Get(install ? "S.Dependency.InstallNow" : "S.Dependency.UpdateNow"),
                    defaultNo: false))
            {
                _updates.DeclineDependency(status);
                OfferTheNextDependency();
                return;
            }

            _updating = true;
            try
            {
                ShowProgress(Strings.Get(install ? "S.Dependency.Installing" : "S.Dependency.Updating", status.Dependency.Name), Strings.Get("S.Progress.Downloading"));
                var progress = new Progress<int>(p =>
                {
                    DialogProgress.Value = p;
                    DialogBodyText.Text = p < 100 ? Strings.Get("S.Progress.DownloadingPercent", p) : Strings.Get("S.Progress.Installing");
                });
                var (ok, message, restartRequired) = await Nextcalibur.Core.Dependencies.DependencyManager.InstallAsync(status, progress);
                Log.Info("dependency", message);
                HideProgress();
                if (ok)
                {
                    if (restartRequired)
                    {
                        _settings.RequestRestart("installer", status.Dependency.Name);
                        _settings.Save();
                        _restartBannerDismissed = false;
                        RefreshBanner();
                    }
                    else if (status.Dependency is Nextcalibur.Core.Dependencies.PawnIoDependency)
                    {
                        _cpuPower.Reset();
                        if (!_cpuPower.CanOpen())
                        {
                            _settings.RequestRestart("pawnio");
                            _settings.Save();
                            _restartBannerDismissed = false;
                            RefreshBanner();
                        }
                    }

                    Dialogs.Tell(Strings.Get("S.Dependency.Title"), message);
                }
                else Dialogs.Warn(Strings.Get("S.Dependency.Title"), message);
            }
            catch (Exception ex) when (ex is not OutOfMemoryException)
            {
                // Whatever the installer did, the progress panel must not be left
                // over the window with no button: it takes every click.
                Log.Error("dependency", "Install failed", ex);
                HideProgress();
                Dialogs.Warn(Strings.Get("S.Dependency.Title"), Strings.Get("S.Dependency.Failed", status.Dependency.Name) + " " + ex.Message);
            }
            finally
            {
                _updating = false;
                OfferTheNextDependency();
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Error("dependency", "OfferDependency failed", ex);
            _updating = false;
            OfferTheNextDependency();
        }
    }

    /// <summary>
    /// The next offer, once the current one is done with. Through the
    /// dispatcher so that this call unwinds first: the offer opens a
    /// dialogue of its own, and one nested message loop inside another's
    /// finally block is not a place to be.
    /// </summary>
    private void OfferTheNextDependency()
    {
        if (_updating || _dependenciesWaiting.Count == 0) return;
        var next = _dependenciesWaiting.Dequeue();
        Dispatcher.BeginInvoke(new Action(() => AnnounceDependency(next)), DispatcherPriority.Background);
    }

    /// <summary>The overlay as a progress panel: title, a line, a bar, no buttons. Not modal - nothing waits on it.</summary>
    private void ShowProgress(string title, string body)
    {
        DialogTitleText.Text = title;
        DialogBodyText.Text = body;
        DialogButtonPrimary.Visibility = DialogButtonSecondary.Visibility = Visibility.Collapsed;
        DialogProgress.Value = 0;
        DialogProgress.Visibility = Visibility.Visible;
        ModalDialogOverlay.Visibility = Visibility.Visible;
    }

    private void HideProgress()
    {
        DialogProgress.Visibility = Visibility.Collapsed;
        DialogButtonPrimary.Visibility = Visibility.Visible;
        ModalDialogOverlay.Visibility = Visibility.Collapsed;
    }

    /// <summary>True once the first frame has been drawn; the in-window dialogue is safe from then on.</summary>
    public bool HasRendered { get; private set; }

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
            MessageBoxButton.YesNo => (MessageBoxResult.Yes, MessageBoxResult.No, Strings.Get("S.Dialog.Yes"), Strings.Get("S.Dialog.No")),
            MessageBoxButton.OKCancel => (MessageBoxResult.OK, MessageBoxResult.Cancel, Strings.Get("S.Dialog.OK"), Strings.Get("S.Dialog.Cancel")),
            _ => (MessageBoxResult.OK, (MessageBoxResult?)null, Strings.Get("S.Dialog.OK"), string.Empty),
        };

        DialogTitleText.Text = title;
        DialogBodyText.Text = body;
        DialogButtonPrimary.Content = primaryText;
        DialogButtonSecondary.Content = secondaryText;
        DialogButtonSecondary.Visibility = secondary is null ? Visibility.Collapsed : Visibility.Visible;
        DialogButtonPrimary.Visibility = Visibility.Visible;
        DialogProgress.Visibility = Visibility.Collapsed;

        // One at a time. A second question raised while the first is up - a
        // timer's dependency offer landing on the exit question, say - would
        // share the same buttons, and one click would answer both: the
        // second with the click meant for it, the first with a click never
        // meant for it. The later question gets its safe answer and is
        // logged; whatever raised it asks again in its own time.
        if (_dialogOpen)
        {
            Log.Warn("ui", $"A dialogue was raised over another and answered with its default: {title}");
            return fallback;
        }
        _dialogOpen = true;

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
            _dialogOpen = false;
        }

        return result;
    }

    private bool _dialogOpen;

    private void SetOverheatWarning(bool on)
    {
        CpuWarnSlider.IsEnabled = GpuWarnSlider.IsEnabled = on;
        if (_settings.OverheatWarningEnabled == on) return;
        _settings.OverheatWarningEnabled = on;
        _settings.Save();
        _tray?.SyncOverheatMenu();
        LoadSettingsPage();
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
        SaveSettingsSoon();
        // A new limit is a new question: let the next hot reading warn again.
        _overheatNotified = false;
    }

    /// <summary>
    /// A slider fires for every pixel of a drag; writing the settings file
    /// for each was dozens of writes a second. One write, half a second
    /// after the last change.
    /// </summary>
    private DispatcherTimer? _settingsSaveDelay;

    private void SaveSettingsSoon()
    {
        if (_settingsSaveDelay is null)
        {
            _settingsSaveDelay = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            _settingsSaveDelay.Tick += (_, _) => { _settingsSaveDelay.Stop(); _settings.Save(); };
        }
        _settingsSaveDelay.Stop();
        _settingsSaveDelay.Start();
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

    /// <summary>The mode written to firmware this session and not yet restarted into, unified under AppSettings.</summary>
    private GpuMode? _gpuPendingRestart
    {
        get
        {
            if (_settings.PendingRestart?.ReasonArguments.TryGetValue("gpu", out var modeStr) == true
                && Enum.TryParse<GpuMode>(modeStr, out var mode))
                return mode;
            return null;
        }
        set
        {
            if (value is { } mode)
                _settings.RequestRestart("gpu", mode.ToString());
            else
                _settings.ClearRestartReason("gpu");
            _settings.Save();
        }
    }

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
        CardIsAwake(config) ? _gpuClock.ReadLoad() : null;

    /// <summary>
    /// Whether the card is awake already, asked without waking it. In
    /// Discrete it always is. In Hybrid the PnP manager's record of the
    /// device's power state says - D0 awake, D3 asleep - and a record that
    /// cannot be read is taken as asleep, because the cost of the wrong
    /// guess that way is a missing number, and the other way a woken card.
    /// Measured 12 September 2026: the record follows the card's runtime
    /// sleep within seconds, and reading it leaves the card where it was.
    /// </summary>
    private bool CardIsAwake(GpuConfiguration config)
    {
        if (config.Mode == GpuMode.Discrete) return true;
        if (config.Mode == GpuMode.Uma || !config.DiscretePresent) return false;
        var id = DiscreteId();
        return id is not null && DevicePowerState.MostRecent(id) == DevicePowerState.D0;
    }


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
        Strings.Get(seconds == 1 ? "S.Gpu.SettlingOne" : "S.Gpu.Settling", seconds);

    private void OnGpuModeChanged(object sender, RoutedEventArgs e)
    {
        if (!_gpuUiReady) return;

        // The running mode is what the last detection said; asking WMI again
        // here cost a stall on the interface thread for an answer that had
        // not changed. A click before the first answer is put back.
        if (_currentGpuMode is not { } current)
        {
            LoadGpuMode();
            return;
        }
        if (sender is not RadioButton button || ReferenceEquals(button, GpuButtonFor(current))) return;
        if (button.Tag is not string tag || !Enum.TryParse<GpuMode>(tag, out var target)) return;

        if (_mailbox is null || !_support.AllowsWrites)
        {
            Dialogs.Warn(Strings.Get("S.Gpu.Title"),
                Strings.Get(_support.AllowsWrites ? "S.Gpu.NoMailbox" : "S.Gpu.ReadOnly"));
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
            Log.Info("gpu", $"Switch to {target}: changed={outcome.Changed} restart={outcome.RestartNeeded}; {outcome.Summary}");

            if (!outcome.Changed)
            {
                Dialogs.Warn(Strings.Get("S.Gpu.Title"), outcome.Summary);
                LoadGpuMode();
                return;
            }

            LockGpuButtonsWhileSettling();

            if (outcome.RestartNeeded)
            {
                _gpuPendingRestart = target;
                _restartBannerDismissed = false;
                RefreshBanner();
                LoadGpuMode();
                OfferRestart(outcome.Summary);
                return;
            }
            else
            {
                // Immediate changes deserve a word too. The first version said
                // nothing after switching the card off, and the only sign that
                // anything had happened was Windows' own elevation prompt.
                Dialogs.Tell(Strings.Get("S.Gpu.Title"), outcome.Summary);
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
            Dialogs.Warn(Strings.Get("S.Gpu.NotFromHere"), ex.Message);
            GpuButtonFor(current).IsChecked = true;
        }
        catch (Exception ex) when (ex is EcMailboxUnavailableException or Win32Exception)
        {
            Dialogs.Warn(Strings.Get("S.Mode.ChangeFailed"), ex.Message);
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
        var answer = Dialogs.Ask(Strings.Get("S.Gpu.Title"), Strings.Get("S.Gpu.ConfirmSwitch", target), defaultNo: true);

        return answer;
    }

    private void OfferRestart(string summary)
    {
        var restart = Dialogs.Ask(Strings.Get("S.Gpu.Title"),
            summary + Environment.NewLine + Environment.NewLine + Strings.Get("S.Gpu.RestartNow"),
            defaultNo: true);

        if (!restart) return;

        if (!RequestRestart())
            Dialogs.Warn(Strings.Get("S.Gpu.Title"), Strings.Get("S.Gpu.RestartRefused"));
    }

    // ----------------------------------------------------- restart, Windows' way

    private const int WmQueryEndSession = 0x0011;
    private const int WmEndSession = 0x0016;
    private const uint EwxReboot = 0x00000002;
    private const uint ShutdownReasonPlannedMaintenance = 0x80040001; // MAJOR_APPLICATION | MINOR_MAINTENANCE | FLAG_PLANNED

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern bool ExitWindowsEx(uint flags, uint reason);

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct Luid { public uint LowPart; public int HighPart; }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 4)]
    private struct TokenPrivileges { public uint Count; public Luid Luid; public uint Attributes; }

    [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenProcessToken(IntPtr process, uint access, out IntPtr token);

    [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    private static extern bool LookupPrivilegeValue(string? system, string name, out Luid luid);

    [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool AdjustTokenPrivileges(IntPtr token, bool disableAll, ref TokenPrivileges newState, uint length, IntPtr previous, IntPtr returnLength);

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);

    /// <summary>
    /// Asks Windows for a restart the way the Start menu does: every
    /// application is asked first, one with unsaved work gets the person
    /// the "restart anyway / cancel" screen, and cancel means cancel. Not
    /// <c>shutdown /r</c>, which forces. Needs the shutdown privilege
    /// enabled on this process's token; every interactive user holds it.
    /// </summary>
    private static bool RequestRestart()
    {
        const uint TokenAdjustPrivileges = 0x0020, TokenQuery = 0x0008, PrivilegeEnabled = 0x0002;
        if (OpenProcessToken(GetCurrentProcess(), TokenAdjustPrivileges | TokenQuery, out var token))
        {
            try
            {
                if (LookupPrivilegeValue(null, "SeShutdownPrivilege", out var luid))
                {
                    var privileges = new TokenPrivileges { Count = 1, Luid = luid, Attributes = PrivilegeEnabled };
                    if (!AdjustTokenPrivileges(token, false, ref privileges, 0, IntPtr.Zero, IntPtr.Zero) ||
                        System.Runtime.InteropServices.Marshal.GetLastWin32Error() != 0)
                    {
                        Log.Warn("restart", $"AdjustTokenPrivileges failed or not all assigned: {System.Runtime.InteropServices.Marshal.GetLastWin32Error()}");
                    }
                }
            }
            finally
            {
                CloseHandle(token);
            }
        }

        if (ExitWindowsEx(EwxReboot, ShutdownReasonPlannedMaintenance))
            return true;

        var err = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
        Log.Warn("restart", $"ExitWindowsEx returned false (error {err}); attempting fallback via shutdown.exe");
        try
        {
            using var proc = Process.Start(new ProcessStartInfo(Nextcalibur.Core.Security.SystemTools.Shutdown, "/r /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
            return proc is not null;
        }
        catch (Exception ex)
        {
            Log.Error("restart", "Fallback restart via shutdown.exe failed", ex);
            return false;
        }
    }

    /// <summary>
    /// The one moment the mode register is written: Windows says the session
    /// is ending for real. <c>WM_ENDSESSION</c> with a true <c>wParam</c>
    /// arrives only after every application has agreed or the person has
    /// chosen "restart anyway"; a cancelled restart sends it with false, and
    /// then nothing is written and the machine stays as it was. The write is
    /// one mailbox call, well inside the grace Windows gives.
    /// </summary>
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        if (System.Windows.Interop.HwndSource.FromHwnd(hwnd) is { } source)
            source.AddHook(OnWindowMessage);

        // A frameless window has no minimise box in its style, and without
        // that bit a click on its taskbar button only ever brings it forward;
        // Windows minimises on the second click only for windows that say
        // they can be minimised. Our own caption button already does this;
        // this makes the taskbar agree.
        KeepMinimiseBox();

        // Our shortcuts carry the same identity as this process, so the
        // pinned icon and the running window are one button. Off the
        // interface thread; it touches a few files.
        Task.Run(AppIdentity.StampShortcuts);
    }

    /// <summary>
    /// WPF rewrites a frameless window's style on every state change, and the
    /// minimise bit goes with it - so after our own minimise and the taskbar's
    /// restore, the next taskbar click no longer minimised. Put it back each
    /// time the state changes.
    /// </summary>
    private void KeepMinimiseBox()
    {
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        if (hwnd == IntPtr.Zero) return;
        var style = GetWindowLong(hwnd, GwlStyle);
        if ((style & WsMinimizeBox) == 0) SetWindowLong(hwnd, GwlStyle, style | WsMinimizeBox);
    }

    private const int GwlStyle = -16;
    private const int WsMinimizeBox = 0x00020000;

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hwnd, int index);

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hwnd, int index, int value);

    private IntPtr OnWindowMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WmQueryEndSession)
        {
            // Never the application holding a restart up.
            _sessionEnding = true;
            handled = true;
            return new IntPtr(1);
        }

        if (msg == WmEndSession)
        {
            if (wParam != IntPtr.Zero)
            {
                Log.Info("session", $"Session ending; pending graphics mode: {_gpuPendingRestart?.ToString() ?? "none"}");
                if (_gpuPendingRestart is { } target && _mailbox is not null)
                {
                    try { _gpu.WriteFirmwareMode(_mailbox, target); Log.Info("gpu", $"Firmware mode written: {target}"); }
                    catch (Exception ex) { Log.Error("gpu", "Firmware mode write failed at session end", ex); }
                }
            }
            else
            {
                // Cancelled at Windows' screen. Nothing was written; the
                // choice stays pending for the restart that does happen.
                _sessionEnding = false;
            }
        }

        return IntPtr.Zero;
    }

    // --------------------------------------------------------------- power mode

    private void LoadPowerModes()
    {
        _modeUiReady = false;

        var active = PowerOverlayService.GetActiveOverlay();
        var mode = _modes.DetectCurrent();
        foreach (var (option, button, text) in PowerModeControls())
        {
            // Within the system mode, not against it: the cards outside the
            // mode's range are shown but cannot be chosen, and say why.
            var allowed = mode is null || SystemModeService.OverlayAllowed(mode.Value, option.Overlay);
            button.IsEnabled = allowed;
            text.Text = allowed
                ? option.Description
                : Strings.Get("S.Power.NotInMode", mode!);
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

            // The choice was within the mode's range, so the mode stands; the
            // System page re-reads to keep its subtitle honest.
            LoadSystemMode();
        }
        catch (Exception ex)
        {
            Dialogs.Warn(Strings.Get("S.Mode.ChangeFailed"), ex.Message);
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

        CpuName.Text = cpu ?? "--";
        GpuName.Text = gpu ?? "--";
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

        // Every fixed drive, the Windows one first. Rows are kept and
        // updated, not rebuilt, so the gauges do not restart each refresh;
        // a drive that appears or disappears changes the list's length only.
        // Read once a minute, not every tick: disk use moves by megabytes an
        // hour, and enumerating volumes is the dearest call on this timer.
        if (DateTime.UtcNow - _drivesReadAt < TimeSpan.FromMinutes(1) && _drives.Count > 0) return;
        _drivesReadAt = DateTime.UtcNow;
        var drives = SystemInfo.FixedDrives();
        while (_drives.Count > drives.Count) _drives.RemoveAt(_drives.Count - 1);
        while (_drives.Count < drives.Count) _drives.Add(new DriveRow());
        for (var i = 0; i < drives.Count; i++)
        {
            var row = _drives[i];
            row.Name = drives[i].Name;
            row.Percent = drives[i].Use.Percent;
            row.PercentText = $"{drives[i].Use.Percent:N1}%";
            row.Detail = drives[i].Use.Describe();
        }
    }

    private DateTime _drivesReadAt = DateTime.MinValue;

    /// <summary>The drives as shown; <c>DriveList</c> binds to this.</summary>
    private readonly System.Collections.ObjectModel.ObservableCollection<DriveRow> _drives = new();

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
        try
        {
            if (!ReadingsAreOnScreen())
            {
                _timer.Stop();
                return;
            }

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
                        SubtitleText.Text = Strings.Get("S.Status.Stalled");
                        _timer.Stop();
                    }
                    return;
                }

                s = reading.Value;

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
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            _sampling = false;
        }
    }

    /// <summary>
    /// Shows the frequencies the parts are actually running at. Either can be
    /// unreadable — no NVIDIA card, an unavailable counter — and then the
    /// reading is left blank rather than filled with a guess.
    /// </summary>
    /// <summary>The discrete adapter's instance id, looked up once; null when there is none.</summary>
    private string? _discreteId;
    private bool _discreteIdLooked;

    /// <summary>
    /// Whether the card is awake right now, from the PnP manager's record -
    /// cheap enough for every tick, and it wakes nothing. Discrete is always
    /// awake; UMA never; Hybrid is whatever the record says.
    /// </summary>
    private bool CardIsAwakeNow()
    {
        if (_currentGpuMode == GpuMode.Discrete) return true;
        if (_currentGpuMode == GpuMode.Uma) return false;
        var id = DiscreteId();
        return id is not null && DevicePowerState.MostRecent(id) == DevicePowerState.D0;
    }

    /// <summary>The one WMI lookup of the adapter's id, from whichever thread asks first.</summary>
    private string? DiscreteId()
    {
        if (!_discreteIdLooked)
        {
            _discreteId = GpuModeService.DiscreteAdapterInstanceId();
            _discreteIdLooked = true;
        }
        return _discreteId;
    }

    /// <summary>The mode as last detected, so the two-second tick need not ask WMI again.</summary>
    private GpuMode? _currentGpuMode;

    /// <summary>
    /// The clocks. The CPU's costs nothing. The GPU's comes from NVML, which
    /// wakes the card - and this ran every two seconds in every mode until
    /// 12 September 2026, which is why the card never slept while the window
    /// was open. Now it is asked only when the card is awake already; asleep,
    /// the field says so.
    /// </summary>
    private void RefreshClocks()
    {
        // The CPU's draw comes through PawnIO when it is installed and the
        // process is elevated; otherwise the clock stands alone.
        CpuClock.Text = (_cpuClock.ReadGhz(), _cpuPower.ReadWatts()) switch
        {
            ({ } ghz, { } watts) => $"{ghz:N2} GHz · {watts:0.0} W",
            ({ } ghz, null) => $"{ghz:N2} GHz",
            _ => "--",   // a failed measurement is a dash, never a blank
        };
        // Clock and draw together, beside the temperature the panel already
        // shows: the card's whole state on one line, in every mode.
        GpuClock.Text = _nvidiaDriver == NvidiaDriverState.Missing ? "--"
            : !CardIsAwakeNow() ? Strings.Get("S.Readings.Asleep")
            : (_gpuClock.ReadGhz(), _gpuClock.ReadLoad()) switch
            {
                ({ } ghz, { } load) => $"{ghz:N2} GHz · {load.Watts:0.0} W",
                ({ } ghz, null) => $"{ghz:N2} GHz",
                _ => "--",
            };
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

    /// <summary>
    /// Notices the card being switched off or on behind our back - the
    /// vendor's software, Device Manager - and re-reads the page. Through
    /// the PnP status word, not WMI: this runs every five seconds on
    /// screen, and a WMI query at that rate was the dearest thing here.
    /// </summary>
    private Task WatchForTheCardBeingSwitched()
    {
        if (DiscreteId() is not { } id) return Task.CompletedTask;

        var disabled = DevicePowerState.IsDisabled(id);
        if (disabled is null) return Task.CompletedTask;
        var enabled = !disabled.Value;

        if (_discreteWasEnabled is { } was && was != enabled)
        {
            _gpuClock.Reset();
            LoadGpuMode();
        }

        _discreteWasEnabled = enabled;
        return Task.CompletedTask;
    }

    private DateTime _registryLookedAt = DateTime.MinValue;
    private DateTime _windowsFaultsLookedAt = DateTime.MinValue;
    private DateTime _lastGpuFaultNotified = DateTime.MinValue;
    private string? _currentGpuFaultText;

    /// <summary>
    /// Windows' own faults: watched on the same slow beat as everything
    /// else, and put right where that is safe. See
    /// <see cref="Nextcalibur.Core.Hardware.WindowsFaults"/> and
    /// <see cref="Nextcalibur.Core.Hardware.GpuClockReader"/> for the rules
    /// this is held to; the switch is on the Settings page.
    /// </summary>
    private void WatchWindowsItself()
    {
        if (!_settings.CompensateWindowsFaults || UserPresence.IsGamingOrHeavyLoad())
        {
            _currentGpuFaultText = null;
            _gpuClock.ResetAwakeFault();
            return;
        }

        foreach (var fault in Nextcalibur.Core.Hardware.WindowsFaults.Check(mayAct: true))
        {
            var title = Strings.Get(fault.Fixed ? "S.WindowsFault.Fixed" : "S.WindowsFault.Found");
            if (!UserPresence.WouldRatherNotBeDisturbed() && !UserPresence.NobodyIsWatching())
            {
                if (!Toasts.TryShow(title, fault.What, Strings.Get("S.Dialog.OK"), () => { }))
                    _tray?.ShowMessage(title, fault.What);
            }
        }

        // Memory trimming when not in game / undisturbed / not under heavy load (allowed while nobody is watching / locked)
        if (!UserPresence.WouldRatherNotBeDisturbed() && !UserPresence.IsGamingOrHeavyLoad())
        {
            if (_settings.TrimDwmMemory)
                Nextcalibur.Core.Hardware.MemoryTrimmer.TrimIfExceeds("dwm", 1536L * 1024 * 1024);
            if (_settings.TrimExplorerMemory)
                Nextcalibur.Core.Hardware.MemoryTrimmer.TrimIfExceeds("explorer", 1228L * 1024 * 1024);
        }

        // §26, §28, §30: The graphics card held awake at full clocks with nothing to draw.
        // Controlled under the same switch, touches nothing on the card.
        // Suppress and reset when in UMA mode, discrete card is off, discrete card is asleep, user is undisturbed, or heavy load / gaming.
        if (!_settings.WatchGpuAwake || _currentGpuMode == GpuMode.Uma || _discreteWasEnabled == false || !CardIsAwakeNow() || UserPresence.WouldRatherNotBeDisturbed() || UserPresence.NobodyIsWatching() || UserPresence.IsGamingOrHeavyLoad())
        {
            _gpuClock.ResetAwakeFault();
            _currentGpuFaultText = null;
            return;
        }

        var gpuFault = _gpuClock.CheckAwakeFault();
        if (gpuFault is not null)
        {
            var desc = gpuFault.Describe();
            var hadFault = _currentGpuFaultText != null;
            _currentGpuFaultText = desc;

            if (DateTime.UtcNow - _lastGpuFaultNotified >= TimeSpan.FromHours(1))
            {
                _lastGpuFaultNotified = DateTime.UtcNow;
                var title = Strings.Get("S.WindowsFault.GpuWakeTitle");
                var buttonText = Strings.Get("S.WindowsFault.OpenGraphicsSettings");
                if (!Toasts.TryShow(title, desc, buttonText, () => Unelevated.Open("ms-settings:display-graphics")))
                    _tray?.ShowMessage(title, desc);
            }

            if (!hadFault && NavDisplay.IsChecked == true)
            {
                LoadGpuMode();
            }
        }
        else if (_currentGpuFaultText != null)
        {
            _currentGpuFaultText = null;
            if (NavDisplay.IsChecked == true)
            {
                LoadGpuMode();
            }
        }
    }

    /// <summary>
    /// How far below its threshold a chip has to read, on the cheap
    /// sensors, before the firmware is left unasked. Ten degrees is more
    /// than either chip travels in the minute between hidden ticks.
    /// </summary>
    private const int CheapGateMarginC = 10;

    /// <summary>
    /// Whether both chips are far enough below their warning thresholds,
    /// according to sensors that cost no interrupt, that the firmware need
    /// not be read at all this tick.
    ///
    /// False whenever it cannot be sure: no PawnIO, an AMD machine, a card
    /// that is asleep or absent, a register that does not answer. The
    /// expensive read is the safe answer, and it is what happened before
    /// these sensors were consulted at all.
    /// </summary>
    private bool NothingNearTheThresholds()
    {
        var cpu = _cpuPower.ReadPackageTemperatureC();
        if (cpu is null || cpu >= _settings.CpuWarningTemperatureC - CheapGateMarginC) return false;

        // The card: a reading that is near the line stops the gate, and no
        // reading at all is fine - a card that NVML cannot see or is asleep
        // is not overheating. Never touch NVML if asleep, to avoid waking it.
        var gpu = CardIsAwakeNow() ? _gpuClock.ReadTemperatureC() : null;
        if (gpu is not null && gpu >= _settings.GpuWarningTemperatureC - CheapGateMarginC) return false;

        return true;
    }

    /// <summary>
    /// Runs one piece of work at a lower thread priority and puts the
    /// priority back: the pool lends its threads out again, so leaving one
    /// demoted would quietly demote something else later.
    /// </summary>
    private static T AtBackgroundPriority<T>(Func<T> work)
    {
        var thread = System.Threading.Thread.CurrentThread;
        var was = thread.Priority;
        try
        {
            thread.Priority = System.Threading.ThreadPriority.BelowNormal;
            return work();
        }
        finally
        {
            try { thread.Priority = was; }
            catch (Exception ex) when (ex is System.Threading.ThreadStateException or ArgumentException) { }
        }
    }

    /// <summary>
    /// How long to wait before the next hidden read. Never shorter because
    /// the machine is hotter: a minute as a rule, two once it is over the
    /// line and has been told. The cost of watching must not rise with the
    /// thing being watched for.
    /// </summary>
    private TimeSpan HiddenIntervalNow()
    {
        if (!_settings.WarnsAboutHeat) return HiddenInterval;

        // Two minutes while the machine is over the line and has been told,
        // and two while a game has the screen. Both for the same reason:
        // every read is a write to the mailbox, every write to the mailbox
        // raises a system-management interrupt - 21 ms on this machine,
        // measured - and an interrupt stops every core for its duration,
        // which is a frame in somebody's game. Windows suppresses our
        // notification in that state anyway, so the reads are only keeping
        // watch until they come back out.
        return _overheatNotified || Nextcalibur.Core.Hardware.UserPresence.WouldRatherNotBeDisturbed() || Nextcalibur.Core.Hardware.UserPresence.NobodyIsWatching() || Nextcalibur.Core.Hardware.UserPresence.IsGamingOrHeavyLoad()
            ? HiddenHotInterval
            : HiddenInterval;
    }

    private void StartSlowTimer()
    {
        _slowTimer = new DispatcherTimer { Interval = VisibleSlowInterval };
        _slowTimer.Tick += async (_, _) =>
        {
            if (_exiting || _sessionEnding) return;
            try
            {
                // Everything below the guard exists to keep the window truthful.
                // While it is in the notification area or when undisturbed/nobody watching/gaming/heavy load,
                // there is nothing to keep truthful, so none of it runs and the timer itself slows down.
                var onScreen = IsVisible && WindowState != WindowState.Minimized && (IsActive || (!Nextcalibur.Core.Hardware.UserPresence.WouldRatherNotBeDisturbed() && !Nextcalibur.Core.Hardware.UserPresence.NobodyIsWatching() && !Nextcalibur.Core.Hardware.UserPresence.IsGamingOrHeavyLoad()));
                _slowTimer.Interval = onScreen ? VisibleSlowInterval : HiddenIntervalNow();

                if (onScreen)
                {
                    _hiddenTicks = 0;
                    RefreshBanner();
                    ApplyPollInterval();
                    SyncSamplingToScreen();
                    RefreshStorage();
                    if (_theme.PollForChange()) ApplyTheme();
                    await WatchForTheCardBeingSwitched();
                }
                else
                {
                    if (_timer.IsEnabled) _timer.Stop();
                }

                // Once a minute whether on screen or hidden: check Windows and GPU faults.
                if (DateTime.UtcNow - _windowsFaultsLookedAt >= TimeSpan.FromMinutes(1))
                {
                    _windowsFaultsLookedAt = DateTime.UtcNow;
                    WatchWindowsItself();
                }

                // Once a minute on screen, once every five while hidden: two
                // registry scans for things that change once in a machine's
                // life - the vendor's plans going, the vendor's software coming
                // or going. Nobody is reading the banner they feed while the
                // window is away.
                if (DateTime.UtcNow - _registryLookedAt >= (onScreen ? TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(5)))
                {
                    _registryLookedAt = DateTime.UtcNow;
                    KeepTheModesPlanAlive();
                    if (_support.Level != SupportLevel.Unsupported)
                    {
                        RecommendRemovingVendorSoftwareOnce();
                        WatchTheVendorComingAndGoing();
                        WatchTheEssentialDrivers();
                    }
                }

                // The overheat warning is the one thing worth a firmware read while
                // hidden — it is the reason the application stays resident at all.
                if (!_settings.WarnsAboutHeat && !onScreen) return;

                if (_thermal is null) return;

                // And before paying for one: the two sensors that cost no
                // interrupt. The processor's own thermal register through
                // PawnIO, and the card's through NVML. Both a long way below
                // the thresholds means there is nothing for the warning to say,
                // and the firmware is left alone entirely - no mailbox write,
                // no system-management interrupt, nothing stopped.
                //
                // A gate, not a replacement: the moment either is anywhere near
                // the line, the tick goes on to read the controller and decides
                // on its number, which is the one the window shows.
                //
                // A game or a heavy load is not a reason to stop watching: it is
                // when the chips get hot. It is only a reason to lean harder on
                // the gate, which it gets through onScreen being false, and on
                // the slower hidden interval. The card is watched too - under a
                // game it is the likelier of the two to reach its line - and the
                // warning itself waits only where Windows says a game has the
                // screen (the busy/away check below), not for a compile in a
                // window beside this one (23 September 2026: the branch that
                // was here read the processor alone and deferred every warning,
                // game or not, for as long as the load lasted).
                if (!onScreen && NothingNearTheThresholds()) return;

                // Off the user-interface thread for the same reason as Sample: see
                // the note there. This read is the one that keeps the tray tooltip
                // and the overheat warning alive while the window is put away.
                // On screen the fast timer has just read the firmware for the
                // window; a second read here for the tooltip and the warning
                // was a third of all mailbox traffic for nothing (12 September
                // 2026). Use that sample while it is fresh; read only when it
                // is not - hidden, or the fast timer stalled.
                // The age is checked at both ends: a sample from the future -
                // which is what a clock put back looks like - is not a fresh
                // sample, and the overheat warning is the last thing that
                // should be reading an hour-old temperature.
                ThermalSample s;
                if (_lastThermal is { } recent && DateTimeOffset.Now - recent.Timestamp is { Ticks: >= 0 } age && age < VisibleSlowInterval)
                {
                    s = recent;
                }
                else
                {
                    var reader = _thermal;
                    try
                    {
                        // Below normal while the window is away: this is a
                        // background errand on somebody else's machine, and on a
                        // machine that is hot or busy - the two go together -
                        // it should lose every tie it is in. It still runs
                        // promptly; it just never takes a turn from the thing
                        // in front of the person.
                        var reading = await Task.Run(() => AtBackgroundPriority(
                            () => reader.TryRead(out var value) ? value : (ThermalSample?)null));
                        if (reading is null) return;
                        s = reading.Value;
                    }
                    catch (Exception ex) when (ex is not OutOfMemoryException)
                    {
                        return;
                    }
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
                var hot = s.CpuTemperatureC >= cpuLimit ? Strings.Get("S.Heat.Cpu", s.CpuTemperatureC)
                        : s.GpuTemperatureC >= gpuLimit ? Strings.Get("S.Heat.Gpu", s.GpuTemperatureC)
                        : null;
                if (_settings.WarnsAboutHeat && hot is not null)
                {
                    var busy = Nextcalibur.Core.Hardware.UserPresence.WouldRatherNotBeDisturbed();
                    var away = Nextcalibur.Core.Hardware.UserPresence.NobodyIsWatching();
                    if (busy || away)
                    {
                        bool cpuHot = s.CpuTemperatureC >= cpuLimit;
                        bool gpuHot = s.GpuTemperatureC >= gpuLimit;
                        bool isCpu = cpuHot && (!gpuHot || s.CpuTemperatureC >= s.GpuTemperatureC);
                        int temp = isCpu ? s.CpuTemperatureC : s.GpuTemperatureC;

                        if (_deferredOverheat is null || temp > _deferredOverheat.MaxTemperatureC)
                        {
                            _deferredOverheat = new DeferredOverheat(isCpu, temp, DateTime.UtcNow);
                        }
                    }
                    else
                    {
                        _deferredOverheat = null;
                        if (!_overheatNotified)
                        {
                            _overheatNotified = true;
                            _tray?.ShowMessage(hot, Strings.Get("S.Heat.Body"));
                        }
                    }
                }
                else
                {
                    if (s.CpuTemperatureC < cpuLimit - 8 && s.GpuTemperatureC < gpuLimit - 8)
                    {
                        // Re-arm only after a clear drop, so the balloon cannot flap.
                        _overheatNotified = false;
                    }

                    if (!Nextcalibur.Core.Hardware.UserPresence.WouldRatherNotBeDisturbed() &&
                        !Nextcalibur.Core.Hardware.UserPresence.NobodyIsWatching())
                    {
                        TryShowDeferredOverheat();
                    }
                }
            }
            catch (Exception ex) when (ex is not OutOfMemoryException)
            {
                Log.Warn("slow-timer", ex.Message);
            }
        };
        _slowTimer.Start();
    }

    /// <summary>
    /// If the machine overheated while the session was locked or the person was
    /// otherwise unreachable, report the highest temperature reached once
    /// someone is here again. Clears without reporting if older than three hours.
    /// </summary>
    private void TryShowDeferredOverheat()
    {
        if (!_settings.WarnsAboutHeat || _deferredOverheat is null) return;

        var overheat = _deferredOverheat;
        _deferredOverheat = null;

        // Clear without showing if older than 3 hours.
        if (DateTime.UtcNow - overheat.TimestampUtc > TimeSpan.FromHours(3))
            return;

        // If nobody is watching (e.g. still locked or screen saver), keep it for later.
        if (Nextcalibur.Core.Hardware.UserPresence.NobodyIsWatching())
        {
            _deferredOverheat = overheat;
            return;
        }

        var chipName = Strings.Get(overheat.IsCpu ? "S.Heat.ChipCpu" : "S.Heat.ChipGpu");
        var title = Strings.Get("S.Heat.WhileAway", overheat.MaxTemperatureC, chipName);
        _tray?.ShowMessage(title, Strings.Get("S.Heat.Body"));

        // Told. If it is still over the line, the next tick must not say it
        // again in other words; the warning re-arms after the usual clear drop.
        _overheatNotified = true;
    }

    // ------------------------------------------------------------------ power

    private void RefreshOverlay()
    {
        var d = _power.Diagnose();
        OverlayState.Text = PowerOverlays.Describe(d.ActiveOverlay);

        if (!d.NeedsRepair)
        {
            OverlayDetail.Text = Strings.Get("S.Overlay.Fine");
            OverlayState.Foreground = (SolidColorBrush)FindResource("Good");
            FixButton.Visibility = Visibility.Collapsed;
            return;
        }

        var problems = new List<string>();
        if (d.OverlayIsStuck)
            problems.Add(Strings.Get("S.Overlay.Stuck"));
        if (d.GuardMissing)
            problems.Add(Strings.Get("S.Overlay.GuardMissing"));

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
                : Strings.Get("S.Overlay.NothingToDo");
            if (outcome.Blocked is { } blocked)
                body += Environment.NewLine + Environment.NewLine + blocked;

            Dialogs.Tell(Strings.Get(outcome.Blocked is null ? "S.Overlay.Fixed" : "S.Overlay.PartlyFixed"), body);
        }
        catch (Exception ex)
        {
            Dialogs.Warn(Strings.Get("S.Overlay.FixFailed"), ex.Message);
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
        QueueLighting(() => _led.SetColour(target, colour.R, colour.G, colour.B));
        RefreshPreview();
    }

    /// <summary>
    /// A drag on the wheel or the brightness slider raises an event per
    /// pixel, and each used to be a firmware write - two mailbox calls and
    /// a 30 ms wait on the interface thread - and a settings file written.
    /// A drag hammered the controller and stuttered. Now the latest value
    /// is kept and written at most every 120 ms; the last one always lands.
    /// </summary>
    private DispatcherTimer? _lightingThrottle;
    private Action? _pendingLighting;

    private void QueueLighting(Action action)
    {
        _pendingLighting = action;
        if (_lightingThrottle is null)
        {
            _lightingThrottle = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(120) };
            _lightingThrottle.Tick += (_, _) =>
            {
                var pending = _pendingLighting;
                _pendingLighting = null;
                if (pending is null) { _lightingThrottle.Stop(); return; }
                RunLighting(pending);
            };
        }
        if (!_lightingThrottle.IsEnabled) _lightingThrottle.Start();
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
        QueueLighting(() => _led.SetBrightness(percent));
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
            Dialogs.Warn(Strings.Get("S.Lighting.Title"), Strings.Get("S.Lighting.NoResponse"));
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

    private NvidiaDriverState _nvidiaDriver = NvidiaDriverState.Present;

    /// <summary>
    /// The drivers the application cannot do without, at start and once a
    /// minute. The NVIDIA driver can be removed while this runs or between
    /// starts; when it is, the Display page is locked, the card's readings
    /// read "--", the banner says why, and the process stops calling into
    /// a library whose driver is gone. When it returns, everything comes
    /// back. Two cheap calls: a PCI device list and a file's existence.
    /// </summary>
    private void WatchTheEssentialDrivers()
    {
        var state = EssentialDrivers.Nvidia();
        if (state == _nvidiaDriver) return;
        var was = _nvidiaDriver;
        _nvidiaDriver = state;
        Log.Info("drivers", $"NVIDIA driver: {was} -> {state}");

        var usable = state != NvidiaDriverState.Missing && _support.AllowsReads;
        NavDisplay.IsEnabled = usable && _support.Level != SupportLevel.Unsupported;
        if (!usable)
        {
            if (NavDisplay.IsChecked == true) NavSystem.IsChecked = true;
            _gpuClock.Reset();
            GpuClock.Text = "--";
        }
        else if (was == NvidiaDriverState.Missing)
        {
            _gpuClock.Reset();
            _discreteIdLooked = false;   // the id is looked up again with the driver back
            LoadGpuMode();
        }
        RefreshBanner();
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
                    Strings.Get("S.Banner.NoAccessTitle"),
                    Strings.Get("S.Banner.NoAccessBody"),
                    (SolidColorBrush)FindResource("Warn"));
                return;
            }

            ShowBanner(
                Strings.Get("S.Banner.UnsupportedTitle"),
                Strings.Get("S.Banner.NoSensorsBody"),
                (SolidColorBrush)FindResource("Bad"));
            return;
        }

        if (_nvidiaDriver == NvidiaDriverState.Missing)
        {
            ShowBanner(
                Strings.Get("S.Banner.NoDriverTitle"),
                Strings.Get("S.Banner.NoDriverBody"),
                (SolidColorBrush)FindResource("Warn"));
            return;
        }

        if (VendorSoftware.IsRunning())
        {
            ShowBanner(
                Strings.Get("S.Banner.VendorOpenTitle"),
                Strings.Get("S.Banner.VendorOpenBody"),
                (SolidColorBrush)FindResource("Warn"));
            return;
        }

        // An installed copy outside Program Files - an earlier version's
        // install in the profile - is prompted at every start (see
        // Elevation.MayStartWithoutPrompt) and told why, and what to do.
        if (Environment.ProcessPath is { } self && Elevation.IsInstalledCopy(self) && !Elevation.MayStartWithoutPrompt(self))
        {
            ShowBanner(
                Strings.Get("S.Banner.ProfileCopyTitle"),
                Strings.Get("S.Banner.ProfileCopyBody"),
                (SolidColorBrush)FindResource("Warn"));
            return;
        }

        if (!_restartBannerDismissed && _settings.HasPendingRestart)
        {
            ShowBanner(
                Strings.Get("S.Banner.RestartTitle"),
                GetRestartReasonText(),
                (SolidColorBrush)FindResource("Warn"),
                showRestartButtons: true);
            return;
        }

        Banner.Visibility = Visibility.Collapsed;
    }

    private bool _restartBannerDismissed;

    private string GetRestartReasonText()
    {
        if (_settings.PendingRestart is null || _settings.PendingRestart.Reasons.Count == 0)
            return string.Empty;

        var messages = new List<string>();
        foreach (var reason in _settings.PendingRestart.Reasons)
        {
            _settings.PendingRestart.ReasonArguments.TryGetValue(reason, out var arg);
            switch (reason.ToLowerInvariant())
            {
                case "ndu":
                    messages.Add(Strings.Get("S.Restart.ReasonNdu"));
                    break;
                case "installer":
                    messages.Add(Strings.Get("S.Restart.ReasonInstaller", arg ?? string.Empty));
                    break;
                case "pawnio":
                    messages.Add(Strings.Get("S.Restart.ReasonPawnIo"));
                    break;
                case "gpu":
                    messages.Add(Strings.Get("S.Gpu.RestartPending", arg ?? string.Empty));
                    break;
                default:
                    if (!string.IsNullOrWhiteSpace(arg))
                        messages.Add(arg);
                    break;
            }
        }

        return string.Join(" ", messages);
    }

    private void OnBannerRestartClicked(object sender, RoutedEventArgs e)
    {
        if (!RequestRestart())
            Dialogs.Warn(Strings.Get("S.Gpu.Title"), Strings.Get("S.Gpu.RestartRefused"));
    }

    private void OnBannerDismissClicked(object sender, RoutedEventArgs e)
    {
        _restartBannerDismissed = true;
        Banner.Visibility = Visibility.Collapsed;
    }

    private void ShowBanner(string title, string body, SolidColorBrush colour, bool showRestartButtons = false)
    {
        BannerTitle.Text = title;
        BannerTitle.Foreground = colour;
        Banner.BorderBrush = colour;
        BannerBody.Text = body;
        BannerActions.Visibility = showRestartButtons ? Visibility.Visible : Visibility.Collapsed;
        Banner.Visibility = Visibility.Visible;
    }

}

