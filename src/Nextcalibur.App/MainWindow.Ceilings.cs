using System.Windows;
using System.Windows.Controls;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
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
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

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

                var test = new System.Windows.Controls.Button
                {
                    Content = Strings.Get("S.CeilingTest.Button"), MinWidth = 90, Height = 28, Margin = new Thickness(8, 0, 0, 0),
                    IsEnabled = _support.AllowsWrites && CpuPowerReader.DriverInstalled,
                    ToolTip = Strings.Get("S.CeilingTest.ButtonTip"),
                };
                test.Click += (_, _) => OpenCeilingTest(chosenMode);
                Grid.SetColumn(test, 2);
                row.Children.Add(test);
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

    // ------------------------------------------------------------ the test

    /// <summary>Opens the ceiling test for a mode.</summary>
    internal void OpenCeilingTest(SystemMode mode)
    {
        if (!_support.AllowsWrites || _settings.FirstChangesRestorePoint is null) return;
        var limits = _settings.LimitsFor(mode);
        var current = mode == SystemMode.Office ? ModeProfile.ClampPercent(mode, limits.PercentCeiling) : ModeProfile.ClampFrequency(limits.FrequencyMhz);
        Log.Info("ceiling-test", $"Opened for {mode}, ceiling in force {current}");
        new CeilingTestWindow(this, mode, current, () => _thermal is { } t && t.TryRead(out var s) ? s.CpuTemperatureC : null).ShowDialog();
    }

    /// <summary>The mode in force, for the test to come back to.</summary>
    internal SystemMode? CurrentModeForTest => _currentMode;

    /// <summary>A mode switched to for the test - and back after - as the mode buttons do it.</summary>
    internal void ApplyModeForTest(SystemMode mode)
    {
        try
        {
            ApplyMode(mode);
            _currentMode = mode;
            LoadSystemMode();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("ceiling-test", $"Could not switch to {mode}: {ex.Message}");
        }
    }

    /// <summary>One step's ceiling, written straight into the mode's plan; the settings are not touched.</summary>
    internal void WriteTestCeiling(SystemMode mode, int value)
    {
        if (SystemModeService.PlanFor(mode) is not { } plan) return;
        if (mode == SystemMode.Office)
        {
            PlanTuning.Write(plan.Plan, PlanTuning.MaxState, new PlanTuning.AcDc(value, value));
            PlanTuning.Write(plan.Plan, PlanTuning.MaxState1, new PlanTuning.AcDc(value, value));
        }
        else
        {
            PlanTuning.Write(plan.Plan, ModeProfile.FrequencyMax, new PlanTuning.AcDc(value, value));
            PlanTuning.Write(plan.Plan, ModeProfile.FrequencyMax1, new PlanTuning.AcDc(value, value));
        }
    }

    /// <summary>After the test, the mode's plan as the settings have it - the ceiling in force, not the last step's.</summary>
    internal void PutModePlanBack(SystemMode mode)
    {
        if (SystemModeService.PlanFor(mode) is { } plan)
            PlanTuning.ApplyProfile(mode, plan.Plan, ModeProfile.ReadBaseline(), _settings.LimitsFor(mode));
    }

    /// <summary>The result, applied the way every change that could need undoing is: asked, with a restore point offered.</summary>
    internal Task<bool> ConfirmCeilingAsync(SystemMode mode, int value) =>
        ConfirmRiskyAsync("ceiling-test", Strings.Get("S.CeilingTest.ApplyTitle"),
            Strings.Get("S.CeilingTest.ApplyBody", ModeName(mode), mode == SystemMode.Office ? Strings.Get("S.Ceiling.Percent", value) : Strings.Get("S.Ceiling.Ghz", (value / 1000.0).ToString("0.0"))));

    /// <summary>Hook for the watch on the plans: what was just written is what the plans should hold. Filled in by MainWindow.PlanWatch.</summary>
    partial void RememberProfileWrites();
}
