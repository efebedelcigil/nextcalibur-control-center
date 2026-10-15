using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Power;
using Button = System.Windows.Controls.Button;

namespace Nextcalibur.App;

/// <summary>
/// The Settings page: every preference that until now lived only in the
/// tray menu, in the window as well, the two kept in step through the one
/// <see cref="AppSettings"/> they share (BRIEF §21, roadmap 4e). The page's
/// controls are found by name so this compiles before the markup exists;
/// each setter writes the setting, applies whatever it governs, saves, and
/// tells the tray to re-tick. The tray's changes come back through
/// <see cref="TrayPresence.SettingsChanged"/>.
/// </summary>
public partial class MainWindow
{
    private Grid? _pageSettings;
    private RadioButton? _navSettings;
    private ToggleButton? _settingWindowsFaults;
    private ToggleButton? _settingFixTextInputHost, _settingFixCrossDevice, _settingFixWidgets,
        _settingWatchGpuAwake, _settingTrimDwmMemory, _settingTrimExplorerMemory, _settingDisableNdu;
    private StackPanel? _panelWindowsFaultsSubOptions;
    private ToggleButton? _settingStartWithWindows, _settingOverheatWarning,
        _settingAutoCooling, _settingAutoFanTurbo;
    private RadioButton? _settingStartInTray, _settingStartOnScreen;
    private readonly List<(RadioButton Button, int Ms)> _settingIntervals = new();
    private RadioButton? _settingLanguageEnglish, _settingLanguageTurkish;
    private Button? _settingCheckNowButton, _settingWinUtilButton, _settingDriversButton, _settingReportButton, _settingPrivacyButton;
    private bool _settingsPageWired;
    private bool _settingsPageLoading;

    /// <summary>Finds the page's parts and wires them; a no-op until the markup is there.</summary>
    private void WireSettingsPage()
    {
        if (_settingsPageWired) return;
        _pageSettings = FindName("PageSettings") as Grid;
        _navSettings = FindName("NavSettings") as RadioButton;
        if (_pageSettings is null || _navSettings is null) return;

        _settingStartWithWindows = FindName("SettingStartWithWindows") as ToggleButton;
        _settingStartInTray = FindName("SettingStartInTray") as RadioButton;
        _settingStartOnScreen = FindName("SettingStartOnScreen") as RadioButton;
        WireBatteryMode();
        _settingWindowsFaults = FindName("SettingWindowsFaults") as ToggleButton;
        _settingFixTextInputHost = FindName("SettingFixTextInputHost") as ToggleButton;
        _settingFixCrossDevice = FindName("SettingFixCrossDevice") as ToggleButton;
        _settingFixWidgets = FindName("SettingFixWidgets") as ToggleButton;
        _settingWatchGpuAwake = FindName("SettingWatchGpuAwake") as ToggleButton;
        _settingTrimDwmMemory = FindName("SettingTrimDwmMemory") as ToggleButton;
        _settingTrimExplorerMemory = FindName("SettingTrimExplorerMemory") as ToggleButton;
        _settingDisableNdu = FindName("SettingDisableNdu") as ToggleButton;
        _panelWindowsFaultsSubOptions = FindName("PanelWindowsFaultsSubOptions") as StackPanel;
        _settingOverheatWarning = FindName("SettingOverheatWarning") as ToggleButton;
        _settingAutoCooling = FindName("SettingAutoCooling") as ToggleButton;
        _settingAutoFanTurbo = FindName("SettingAutoFanTurbo") as ToggleButton;
        _settingCheckNowButton = FindName("SettingCheckNowButton") as Button;
        _settingWinUtilButton = FindName("SettingWinUtilButton") as Button;
        _settingDriversButton = FindName("SettingDriversButton") as Button;
        _settingReportButton = FindName("SettingReportButton") as Button;
        _settingPrivacyButton = FindName("SettingPrivacyButton") as Button;
        foreach (var ms in new[] { 1000, 2000, 5000, 10000 })
            if (FindName($"SettingInterval{ms / 1000}") is RadioButton button)
                _settingIntervals.Add((button, ms));
        _settingLanguageEnglish = FindName("SettingLanguageEnglish") as RadioButton;
        _settingLanguageTurkish = FindName("SettingLanguageTurkish") as RadioButton;

        Wire(_settingStartWithWindows, on =>
        {
            var exe = Environment.ProcessPath ?? throw new InvalidOperationException("Could not determine the executable path.");
            StartupRegistration.Set(on, exe);
            if (_settingStartInTray is not null) _settingStartInTray.IsEnabled = on;
            if (_settingStartOnScreen is not null) _settingStartOnScreen.IsEnabled = on;
            if (on && _settingStartInTray is not null && _settingStartOnScreen is not null
                   && _settingStartInTray.IsChecked != true && _settingStartOnScreen.IsChecked != true)
            {
                _settingStartInTray.IsChecked = true;
                _settings.StartMinimised = true;
            }
        });
        if (_settingStartInTray is not null)
            _settingStartInTray.Checked += (_, _) => ChangeSetting(() => _settings.StartMinimised = true);
        if (_settingStartOnScreen is not null)
            _settingStartOnScreen.Checked += (_, _) => ChangeSetting(() => _settings.StartMinimised = false);

        Wire(_settingWindowsFaults, on =>
        {
            _settings.CompensateWindowsFaults = on;
            if (_panelWindowsFaultsSubOptions is not null)
                _panelWindowsFaultsSubOptions.IsEnabled = on;
            // A change of mind starts the watching again from nothing, so a
            // switch flicked off and on does not act on a window that was
            // half measured while it was off.
            Nextcalibur.Core.Hardware.WindowsFaults.Forget();
        });
        Wire(_settingFixTextInputHost, on => _settings.FixTextInputHost = on);
        Wire(_settingFixCrossDevice, on => _settings.FixCrossDeviceService = on);
        Wire(_settingFixWidgets, on => _settings.FixWidgets = on);
        Wire(_settingWatchGpuAwake, on => _settings.WatchGpuAwake = on);
        Wire(_settingTrimDwmMemory, on => _settings.TrimDwmMemory = on);
        Wire(_settingTrimExplorerMemory, on => _settings.TrimExplorerMemory = on);
        if (_settingDisableNdu is not null)
        {
            // Asked first, with a restore point offered: it changes how a
            // Windows driver starts. A "no" puts the switch back.
            _settingDisableNdu.Checked += async (_, _) => await ConfirmNdu(true);
            _settingDisableNdu.Unchecked += async (_, _) => await ConfirmNdu(false);
        }
        async Task ConfirmNdu(bool on)
        {
            if (_settingsPageLoading) return;
            if (await ConfirmRiskyAsync("ndu", Strings.Get("S.Ndu.ConfirmTitle"), Strings.Get(on ? "S.Ndu.ConfirmOff" : "S.Ndu.ConfirmOn")))
            {
                ChangeSetting(() => ApplyNdu(on));
                Audit("ndu", "done", ("disabled", on));
                return;
            }
            _settingsPageLoading = true;
            _settingDisableNdu!.IsChecked = !on;
            _settingsPageLoading = false;
        }
        void ApplyNdu(bool on)
        {
            _settings.DisableNdu = on;
            Nextcalibur.Core.Hardware.NduFix.SetNduDisabled(on, _settings);
            if (on)
                _settings.RequestRestart("ndu");
            else
                _settings.ClearRestartReason("ndu");
            _settings.Save();
            _restartBannerDismissed = false;
            RefreshBanner();
        }
        Wire(_settingOverheatWarning, on =>
        {
            // The readings panel's toggle is the same setting; its handler
            // writes and saves, so drive it rather than write twice.
            OverheatWarningToggle.IsChecked = on;
            SetOverheatWarning(on);
        });
        // Turned off while cooling is on: CoolingTick undoes it at the next reading.
        Wire(_settingAutoCooling, on => _settings.AutoCooling = on);
        Wire(_settingAutoFanTurbo, on => _settings.AutoFanTurbo = on);
        foreach (var (button, ms) in _settingIntervals)
            button.Checked += (_, _) => ChangeSetting(() => _settings.PollIntervalMs = ms);

        // The language: chosen once here, remembered, applied at once. The
        // first start takes Windows' language; from then on the choice.
        if (_settingLanguageEnglish is not null)
            _settingLanguageEnglish.Checked += (_, _) => ChangeSetting(() => ChooseLanguage(UiLanguage.English));
        if (_settingLanguageTurkish is not null)
            _settingLanguageTurkish.Checked += (_, _) => ChangeSetting(() => ChooseLanguage(UiLanguage.Turkish));
        if (_settingCheckNowButton is not null)
            _settingCheckNowButton.Click += (_, _) => CheckForUpdatesByHand();
        if (_settingWinUtilButton is not null)
            _settingWinUtilButton.Click += (_, _) => OpenWinUtil();
        if (_settingDriversButton is not null)
            _settingDriversButton.Click += (_, _) => OpenVendorDrivers();
        if (_settingReportButton is not null)
            _settingReportButton.Click += (_, _) => OpenLink(UpdateService.Repository + "/issues", "issues", "Opened the repository's issues page");
        if (_settingPrivacyButton is not null)
            _settingPrivacyButton.Click += (_, _) => ShowPrivacy();

        if (FindName("SettingDebloatButton") is Button debloat)
            debloat.Click += (_, _) => OpenWin11Debloat();
        if (FindName("SettingBatteryReportButton") is Button batteryReport)
            batteryReport.Click += async (_, _) =>
            {
                batteryReport.IsEnabled = false;
                try { await OpenBatteryReportAsync(); }
                finally { batteryReport.IsEnabled = true; }
            };

        if (_tray is not null)
            _tray.SettingsChanged += (_, _) => Dispatcher.BeginInvoke(LoadSettingsPage);
        WireFnShortcutsPage();
        WireProgramRulesPage();
        WireIdlePage();
        WireCeilingsPage();
        WireNvPower();
        WireIdleLoadPage();
        _settingsPageWired = true;
        LoadSettingsPage();
    }

    private void Wire(ToggleButton? toggle, Action<bool> apply)
    {
        if (toggle is null) return;
        toggle.Checked += (_, _) => ChangeSetting(() => apply(true));
        toggle.Unchecked += (_, _) => ChangeSetting(() => apply(false));
    }

    /// <summary>One change from the page: apply, save, tell the tray - unless the page is being loaded.</summary>
    private void ChangeSetting(Action apply)
    {
        if (_settingsPageLoading) return;
        try
        {
            apply();
            _settings.Save();
            _tray?.SyncFromSettings();
        }
        catch (Exception ex) when (ex is InvalidOperationException or System.ComponentModel.Win32Exception or UnauthorizedAccessException)
        {
            Dialogs.Warn("Nextcalibur", ex.Message);
            LoadSettingsPage();
        }
    }

    /// <summary>Reads every setting into the page's controls without firing their handlers.</summary>
    private void LoadSettingsPage()
    {
        if (!_settingsPageWired) return;
        _settingsPageLoading = true;
        try
        {
            if (_settingStartWithWindows is not null)
            {
                var startupOn = StartupRegistration.IsEnabled;
                _settingStartWithWindows.IsChecked = startupOn;
                if (_settingStartInTray is not null) _settingStartInTray.IsEnabled = startupOn;
                if (_settingStartOnScreen is not null) _settingStartOnScreen.IsEnabled = startupOn;
            }
            if (_settingStartInTray is not null) _settingStartInTray.IsChecked = _settings.StartMinimised;
            if (_settingStartOnScreen is not null) _settingStartOnScreen.IsChecked = !_settings.StartMinimised;
            LoadBatteryMode();
            if (_settingWindowsFaults is not null)
            {
                _settingWindowsFaults.IsChecked = _settings.CompensateWindowsFaults;
                if (_panelWindowsFaultsSubOptions is not null)
                    _panelWindowsFaultsSubOptions.IsEnabled = _settings.CompensateWindowsFaults;
            }
            if (_settingFixTextInputHost is not null) _settingFixTextInputHost.IsChecked = _settings.FixTextInputHost;
            if (_settingFixCrossDevice is not null) _settingFixCrossDevice.IsChecked = _settings.FixCrossDeviceService;
            if (_settingFixWidgets is not null) _settingFixWidgets.IsChecked = _settings.FixWidgets;
            if (_settingWatchGpuAwake is not null) _settingWatchGpuAwake.IsChecked = _settings.WatchGpuAwake;
            if (_settingTrimDwmMemory is not null) _settingTrimDwmMemory.IsChecked = _settings.TrimDwmMemory;
            if (_settingTrimExplorerMemory is not null) _settingTrimExplorerMemory.IsChecked = _settings.TrimExplorerMemory;
            if (_settingDisableNdu is not null)
                _settingDisableNdu.IsChecked = _settings.DisableNdu || Nextcalibur.Core.Hardware.NduFix.IsNduDisabled();
            if (_settingOverheatWarning is not null) _settingOverheatWarning.IsChecked = _settings.OverheatWarningEnabled;
            if (_settingAutoCooling is not null) _settingAutoCooling.IsChecked = _settings.AutoCooling;
            if (_settingAutoFanTurbo is not null) _settingAutoFanTurbo.IsChecked = _settings.AutoFanTurbo;
            foreach (var (button, ms) in _settingIntervals)
                button.IsChecked = _settings.PollIntervalMs == ms;
            if (_settingLanguageEnglish is not null) _settingLanguageEnglish.IsChecked = Strings.Current == UiLanguage.English;
            if (_settingLanguageTurkish is not null) _settingLanguageTurkish.IsChecked = Strings.Current == UiLanguage.Turkish;
        }
        finally
        {
            _settingsPageLoading = false;
        }
    }

    /// <summary>
    /// Raphire's Win11Debloat, the owner's ask (14 October 2026) for the ads
    /// inside Windows 11 - Start, Settings, the lock screen, search. Chosen
    /// for being kept up - a release most months, MIT - and for turning off
    /// Windows' own suggestions rather than blocking domains. Run the way its
    /// author documents, in an administrator PowerShell
    /// with its own menu; asked first like WinUtil, with a restore point
    /// offered, since it changes Windows' settings. What is chosen in it is
    /// the person's.
    /// </summary>
    private async void OpenWin11Debloat()
    {
        if (!await ConfirmRiskyAsync("win11debloat", Strings.Get("S.Debloat.Title"), Strings.Get("S.Debloat.Body")))
            return;

        try
        {
            using var process = Process.Start(new ProcessStartInfo(Nextcalibur.Core.Security.SystemTools.PowerShell,
                "-NoProfile -ExecutionPolicy Bypass -Command \"& ([scriptblock]::Create((irm 'https://debloat.raphi.re/')))\"")
            {
                UseShellExecute = true,
            });
            Log.Info("win11debloat", "Opened Win11Debloat in an administrator PowerShell");
            Audit("win11debloat", "done");
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            Dialogs.Warn("Nextcalibur", Strings.Get("S.WinUtil.Failed") + " " + ex.Message);
        }
    }

    /// <summary>
    /// Windows' own battery report (powercfg /batteryreport): the battery's
    /// design and present capacity, its wear over time and recent use - the
    /// Tools card's empty slot (the owner's ask of 14 October 2026). Nothing
    /// is downloaded or changed; the report is written to the person's temp
    /// folder and opened in their browser, not as administrator.
    /// </summary>
    private async Task OpenBatteryReportAsync()
    {
        var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Nextcalibur-battery-report.html");
        try
        {
            var code = await Task.Run(() =>
            {
                using var process = Process.Start(new ProcessStartInfo(Nextcalibur.Core.Security.SystemTools.Powercfg,
                    $"/batteryreport /output \"{path}\"")
                {
                    UseShellExecute = false, CreateNoWindow = true,
                    RedirectStandardOutput = true, RedirectStandardError = true,
                });
                if (process is null) return -1;
                process.StandardOutput.ReadToEnd();
                if (!process.WaitForExit(30_000)) { process.Kill(); return -2; }
                return process.ExitCode;
            });
            if (code != 0 || !System.IO.File.Exists(path))
            {
                Log.Warn("battery-report", $"powercfg /batteryreport ended with {code}");
                Dialogs.Warn("Nextcalibur", Strings.Get("S.BatteryReport.Failed"));
                return;
            }
            Unelevated.Open(path);
            Log.Info("battery-report", "Battery report written and opened: " + path);
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException or System.IO.IOException)
        {
            Log.Warn("battery-report", ex.Message);
            Dialogs.Warn("Nextcalibur", Strings.Get("S.BatteryReport.Failed") + " " + ex.Message);
        }
    }

    private System.Windows.Controls.ComboBox? _settingBatteryMode;
    private bool _batteryModeLoading;

    /// <summary>
    /// The mode on battery: Power Saver, Balanced, or leave it alone (the
    /// owner's choice of 14 October 2026, replacing the Office-on-battery
    /// switch). Shared with the tray's submenu through the settings.
    /// </summary>
    private void WireBatteryMode()
    {
        _settingBatteryMode = FindName("SettingBatteryMode") as System.Windows.Controls.ComboBox;
        if (_settingBatteryMode is null) return;
        _settingBatteryMode.SelectionChanged += (_, _) =>
        {
            if (_batteryModeLoading || Localized.Refreshing || _settingBatteryMode.SelectedItem is not System.Windows.Controls.ComboBoxItem { Tag: var tag }) return;
            SetBatteryMode(tag as SystemMode?);
        };
    }

    private void LoadBatteryMode()
    {
        if (_settingBatteryMode is null) return;
        _batteryModeLoading = true;
        try
        {
            _settingBatteryMode.Items.Clear();
            foreach (var mode in new SystemMode?[] { SystemMode.Office, SystemMode.Balanced, null })
            {
                var each = mode;
                var item = Localized.Content(new System.Windows.Controls.ComboBoxItem { Tag = mode },
                    () => each is { } m ? ModeName(m) : Strings.Get("S.Settings.BatteryModeOff"));
                _settingBatteryMode.Items.Add(item);
                if (mode == _settings.ModeOnBattery) _settingBatteryMode.SelectedItem = item;
            }
            _settingBatteryMode.IsEnabled = _support.AllowsWrites;
            Localized.Items(_settingBatteryMode);
        }
        finally
        {
            _batteryModeLoading = false;
        }
    }

    /// <summary>Records the battery's mode and applies it now if on battery.</summary>
    internal void SetBatteryMode(SystemMode? mode)
    {
        _settings.BatteryMode = mode;
        _settings.BatteryModeSet = true;
        _settings.QuietOnBattery = mode is not null;
        _settings.Save();
        _battery.QuietMode = mode ?? BatteryModePolicy.DefaultQuietMode;
        Log.Info("power", $"Mode on battery: {mode?.ToString() ?? "unchanged"}");
        _tray?.SyncFromSettings();
        LoadBatteryMode();
        EvaluateProgramRules();
    }

    /// <summary>
    /// Chris Titus Tech's WinUtil, the owner's ask (12 September 2026): a
    /// Windows debloat and tweak script, run the way its author documents -
    /// downloaded from christitus.com into an administrator PowerShell.
    /// Nothing of it is shipped or vetted here, and the dialogue says so;
    /// the PowerShell inherits this process's elevation, so no second
    /// prompt. What is done inside it is the person's.
    /// </summary>
    private async void OpenWinUtil()
    {
        if (!await ConfirmRiskyAsync("winutil", Strings.Get("S.WinUtil.Title"), Strings.Get("S.WinUtil.Body")))
            return;

        try
        {
            using var process = Process.Start(new ProcessStartInfo(Nextcalibur.Core.Security.SystemTools.PowerShell,
                "-NoProfile -ExecutionPolicy Bypass -Command \"irm https://christitus.com/win | iex\"")
            {
                UseShellExecute = true,
            });
            Log.Info("winutil", "Opened WinUtil in an administrator PowerShell");
            Audit("winutil", "done");
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            Dialogs.Warn("Nextcalibur", Strings.Get("S.WinUtil.Failed") + " " + ex.Message);
        }
    }

    /// <summary>
    /// The vendor's own driver download page, in the default browser. A link
    /// and nothing more: the drivers are theirs, and this is where they
    /// keep them. The owner's ask, 12 September 2026.
    /// </summary>
    private const string VendorDriverPage = "https://www.casper.com.tr/driver-indirme";

    private void OpenVendorDrivers() => OpenLink(VendorDriverPage, "drivers", "Opened the vendor's driver page");

    /// <summary>A page in the default browser; the failure, if any, is said in the window.</summary>
    private void OpenLink(string url, string category, string logLine)
    {
        try
        {
            Unelevated.Open(url);   // the browser as the person, not as administrator
            Log.Info(category, logLine);
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            Dialogs.Warn("Nextcalibur", Strings.Get("S.Link.Failed") + " " + ex.Message);
        }
    }

    /// <summary>
    /// The privacy policy, in the application's own words first and the
    /// full page (docs/PRIVACY.md, kept with the source) on a yes. The
    /// summary is the policy in four lines; the page is the same, with
    /// the table of every request the application ever makes.
    /// </summary>
    private void ShowPrivacy()
    {
        if (Dialogs.Ask(Strings.Get("S.Privacy.Title"), Strings.Get("S.Privacy.Body"), defaultNo: true))
            OpenLink(UpdateService.Repository + "/blob/main/docs/PRIVACY.md", "privacy", "Opened the privacy policy");
    }

    private void ChooseLanguage(UiLanguage language)
    {
        _settings.Language = language;
        Strings.Apply(language);   // saved by ChangeSetting; the swap re-says everything the code wrote
    }

    private void ShowSettingsPageIfChosen()
    {
        if (_pageSettings is null || _navSettings is null) return;
        var chosen = _navSettings.IsChecked == true;
        _pageSettings.Visibility = chosen ? Visibility.Visible : Visibility.Collapsed;
        if (chosen) LoadSettingsPage();
    }

    /// <summary>On an unsupported machine the page shows the settings and lets none of them be changed.</summary>
    /// <summary>
    /// On a machine that reads but is not the vendor's: the settings that
    /// drive its modes, its fans or stand in for its keys cannot be clicked;
    /// the rest of the page - Windows' own repairs, updates, language - stays.
    /// </summary>
    private void LockCasperOnlySettings()
    {
        foreach (var name in new[] { "SettingOfficeOnBattery", "SettingAutoFanTurbo", "SettingAutoCooling", "SettingFnShortcuts", "FnSelectAll", "FnShortcutRows",
                                     "SettingProgramRules", "ProgramRuleRows", "ProgramRuleAddRow" })
            if (FindName(name) is UIElement element) element.IsEnabled = false;
    }

    private void LockSettingsPage()
    {
        if (FindName("NavSettings") is RadioButton nav) nav.IsEnabled = false;
        if (FindName("PageSettings") is Grid page) page.IsEnabled = false;
    }
}
