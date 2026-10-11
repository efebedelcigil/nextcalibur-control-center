using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Input;
using Nextcalibur.Core.Power;
using Button = System.Windows.Controls.Button;
using CheckBox = System.Windows.Controls.CheckBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Nextcalibur.App;

/// <summary>
/// The Fn keys for an external keyboard: shortcuts registered with Windows
/// for what the laptop's own keyboard does with Fn. Every check is in
/// <see cref="FnShortcuts"/>; this registers what passes, says why for what
/// does not, and does the work when one is pressed.
///
/// Registration is the conflict check that sees other programs: Windows
/// refuses a combination somebody else holds. Ours held, nobody else can
/// take it; one refused is tried again every minute, so a shortcut that was
/// taken becomes ours when the program holding it goes.
/// </summary>
public partial class MainWindow
{
    private const int WmHotkey = 0x0312;
    private const int WmInputLangChange = 0x0051;
    private const uint ModNoRepeat = 0x4000;
    private const int ErrorHotkeyAlreadyRegistered = 1409;
    private const int HotkeyIdBase = 0x4E43; // "NC"

    private HwndSource? _hotkeySource;

    /// <summary>What each shortcut's last check found; null for none - registered and working.</summary>
    private readonly Dictionary<FnAction, (ShortcutProblem? Problem, string? Detail)> _shortcutState = new();
    private readonly HashSet<FnAction> _registered = new();
    private DateTime _shortcutsRetriedAt = DateTime.MinValue;

    /// <summary>The shortcut being recorded on the Settings page, if any.</summary>
    private FnShortcut? _recording;

    private void StartFnShortcuts()
    {
        var handle = new WindowInteropHelper(this).EnsureHandle();
        _hotkeySource = HwndSource.FromHwnd(handle);
        _hotkeySource?.AddHook(HotkeyHook);
        ApplyFnShortcuts();
    }

    private void StopFnShortcuts()
    {
        UnregisterAllShortcuts();
        _hotkeySource?.RemoveHook(HotkeyHook);
    }

    private IntPtr HotkeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WmHotkey)
        {
            var id = wParam.ToInt32() - HotkeyIdBase;
            if (Enum.IsDefined(typeof(FnAction), id) && _recording is null)
            {
                handled = true;
                RunFnAction((FnAction)id);
            }
        }
        else if (msg == WmInputLangChange)
        {
            // Another layout, other AltGr characters: check again.
            Dispatcher.BeginInvoke(ApplyFnShortcuts);
        }
        return IntPtr.Zero;
    }

    /// <summary>Registers every enabled shortcut that passes the checks, and records why for those that do not.</summary>
    private void ApplyFnShortcuts()
    {
        var before = new Dictionary<FnAction, (ShortcutProblem? Problem, string? Detail)>(_shortcutState);
        UnregisterAllShortcuts();
        _shortcutState.Clear();
        var handle = _hotkeySource?.Handle ?? IntPtr.Zero;

        foreach (var s in _settings.FnShortcuts)
        {
            if (!_settings.FnShortcutsEnabled || !s.Enabled) continue;
            if (!ActionAvailable(s.Action)) continue;

            if (FnShortcuts.CheckShape(s, _settings.FnShortcuts) is { } shape) { _shortcutState[s.Action] = (shape, null); continue; }
            if (FnShortcuts.CharacterOnSomeLayout(s.Modifiers, s.Key) is { } typed) { _shortcutState[s.Action] = (ShortcutProblem.TypesACharacter, typed); continue; }
            if (handle == IntPtr.Zero) continue;

            if (RegisterHotKey(handle, HotkeyIdBase + (int)s.Action, (uint)s.Modifiers | ModNoRepeat, s.Key))
            {
                _registered.Add(s.Action);
                _shortcutState[s.Action] = (null, null);
            }
            else
            {
                var error = Marshal.GetLastWin32Error();
                _shortcutState[s.Action] = (ShortcutProblem.TakenByAnotherProgram,
                    error == ErrorHotkeyAlreadyRegistered ? null : $"error {error}");
            }
        }

        WarnAboutNewConflicts(before);

        var taken = _shortcutState.Count(p => p.Value.Problem is not null);
        if (_settings.FnShortcutsEnabled)
            Log.Info("keys", $"Shortcuts: {_registered.Count} registered, {taken} not ({string.Join(", ", _shortcutState.Where(p => p.Value.Problem is not null).Select(p => $"{p.Key}: {p.Value.Problem}"))})");
        RefreshShortcutRows();
    }

    /// <summary>
    /// A shortcut that has just stopped working says so - another program
    /// took it, a new keyboard layout types with it, or it was set to keys
    /// that cannot be used. Once per change: a conflict that stays is in its
    /// row on the Settings page, not in a notification every minute.
    /// </summary>
    private void WarnAboutNewConflicts(Dictionary<FnAction, (ShortcutProblem? Problem, string? Detail)> before)
    {
        if (!_settings.FnShortcutsEnabled) return;
        foreach (var (action, now) in _shortcutState)
        {
            if (now.Problem is not { } problem) continue;
            if (before.TryGetValue(action, out var was) && was.Problem == problem) continue;
            if (_settings.FnShortcuts.FirstOrDefault(s => s.Action == action) is not { } s) continue;

            var reason = problem == ShortcutProblem.TypesACharacter
                ? Strings.Get("S.Fn.Problem.TypesACharacter", now.Detail ?? "?")
                : Strings.Get($"S.Fn.Problem.{problem}");
            var body = Strings.Get("S.Fn.ConflictBody", FnShortcuts.Describe(s.Modifiers, s.Key), Strings.Get($"S.Fn.{action}"), reason);
            Log.Warn("keys", body);
            _tray?.ShowMessage(Strings.Get("S.Fn.ConflictTitle"), body);
        }
    }

    /// <summary>Once a minute from the slow timer: a shortcut another program held may be free now.</summary>
    private void RetryTakenShortcuts()
    {
        if (!_settings.FnShortcutsEnabled || _recording is not null) return;
        if (DateTime.UtcNow - _shortcutsRetriedAt < TimeSpan.FromMinutes(1)) return;
        _shortcutsRetriedAt = DateTime.UtcNow;
        if (_shortcutState.Values.Any(v => v.Problem == ShortcutProblem.TakenByAnotherProgram)) ApplyFnShortcuts();
    }

    private void UnregisterAllShortcuts()
    {
        var handle = _hotkeySource?.Handle ?? IntPtr.Zero;
        foreach (var action in _registered) UnregisterHotKey(handle, HotkeyIdBase + (int)action);
        _registered.Clear();
    }

    /// <summary>Whether this machine can do the action at all - the turbo only once its direction is measured.</summary>
    private bool ActionAvailable(FnAction action) => action switch
    {
        FnAction.FanTurbo => false,   // no software path to it; see FanTurbo
        FnAction.KeyboardBacklight => _led is not null && _support.AllowsWrites,
        FnAction.SystemMode => _support.AllowsWrites,
        _ => false,
    };

    private void RunFnAction(FnAction action)
    {
        Log.Info("keys", $"Shortcut: {action}");
        try
        {
            switch (action)
            {
                case FnAction.KeyboardBacklight when _led is { } led:
                    // Fn+Space's own order: full, off, half.
                    led.HardwareLevel = led.HardwareLevel switch
                    {
                        LedBrightness.Full => LedBrightness.Off,
                        LedBrightness.Off => LedBrightness.Half,
                        _ => LedBrightness.Full,
                    };
                    var mb = _mailbox;
                    _ = Task.Run(() => { try { using (mb?.Hold()) led.Apply(); } catch (Exception ex) when (ex is not OutOfMemoryException) { Log.Warn("keys", ex.Message); } });
                    BrightnessSlider.Value = led.EffectiveBrightnessPercent;
                    BrightnessValue.Text = $"{led.EffectiveBrightnessPercent}%";
                    break;

                case FnAction.SystemMode:
                    var next = _currentMode switch
                    {
                        SystemMode.Office => SystemMode.Gaming,
                        SystemMode.Gaming => SystemMode.Performance,
                        _ => SystemMode.Office,
                    };
                    if (next == SystemMode.Performance && !PowerOverlayService.PerformanceModeIsSafe(new PowerOverlayService().Diagnose()))
                        next = SystemMode.Office;
                    ModeButtonFor(next).IsChecked = true;
                    _tray?.ShowMessage(Strings.Get("S.Fn.ModeTitle"), Strings.Get("S.Status.Mode", next, Strings.Get(_onBattery ? "S.Status.OnBattery" : "S.Status.OnAc")));
                    break;
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Warn("keys", $"{action} failed: {ex.Message}");
        }
    }

    // --------------------------------------------------------- settings page

    private CheckBox? _fnSelectAll;
    private ToggleButton? _fnMaster;
    private StackPanel? _fnRows;
    private bool _fnUiLoading;

    private void WireFnShortcutsPage()
    {
        _fnMaster = FindName("SettingFnShortcuts") as ToggleButton;
        _fnSelectAll = FindName("FnSelectAll") as CheckBox;
        _fnRows = FindName("FnShortcutRows") as StackPanel;
        if (_fnMaster is null || _fnRows is null) return;

        _fnMaster.Checked += (_, _) => SetFnMaster(true);
        _fnMaster.Unchecked += (_, _) => SetFnMaster(false);
        if (_fnSelectAll is not null)
        {
            _fnSelectAll.Checked += (_, _) => SelectAllShortcuts(true);
            _fnSelectAll.Unchecked += (_, _) => SelectAllShortcuts(false);
        }
        PreviewKeyDown += RecordShortcutKey;
        RefreshShortcutRows();
    }

    private void SetFnMaster(bool on)
    {
        if (_fnUiLoading) return;
        _settings.FnShortcutsEnabled = on;
        _settings.Save();
        ApplyFnShortcuts();
    }

    private void SelectAllShortcuts(bool on)
    {
        if (_fnUiLoading) return;
        foreach (var s in _settings.FnShortcuts) s.Enabled = on;
        _settings.Save();
        ApplyFnShortcuts();
    }

    /// <summary>Rebuilds the rows: a check box, the action, the keys as a button that records new ones, and what was found.</summary>
    private void RefreshShortcutRows()
    {
        if (_fnRows is null || _fnMaster is null) return;
        _fnUiLoading = true;
        try
        {
            // The vendor's keys stand in for the vendor's machine only.
            var casper = _support.AllowsWrites;
            _fnMaster.IsChecked = _settings.FnShortcutsEnabled;
            _fnMaster.IsEnabled = casper;
            _fnRows.IsEnabled = casper && _settings.FnShortcutsEnabled;
            if (_fnSelectAll is not null)
            {
                _fnSelectAll.IsEnabled = casper && _settings.FnShortcutsEnabled;
                var on = _settings.FnShortcuts.Count(s => s.Enabled);
                _fnSelectAll.IsChecked = on == _settings.FnShortcuts.Count ? true : on == 0 ? false : null;
            }

            _fnRows.Children.Clear();
            foreach (var s in _settings.FnShortcuts)
            {
                var row = new Grid { Margin = new Thickness(0, 4, 0, 4) };
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var available = ActionAvailable(s.Action);
                var box = new CheckBox { IsChecked = s.Enabled && available, IsEnabled = available, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 10, 0) };
                var shortcut = s;
                box.Checked += (_, _) => { if (_fnUiLoading) return; shortcut.Enabled = true; _settings.Save(); ApplyFnShortcuts(); };
                box.Unchecked += (_, _) => { if (_fnUiLoading) return; shortcut.Enabled = false; _settings.Save(); ApplyFnShortcuts(); };
                row.Children.Add(box);

                var text = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
                text.Children.Add(new TextBlock { Text = Strings.Get($"S.Fn.{s.Action}"), FontSize = 12, FontWeight = FontWeights.SemiBold, Foreground = (System.Windows.Media.Brush)FindResource("Ink") });
                text.Children.Add(new TextBlock { Text = StatusFor(s, available), TextWrapping = TextWrapping.Wrap, Style = (Style)FindResource("StatusTextStyle"), Margin = new Thickness(0, 2, 0, 0) });
                Grid.SetColumn(text, 1);
                row.Children.Add(text);

                var keys = new Button
                {
                    Content = _recording == s ? Strings.Get("S.Fn.PressKeys") : FnShortcuts.Describe(s.Modifiers, s.Key),
                    MinWidth = 130, Height = 28, VerticalAlignment = VerticalAlignment.Center, IsEnabled = available,
                };
                keys.Click += (_, _) => StartRecording(shortcut);
                Grid.SetColumn(keys, 2);
                row.Children.Add(keys);

                _fnRows.Children.Add(row);
            }
        }
        finally
        {
            _fnUiLoading = false;
        }
    }

    private string StatusFor(FnShortcut s, bool available)
    {
        if (!available) return Strings.Get(s.Action == FnAction.FanTurbo ? "S.Fn.TurboUnknown" : "S.Fn.Unavailable");
        if (!_settings.FnShortcutsEnabled || !s.Enabled) return Strings.Get($"S.Fn.{s.Action}Desc");
        if (!_shortcutState.TryGetValue(s.Action, out var state)) return Strings.Get($"S.Fn.{s.Action}Desc");
        return state.Problem switch
        {
            null => Strings.Get("S.Fn.Working"),
            ShortcutProblem.TypesACharacter => Strings.Get("S.Fn.Problem.TypesACharacter", state.Detail ?? "?"),
            { } p => Strings.Get($"S.Fn.Problem.{p}"),
        };
    }

    private void StartRecording(FnShortcut s)
    {
        // Our own shortcuts let go while recording, or pressing the current
        // one would do its job instead of being recorded.
        UnregisterAllShortcuts();
        _recording = s;
        RefreshShortcutRows();
    }

    private void RecordShortcutKey(object sender, KeyEventArgs e)
    {
        if (_recording is not { } s) return;
        e.Handled = true;

        var key = e.Key == Key.System ? e.SystemKey : e.Key;
        if (key == Key.Escape) { _recording = null; ApplyFnShortcuts(); return; }
        if (key is Key.LeftCtrl or Key.RightCtrl or Key.LeftAlt or Key.RightAlt or Key.LeftShift or Key.RightShift or Key.LWin or Key.RWin)
            return;

        var m = HotkeyModifiers.None;
        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control)) m |= HotkeyModifiers.Control;
        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt)) m |= HotkeyModifiers.Alt;
        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)) m |= HotkeyModifiers.Shift;
        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Windows)) m |= HotkeyModifiers.Windows;

        s.Modifiers = m;
        s.Key = (uint)KeyInterop.VirtualKeyFromKey(key);
        s.Enabled = true;
        _recording = null;
        _settings.Save();
        Log.Info("keys", $"{s.Action} set to {FnShortcuts.Describe(s.Modifiers, s.Key)}");
        ApplyFnShortcuts();
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool RegisterHotKey(IntPtr hwnd, int id, uint modifiers, uint key);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnregisterHotKey(IntPtr hwnd, int id);
}
