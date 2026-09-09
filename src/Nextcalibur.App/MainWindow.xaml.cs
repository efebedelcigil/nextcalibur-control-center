using System.ComponentModel;
using System.Diagnostics;
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
    private readonly AppSettings _settings = AppSettings.Load();
    private readonly DispatcherTimer _timer = new();

    private EcMailbox? _mailbox;
    private ThermalReader? _thermal;
    private LedController? _led;
    private TrayPresence? _tray;

    private int _consecutiveFailures;
    private bool _exiting;
    private bool _overheatNotified;
    private bool _mailboxFailed;

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

    public MainWindow()
    {
        InitializeComponent();
        _timer.Interval = TimeSpan.FromMilliseconds(_settings.PollIntervalMs);
        Loaded += OnLoaded;
        StateChanged += OnStateChanged;
        Closing += OnClosing;
    }

    // ---------------------------------------------------------------- startup

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _tray = new TrayPresence(this, _settings);
        _tray.ExitRequested += (_, _) => Exit();

        if (_settings.StartMinimised &&
            Environment.GetCommandLineArgs().Contains("--tray", StringComparer.OrdinalIgnoreCase))
        {
            Hide();
        }

        RefreshOverlay();

        RefreshBanner();

        if (!EcMailbox.IsSupported())
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
        LoadGpuMode();
        LoadLightingUi();

        _timer.Tick += (_, _) => Sample();
        Sample();
        if (IsVisible) _timer.Start();
        StartTrayPolling();
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
        if (WindowState == WindowState.Minimized) _timer.Stop();
        else if (_thermal is not null) _timer.Start();
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (_exiting || !_settings.MinimiseToTray || _tray is null)
        {
            _timer.Stop();
            _tray?.Dispose();
            _mailbox?.Dispose();
            _settings.Save();
            return;
        }

        // Closing keeps the app alive in the notification area, which is the
        // only way the temperature warning is of any use.
        e.Cancel = true;
        Hide();
        _timer.Stop();
    }

    // ------------------------------------------------------------ window chrome

    // The native title bar is switched off, so dragging and the caption buttons
    // are ours to provide.

    private void OnTitleBarDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed) DragMove();
    }

    private void OnMinimiseClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

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
            MessageBox.Show(this, ex.Message, "Could not switch mode",
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

    // ---------------------------------------------------------------- sensors

    private void Sample()
    {
        if (_thermal is null) return;

        if (!_thermal.TryRead(out var s))
        {
            // A single miss is normal when something else touches the mailbox.
            if (++_consecutiveFailures >= 5)
            {
                SubtitleText.Text = "Sensor readings stalled - close the vendor Control Center and reopen.";
                _timer.Stop();
            }
            return;
        }

        _consecutiveFailures = 0;

        CpuTemp.Text = $"{s.CpuTemperatureC} °C";
        GpuTemp.Text = $"{s.GpuTemperatureC} °C";
        CpuFan.Text = $"{s.CpuFanRpm} rpm";
        GpuFan.Text = $"{s.GpuFanRpm} rpm";

        CpuTemp.Foreground = TemperatureBrush(s.CpuTemperatureC);
        GpuTemp.Foreground = TemperatureBrush(s.GpuTemperatureC);

        SubtitleText.Text = $"Live - updated {s.Timestamp:HH:mm:ss}";
    }

    private SolidColorBrush TemperatureBrush(int celsius) => celsius switch
    {
        >= 90 => (SolidColorBrush)FindResource("Bad"),
        >= 80 => (SolidColorBrush)FindResource("Warn"),
        _ => (SolidColorBrush)FindResource("Ink"),
    };

    private void StartTrayPolling()
    {
        var slow = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
        slow.Tick += (_, _) =>
        {
            RefreshBanner();

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
                "Your power plan is in control. The Best-performance overlay is neutralised " +
                $"(minimum processor state {d.MinProcessorStateOverride}%), so it cannot pin the CPU " +
                "if another application switches to it.";
            OverlayState.Foreground = (SolidColorBrush)FindResource("Good");
            FixButton.Visibility = Visibility.Collapsed;
            return;
        }

        var problems = new List<string>();
        if (d.OverlayIsStuck)
            problems.Add(
                "The Best-performance overlay is active. It overrides your power plan and pins the " +
                "CPU at maximum frequency even at idle - which is why changing modes appears to do nothing.");
        if (d.GuardMissing)
            problems.Add(
                "No guard is in place, so any application can re-activate that overlay and pin the CPU again.");

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
                "Repair complete", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show(this,
                "Windows refused the change. Run Nextcalibur as administrator and try again.",
                "Repair failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Repair failed",
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
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Lighting", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    private void RefreshBanner()
    {
        if (_mailboxFailed || !EcMailbox.IsSupported())
        {
            ShowBanner(
                "This laptop isn't supported",
                "Nextcalibur can't find the sensors and lighting on this machine, so those " +
                "pages won't work. Power settings still do.",
                (SolidColorBrush)FindResource("Bad"));
            return;
        }

        if (StockSoftwareIsRunning())
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

    private static bool StockSoftwareIsRunning()
    {
        foreach (var name in new[] { "ControlCenter", "ControlCenterDaemon" })
        {
            var found = Process.GetProcessesByName(name);
            try
            {
                if (found.Length > 0) return true;
            }
            finally
            {
                foreach (var p in found) p.Dispose();
            }
        }
        return false;
    }
}
