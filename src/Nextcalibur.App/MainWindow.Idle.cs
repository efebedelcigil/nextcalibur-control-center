using System.Windows;
using System.Windows.Controls;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;
using ComboBox = System.Windows.Controls.ComboBox;
using ComboBoxItem = System.Windows.Controls.ComboBoxItem;

namespace Nextcalibur.App;

/// <summary>
/// Settings > Sleep and screen: each mode's display and sleep timeouts,
/// plugged in and on battery, and the screen saver; a value changed in
/// Windows shows here and is carried to the other modes (see
/// <see cref="IdleTimeouts"/>).
/// </summary>
public partial class MainWindow
{
    /// <summary>The plans the page offers: each mode's, or the active plan where there are no modes' plans.</summary>
    private List<(SystemMode? Mode, Guid Plan)> _idlePlans = [];

    /// <summary>Each plan's timeouts as last seen or written here; a difference is a change from outside.</summary>
    private readonly Dictionary<Guid, Timeouts> _idleKnown = [];

    private int _screenSaverKnown = -1;
    private ComboBox? _idleMode, _idleDisplayAc, _idleDisplayDc, _idleSleepAc, _idleSleepDc, _idleScreenSaver;
    private bool _idleUiLoading;

    private void WireIdlePage()
    {
        _idleMode = FindName("IdleMode") as ComboBox;
        _idleDisplayAc = FindName("IdleDisplayAc") as ComboBox;
        _idleDisplayDc = FindName("IdleDisplayDc") as ComboBox;
        _idleSleepAc = FindName("IdleSleepAc") as ComboBox;
        _idleSleepDc = FindName("IdleSleepDc") as ComboBox;
        _idleScreenSaver = FindName("IdleScreenSaver") as ComboBox;
        if (_idleMode is null || _idleDisplayAc is null || _idleDisplayDc is null
            || _idleSleepAc is null || _idleSleepDc is null || _idleScreenSaver is null) return;

        _idleMode.SelectionChanged += (_, _) => { if (!_idleUiLoading) RefreshIdleRows(); };
        _idleDisplayAc.SelectionChanged += (_, _) => IdleChosen(IdleSetting.DisplayOff, pluggedIn: true, _idleDisplayAc);
        _idleDisplayDc.SelectionChanged += (_, _) => IdleChosen(IdleSetting.DisplayOff, pluggedIn: false, _idleDisplayDc);
        _idleSleepAc.SelectionChanged += (_, _) => IdleChosen(IdleSetting.Sleep, pluggedIn: true, _idleSleepAc);
        _idleSleepDc.SelectionChanged += (_, _) => IdleChosen(IdleSetting.Sleep, pluggedIn: false, _idleSleepDc);
        _idleScreenSaver.SelectionChanged += (_, _) => ScreenSaverChosen();

        RememberIdleTimeouts();
    }

    /// <summary>
    /// Takes the plans' timeouts as they are now as the ones known. After
    /// anything of ours writes them - the start's display repair - or the
    /// next tick takes our own write for a change made in Windows and
    /// carries it to every mode.
    /// </summary>
    private void RememberIdleTimeouts()
    {
        if (_idleMode is null) return;
        ResolveIdlePlans(force: true);
        _idleKnown.Clear();
        foreach (var (_, plan) in _idlePlans)
            if (IdleTimeouts.Read(plan) is { } t) _idleKnown[plan] = t;
        RefreshIdleRows();
    }

    private DateTime _idlePlansResolvedAt = DateTime.MinValue;

    /// <summary>Which plans are the modes': looked up once a minute, a scan of every plan each time.</summary>
    private void ResolveIdlePlans(bool force = false)
    {
        if (!force && DateTime.UtcNow - _idlePlansResolvedAt < TimeSpan.FromMinutes(1)) return;
        _idlePlansResolvedAt = DateTime.UtcNow;

        var plans = new List<(SystemMode?, Guid)>();
        foreach (var mode in new[] { SystemMode.Office, SystemMode.Gaming, SystemMode.Performance })
            if (SystemModeService.PlanFor(mode) is { } p && plans.All(x => x.Item2 != p.Plan)) plans.Add((mode, p.Plan));

        // On a machine without the modes' plans, Windows' active plan is all there is.
        if (plans.Count == 0 && PlanTuning.ActivePlan() is var active && active != Guid.Empty) plans.Add((null, active));
        _idlePlans = plans;
    }

    /// <summary>From the slow timer: a change made in Windows shows here and goes to the other modes.</summary>
    private void IdleTimeoutsTick()
    {
        if (_idleMode is null) return;
        ResolveIdlePlans();

        var now = new Dictionary<Guid, Timeouts>();
        foreach (var (_, plan) in _idlePlans)
            if (IdleTimeouts.Read(plan) is { } t) now[plan] = t;

        var refresh = false;
        if (IdleTimeouts.ChangedOutside(_idleKnown, now, PlanTuning.ActivePlan()) is { } changed)
        {
            var differences = IdleTimeouts.Differences(_idleKnown[changed], now[changed]);
            foreach (var (setting, pluggedIn, seconds) in differences)
            {
                Log.Info("idle", $"{setting} {(pluggedIn ? "plugged in" : "on battery")} changed in Windows to {seconds} s; given to the other modes");
                foreach (var (_, other) in _idlePlans)
                {
                    if (other == changed || !now.TryGetValue(other, out var theirs)) continue;
                    var value = IdleTimeouts.Carry(theirs[setting], pluggedIn, seconds);
                    if (value != theirs[setting] && IdleTimeouts.Write(other, setting, value)) now[other] = theirs.With(setting, value);
                }
            }
            refresh = true;
        }

        // Plans seen for the first time are only remembered.
        foreach (var (plan, t) in now) _idleKnown[plan] = t;

        if (ScreenSaver.Current() != _screenSaverKnown) refresh = true;
        if (refresh) RefreshIdleRows();
    }

    private void RefreshIdleRows()
    {
        if (_idleMode is null || _idleDisplayAc is null || _idleDisplayDc is null
            || _idleSleepAc is null || _idleSleepDc is null || _idleScreenSaver is null) return;
        _idleUiLoading = true;
        try
        {
            // The mode list, keeping the choice; the mode in use when there is none yet.
            var selected = (_idleMode.SelectedItem as ComboBoxItem)?.Tag as Guid?;
            _idleMode.Items.Clear();
            foreach (var (mode, plan) in _idlePlans)
            {
                var item = new ComboBoxItem
                {
                    Content = mode is { } m ? m.ToString() : Strings.Get("S.Idle.ActivePlan"),
                    Tag = plan,
                };
                _idleMode.Items.Add(item);
                if (plan == selected || (selected is null && mode == _currentMode)) _idleMode.SelectedItem = item;
            }
            if (_idleMode.SelectedItem is null && _idleMode.Items.Count > 0) _idleMode.SelectedIndex = 0;

            var shown = (_idleMode.SelectedItem as ComboBoxItem)?.Tag as Guid?;
            var timeouts = shown is { } p && _idleKnown.TryGetValue(p, out var t) ? t : (Timeouts?)null;
            foreach (var box in new[] { _idleDisplayAc, _idleDisplayDc, _idleSleepAc, _idleSleepDc }) box.IsEnabled = timeouts is not null;
            if (timeouts is { } v)
            {
                FillIdleChoices(_idleDisplayAc, v.Display.Ac, "S.Idle.Never");
                FillIdleChoices(_idleDisplayDc, v.Display.Dc, "S.Idle.Never");
                FillIdleChoices(_idleSleepAc, v.Sleep.Ac, "S.Idle.Never");
                FillIdleChoices(_idleSleepDc, v.Sleep.Dc, "S.Idle.Never");
            }

            _screenSaverKnown = ScreenSaver.Current();
            FillIdleChoices(_idleScreenSaver, _screenSaverKnown, "S.Idle.Off");
        }
        finally
        {
            _idleUiLoading = false;
        }
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

    private void IdleChosen(IdleSetting setting, bool pluggedIn, ComboBox box)
    {
        if (_idleUiLoading || box.SelectedItem is not ComboBoxItem { Tag: int seconds }) return;
        if ((_idleMode?.SelectedItem as ComboBoxItem)?.Tag is not Guid plan || !_idleKnown.TryGetValue(plan, out var known)) return;

        var value = IdleTimeouts.Carry(known[setting], pluggedIn, seconds);
        if (value == known[setting]) return;
        if (IdleTimeouts.Write(plan, setting, value))
        {
            _idleKnown[plan] = known.With(setting, value);
            Log.Info("idle", $"{setting} {(pluggedIn ? "plugged in" : "on battery")} set to {seconds} s for {(_idleMode!.SelectedItem as ComboBoxItem)?.Content}");
        }
        else
        {
            Log.Warn("idle", $"Could not set {setting} for {plan:D}");
            RefreshIdleRows();
        }
    }

    private void ScreenSaverChosen()
    {
        if (_idleUiLoading || _idleScreenSaver?.SelectedItem is not ComboBoxItem { Tag: int seconds }) return;
        if (ScreenSaver.Set(seconds)) Log.Info("idle", $"Screen saver: {(seconds == 0 ? "off" : $"{seconds} s")}");
        else Log.Warn("idle", "Could not set the screen saver");
        RefreshIdleRows();
    }
}
