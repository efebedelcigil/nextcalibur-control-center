using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using CheckBox = System.Windows.Controls.CheckBox;

namespace Nextcalibur.App;

/// <summary>
/// Settings > Programs busy at idle: what keeps the machine working while
/// nobody uses it, found on a scan, and Windows' Efficiency mode for the ones
/// picked (see <see cref="EfficiencyMode"/>). Never Windows itself, drivers'
/// or security software - the same rules as the ceiling test's list. The
/// picked ones are set again at every start and whenever they start again.
///
/// The automatic side (the owner's ask of 14 October 2026), off until
/// switched on: every quarter of an hour, only while nobody has touched the
/// machine for a few minutes and nothing is full screen, a five-second scan
/// on a low-priority thread; a background program busy on two such scans in
/// a row is put into Efficiency mode (<see cref="IdleLoadPicker"/>). One the
/// person unticks is never added again.
/// </summary>
public partial class MainWindow
{
    private static readonly TimeSpan AutoScanEvery = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan AutoScanAfterIdle = TimeSpan.FromMinutes(3);

    private StackPanel? _idleLoadRows;
    private ToggleButton? _idleLoadAuto;
    private IReadOnlyList<RunningItem> _idleLoadFound = [];
    private IReadOnlyList<RunningItem> _autoScanPrevious = [];
    private DateTime _autoScanPreviousAt = DateTime.MinValue;
    private DateTime _autoScannedAt = DateTime.MinValue;
    private bool _autoScanRunning;
    private DateTime _efficiencyAppliedAt = DateTime.MinValue;

    private void WireIdleLoadPage()
    {
        _idleLoadRows = FindName("IdleLoadRows") as StackPanel;
        if (FindName("IdleLoadScan") is System.Windows.Controls.Button scan)
            scan.Click += async (_, _) =>
            {
                scan.IsEnabled = false;
                try
                {
                    var found = await Task.Run(() => TestPreflight.Scan(TimeSpan.FromSeconds(5)));
                    // Busy without a window in front: the background load, not what the person is using.
                    _idleLoadFound = found.ToClose
                        .Where(i => i.Kind is RunningKind.Background or RunningKind.App && i.CpuPercent >= 0.3)
                        .OrderByDescending(i => i.CpuPercent).ToList();
                    Log.Info("idle-load", "Scan: " + string.Join("; ", _idleLoadFound.Select(i => $"{i.Name} {i.CpuPercent:0.0}%")));
                    RefreshIdleLoadRows();
                }
                finally
                {
                    scan.IsEnabled = true;
                }
            };

        _idleLoadAuto = FindName("SettingIdleLoadAuto") as ToggleButton;
        if (_idleLoadAuto is not null)
        {
            _idleLoadAuto.IsChecked = _settings.EfficiencyModeAuto;
            _idleLoadAuto.Click += (_, _) =>
            {
                _settings.EfficiencyModeAuto = _idleLoadAuto.IsChecked == true;
                _settings.Save();
                _autoScanPrevious = [];
                _autoScannedAt = DateTime.MinValue;
                Log.Info("idle-load", "Automatic Efficiency mode " + (_settings.EfficiencyModeAuto ? "on" : "off"));
                Audit("efficiency-mode-auto", _settings.EfficiencyModeAuto ? "on" : "off");
            };
        }

        RefreshIdleLoadRows();
        ApplyEfficiencyModes();
    }

    private void RefreshIdleLoadRows()
    {
        if (_idleLoadRows is null) return;
        _idleLoadRows.Children.Clear();
        var names = _settings.EfficiencyModePrograms
            .Concat(_idleLoadFound.Select(i => ProgramRules.Normalise(i.Name)).OfType<string>())
            .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        if (names.Count == 0)
        {
            _idleLoadRows.Children.Add(new TextBlock { Text = Strings.Get("S.IdleLoad.NothingYet"), Style = (Style)FindResource("StatusTextStyle") });
            return;
        }
        foreach (var name in names)
        {
            var found = _idleLoadFound.FirstOrDefault(i => string.Equals(ProgramRules.Normalise(i.Name), name, StringComparison.OrdinalIgnoreCase));
            var on = _settings.EfficiencyModePrograms.Contains(name, StringComparer.OrdinalIgnoreCase);
            var auto = _settings.EfficiencyModeAutoAdded.Contains(name, StringComparer.OrdinalIgnoreCase);
            var text = found is null ? name : Strings.Get("S.IdleLoad.Row", name, found.CpuPercent.ToString("0.0"));
            if (auto) text = Strings.Get("S.IdleLoad.RowAuto", text);
            var box = new CheckBox
            {
                IsChecked = on, Margin = new Thickness(0, 3, 0, 3), Foreground = (System.Windows.Media.Brush)FindResource("Ink"),
                Content = text,
            };
            var program = name;
            box.Click += (_, _) => SetEfficiency(program, box.IsChecked == true, byHand: true);
            _idleLoadRows.Children.Add(box);
        }
    }

    private void SetEfficiency(string program, bool on, bool byHand)
    {
        var comparer = StringComparer.OrdinalIgnoreCase;
        if (on && !_settings.EfficiencyModePrograms.Contains(program, comparer))
            _settings.EfficiencyModePrograms.Add(program);
        if (!on) _settings.EfficiencyModePrograms.RemoveAll(p => comparer.Equals(p, program));

        if (byHand)
        {
            // A hand on it decides: taken out, the automatic scan leaves it be
            // for good; put back, it is the person's own pick.
            _settings.EfficiencyModeAutoAdded.RemoveAll(p => comparer.Equals(p, program));
            if (!on && !_settings.EfficiencyModeExcluded.Contains(program, comparer)) _settings.EfficiencyModeExcluded.Add(program);
            if (on) _settings.EfficiencyModeExcluded.RemoveAll(p => comparer.Equals(p, program));
        }
        else if (on && !_settings.EfficiencyModeAutoAdded.Contains(program, comparer))
        {
            _settings.EfficiencyModeAutoAdded.Add(program);
        }
        _settings.Save();

        var changed = EfficiencyMode.SetByName(program, on);
        Log.Info("idle-load", $"Efficiency mode {(on ? "on" : "off")} for {program}{(byHand ? "" : " (automatic)")}: {changed} process(es)");
        Audit("efficiency-mode", on ? "on" : "off", ("program", program), ("processes", changed), ("automatic", !byHand));
    }

    /// <summary>The picked programs into Efficiency mode, at start and then once a minute (a restarted program starts without it).</summary>
    private void ApplyEfficiencyModes()
    {
        if (_settings.EfficiencyModePrograms.Count == 0 || DateTime.UtcNow - _efficiencyAppliedAt < TimeSpan.FromMinutes(1)) return;
        // During a game nothing new is looked for; what is already set stays set.
        if (NoticeGate.ShouldWait) return;
        _efficiencyAppliedAt = DateTime.UtcNow;
        foreach (var program in _settings.EfficiencyModePrograms) EfficiencyMode.SetByName(program, true);
    }

    /// <summary>From the slow timer: the automatic scan, when it is time and nobody is using the machine.</summary>
    private async void AutoEfficiencyTick()
    {
        if (!_settings.EfficiencyModeAuto || _autoScanRunning || CeilingTestRunning) return;
        if (DateTime.UtcNow - _autoScannedAt < AutoScanEvery) return;
        if (UserPresence.SinceLastInput() < AutoScanAfterIdle || NoticeGate.ShouldWait || _cooling.Engaged) return;

        _autoScanRunning = true;
        _autoScannedAt = DateTime.UtcNow;
        try
        {
            var now = await Task.Run(() =>
            {
                // Below everything else the machine is doing: this is the
                // one part of Nextcalibur that looks for work to save.
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                try { return TestPreflight.Scan(TimeSpan.FromSeconds(5)).ToClose; }
                finally { Thread.CurrentThread.Priority = ThreadPriority.Normal; }
            });
            // Someone came back during the five seconds: what was measured was them.
            if (UserPresence.SinceLastInput() < TimeSpan.FromSeconds(10)) return;

            var previous = DateTime.UtcNow - _autoScanPreviousAt <= AutoScanEvery * 2.5 ? _autoScanPrevious : [];
            var picked = IdleLoadPicker.Choose(previous, now, _settings.EfficiencyModePrograms, _settings.EfficiencyModeExcluded);
            _autoScanPrevious = now;
            _autoScanPreviousAt = DateTime.UtcNow;
            foreach (var name in picked) SetEfficiency(name, on: true, byHand: false);
            if (picked.Count > 0) RefreshIdleLoadRows();
            Log.Info("idle-load", $"Automatic scan: {now.Count(i => i.Kind == RunningKind.Background)} busy in the background, {picked.Count} put into Efficiency mode");
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("idle-load", "Automatic scan: " + ex.Message);
        }
        finally
        {
            _autoScanRunning = false;
        }
    }
}
