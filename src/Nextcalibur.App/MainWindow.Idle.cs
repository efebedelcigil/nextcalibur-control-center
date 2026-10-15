using System.Windows;
using System.Windows.Controls;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;
using ComboBox = System.Windows.Controls.ComboBox;
using ComboBoxItem = System.Windows.Controls.ComboBoxItem;
using ToggleButton = System.Windows.Controls.Primitives.ToggleButton;
using TextBlock = System.Windows.Controls.TextBlock;

namespace Nextcalibur.App;

/// <summary>
/// Settings > Sleep and screen: every mode's display and sleep timeouts,
/// plugged in and on battery, side by side, and the screen saver. Each row
/// is that mode's power plan - the same values the Control Panel's "Change
/// plan settings" shows - so a change here is a change there, and one made
/// there shows here (see <see cref="IdleTimeouts"/>).
/// </summary>
public partial class MainWindow
{
    /// <summary>The plans the page shows: each mode's, or the active plan where there are no modes' plans.</summary>
    private List<(SystemMode? Mode, Guid Plan)> _idlePlans = [];

    /// <summary>Each plan's timeouts as last shown; a difference means the page is out of date.</summary>
    private readonly Dictionary<Guid, Timeouts> _idleKnown = [];

    private int _screenSaverKnown = -1;
    private ToggleButton? _usbSaving, _wifiSaving;
    private TextBlock? _usbSavingState, _wifiSavingState;
    private readonly Dictionary<(Guid, DeviceSaving), PlanTuning.AcDc> _savingKnown = [];
    private StackPanel? _idleRows;
    private ComboBox? _idleScreenSaver;
    private bool _idleUiLoading;

    private static readonly (IdleSetting Setting, bool PluggedIn)[] IdleColumns =
    [
        (IdleSetting.DisplayOff, true), (IdleSetting.DisplayOff, false),
        (IdleSetting.Sleep, true), (IdleSetting.Sleep, false),
    ];

    private void WireIdlePage()
    {
        _idleRows = FindName("IdleRows") as StackPanel;
        _idleScreenSaver = FindName("IdleScreenSaver") as ComboBox;
        if (_idleRows is null || _idleScreenSaver is null) return;

        _idleScreenSaver.SelectionChanged += (_, _) => ScreenSaverChosen();

        _usbSaving = FindName("SettingUsbSaving") as ToggleButton;
        _wifiSaving = FindName("SettingWifiSaving") as ToggleButton;
        _usbSavingState = FindName("SettingUsbSavingState") as TextBlock;
        _wifiSavingState = FindName("SettingWifiSavingState") as TextBlock;
        if (_usbSaving is not null) _usbSaving.Click += (_, _) => SavingChosen(DeviceSaving.Usb, _usbSaving.IsChecked == true);
        if (_wifiSaving is not null) _wifiSaving.Click += (_, _) => SavingChosen(DeviceSaving.Wifi, _wifiSaving.IsChecked == true);
        if (FindName("IdleRecommended") is System.Windows.Controls.Button recommended)
            recommended.Click += (_, _) => ApplyRecommendedIdle();
        RememberIdleTimeouts();
    }

    /// <summary>
    /// The "Recommended" button: every mode's plan gets the timeouts in
    /// <see cref="IdleTimeouts.Recommended"/>, after a yes. The person's own
    /// choice - Nextcalibur never does this by itself.
    /// </summary>
    private void ApplyRecommendedIdle()
    {
        if (!Dialogs.Ask(Strings.Get("S.Idle.RecommendedTitle"), Strings.Get("S.Idle.RecommendedBody"), defaultNo: true)) return;
        ResolveIdlePlans(force: true);
        var written = 0;
        foreach (var (mode, plan) in _idlePlans)
        {
            if (mode is not { } m) continue;
            var t = IdleTimeouts.Recommended(m);
            if (IdleTimeouts.Write(plan, IdleSetting.DisplayOff, t.Display)) written++;
            if (IdleTimeouts.Write(plan, IdleSetting.Sleep, t.Sleep)) written++;
            Log.Info("idle", "Recommended timeouts written", ("mode", m), ("plan", plan.ToString("D")),
                ("displayAc", t.Display.Ac), ("displayDc", t.Display.Dc), ("sleepAc", t.Sleep.Ac), ("sleepDc", t.Sleep.Dc));
        }
        Audit("idle-timeouts", "recommended", ("settings", written));
        RememberIdleTimeouts();
        foreach (var (_, plan) in _idlePlans)
            if (_idleKnown.TryGetValue(plan, out var now)) KeepIdle(plan, now);
    }

    /// <summary>Reads every plan's timeouts afresh and redraws the table.</summary>
    private void RememberIdleTimeouts()
    {
        if (_idleRows is null) return;
        ResolveIdlePlans(force: true);
        _idleKnown.Clear();
        foreach (var (_, plan) in _idlePlans)
            if (IdleTimeouts.Read(plan) is { } t) _idleKnown[plan] = t;
        ReadSavings();
        RefreshIdleRows();
    }

    private DateTime _idlePlansResolvedAt = DateTime.MinValue;

    /// <summary>Which plans are the modes': looked up once a minute, a scan of every plan each time.</summary>
    private void ResolveIdlePlans(bool force = false)
    {
        if (!force && DateTime.UtcNow - _idlePlansResolvedAt < TimeSpan.FromMinutes(1)) return;
        _idlePlansResolvedAt = DateTime.UtcNow;

        var plans = new List<(SystemMode?, Guid)>();
        foreach (var mode in SystemModes.InOrder)
            if (SystemModeService.PlanFor(mode) is { } p && plans.All(x => x.Item2 != p.Plan)) plans.Add((mode, p.Plan));

        // On a machine without the modes' plans, Windows' active plan is all there is.
        if (plans.Count == 0 && PlanTuning.ActivePlan() is var active && active != Guid.Empty) plans.Add((null, active));
        _idlePlans = plans;
    }

    /// <summary>From the slow timer: a change made in Windows - the Control Panel, Settings, powercfg - shows here.</summary>
    private void IdleTimeoutsTick()
    {
        // During a game the table waits: nobody is looking at it.
        if (_idleRows is null || NoticeGate.ShouldWait) return;
        var before = _idlePlans;
        ResolveIdlePlans();

        var changed = !ReferenceEquals(before, _idlePlans) && !before.SequenceEqual(_idlePlans);
        foreach (var (_, plan) in _idlePlans)
        {
            if (IdleTimeouts.Read(plan) is not { } now) continue;
            if (!_idleKnown.TryGetValue(plan, out var known) || known != now)
            {
                if (_idleKnown.ContainsKey(plan))
                {
                    // Changed in Windows while Nextcalibur runs: the person's, and kept as theirs.
                    Log.Info("idle", $"Timeouts of {plan:D} changed in Windows");
                    KeepIdle(plan, now);
                }
                _idleKnown[plan] = now;
                changed = true;
            }
        }

        if (ScreenSaver.Current() != _screenSaverKnown) changed = true;
        if (ReadSavings()) changed = true;
        if (changed) RefreshIdleRows();
    }

    private void RefreshIdleRows()
    {
        if (_idleRows is null || _idleScreenSaver is null) return;

        // A list open under the pointer is not pulled away while it is used.
        if (_idleRows.Children.OfType<Grid>().SelectMany(g => g.Children.OfType<ComboBox>()).Any(c => c.IsDropDownOpen)) return;

        _idleUiLoading = true;
        try
        {
            _idleRows.Children.Clear();
            _idleRows.Children.Add(IdleRow(null, Strings.Get("S.Idle.Mode"), null));
            foreach (var (mode, plan) in _idlePlans)
            {
                var label = mode is { } m ? ModeName(m) : Strings.Get("S.Idle.ActivePlan");
                _idleRows.Children.Add(IdleRow(plan, label, _idleKnown.TryGetValue(plan, out var t) ? t : null));
            }

            _screenSaverKnown = ScreenSaver.Current();
            FillIdleChoices(_idleScreenSaver, _screenSaverKnown, "S.Idle.Off");
            ShowSaving(DeviceSaving.Usb, _usbSaving, _usbSavingState);
            ShowSaving(DeviceSaving.Wifi, _wifiSaving, _wifiSavingState);
        }
        finally
        {
            _idleUiLoading = false;
        }
    }

    /// <summary>One row of the table: a mode and its four timeouts, or - with no plan - the column headings.</summary>
    private Grid IdleRow(Guid? plan, string label, Timeouts? timeouts)
    {
        var row = new Grid { Margin = new Thickness(0, 3, 0, 3) };
        row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 90 });
        foreach (var _ in IdleColumns) row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(112) });

        var name = new TextBlock { Text = label, VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap };
        if (plan is null) name.Style = (Style)FindResource("StatusTextStyle");
        else
        {
            name.FontSize = 12;
            name.FontWeight = FontWeights.SemiBold;
            name.Foreground = (System.Windows.Media.Brush)FindResource("Ink");
        }
        row.Children.Add(name);

        for (var i = 0; i < IdleColumns.Length; i++)
        {
            var (setting, pluggedIn) = IdleColumns[i];
            FrameworkElement cell;
            if (plan is not { } p)
            {
                var key = (setting == IdleSetting.DisplayOff ? "S.Idle.Display" : "S.Idle.Sleep") + (pluggedIn ? "Ac" : "Dc");
                cell = new TextBlock { Text = Strings.Get(key), Style = (Style)FindResource("StatusTextStyle"), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Bottom };
            }
            else
            {
                var box = new ComboBox { Height = 28, IsEnabled = timeouts is not null };
                if (timeouts is { } t) FillIdleChoices(box, pluggedIn ? t[setting].Ac : t[setting].Dc, "S.Idle.Never");
                box.SelectionChanged += (_, _) => IdleChosen(p, setting, pluggedIn, box);
                cell = box;
            }
            cell.Margin = new Thickness(8, 0, 0, 0);
            Grid.SetColumn(cell, i + 1);
            row.Children.Add(cell);
        }
        return row;
    }

    private static void FillIdleChoices(ComboBox box, int current, string zeroKey)
    {
        box.Items.Clear();
        foreach (var seconds in IdleTimeouts.Choices(current))
        {
            var item = new ComboBoxItem { Content = IdleLabel(seconds, zeroKey), Tag = seconds };
            box.Items.Add(item);
            if (seconds == current) box.SelectedItem = item;
        }
    }

    private static string IdleLabel(int seconds, string zeroKey)
    {
        if (seconds <= 0) return Strings.Get(zeroKey);
        if (seconds % 3600 == 0) return Strings.Get("S.Idle.Hours", seconds / 3600);
        if (seconds % 60 == 0) return Strings.Get("S.Idle.Minutes", seconds / 60);
        return Strings.Get("S.Idle.Seconds", seconds);
    }

    private void IdleChosen(Guid plan, IdleSetting setting, bool pluggedIn, ComboBox box)
    {
        if (_idleUiLoading || box.SelectedItem is not ComboBoxItem { Tag: int seconds }) return;
        if (!_idleKnown.TryGetValue(plan, out var known)) return;

        var value = IdleTimeouts.Carry(known[setting], pluggedIn, seconds);
        if (value == known[setting]) return;
        if (IdleTimeouts.Write(plan, setting, value))
        {
            _idleKnown[plan] = known.With(setting, value);
            KeepIdle(plan, _idleKnown[plan]);
            var mode = _idlePlans.FirstOrDefault(x => x.Plan == plan).Mode;
            Log.Info("idle", $"{setting} {(pluggedIn ? "plugged in" : "on battery")} set to {seconds} s for {mode?.ToString() ?? "the active plan"}");
        }
        else
        {
            Log.Warn("idle", $"Could not set {setting} for {plan:D}");
            Dispatcher.BeginInvoke(RememberIdleTimeouts);
        }
    }

    /// <summary>Reads the USB and wireless savings of every mode's plan; true when anything changed since the last read.</summary>
    private bool ReadSavings()
    {
        var changed = false;
        foreach (var (_, plan) in _idlePlans)
        foreach (var kind in new[] { DeviceSaving.Usb, DeviceSaving.Wifi })
        {
            if (DevicePowerSaving.Read(plan, kind) is not { } now) continue;
            if (!_savingKnown.TryGetValue((plan, kind), out var known) || known != now)
            {
                if (_savingKnown.ContainsKey((plan, kind))) Log.Info("idle", $"{kind} power saving of {plan:D} changed in Windows");
                _savingKnown[(plan, kind)] = now;
                changed = true;
            }
        }
        return changed;
    }

    /// <summary>A switch as the plans have it: on, off, or - when the modes differ - which mode has what.</summary>
    private void ShowSaving(DeviceSaving kind, ToggleButton? toggle, TextBlock? state)
    {
        if (toggle is null || state is null) return;
        var values = _idlePlans.Where(p => _savingKnown.ContainsKey((p.Plan, kind)))
            .Select(p => (p.Mode, Value: _savingKnown[(p.Plan, kind)])).ToList();
        var on = DevicePowerSaving.State(values.Select(v => v.Value));
        toggle.IsChecked = on == true;
        toggle.IsEnabled = values.Count > 0;
        state.Text = on is null && values.Count > 0
            ? Strings.Get("S.Settings.SavingMixed", string.Join(", ", values.Select(v =>
                $"{(v.Mode is { } m ? ModeName(m) : Strings.Get("S.Idle.ActivePlan"))}: {Strings.Get(DevicePowerSaving.Saving(v.Value) ? "S.Settings.SavingOn" : "S.Settings.SavingOff")}")))
            : string.Empty;
    }

    /// <summary>The person's switch: the same value into every mode's plan, plugged in and on battery.</summary>
    private void SavingChosen(DeviceSaving kind, bool on)
    {
        var value = on
            ? DevicePowerSaving.On(kind, DevicePowerSaving.Read(ModeProfile.BalancedPlan, kind))
            : DevicePowerSaving.Off;
        var written = 0;
        foreach (var (_, plan) in _idlePlans)
        {
            if (DevicePowerSaving.Write(plan, kind, value))
            {
                _savingKnown[(plan, kind)] = value;
                written++;
            }
        }
        Log.Info("idle", $"{kind} power saving {(on ? "on" : "off")} ({value.Ac}/{value.Dc}) in {written} plan(s)");
        Audit($"{kind.ToString().ToLowerInvariant()}-power-saving", "done", ("on", on), ("ac", value.Ac), ("dc", value.Dc), ("plans", written));
        RefreshIdleRows();
    }

    private void ScreenSaverChosen()
    {
        if (_idleUiLoading || _idleScreenSaver?.SelectedItem is not ComboBoxItem { Tag: int seconds }) return;
        if (ScreenSaver.Set(seconds)) Log.Info("idle", $"Screen saver: {(seconds == 0 ? "off" : $"{seconds} s")}");
        else Log.Warn("idle", "Could not set the screen saver");
        _screenSaverKnown = ScreenSaver.Current();
    }
}
