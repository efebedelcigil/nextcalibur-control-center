using System.Diagnostics;
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
        _settingOfficeOnBattery = FindName("SettingOfficeOnBattery") as ToggleButton;
        _settingOverheatWarning = FindName("SettingOverheatWarning") as ToggleButton;
        _settingAutoCheckUpdates = FindName("SettingAutoCheckUpdates") as ToggleButton;
        _settingAutoInstallUpdates = FindName("SettingAutoInstallUpdates") as ToggleButton;
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
            if (_settingLanguageEnglish is not null) _settingLanguageEnglish.IsChecked = Strings.Current == UiLanguage.English;
            if (_settingLanguageTurkish is not null) _settingLanguageTurkish.IsChecked = Strings.Current == UiLanguage.Turkish;
        }
        finally
        {
            _settingsPageLoading = false;
        }
    }

    /// <summary>
    /// Chris Titus Tech's WinUtil, the owner's ask (12 September 2026): a
    /// Windows debloat and tweak script, run the way its author documents -
    /// downloaded from christitus.com into an administrator PowerShell.
    /// Nothing of it is shipped or vetted here, and the dialogue says so;
    /// the PowerShell inherits this process's elevation, so no second
    /// prompt. What is done inside it is the person's.
    /// </summary>
    private void OpenWinUtil()
    {
        if (!Dialogs.Ask("Open WinUtil?",
                "This opens Chris Titus Tech's WinUtil in an administrator PowerShell window: the script is " +
                "downloaded from christitus.com and run. It is not part of Nextcalibur and nothing here checks " +
                "it; what you change there - services, apps, settings - is between you and it." +
                Environment.NewLine + Environment.NewLine + "Continue?",
                defaultNo: true))
            return;

        try
        {
            Process.Start(new ProcessStartInfo("powershell.exe",
                "-NoProfile -ExecutionPolicy Bypass -Command \"irm https://christitus.com/win | iex\"")
            {
                UseShellExecute = true,
            });
            Log.Info("winutil", "Opened WinUtil in an administrator PowerShell");
        }
        catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            Dialogs.Warn("Nextcalibur", "PowerShell could not be started: " + ex.Message);
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
            Dialogs.Warn("Nextcalibur", "The browser could not be opened: " + ex.Message);
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
        if (Dialogs.Ask("Privacy",
                "Nextcalibur collects nothing about you and sends nothing about you anywhere." +
                Environment.NewLine + Environment.NewLine +
                "The only requests it makes are to GitHub, to find its own updates and PawnIO's - and none at all " +
                "with automatic checks off - and they carry nothing of yours beyond what any download does. " +
                "Settings and the log stay in your profile; nothing is uploaded unless you attach it to a report yourself. " +
                "No account, no telemetry, no analytics." +
                Environment.NewLine + Environment.NewLine +
                "Open the full policy, with the list of every request, in your browser?",
                defaultNo: true))
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
    private void LockSettingsPage()
    {
        if (FindName("NavSettings") is RadioButton nav) nav.IsEnabled = false;
        if (FindName("PageSettings") is Grid page) page.IsEnabled = false;
    }
}
