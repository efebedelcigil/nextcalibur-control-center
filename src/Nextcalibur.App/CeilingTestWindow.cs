using System.Diagnostics;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Nextcalibur.Core.Security;
using Button = System.Windows.Controls.Button;
using CheckBox = System.Windows.Controls.CheckBox;

namespace Nextcalibur.App;

/// <summary>
/// The ceiling test, for one mode (the owner's design of 14 October 2026):
/// what is running is listed and kept current; work that must not be
/// interrupted - an update, a driver or BIOS install, maintenance - keeps
/// the test from starting at all; the person confirms, taking responsibility
/// for anything left open being closed; then, at full load, ceilings from the
/// one in force down until one holds the mode's temperature. Nothing is
/// written until the person applies the result; the services stopped are
/// started again, and the programs closed are listed for opening again.
/// Everything is in the log and the audit log - before, during and after.
/// </summary>
internal sealed class CeilingTestWindow : Window
{
    private readonly MainWindow _main;
    private readonly SystemMode _mode;
    private readonly int _current;
    private readonly Func<int?> _ecCpu;

    private readonly StackPanel _body = new();
    private readonly StackPanel _lists = new();
    private readonly TextBlock _blocked = new() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 6, 0, 6) };
    private readonly CheckBox _consent = new() { Margin = new Thickness(0, 10, 0, 0) };
    private readonly Button _start = new() { MinWidth = 130, Height = 30, Margin = new Thickness(0, 0, 8, 0), IsEnabled = false };
    private readonly Button _cancel = new() { MinWidth = 110, Height = 30 };
    private readonly TextBlock _status = new() { TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0, 8, 0, 0) };

    private Preflight? _latest;
    private int? _proposed;
    private CancellationTokenSource _scanning = new();
    private CancellationTokenSource? _running;
    private readonly List<string> _closed = [];
    private readonly List<string> _stoppedServices = [];

    public CeilingTestWindow(MainWindow main, SystemMode mode, int current, Func<int?> ecCpu)
    {
        _main = main;
        _mode = mode;
        _current = current;
        _ecCpu = ecCpu;
        Owner = main;
        Title = Strings.Get("S.CeilingTest.Title", MainWindow.ModeName(mode));
        Width = 640;
        SizeToContent = SizeToContent.Height;
        MaxHeight = 720;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        ResizeMode = ResizeMode.NoResize;
        Background = (Brush)FindResource("PanelBackground");
        Foreground = (Brush)FindResource("Ink");

        var buttons = new StackPanel { Orientation = System.Windows.Controls.Orientation.Horizontal, HorizontalAlignment = System.Windows.HorizontalAlignment.Right, Margin = new Thickness(0, 12, 0, 0) };
        _start.Content = Strings.Get("S.CeilingTest.Start");
        _cancel.Content = Strings.Get("S.CeilingTest.Cancel");
        buttons.Children.Add(_start);
        buttons.Children.Add(_cancel);

        _consent.Content = new TextBlock { Text = Strings.Get("S.CeilingTest.Consent"), TextWrapping = TextWrapping.Wrap, MaxWidth = 560 };
        _consent.Checked += (_, _) => UpdateStart();
        _consent.Unchecked += (_, _) => UpdateStart();
        _start.Click += async (_, _) =>
        {
            // One button, two jobs: start the test, then apply its result.
            if (_proposed is { } value)
            {
                if (!await _main.ConfirmCeilingAsync(_mode, value)) return;
                _main.SetCeiling(_mode, value, "ceiling test");
                Close();
            }
            else await StartAsync();
        };
        _cancel.Click += (_, _) => CancelOrClose();
        Closing += (_, e) =>
        {
            if (_running is not null) { e.Cancel = true; _running.Cancel(); }
            else _scanning.Cancel();
        };

        _body.Margin = new Thickness(18);
        _body.Children.Add(new TextBlock
        {
            Text = Strings.Get("S.CeilingTest.Intro", MainWindow.ModeName(mode), CeilingTest.TargetC(mode)),
            TextWrapping = TextWrapping.Wrap,
        });
        _body.Children.Add(_blocked);
        _body.Children.Add(new ScrollViewer { Content = _lists, MaxHeight = 360, VerticalScrollBarVisibility = ScrollBarVisibility.Auto });
        _body.Children.Add(_consent);
        _body.Children.Add(_status);
        _body.Children.Add(buttons);
        Content = _body;

        Loaded += (_, _) => _ = KeepScanningAsync(_scanning.Token);
    }

    // ------------------------------------------------------------- before

    /// <summary>The list, kept current both ways: what was closed drops out, what was opened comes in.</summary>
    private async Task KeepScanningAsync(CancellationToken stop)
    {
        while (!stop.IsCancellationRequested)
        {
            var found = await Task.Run(() => TestPreflight.Scan());
            if (stop.IsCancellationRequested) return;
            _latest = found;
            ShowPreflight(found);
            try { await Task.Delay(2000, stop); } catch (TaskCanceledException) { return; }
        }
    }

    private void ShowPreflight(Preflight found)
    {
        _blocked.Text = found.CriticalWork.Count > 0 ? Strings.Get("S.CeilingTest.Blocked", string.Join(", ", found.CriticalWork)) : string.Empty;
        _blocked.Foreground = (Brush)FindResource("Ink");
        _lists.Children.Clear();
        foreach (var kind in new[] { RunningKind.Transfer, RunningKind.App, RunningKind.Background, RunningKind.Service })
        {
            var items = found.ToClose.Where(i => i.Kind == kind).ToList();
            if (items.Count == 0) continue;
            _lists.Children.Add(new TextBlock
            {
                Text = Strings.Get($"S.CeilingTest.Group.{kind}"), FontWeight = FontWeights.SemiBold, Margin = new Thickness(0, 8, 0, 2),
            });
            foreach (var i in items)
            {
                var detail = kind == RunningKind.Transfer ? $"{i.MegabytesPerSecond:0.0} MB/s" : i.CpuPercent >= 1 ? $"%{i.CpuPercent:0}" : string.Empty;
                _lists.Children.Add(new TextBlock { Text = $"  {i.Name}  {detail}", Style = (Style)FindResource("StatusTextStyle") });
            }
        }
        if (_lists.Children.Count == 0)
            _lists.Children.Add(new TextBlock { Text = Strings.Get("S.CeilingTest.NothingRunning"), Style = (Style)FindResource("StatusTextStyle") });
        UpdateStart();
    }

    private void UpdateStart()
    {
        if (_proposed is not null) return;
        _start.IsEnabled = _running is null && _consent.IsChecked == true && _latest is { CriticalWork.Count: 0 };
    }

    // ------------------------------------------------------------- during

    private async Task StartAsync()
    {
        _scanning.Cancel();
        _start.IsEnabled = false;
        _consent.IsEnabled = false;

        // One last look, right before anything is closed.
        var final = await Task.Run(() => TestPreflight.Scan());
        if (final.CriticalWork.Count > 0)
        {
            _status.Text = Strings.Get("S.CeilingTest.Blocked", string.Join(", ", final.CriticalWork));
            Log.Info("ceiling-test", "Not started: " + string.Join(", ", final.CriticalWork));
            _consent.IsEnabled = true;
            _scanning = new CancellationTokenSource();
            _ = KeepScanningAsync(_scanning.Token);
            return;
        }

        var consentText = Strings.Get("S.CeilingTest.Consent");
        LogRunning("before", final);
        AuditLog.Record("ceiling-test-consent", new Dictionary<string, object?>
        {
            ["mode"] = _mode.ToString(),
            ["consent"] = consentText,
            ["toClose"] = string.Join("; ", final.ToClose.Select(i => $"{i.Kind}:{i.Name}#{i.Pid}")),
        });

        _running = new CancellationTokenSource();
        _cancel.Content = Strings.Get("S.CeilingTest.Stop");
        var previousMode = _main.CurrentModeForTest;
        List<CeilingStep> steps = [];
        try
        {
            _status.Text = Strings.Get("S.CeilingTest.Closing");
            await Task.Run(() => CloseAll(final));
            _main.ApplyModeForTest(_mode);
            steps = await RunStepsAsync(_running.Token);
        }
        catch (OperationCanceledException)
        {
            Log.Info("ceiling-test", "Stopped by the person");
        }
        finally
        {
            await Task.Run(() => _main.PutModePlanBack(_mode));
            await Task.Run(RestartServices);
            if (previousMode is { } back && back != _mode) _main.ApplyModeForTest(back);
            LogRunning("after", await Task.Run(() => TestPreflight.Scan(TimeSpan.FromSeconds(1))));
            _running = null;
        }
        ShowResult(steps);
    }

    private void CloseAll(Preflight found)
    {
        foreach (var item in found.ToClose)
        {
            try
            {
                if (item.Kind == RunningKind.Service && item.ServiceName is { } service)
                {
                    if (InvokeService(service, "StopService")) _stoppedServices.Add(service);
                    Log.Info("ceiling-test", $"Service stopped: {service} ({item.Name})");
                    continue;
                }

                using var p = Process.GetProcessById(item.Pid);
                var how = "closed";
                if (item.Kind == RunningKind.App && p.CloseMainWindow() && p.WaitForExit(10_000)) how = "closed";
                else
                {
                    p.Kill(entireProcessTree: false);
                    p.WaitForExit(5_000);
                    how = "forced";
                }
                _closed.Add(item.Name);
                Log.Info("ceiling-test", $"{item.Kind} {how}: {item.Name} (pid {item.Pid}, {item.Path})");
                AuditLog.Record("ceiling-test-closed", new Dictionary<string, object?>
                {
                    ["kind"] = item.Kind.ToString(), ["name"] = item.Name, ["pid"] = item.Pid, ["path"] = item.Path, ["how"] = how,
                });
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or System.ComponentModel.Win32Exception)
            {
                // Gone already.
            }
        }
    }

    private void RestartServices()
    {
        foreach (var service in _stoppedServices)
        {
            var ok = InvokeService(service, "StartService");
            Log.Info("ceiling-test", $"Service started again: {service} ({(ok ? "ok" : "failed")})");
            AuditLog.Record("ceiling-test-service", new Dictionary<string, object?> { ["service"] = service, ["startedAgain"] = ok });
        }
        _stoppedServices.Clear();
    }

    private static bool InvokeService(string name, string method)
    {
        try
        {
            using var service = new ManagementObject($"Win32_Service.Name='{name.Replace("'", "")}'");
            var result = service.InvokeMethod(method, null);
            return result is uint code && code is 0 or 10; // 10: already in that state
        }
        catch (Exception ex) when (ex is ManagementException or System.Runtime.InteropServices.COMException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    private async Task<List<CeilingStep>> RunStepsAsync(CancellationToken cancel)
    {
        var results = new List<CeilingStep>();
        using var msr = new CpuPowerReader();
        using var clock = new CpuClockReader();
        // With no limit, the firmware's top clock is where the steps start below.
        var values = CeilingTest.Steps(_mode, _current, MachineScan.Firmware.ProcessorMaxMhz ?? 0).ToList();
        for (var i = 0; i < values.Count; i++)
        {
            cancel.ThrowIfCancellationRequested();
            var value = values[i];
            _main.WriteTestCeiling(_mode, value);
            _status.Text = Strings.Get("S.CeilingTest.Step", Label(value), i + 1, values.Count);
            var step = await Task.Run(() => Measure(value, msr, clock, cancel), cancel);
            results.Add(step);
            Log.Info("ceiling-test", $"{_mode} {Label(value)}: hold {step.HoldC:0} °C, peak {step.PeakC:0} °C, {step.Watts:0.0} W, {step.Ghz:0.00} GHz");
            AuditLog.Record("ceiling-test-step", new Dictionary<string, object?>
            {
                ["mode"] = _mode.ToString(), ["value"] = value, ["holdC"] = Math.Round(step.HoldC), ["peakC"] = Math.Round(step.PeakC),
                ["watts"] = Math.Round(step.Watts, 1), ["ghz"] = Math.Round(step.Ghz, 2),
            });
            LogRunning($"step {i + 1}", await Task.Run(() => TestPreflight.Scan(TimeSpan.FromSeconds(1)), cancel));

            if (CeilingTest.Held(_mode, step)) break;

            // With no limit, the steps below start from the clock this one reached.
            if (i == 0 && _mode != SystemMode.Office && _current == 0 && MachineScan.Firmware.ProcessorMaxMhz is null)
                values = [value, .. CeilingTest.Steps(_mode, 0, (int)(step.Ghz * 1000)).Skip(1)];

            _status.Text = Strings.Get("S.CeilingTest.Cooling");
            await CoolAsync(msr, cancel);
        }
        return results;
    }

    private CeilingStep Measure(int value, CpuPowerReader msr, CpuClockReader clock, CancellationToken cancel)
    {
        using var stop = new CancellationTokenSource();
        for (var t = 0; t < Environment.ProcessorCount; t++)
            new Thread(() => { var x = 1.0; while (!stop.IsCancellationRequested) x = Math.Sqrt(x + 1.5); }) { IsBackground = true }.Start();

        var samples = new List<(double C, double W, double Ghz)>();
        msr.ReadWatts();
        var end = DateTime.UtcNow + CeilingTest.StepLength;
        var target = CeilingTest.TargetC(_mode);
        var overSince = (DateTime?)null;
        try
        {
            while (DateTime.UtcNow < end && !cancel.IsCancellationRequested)
            {
                Thread.Sleep(1000);
                // The processor's own reading first - at once, no interrupt -
                // the controller's when it has none (the owner's rule).
                double? c = msr.ReadPackageTemperatureC() ?? _ecCpu();
                if (c is not { } celsius) continue;
                samples.Add((celsius, msr.ReadWatts() ?? 0, clock.ReadGhz() ?? 0));
                Dispatcher.BeginInvoke(() => _status.Text = Strings.Get("S.CeilingTest.Reading", Label(value), celsius, samples[^1].W, samples[^1].Ghz));

                // Clearly over for ten seconds: this step will not hold; no need to keep the heat on.
                if (celsius > target + CeilingTest.PeakAllowanceC) overSince ??= DateTime.UtcNow; else overSince = null;
                if (overSince is { } since && DateTime.UtcNow - since > TimeSpan.FromSeconds(10)) break;
            }
        }
        finally
        {
            stop.Cancel();
        }
        cancel.ThrowIfCancellationRequested();

        var second = samples.Skip(samples.Count / 2).ToList();
        if (second.Count == 0) second = samples;
        return second.Count == 0
            ? new CeilingStep(value, 999, 999, 0, 0)
            : new CeilingStep(value, second.Average(s => s.C), samples.Max(s => s.C), second.Average(s => s.W), second.Average(s => s.Ghz));
    }

    private async Task CoolAsync(CpuPowerReader msr, CancellationToken cancel)
    {
        var until = DateTime.UtcNow + CeilingTest.CoolDown;
        while (DateTime.UtcNow < until)
        {
            await Task.Delay(2000, cancel);
            if (msr.ReadPackageTemperatureC() is { } c && c <= 60) return;
        }
    }

    // ------------------------------------------------------------- after

    private void ShowResult(List<CeilingStep> steps)
    {
        _cancel.Content = Strings.Get("S.CeilingTest.Close");
        _lists.Children.Clear();
        _blocked.Text = string.Empty;
        _consent.Visibility = Visibility.Collapsed;

        foreach (var s in steps)
            _lists.Children.Add(new TextBlock
            {
                Text = Strings.Get("S.CeilingTest.Row", Label(s.Value), Math.Round(s.HoldC), Math.Round(s.PeakC), s.Watts.ToString("0.0"), s.Ghz.ToString("0.00"),
                    Strings.Get(CeilingTest.Held(_mode, s) ? "S.CeilingTest.HeldYes" : "S.CeilingTest.HeldNo")),
                Style = (Style)FindResource("StatusTextStyle"),
            });

        var chosen = CeilingTest.Choose(_mode, steps);
        var closedNote = _closed.Count > 0 ? Environment.NewLine + Environment.NewLine + Strings.Get("S.CeilingTest.ClosedList", string.Join(", ", _closed.Distinct())) : string.Empty;
        AuditLog.Record("ceiling-test-result", new Dictionary<string, object?>
        {
            ["mode"] = _mode.ToString(), ["current"] = _current, ["chosen"] = chosen, ["steps"] = steps.Count,
        });

        if (steps.Count == 0)
        {
            _status.Text = Strings.Get("S.CeilingTest.Stopped") + closedNote;
            return;
        }
        if (chosen is not { } value)
        {
            _status.Text = Strings.Get("S.CeilingTest.NoneHeld", CeilingTest.TargetC(_mode)) + closedNote;
            return;
        }
        if (value == _current)
        {
            _status.Text = Strings.Get("S.CeilingTest.KeepCurrent", Label(value)) + closedNote;
            return;
        }

        _status.Text = Strings.Get("S.CeilingTest.Propose", Label(value), Label(_current)) + closedNote;
        _start.Content = Strings.Get("S.CeilingTest.Apply");
        _proposed = value;
        _start.IsEnabled = true;
    }

    private void CancelOrClose()
    {
        if (_running is not null) _running.Cancel();
        else Close();
    }

    private string Label(int value) =>
        _mode == SystemMode.Office ? Strings.Get("S.Ceiling.Percent", value)
        : value == 0 ? Strings.Get("S.Ceiling.NoLimit")
        : Strings.Get("S.Ceiling.Ghz", (value / 1000.0).ToString("0.0"));

    private static void LogRunning(string when, Preflight found)
    {
        Log.Info("ceiling-test", $"Running {when}: critical [{string.Join(", ", found.CriticalWork)}]; " +
            string.Join("; ", found.ToClose.Select(i => $"{i.Kind}:{i.Name}#{i.Pid} cpu {i.CpuPercent:0.0}% io {i.MegabytesPerSecond:0.00}MB/s")));
    }
}
