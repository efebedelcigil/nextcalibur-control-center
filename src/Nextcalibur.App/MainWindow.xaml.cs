using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
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
    private TrayPresence? _tray;
    private LedController? _led;
    private bool _ledUiReady;
    private int _consecutiveFailures;
    private bool _exiting;
    private bool _overheatNotified;

    public MainWindow()
    {
        InitializeComponent();
        _timer.Interval = TimeSpan.FromMilliseconds(_settings.PollIntervalMs);
        Loaded += OnLoaded;
        StateChanged += OnStateChanged;
        Closing += OnClosing;
    }

    /// <summary>Ends the process rather than hiding to the tray.</summary>
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

        // Closing the window keeps the app alive in the notification area, which
        // is the only way the temperature warning is of any use.
        e.Cancel = true;
        Hide();
        _timer.Stop();
    }

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
                "Nextcalibur could not find the RW_GMWMI firmware interface. Sensor readings are " +
                "unavailable. Power-mode repair still works.",
                (SolidColorBrush)FindResource("Bad"));
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
        }
        catch (EcMailboxUnavailableException ex)
        {
            ShowBanner("Could not open the firmware interface", ex.Message,
                (SolidColorBrush)FindResource("Bad"));
            return;
        }

        SetUpLighting();

        _timer.Tick += (_, _) => Sample();
        Sample();

        // A hidden window has nothing to draw; the tray tooltip is refreshed on
        // the slow timer below instead.
        if (IsVisible) _timer.Start();
        StartTrayPolling();
    }

    private void SetUpLighting()
    {
        if (_mailbox is null) { LedCard.Visibility = Visibility.Collapsed; return; }

        _led = new LedController(_mailbox);

        EffectBox.ItemsSource = new[]
        {
            LedEffect.Static, LedEffect.Breathing, LedEffect.Blink, LedEffect.Heartbeat,
            LedEffect.ColourCycle, LedEffect.Wave, LedEffect.Off,
        };
        EffectBox.SelectedItem = _led.State.Effect;
        BrightnessSlider.Value = _led.State.BrightnessPercent;
        BrightnessValue.Text = $"{_led.State.BrightnessPercent}%";
        foreach (var (button, zone) in ZoneButtons()) button.Background = BrushFor(zone);

        // Only now may the selection handlers write to hardware; assigning the
        // values above raises SelectionChanged.
        _ledUiReady = true;
    }

    private IEnumerable<(System.Windows.Controls.Button Button, LedZone Zone)> ZoneButtons()
    {
        yield return (ZoneA, LedZone.Left);
        yield return (ZoneB, LedZone.Middle);
        yield return (ZoneC, LedZone.Right);
    }

    private SolidColorBrush BrushFor(LedZone zone)
    {
        var (r, g, b) = _led!.State.GetColour(zone);
        return new SolidColorBrush(Color.FromRgb(r, g, b));
    }

    private void OnZoneClick(object sender, RoutedEventArgs e)
    {
        if (_led is null || sender is not System.Windows.Controls.Button button) return;
        if (!Enum.TryParse<LedZone>(button.Tag?.ToString(), out var zone)) return;

        var (r, g, b) = _led.State.GetColour(zone);
        using var dialog = new System.Windows.Forms.ColorDialog
        {
            Color = System.Drawing.Color.FromArgb(r, g, b),
            FullOpen = true,
        };
        if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

        RunLighting(() => _led.SetColour(zone, dialog.Color.R, dialog.Color.G, dialog.Color.B));
        button.Background = BrushFor(zone);
    }

    private void OnEffectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (!_ledUiReady || _led is null || EffectBox.SelectedItem is not LedEffect effect) return;
        RunLighting(() => _led.SetEffect(effect));
    }

    private void OnBrightnessChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        var percent = (int)Math.Round(e.NewValue);
        BrightnessValue.Text = $"{percent}%";
        if (!_ledUiReady || _led is null) return;
        RunLighting(() => _led.SetBrightness(percent));
    }

    private void OnLightsOffClick(object sender, RoutedEventArgs e)
    {
        if (_led is null) return;
        RunLighting(_led.TurnOff);
    }

    /// <summary>
    /// Runs a lighting change, pausing sensor sampling first. Both share the
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

    /// <summary>
    /// Refreshes the tray tooltip and the overheat warning on a slow cadence
    /// that runs whether or not the window is on screen.
    /// </summary>
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

    private void RefreshOverlay()
    {
        var d = _power.Diagnose();
        OverlayState.Text = PowerOverlays.Describe(d.ActiveOverlay);

        if (!d.NeedsRepair)
        {
            OverlayDetail.Text =
                $"Your power plan is in control. The Best-performance overlay is neutralised " +
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
