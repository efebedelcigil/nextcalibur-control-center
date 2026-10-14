using System.Windows;
using System.Windows.Controls;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using CheckBox = System.Windows.Controls.CheckBox;

namespace Nextcalibur.App;

/// <summary>
/// Settings > Programs busy at idle: what keeps the machine working while
/// nobody uses it, found on a scan the person starts, and Windows'
/// Efficiency mode for the ones they pick (see <see cref="EfficiencyMode"/>).
/// Never Windows itself, drivers' or security software - the same rules as
/// the ceiling test's list. The picked ones are set again at every start and
/// whenever they start again.
/// </summary>
public partial class MainWindow
{
    private StackPanel? _idleLoadRows;
    private IReadOnlyList<RunningItem> _idleLoadFound = [];
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
            var box = new CheckBox
            {
                IsChecked = on, Margin = new Thickness(0, 3, 0, 3), Foreground = (System.Windows.Media.Brush)FindResource("Ink"),
                Content = found is null ? name : Strings.Get("S.IdleLoad.Row", name, found.CpuPercent.ToString("0.0")),
            };
            var program = name;
            box.Click += (_, _) => SetEfficiency(program, box.IsChecked == true);
            _idleLoadRows.Children.Add(box);
        }
    }

    private void SetEfficiency(string program, bool on)
    {
        if (on && !_settings.EfficiencyModePrograms.Contains(program, StringComparer.OrdinalIgnoreCase))
            _settings.EfficiencyModePrograms.Add(program);
        if (!on) _settings.EfficiencyModePrograms.RemoveAll(p => string.Equals(p, program, StringComparison.OrdinalIgnoreCase));
        _settings.Save();
        var changed = EfficiencyMode.SetByName(program, on);
        Log.Info("idle-load", $"Efficiency mode {(on ? "on" : "off")} for {program}: {changed} process(es)");
        Audit("efficiency-mode", on ? "on" : "off", ("program", program), ("processes", changed));
    }

    /// <summary>The picked programs into Efficiency mode, at start and then once a minute (a restarted program starts without it).</summary>
    private void ApplyEfficiencyModes()
    {
        if (_settings.EfficiencyModePrograms.Count == 0 || DateTime.UtcNow - _efficiencyAppliedAt < TimeSpan.FromMinutes(1)) return;
        _efficiencyAppliedAt = DateTime.UtcNow;
        foreach (var program in _settings.EfficiencyModePrograms) EfficiencyMode.SetByName(program, true);
    }
}
