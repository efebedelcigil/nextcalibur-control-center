using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;

namespace Nextcalibur.App;

public partial class MainWindow : Window
{
    private readonly PowerOverlayService _power = new();
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };

    private EcMailbox? _mailbox;
    private ThermalReader? _thermal;
    private int _consecutiveFailures;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Closed += (_, _) => { _timer.Stop(); _mailbox?.Dispose(); };
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
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

        _timer.Tick += (_, _) => Sample();
        _timer.Start();
        Sample();
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
