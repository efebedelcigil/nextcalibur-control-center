using System.Windows;
using System.Windows.Controls;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;
using ComboBox = System.Windows.Controls.ComboBox;
using ComboBoxItem = System.Windows.Controls.ComboBoxItem;

namespace Nextcalibur.App;

/// <summary>
/// Settings > Processor ceilings: each mode's ceiling, chosen by the person
/// within the measured upper bounds (the owner's rule, 14 October 2026).
/// Power Saver as a percentage, never above 90 - any value under 100 turns
/// the boost off; the boosting modes as a clock limit, the boost kept up to
/// it. Written into the mode's plan at once, and again at every start.
/// </summary>
public partial class MainWindow
{
    private StackPanel? _ceilingRows;
    private bool _ceilingsLoading;

    private static readonly int[] PercentChoices = [90, 85, 80, 75, 70, 65, 60, 55, 50];
    private static readonly int[] FrequencyChoices = [0, 4600, 4400, 4200, 4000, 3800, 3600, 3400, 3200, 3000, 2800, 2600, 2400];

    private void WireCeilingsPage()
    {
        _ceilingRows = FindName("CeilingRows") as StackPanel;
        RefreshCeilingRows();
    }

    private void RefreshCeilingRows()
    {
        if (_ceilingRows is null) return;
        _ceilingsLoading = true;
        try
        {
            _ceilingRows.Children.Clear();
            foreach (var mode in SystemModes.InOrder)
            {
                var row = new Grid { Margin = new Thickness(0, 3, 0, 3) };
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });

                row.Children.Add(new TextBlock
                {
                    Text = ModeName(mode), FontSize = 12, FontWeight = FontWeights.SemiBold, VerticalAlignment = VerticalAlignment.Center,
                    Foreground = (System.Windows.Media.Brush)FindResource("Ink"),
                });

                var box = new ComboBox { Height = 28, IsEnabled = _support.AllowsWrites };
                var limits = _settings.LimitsFor(mode);
                if (mode == SystemMode.Office)
                {
                    var current = ModeProfile.ClampPercent(mode, limits.PercentCeiling);
                    foreach (var p in PercentChoices)
                    {
                        var label = p == ModeProfile.UpperPercent(mode) ? Strings.Get("S.Ceiling.PercentTop", p) : Strings.Get("S.Ceiling.Percent", p);
                        var item = new ComboBoxItem { Content = label, Tag = p };
                        box.Items.Add(item);
                        if (p == current) box.SelectedItem = item;
                    }
                }
                else
                {
                    var current = ModeProfile.ClampFrequency(limits.FrequencyMhz);
                    foreach (var f in FrequencyChoices)
                    {
                        var label = f == 0 ? Strings.Get("S.Ceiling.NoLimit") : Strings.Get("S.Ceiling.Ghz", (f / 1000.0).ToString("0.0"));
                        var item = new ComboBoxItem { Content = label, Tag = f };
                        box.Items.Add(item);
                        if (f == current) box.SelectedItem = item;
                    }
                    if (box.SelectedItem is null && current > 0)
                    {
                        var item = new ComboBoxItem { Content = Strings.Get("S.Ceiling.Ghz", (current / 1000.0).ToString("0.0")), Tag = current };
                        box.Items.Insert(1, item);
                        box.SelectedItem = item;
                    }
                }
                var chosenMode = mode;
                box.SelectionChanged += (_, _) =>
                {
                    if (_ceilingsLoading || box.SelectedItem is not ComboBoxItem { Tag: int value }) return;
                    SetCeiling(chosenMode, value);
                };
                Grid.SetColumn(box, 1);
                row.Children.Add(box);
                _ceilingRows.Children.Add(row);
            }
        }
        finally
        {
            _ceilingsLoading = false;
        }
    }

    /// <summary>Records a mode's ceiling and writes it into the mode's plan now.</summary>
    internal void SetCeiling(SystemMode mode, int value, string how = "chosen")
    {
        if (mode == SystemMode.Office) _settings.SaverCeilingPercent = ModeProfile.ClampPercent(mode, value);
        else _settings.FrequencyLimitsMhz[mode.ToString()] = ModeProfile.ClampFrequency(value);
        _settings.Save();

        var written = 0;
        if (_support.AllowsWrites && _settings.FirstChangesRestorePoint is not null && SystemModeService.PlanFor(mode) is { } plan)
        {
            try
            {
                written = PlanTuning.ApplyProfile(mode, plan.Plan, ModeProfile.ReadBaseline(), _settings.LimitsFor(mode)).Count;
                RememberProfileWrites();
            }
            catch (Exception ex) when (ex is not OutOfMemoryException)
            {
                Log.Warn("ceiling", $"Could not write {mode}'s ceiling: {ex.Message}");
            }
        }
        Log.Info("ceiling", $"{mode} ceiling {(mode == SystemMode.Office ? $"{value}%" : value == 0 ? "no limit" : $"{value} MHz")} ({how}); {written} setting(s) written");
        Audit("ceiling", "done", ("mode", mode.ToString()), ("value", value), ("unit", mode == SystemMode.Office ? "%" : "MHz"), ("how", how));
        RefreshCeilingRows();
    }

    /// <summary>Hook for the watch on the plans: what was just written is what the plans should hold. Filled in by MainWindow.PlanWatch.</summary>
    partial void RememberProfileWrites();
}
