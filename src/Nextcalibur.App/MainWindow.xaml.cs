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
    private readonly AppSettings _settings = AppSettings.Load();
    private readonly DispatcherTimer _timer = new();

    private EcMailbox? _mailbox;
    private ThermalReader? _thermal;
    private LedController? _led;
    private TrayPresence? _tray;

    private int _consecutiveFailures;
    private bool _exiting;
    private bool _overheatNotified;

    /// <summary>
    /// Suppresses hardware writes while the lighting controls are being filled
    /// in from stored state. Assigning IsChecked and Value raises the same
    /// events a click does.
    /// </summary>
    private bool _ledUiReady;

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

        if (!EcMailbox.IsSupported())
        {
            ShowBanner(
                "This machine is not supported",
                "Nextcalibur could not find the RW_GMWMI firmware interface. Sensor readings and " +
                "lighting are unavailable. Power-mode repair still works.",
                (SolidColorBrush)FindResource("Bad"));
            NavLighting.IsEnabled = false;
            return;
        }

        if (StockSoftwareIsRunning())
        {
            ShowBanner(
                "The vendor Control Center is running",
                "It writes to the same firmware mailbox as Nextcalibur, so both will compete and " +
                "readings may stall. Close it for reliable results.",
                (SolidColorBrush)FindResource("Warn"));
        }

        try
        {
            _mailbox = new EcMailbox();
            _thermal = new ThermalReader(_mailbox);
            _led = new LedController(_mailbox);
        }
        catch (EcMailboxUnavailableException ex)
        {
            ShowBanner("Could not open the firmware interface", ex.Message,
                (SolidColorBrush)FindResource("Bad"));
            NavLighting.IsEnabled = false;
            return;
        }

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

    // ------------------------------------------------------------- navigation

    private void OnNavChanged(object sender, RoutedEventArgs e)
    {
        if (PageSystem is null) return;   // fires once before the tree is built

        PageSystem.Visibility = NavSystem.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        PagePower.Visibility = NavPower.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        PageLighting.Visibility = NavLighting.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;

        if (NavPower.IsChecked == true) RefreshOverlay();
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
        var slow = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
        slow.Tick += (_, _) =>
        {
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
