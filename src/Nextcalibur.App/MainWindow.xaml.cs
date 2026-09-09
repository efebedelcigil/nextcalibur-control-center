using System.ComponentModel;
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
    private readonly GpuModeService _gpu = new();
    private readonly ThemeService _theme = new();
    private readonly CpuClockReader _cpuClock = new();
    private readonly GpuClockReader _gpuClock = new();
    private readonly AppSettings _settings = AppSettings.Load();
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

    private EcMailbox? _mailbox;
    private ThermalReader? _thermal;
    private LedController? _led;
    private TrayPresence? _tray;

    private int _consecutiveFailures;
    private bool _exiting;
    private bool _overheatNotified;
    private bool _mailboxFailed;

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
        ApplyPollInterval();
        Loaded += OnLoaded;
        StateChanged += OnStateChanged;
        Closing += OnClosing;
    }

    // ---------------------------------------------------------------- startup

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        LoadTheme();

        _tray = new TrayPresence(this, _settings);
        _tray.ExitRequested += (_, _) => Exit();

        if (_settings.StartMinimised &&
            Environment.GetCommandLineArgs().Contains("--tray", StringComparer.OrdinalIgnoreCase))
        {
            Hide();
        }

        // None of this needs the firmware interface, so it runs before the
        // check that can bail out — an unsupported machine still gets its
        // device names, power page and storage readings.
        _mailboxSupported = EcMailbox.IsSupported();

        RefreshOverlay();
        LoadPowerModes();
        LoadDeviceNames();
        LoadGpuMode();
        RefreshStorage();
        RefreshBanner();
        StartSlowTimer();

        if (!_mailboxSupported)
        {
            NavLighting.IsEnabled = false;
            return;
        }

        try
        {
            _mailbox = new EcMailbox();
            _thermal = new ThermalReader(_mailbox);
            _led = new LedController(_mailbox);
        }
        catch (EcMailboxUnavailableException)
        {
            _mailboxFailed = true;
            RefreshBanner();
            NavLighting.IsEnabled = false;
            return;
        }

        LoadSystemMode();
        LoadLightingUi();

        _timer.Tick += (_, _) => Sample();
        Sample();
        RefreshClocks();
        if (IsVisible) _timer.Start();
    }

    private void Exit()
    {
        _exiting = true;
        Close();
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

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (_exiting || !_settings.MinimiseToTray || _tray is null)
        {
            _timer.Stop();
            _tray?.Dispose();
            _mailbox?.Dispose();
            _cpuClock.Dispose();
            _gpuClock.Dispose();
            _settings.Save();
            return;
        }

        // Closing keeps the app alive in the notification area, which is the
        // only way the temperature warning is of any use.
        e.Cancel = true;
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
        if (current is { } mode) ModeButtonFor(mode).IsChecked = true;
        _modeUiReady = true;
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
            SubtitleText.Text = _modes.Apply(mode);
            RefreshOverlay();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not change mode",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            LoadSystemMode();
        }
    }

    // ------------------------------------------------------------ graphics

    private void LoadGpuMode()
    {
        var config = _gpu.Detect();
        GpuModeDetail.Text = GpuModeService.Describe(config);

        if (config.Mode is not { } mode) return;
        GpuButtonFor(mode).IsChecked = true;
    }

    private RadioButton GpuButtonFor(GpuMode mode) => mode switch
    {
        GpuMode.Hybrid => ModeHybrid,
        GpuMode.Uma => ModeUma,
        _ => ModeDiscrete,
    };

    /// <summary>
    /// Switching graphics mode is not implemented yet, so this reports the
    /// current setting and puts the selection back.
    ///
    /// The vendor software does this by disabling the graphics card as a device.
    /// Which of its three buttons produces which device state has not been
    /// observed on real hardware, and guessing could leave the machine with no
    /// working display path — so nothing is changed until that is known.
    /// </summary>
    private void OnGpuModeChanged(object sender, RoutedEventArgs e)
    {
        var config = _gpu.Detect();
        GpuModeDetail.Text = GpuModeService.Describe(config);

        if (config.Mode is not { } current) return;
        if (sender is not RadioButton button || ReferenceEquals(button, GpuButtonFor(current))) return;

        MessageBox.Show(this,
            """
            Changing graphics mode is not available yet.

            This setting decides whether your screen is driven by the graphics card or
            the built-in graphics. Getting it wrong can leave you with a blank screen,
            so Nextcalibur will not change it until the switch has been proven safe on
            this model.

            For now you can change it in your laptop's BIOS setup.
            """,
            "Graphics mode", MessageBoxButton.OK, MessageBoxImage.Information);

        GpuButtonFor(current).IsChecked = true;
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
            MessageBox.Show(this, ex.Message, "Could not change mode",
                MessageBoxButton.OK, MessageBoxImage.Warning);
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

    private void Sample()
    {
        if (_thermal is null) return;

        if (!_thermal.TryRead(out var s))
        {
            // A single miss is normal when something else touches the mailbox.
            if (++_consecutiveFailures >= 5)
            {
                SubtitleText.Text = "Readings have stalled. Close Casper's Control Center and reopen this window.";
                _timer.Stop();
            }
            return;
        }

        _consecutiveFailures = 0;

        CpuTemp.Text = $"{s.CpuTemperatureC} °C";
        GpuTemp.Text = $"{s.GpuTemperatureC} °C";
        CpuFan.Text = $"{s.CpuFanRpm} rpm";
        GpuFan.Text = $"{s.GpuFanRpm} rpm";

        ColourByTemperature(CpuTemp, s.CpuTemperatureC);
        ColourByTemperature(GpuTemp, s.GpuTemperatureC);

        RefreshClocks();
        SubtitleText.Text = $"Updated {s.Timestamp:HH:mm:ss}";
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

    private void StartSlowTimer()
    {
        var slow = new DispatcherTimer { Interval = VisibleSlowInterval };
        slow.Tick += (_, _) =>
        {
            // Everything below the guard exists to keep the window truthful.
            // While it is in the notification area there is nothing to keep
            // truthful, so none of it runs and the timer itself slows down.
            var onScreen = IsVisible && WindowState != WindowState.Minimized;
            slow.Interval = onScreen ? VisibleSlowInterval : HiddenSlowInterval;

            if (onScreen)
            {
                RefreshBanner();
                ApplyPollInterval();
                RefreshStorage();
                if (_theme.PollForChange()) ApplyTheme();
            }

            // The overheat warning is the one thing worth a firmware read while
            // hidden — it is the reason the application stays resident at all.
            if (_settings.CpuWarningTemperatureC <= 0 && !onScreen) return;

            if (_thermal is null || !_thermal.TryRead(out var s)) return;

            _tray?.UpdateStatus(s.CpuTemperatureC, s.GpuTemperatureC, s.CpuFanRpm);

            var limit = _settings.CpuWarningTemperatureC;
            if (limit > 0 && s.CpuTemperatureC >= limit)
            {
                if (!_overheatNotified)
                {
                    _overheatNotified = true;
                    _tray?.ShowMessage(
                        $"CPU at {s.CpuTemperatureC} °C",
                        "Sustained temperatures this high usually mean the heatsink needs cleaning.");
                }
            }
            else if (s.CpuTemperatureC < limit - 8)
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
            var actions = _power.Repair();
            RefreshOverlay();
            MessageBox.Show(this,
                actions.Count == 0 ? "Nothing needed changing." : string.Join("\n\n", actions),
                "Fixed", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show(this,
                "Windows would not allow the change. Try running Nextcalibur as administrator.",
                "Could not fix it", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Could not fix it",
                MessageBoxButton.OK, MessageBoxImage.Error);
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
        BrightnessSlider.Value = _led.State.BrightnessPercent;
        BrightnessValue.Text = $"{_led.State.BrightnessPercent}%";
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
            border.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
            border.BorderBrush = all || zone == selected ? accent : Brushes.Transparent;
        }

        Wheel.SelectedColour = ColourOf(selected);
    }

    private Color ColourOf(LedZone zone)
    {
        var (r, g, b) = _led!.State.GetColour(zone);
        return Color.FromRgb(r, g, b);
    }

    private void OnZoneTabChanged(object sender, RoutedEventArgs e)
    {
        if (!_ledUiReady) return;
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
            action();
        }
        catch (EcMailboxUnavailableException)
        {
            // The message deliberately names the likely cause rather than
            // repeating the exception, which talks about mailboxes and retries.
            MessageBox.Show(this,
                "The keyboard lighting did not respond. If Casper's Control Center is open, " +
                "close it and try again.",
                "Lighting", MessageBoxButton.OK, MessageBoxImage.Warning);
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

