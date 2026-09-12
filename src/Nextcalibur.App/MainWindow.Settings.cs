using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Nextcalibur.Core.Configuration;
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
    private ToggleButton? _settingStartWithWindows, _settingOfficeOnBattery, _settingOverheatWarning,
        _settingAutoCheckUpdates, _settingAutoInstallUpdates;
    private readonly List<(RadioButton Button, int Ms)> _settingIntervals = new();
    private Button? _settingCheckNowButton;
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
        _settingOfficeOnBattery = FindName("SettingOfficeOnBattery") as ToggleButton;
        _settingOverheatWarning = FindName("SettingOverheatWarning") as ToggleButton;
        _settingAutoCheckUpdates = FindName("SettingAutoCheckUpdates") as ToggleButton;
        _settingAutoInstallUpdates = FindName("SettingAutoInstallUpdates") as ToggleButton;
        _settingCheckNowButton = FindName("SettingCheckNowButton") as Button;
        foreach (var ms in new[] { 1000, 2000, 5000, 10000 })
            if (FindName($"SettingInterval{ms / 1000}") is RadioButton button)
                _settingIntervals.Add((button, ms));

        Wire(_settingStartWithWindows, on =>
        {
            var exe = Environment.ProcessPath ?? throw new InvalidOperationException("Could not determine the executable path.");
            StartupRegistration.Set(on, exe);
            _settings.StartMinimised = on;
        });
        Wire(_settingOfficeOnBattery, on => _settings.QuietOnBattery = on);
        Wire(_settingOverheatWarning, on =>
        {
            // The readings panel's toggle is the same setting; its handler
            // writes and saves, so drive it rather than write twice.
            OverheatWarningToggle.IsChecked = on;
            SetOverheatWarning(on);
        });
        Wire(_settingAutoCheckUpdates, on =>
        {
            _settings.AutoCheckForUpdates = on;
            if (_settingAutoInstallUpdates is not null) _settingAutoInstallUpdates.IsEnabled = on;
        });
        Wire(_settingAutoInstallUpdates, on => _settings.AutoInstallUpdates = on);
        foreach (var (button, ms) in _settingIntervals)
            button.Checked += (_, _) => ChangeSetting(() => _settings.PollIntervalMs = ms);
        if (_settingCheckNowButton is not null)
            _settingCheckNowButton.Click += (_, _) => CheckForUpdatesByHand();

        if (_tray is not null)
            _tray.SettingsChanged += (_, _) => Dispatcher.BeginInvoke(LoadSettingsPage);
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
            if (_settingStartWithWindows is not null) _settingStartWithWindows.IsChecked = StartupRegistration.IsEnabled;
            if (_settingOfficeOnBattery is not null) _settingOfficeOnBattery.IsChecked = _settings.QuietOnBattery;
            if (_settingOverheatWarning is not null) _settingOverheatWarning.IsChecked = _settings.OverheatWarningEnabled;
            if (_settingAutoCheckUpdates is not null) _settingAutoCheckUpdates.IsChecked = _settings.AutoCheckForUpdates;
            if (_settingAutoInstallUpdates is not null)
            {
                _settingAutoInstallUpdates.IsChecked = _settings.AutoInstallUpdates;
                _settingAutoInstallUpdates.IsEnabled = _settings.AutoCheckForUpdates;
            }
            foreach (var (button, ms) in _settingIntervals)
                button.IsChecked = _settings.PollIntervalMs == ms;
        }
        finally
        {
            _settingsPageLoading = false;
        }
    }

    private void ShowSettingsPageIfChosen()
    {
        if (_pageSettings is null || _navSettings is null) return;
        var chosen = _navSettings.IsChecked == true;
        _pageSettings.Visibility = chosen ? Visibility.Visible : Visibility.Collapsed;
        if (chosen) LoadSettingsPage();
    }

    /// <summary>On an unsupported machine the page shows the settings and lets none of them be changed.</summary>
    private void LockSettingsPage()
    {
        if (FindName("NavSettings") is RadioButton nav) nav.IsEnabled = false;
        if (FindName("PageSettings") is Grid page) page.IsEnabled = false;
    }
}
