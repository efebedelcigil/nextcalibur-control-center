using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Power;
using Button = System.Windows.Controls.Button;
using CheckBox = System.Windows.Controls.CheckBox;
using ComboBox = System.Windows.Controls.ComboBox;

namespace Nextcalibur.App;

/// <summary>
/// "While this program runs, this mode": the Settings page's program rules
/// (owner, 12 October 2026). Programs starting and stopping arrive as
/// Windows' own process-trace events, asked for by name - nothing polls.
/// The decision is <see cref="ProgramRules.Winning"/>; this switches and
/// switches back, and steps aside for a mode the person picks by hand.
/// </summary>
public partial class MainWindow
{
    private ManagementEventWatcher? _ruleStarts, _ruleStops;
    private ProgramRule? _activeRule;
    private SystemMode? _ruleReturnMode;

    /// <summary>Set when the person chose a mode by hand while a rule was in force: theirs stands until the rule ends.</summary>
    private bool _ruleOverridden;

    private bool _applyingRule;

    /// <summary>(Re)starts the watch for the programs named in the rules, and looks once now.</summary>
    private void StartProgramRules()
    {
        StopProgramRuleWatch();
        var names = ActiveRuleNames();
        if (names.Count == 0)
        {
            EvaluateProgramRules();
            return;
        }

        // The trace events carry the kernel's image name, which is cut at 15
        // characters ("AddOnInstaller.exe" arrives as "AddOnInstaller."), so
        // both spellings are asked for; the name itself is checked again by
        // EvaluateProgramRules, against the full process name.
        var spellings = names.SelectMany(n => new[] { n + ".exe", (n + ".exe")[..Math.Min(15, n.Length + 4)] })
            .Distinct(StringComparer.OrdinalIgnoreCase);
        var filter = string.Join(" OR ", spellings.Select(n => $"ProcessName = '{n}'"));
        try
        {
            _ruleStarts = new ManagementEventWatcher(new WqlEventQuery($"SELECT ProcessName FROM Win32_ProcessStartTrace WHERE {filter}"));
            _ruleStops = new ManagementEventWatcher(new WqlEventQuery($"SELECT ProcessName FROM Win32_ProcessStopTrace WHERE {filter}"));
            _ruleStarts.EventArrived += (_, _) => Dispatcher.BeginInvoke(EvaluateProgramRules);
            // A process that has just stopped can still be listed for a moment:
            // looked at again a little later, or the rule would never end.
            _ruleStops.EventArrived += (_, _) => Dispatcher.BeginInvoke(async () =>
            {
                EvaluateProgramRules();
                await Task.Delay(1500);
                EvaluateProgramRules();
            });
            _ruleStarts.Start();
            _ruleStops.Start();
            Log.Info("rules", $"Watching for {string.Join(", ", names)}");
        }
        catch (Exception ex) when (ex is ManagementException or UnauthorizedAccessException or System.Runtime.InteropServices.COMException)
        {
            Log.Warn("rules", "Could not watch for programs starting: " + ex.Message);
            StopProgramRuleWatch();
        }
        EvaluateProgramRules();
    }

    private void StopProgramRuleWatch()
    {
        foreach (var watcher in new[] { _ruleStarts, _ruleStops })
        {
            if (watcher is null) continue;
            try { watcher.Stop(); } catch (ManagementException) { }
            watcher.Dispose();
        }
        _ruleStarts = _ruleStops = null;
    }

    /// <summary>The names to watch: enabled rules, valid names, only where modes may be changed at all.</summary>
    private List<string> ActiveRuleNames() =>
        !_settings.ProgramRulesEnabled || !_support.AllowsWrites
            ? []
            : _settings.ProgramRules.Where(r => r.Enabled)
                .Select(r => ProgramRules.Normalise(r.Process)).OfType<string>()
                .Distinct(StringComparer.OrdinalIgnoreCase).ToList();

    /// <summary>Looks at what is running now and puts the right mode in force.</summary>
    private void EvaluateProgramRules()
    {
        // On a plan that is none of the modes', rules do not switch it away.
        if (OnForeignPlan) return;
        if (_exiting) return;
        var names = ActiveRuleNames();
        var running = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
        foreach (var name in names)
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName(name))
            {
                using (process)
                {
                    DateTime started;
                    try { started = process.StartTime; } catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException) { started = DateTime.MinValue; }
                    if (!running.TryGetValue(name, out var seen) || started > seen) running[name] = started;
                }
            }
        }

        var winner = ProgramRules.Winning(_settings.ProgramRules, running, _onBattery ? _settings.ModeOnBattery : null);

        if (winner is not null)
        {
            if (_activeRule is null) _ruleReturnMode = _currentMode;
            var changed = !ReferenceEquals(_activeRule, winner);

            // A choice by hand stood against the rule then in force; another
            // program's rule is a new rule.
            if (changed) _ruleOverridden = false;
            _activeRule = winner;
            if (changed && !_ruleOverridden && _currentMode != winner.Mode && ApplyRuleMode(winner.Mode))
                _tray?.ShowMessage(Strings.Get("S.Rules.Title"), Strings.Get("S.Rules.Started", winner.Process, ModeName(winner.Mode)));
            if (changed) RefreshProgramRuleRows();
            return;
        }

        EndRuleNow();
    }

    /// <summary>A mode for a rule: applied as a click would, but not remembered as the person's choice.</summary>
    private bool ApplyRuleMode(SystemMode mode)
    {
        if (mode == SystemMode.Performance && !PowerOverlayService.PerformanceModeIsSafe(new PowerOverlayService().Diagnose()))
            return false;
        try
        {
            _applyingRule = true;
            ApplyMode(mode);
            _currentMode = mode;
            LoadSystemMode();
            RefreshOverlay();
            Log.Info("rules", $"Mode {mode} for a program rule");
            return true;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("rules", $"Could not switch to {mode}: {ex.Message}");
            return false;
        }
        finally
        {
            _applyingRule = false;
        }
    }

    /// <summary>
    /// Called from the mode buttons: a choice by hand while a rule is in
    /// force wins over that rule, and is the mode to go back to when the
    /// rules end.
    /// </summary>
    private void NoteModeChosenByHand(SystemMode chosen)
    {
        if (_applyingRule || _activeRule is null) return;
        _ruleOverridden = true;
        _ruleReturnMode = chosen;
        Log.Info("rules", $"{chosen} chosen by hand while {_activeRule.Process} runs; the rule steps aside");
    }

    private DateTime _rulesLookedAt = DateTime.MinValue;

    /// <summary>
    /// Once a minute from the slow timer while a rule is in force: the safety
    /// net under the stop event, should Windows' event not come.
    /// </summary>
    private void ProgramRulesTick()
    {
        if (_activeRule is null || DateTime.UtcNow - _rulesLookedAt < TimeSpan.FromMinutes(1)) return;
        _rulesLookedAt = DateTime.UtcNow;
        EvaluateProgramRules();
    }

    // --------------------------------------------------------- settings page

    private ToggleButton? _rulesMaster;
    private StackPanel? _rulesRows;
    private ComboBox? _rulesName, _rulesMode;
    private bool _rulesUiLoading;

    private void WireProgramRulesPage()
    {
        _rulesMaster = FindName("SettingProgramRules") as ToggleButton;
        _rulesRows = FindName("ProgramRuleRows") as StackPanel;
        _rulesName = FindName("ProgramRuleName") as ComboBox;
        _rulesMode = FindName("ProgramRuleMode") as ComboBox;
        if (_rulesMaster is null || _rulesRows is null || _rulesName is null || _rulesMode is null) return;

        foreach (var mode in SystemModes.InOrder) _rulesMode.Items.Add(new ModeChoice(mode));
        _rulesMode.SelectedIndex = 0;

        if (FindName("SettingAskGameMode") is ToggleButton askGames)
        {
            askGames.IsChecked = _settings.AskModeForGames;
            askGames.IsEnabled = _support.AllowsWrites;
            askGames.Checked += (_, _) => { _settings.AskModeForGames = true; _settings.Save(); };
            askGames.Unchecked += (_, _) => { _settings.AskModeForGames = false; _settings.Save(); };
        }

        _rulesMaster.Checked += (_, _) => SetRulesMaster(true);
        _rulesMaster.Unchecked += (_, _) => SetRulesMaster(false);
        _rulesName.DropDownOpened += (_, _) => FillRunningPrograms();
        if (FindName("ProgramRuleAdd") is Button add) add.Click += (_, _) => AddProgramRule();
        RefreshProgramRuleRows();
    }

    private void SetRulesMaster(bool on)
    {
        if (_rulesUiLoading) return;
        _settings.ProgramRulesEnabled = on;
        _settings.Save();
        if (!on) EndRuleNow();
        StartProgramRules();
        RefreshProgramRuleRows();
    }

    /// <summary>
    /// The rule in force ends: its program closed, or the rule was switched
    /// off or removed. The mode goes back to what it was before the rule -
    /// unless the person chose one by hand in the meantime.
    /// </summary>
    private void EndRuleNow()
    {
        if (_activeRule is null) return;
        var ended = _activeRule;
        _activeRule = null;

        // On battery with Office on battery on, the battery's rule has the
        // mode: going back to Gaming or Performance there would undo it.
        var ceiling = _onBattery ? _settings.ModeOnBattery : null;
        if (!_ruleOverridden && _ruleReturnMode is { } back && _currentMode != back
            && !(ceiling is { } c && !SystemModes.AtMost(back, c)) && ApplyRuleMode(back))
            _tray?.ShowMessage(Strings.Get("S.Rules.Title"), Strings.Get("S.Rules.Ended", ended.Process, ModeName(back)));
        _ruleReturnMode = null;
        _ruleOverridden = false;
        RefreshProgramRuleRows();
    }

    /// <summary>The programs running now with a window, first, then the rest - for picking from rather than typing.</summary>
    private void FillRunningPrograms()
    {
        if (_rulesName is null) return;
        var typed = _rulesName.Text;
        var withWindow = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
        var others = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var process in System.Diagnostics.Process.GetProcesses())
        {
            using (process)
            {
                if (process.Id is 0 or 4 || ProgramRules.Normalise(process.ProcessName) is not { } name) continue;
                try { (process.MainWindowHandle != IntPtr.Zero ? withWindow : others).Add(name); }
                catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception) { others.Add(name); }
            }
        }
        others.ExceptWith(withWindow);
        _rulesName.Items.Clear();
        foreach (var name in withWindow.Concat(others)) _rulesName.Items.Add(name);
        _rulesName.Text = typed;
    }

    private void AddProgramRule()
    {
        if (_rulesName is null || _rulesMode?.SelectedItem is not ModeChoice { Mode: var mode }) return;
        if (ProgramRules.Normalise(_rulesName.Text) is not { } name)
        {
            Dialogs.Tell(Strings.Get("S.Rules.Title"), Strings.Get("S.Rules.BadName"));
            return;
        }
        if (_settings.ProgramRules.FirstOrDefault(r => string.Equals(r.Process, name, StringComparison.OrdinalIgnoreCase)) is { } existing)
            existing.Mode = mode;
        else
            _settings.ProgramRules.Add(new ProgramRule { Process = name, Mode = mode });
        _settings.Save();
        Log.Info("rules", $"Rule: {name} -> {mode}");
        _rulesName.Text = string.Empty;
        StartProgramRules();
        RefreshProgramRuleRows();
    }

    private void RefreshProgramRuleRows()
    {
        if (_rulesRows is null || _rulesMaster is null) return;
        _rulesUiLoading = true;
        try
        {
            var casper = _support.AllowsWrites;
            _rulesMaster.IsChecked = _settings.ProgramRulesEnabled;
            _rulesMaster.IsEnabled = casper;
            _rulesRows.IsEnabled = casper && _settings.ProgramRulesEnabled;
            if (FindName("ProgramRuleAddRow") is UIElement addRow) addRow.IsEnabled = casper && _settings.ProgramRulesEnabled;

            _rulesRows.Children.Clear();
            foreach (var rule in _settings.ProgramRules.ToList())
            {
                var row = new Grid { Margin = new Thickness(0, 3, 0, 3) };
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var current = rule;
                var box = new CheckBox { IsChecked = rule.Enabled, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 10, 0) };
                box.Checked += (_, _) => { if (_rulesUiLoading) return; current.Enabled = true; _settings.Save(); StartProgramRules(); };
                box.Unchecked += (_, _) => { if (_rulesUiLoading) return; current.Enabled = false; _settings.Save(); if (ReferenceEquals(_activeRule, current)) EndRuleNow(); StartProgramRules(); };
                row.Children.Add(box);

                var text = new TextBlock
                {
                    Text = ReferenceEquals(_activeRule, rule) ? Strings.Get("S.Rules.RowActive", rule.Process) : rule.Process,
                    FontSize = 12, FontWeight = FontWeights.SemiBold, VerticalAlignment = VerticalAlignment.Center,
                    Foreground = (System.Windows.Media.Brush)FindResource("Ink"),
                };
                Grid.SetColumn(text, 1);
                row.Children.Add(text);

                var mode = new ComboBox { MinWidth = 120, Margin = new Thickness(8, 0, 8, 0), VerticalAlignment = VerticalAlignment.Center };
                foreach (var m in SystemModes.InOrder) mode.Items.Add(new ModeChoice(m));
                mode.SelectedIndex = SystemModes.Rank(rule.Mode);
                mode.SelectionChanged += (_, _) =>
                {
                    if (_rulesUiLoading || mode.SelectedItem is not ModeChoice { Mode: var chosen }) return;
                    current.Mode = chosen;
                    _settings.Save();
                    // In force now: the new mode at once; the mode to go back to stays.
                    if (ReferenceEquals(_activeRule, current) && !_ruleOverridden && _currentMode != chosen) ApplyRuleMode(chosen);
                };
                Grid.SetColumn(mode, 2);
                row.Children.Add(mode);

                var remove = new Button { Content = "✕", Width = 28, Height = 28, ToolTip = Strings.Get("S.Rules.Remove") };
                remove.Click += (_, _) =>
                {
                    if (ReferenceEquals(_activeRule, current)) EndRuleNow();
                    _settings.ProgramRules.Remove(current);
                    _settings.Save();
                    Log.Info("rules", $"Rule removed: {current.Process}");
                    StartProgramRules();
                    RefreshProgramRuleRows();
                };
                Grid.SetColumn(remove, 3);
                row.Children.Add(remove);

                _rulesRows.Children.Add(row);
            }
        }
        finally
        {
            _rulesUiLoading = false;
        }
    }
}
