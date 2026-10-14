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

        // One watch for every program start and stop, the names checked here
        // (14 October 2026): a WHERE clause with a term for each name was
        // refused by Windows ("Invalid parameter") once the game scan had
        // added a dozen, and every rule silently stopped working. Process
        // starts are a few a second at most; a set lookup each costs nothing.
        var watched = new HashSet<string>(names, StringComparer.OrdinalIgnoreCase);
        try
        {
            _ruleStarts = new ManagementEventWatcher(new WqlEventQuery("SELECT ProcessName FROM Win32_ProcessStartTrace"));
            _ruleStops = new ManagementEventWatcher(new WqlEventQuery("SELECT ProcessName FROM Win32_ProcessStopTrace"));
            _ruleStarts.EventArrived += (_, e) =>
            {
                if (ProgramRules.IsWatched(watched, e.NewEvent["ProcessName"] as string)) Dispatcher.BeginInvoke(EvaluateProgramRules);
            };
            // A process that has just stopped can still be listed for a moment:
            // looked at again a little later, or the rule would never end.
            _ruleStops.EventArrived += (_, e) =>
            {
                if (!ProgramRules.IsWatched(watched, e.NewEvent["ProcessName"] as string)) return;
                Dispatcher.BeginInvoke(async () =>
                {
                    EvaluateProgramRules();
                    await Task.Delay(1500);
                    EvaluateProgramRules();
                });
            };
            _ruleStarts.Start();
            _ruleStops.Start();
            Log.Info("rules", "Watching for program starts", ("programs", names.Count), ("names", string.Join(", ", names)));
        }
        catch (Exception ex) when (ex is ManagementException or UnauthorizedAccessException or System.Runtime.InteropServices.COMException)
        {
            // Not silent and not stuck: the slow timer looks instead (see ProgramRulesTick).
            Log.Warn("rules", "Could not watch for programs starting; looking every minute instead: " + ex.Message);
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
        // On a plan that is none of the modes', rules do not switch it away;
        // nor during a ceiling test, which holds its own mode.
        if (OnForeignPlan || CeilingTestRunning) return;
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
        // With no watch (Windows refused it), the rules are looked at here instead.
        var noWatch = _ruleStarts is null && ActiveRuleNames().Count > 0;
        if ((_activeRule is null && !noWatch) || DateTime.UtcNow - _rulesLookedAt < TimeSpan.FromMinutes(1)) return;
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
        WireGameScan();
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
        _settings.GamesExcluded.RemoveAll(g => string.Equals(g, name, StringComparison.OrdinalIgnoreCase));
        _settings.Save();
        Log.Info("rules", $"Rule: {name} -> {mode}");
        _rulesName.Text = string.Empty;
        StartProgramRules();
        RefreshProgramRuleRows();
    }

    /// <summary>The games whose programs are shown opened out under their name.</summary>
    private readonly HashSet<string> _rulesExpanded = new(StringComparer.CurrentCultureIgnoreCase);

    /// <summary>
    /// The list. A game the scan found with more than one program (GTA V:
    /// GTA5 and PlayGTAV) is one row under its name, with a small arrow that
    /// opens its programs underneath like a folder tree (the owner's ask of
    /// 14 October 2026); its switch, mode and remove act on all of them. A
    /// program added by hand is a row of its own.
    /// </summary>
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
            var groups = _settings.ProgramRules.ToList()
                .GroupBy(r => r.Label is { Length: > 0 } label ? "L:" + label : "P:" + r.Process, StringComparer.CurrentCultureIgnoreCase);
            foreach (var group in groups)
            {
                var rules = group.ToList();
                if (rules.Count == 1) { _rulesRows.Children.Add(RuleRow(rules, rules[0].Label is { Length: > 0 } l ? $"{l}  ({rules[0].Process})" : rules[0].Process, expander: null)); continue; }

                var label = rules[0].Label!;
                var open = _rulesExpanded.Contains(label);
                var children = new StackPanel { Margin = new Thickness(26, 0, 0, 2), Visibility = open ? Visibility.Visible : Visibility.Collapsed };
                for (var i = 0; i < rules.Count; i++)
                    children.Children.Add(ChildRow(rules[i], last: i == rules.Count - 1));
                _rulesRows.Children.Add(RuleRow(rules, label, Chevron(open, () =>
                {
                    var nowOpen = children.Visibility != Visibility.Visible;
                    children.Visibility = nowOpen ? Visibility.Visible : Visibility.Collapsed;
                    if (nowOpen) _rulesExpanded.Add(label); else _rulesExpanded.Remove(label);
                    return nowOpen;
                })));
                _rulesRows.Children.Add(children);
            }
        }
        finally
        {
            _rulesUiLoading = false;
        }
    }

    /// <summary>One row: a rule, or a game's rules together.</summary>
    private Grid RuleRow(List<ProgramRule> rules, string shown, FrameworkElement? expander)
    {
        var row = new Grid { Margin = new Thickness(0, 3, 0, 3) };
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        // The arrow's column is kept for every row, so the names line up.
        var arrow = expander ?? new Border { Width = 18 };
        row.Children.Add(arrow);

        var box = new CheckBox { IsChecked = rules.All(r => r.Enabled), VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 10, 0) };
        box.Click += (_, _) =>
        {
            var on = box.IsChecked == true;
            foreach (var r in rules)
            {
                r.Enabled = on;
                if (!on && ReferenceEquals(_activeRule, r)) EndRuleNow();
            }
            _settings.Save();
            StartProgramRules();
            RefreshProgramRuleRows();
        };
        Grid.SetColumn(box, 1);
        row.Children.Add(box);

        var active = rules.Any(r => ReferenceEquals(_activeRule, r));
        var text = new TextBlock
        {
            Text = active ? Strings.Get("S.Rules.RowActive", shown) : shown,
            TextTrimming = TextTrimming.CharacterEllipsis, ToolTip = string.Join(", ", rules.Select(r => r.Process)),
            FontSize = 12, FontWeight = FontWeights.SemiBold, VerticalAlignment = VerticalAlignment.Center,
            Foreground = (System.Windows.Media.Brush)FindResource("Ink"),
        };
        Grid.SetColumn(text, 2);
        row.Children.Add(text);

        var mode = new ComboBox { MinWidth = 120, Margin = new Thickness(8, 0, 8, 0), VerticalAlignment = VerticalAlignment.Center };
        foreach (var m in SystemModes.InOrder) mode.Items.Add(new ModeChoice(m));
        mode.SelectedIndex = SystemModes.Rank(rules[0].Mode);
        mode.SelectionChanged += (_, _) =>
        {
            if (_rulesUiLoading || mode.SelectedItem is not ModeChoice { Mode: var chosen }) return;
            foreach (var r in rules)
            {
                r.Mode = chosen;
                // In force now: the new mode at once; the mode to go back to stays.
                if (ReferenceEquals(_activeRule, r) && !_ruleOverridden && _currentMode != chosen) ApplyRuleMode(chosen);
            }
            _settings.Save();
        };
        Grid.SetColumn(mode, 3);
        row.Children.Add(mode);

        var remove = new Button { Content = "âœ•", Width = 28, Height = 28, ToolTip = Strings.Get("S.Rules.Remove") };
        remove.Click += (_, _) =>
        {
            foreach (var r in rules)
            {
                if (ReferenceEquals(_activeRule, r)) EndRuleNow();
                _settings.ProgramRules.Remove(r);
                // A found game taken off the list stays off it: the daily scan does not bring it back.
                if (r.FromScan && !_settings.GamesExcluded.Contains(r.Process, StringComparer.OrdinalIgnoreCase))
                    _settings.GamesExcluded.Add(r.Process);
                Log.Info("rules", $"Rule removed: {r.Process}");
            }
            _settings.Save();
            StartProgramRules();
            RefreshProgramRuleRows();
        };
        Grid.SetColumn(remove, 4);
        row.Children.Add(remove);
        return row;
    }

    /// <summary>One of a game's programs, under it, drawn as a branch of a tree.</summary>
    private Grid ChildRow(ProgramRule rule, bool last)
    {
        var row = new Grid { Margin = new Thickness(0, 1, 0, 1) };
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(18) });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        var line = (System.Windows.Media.Brush)FindResource("Line");
        var branch = new System.Windows.Shapes.Path
        {
            // A vertical line, and a short one across to the name: â”” for the last, â”œ otherwise.
            Data = System.Windows.Media.Geometry.Parse(last ? "M5,0 L5,10 L14,10" : "M5,0 L5,22 M5,10 L14,10"),
            Stroke = line, StrokeThickness = 1.2, VerticalAlignment = VerticalAlignment.Top, SnapsToDevicePixels = true,
        };
        row.Children.Add(branch);

        var current = rule;
        var box = new CheckBox { IsChecked = rule.Enabled, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 8, 0) };
        box.Click += (_, _) =>
        {
            current.Enabled = box.IsChecked == true;
            if (!current.Enabled && ReferenceEquals(_activeRule, current)) EndRuleNow();
            _settings.Save();
            StartProgramRules();
            RefreshProgramRuleRows();
        };
        Grid.SetColumn(box, 1);
        row.Children.Add(box);

        var name = new TextBlock
        {
            Text = ReferenceEquals(_activeRule, rule) ? Strings.Get("S.Rules.RowActive", rule.Process) : rule.Process,
            Style = (Style)FindResource("StatusTextStyle"), VerticalAlignment = VerticalAlignment.Center,
        };
        Grid.SetColumn(name, 2);
        row.Children.Add(name);
        return row;
    }

    /// <summary>The small arrow that opens a game's programs: pointing right when shut, down when open; the accent under the pointer.</summary>
    private FrameworkElement Chevron(bool open, Func<bool> toggle)
    {
        var path = new System.Windows.Shapes.Path
        {
            Data = System.Windows.Media.Geometry.Parse("M0,0 L4.5,4.5 L0,9"),
            Stroke = (System.Windows.Media.Brush)FindResource("Muted"), StrokeThickness = 1.6,
            StrokeStartLineCap = System.Windows.Media.PenLineCap.Round, StrokeEndLineCap = System.Windows.Media.PenLineCap.Round,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center,
            RenderTransformOrigin = new Point(0.5, 0.5),
            RenderTransform = new System.Windows.Media.RotateTransform(open ? 90 : 0),
        };
        var hit = new Border { Width = 18, Height = 24, Background = System.Windows.Media.Brushes.Transparent, Cursor = System.Windows.Input.Cursors.Hand, Child = path };
        hit.MouseEnter += (_, _) => path.Stroke = (System.Windows.Media.Brush)FindResource("Accent");
        hit.MouseLeave += (_, _) => path.Stroke = (System.Windows.Media.Brush)FindResource("Muted");
        hit.MouseLeftButtonUp += (_, _) =>
        {
            var nowOpen = toggle();
            path.RenderTransform = new System.Windows.Media.RotateTransform(nowOpen ? 90 : 0);
        };
        return hit;
    }
}
