using System.Drawing;
using System.Windows;
using Nextcalibur.Core.Configuration;
using Forms = System.Windows.Forms;

namespace Nextcalibur.App;

/// <summary>
/// The notification-area icon and its menu.
///
/// WPF has no tray primitive, so this wraps the Windows Forms one. The icon also
/// carries the current CPU temperature in its tooltip, which is the whole point
/// of leaving the app running with its window closed.
/// </summary>
public sealed class TrayPresence : IDisposable
{
    private readonly Forms.NotifyIcon _icon;
    private readonly Window _window;
    private readonly AppSettings _settings;
    private Forms.ToolStripMenuItem _startupItem = null!;
    private bool _disposed;

    /// <summary>Raised by the menu's "Check for updates now"; the window runs the check and makes the offer.</summary>
    public event EventHandler? CheckForUpdatesRequested;

    /// <summary>Raised when the person clicks a notification balloon; the window is already shown.</summary>
    public event EventHandler? BalloonClicked;

    /// <summary>Raised when the tray changed the overheat setting, so the window's switch can follow.</summary>
    public event EventHandler? OverheatSettingChanged;

    public TrayPresence(Window window, AppSettings settings)
    {
        _window = window;
        _settings = settings;

        _icon = new Forms.NotifyIcon
        {
            Icon = LoadIcon(),
            Visible = true,
            Text = "Nextcalibur Control Center",
            ContextMenuStrip = BuildMenu(),
        };
        _icon.DoubleClick += (_, _) => ShowWindow();
        _icon.BalloonTipClicked += (_, _) => { ShowWindow(); BalloonClicked?.Invoke(this, EventArgs.Empty); };
    }

    /// <summary>
    /// The menu, in the current language. Built once at start and again on
    /// a language swap (<see cref="RebuildMenu"/>): WinForms items carry
    /// their text as a plain string, so the whole menu is remade rather
    /// than each item retitled - it is a dozen items and takes a moment.
    /// </summary>
    private Forms.ContextMenuStrip BuildMenu()
    {
        _startupItem = new Forms.ToolStripMenuItem(Strings.Get("S.Tray.StartWithWindows"))
        {
            CheckOnClick = true,
            Checked = StartupRegistration.IsEnabled,
        };
        _startupItem.CheckedChanged += OnStartupToggled;

        var menu = new Forms.ContextMenuStrip();
        menu.Items.Add(Strings.Get("S.Tray.Open"), null, (_, _) => ShowWindow());
        menu.Items.Add(new Forms.ToolStripSeparator());
        menu.Items.Add(_startupItem);
        menu.Items.Add(QuietOnBatteryItem());
        menu.Items.Add(OverheatWarningMenu());
        menu.Items.Add(ReadingIntervalMenu());
        menu.Items.Add(new Forms.ToolStripSeparator());
        menu.Items.Add(AutoUpdateItem());
        menu.Items.Add(AutoInstallItem());
        menu.Items.Add(Strings.Get("S.Tray.CheckNow"), null, (_, _) => CheckForUpdatesRequested?.Invoke(this, EventArgs.Empty));
        menu.Items.Add(Strings.Get("S.Tray.OpenLog"), null, (_, _) =>
        {
            try { Unelevated.Open(Log.Folder); }
            catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException) { }
        });
        menu.Items.Add(new Forms.ToolStripSeparator());
        menu.Items.Add(Strings.Get("S.Tray.Exit"), null, (_, _) => ExitRequested?.Invoke(this, EventArgs.Empty));
        return menu;
    }

    /// <summary>Remakes the menu in the current language; the old one is disposed.</summary>
    public void RebuildMenu()
    {
        if (_disposed) return;
        var old = _icon.ContextMenuStrip;
        _intervalItems.Clear();
        _icon.ContextMenuStrip = BuildMenu();
        old?.Dispose();
    }

    /// <summary>Raised when the user chooses Exit.</summary>
    public event EventHandler? ExitRequested;

    /// <summary>
    /// Raised after any setting is changed from this menu, so the window's
    /// settings page can re-read. The reverse direction is
    /// <see cref="SyncFromSettings"/>. Both surfaces write the one
    /// <see cref="AppSettings"/>; neither caches.
    /// </summary>
    public event EventHandler? SettingsChanged;

    private Forms.ToolStripMenuItem? _quietItem, _autoCheckItem, _autoInstallItem;
    private readonly List<(Forms.ToolStripMenuItem Item, int Ms)> _intervalItems = new();

    /// <summary>Re-ticks every item from the settings, for when the window changed one.</summary>
    public void SyncFromSettings()
    {
        if (_disposed) return;
        Quietly(_startupItem, OnStartupToggled, StartupRegistration.IsEnabled);
        if (_quietItem is not null) Quietly(_quietItem, OnQuietToggled, _settings.QuietOnBattery);
        if (_autoCheckItem is not null) Quietly(_autoCheckItem, OnAutoCheckToggled, _settings.AutoCheckForUpdates);
        if (_autoInstallItem is not null)
        {
            Quietly(_autoInstallItem, OnAutoInstallToggled, _settings.AutoInstallUpdates);
            _autoInstallItem.Enabled = _settings.AutoCheckForUpdates;
        }
        foreach (var (item, ms) in _intervalItems) item.Checked = _settings.PollIntervalMs == ms;
        SyncOverheatMenu();
    }

    private static void Quietly(Forms.ToolStripMenuItem item, EventHandler handler, bool value)
    {
        item.CheckedChanged -= handler;
        item.Checked = value;
        item.CheckedChanged += handler;
    }

    // ------------------------------------------------------------- settings
    //
    // These three settings were honoured by the application and reachable only
    // by hand-editing the settings file — the overheat threshold in particular,
    // fixed at 90 °C on machines that idle warmer than that. They live here
    // rather than in the window because this is a tray application and it is
    // where Windows users look for its preferences; adding a fifth page would
    // also break the deliberate resemblance to the software this replaces.
    //
    // Nothing has to be told about a change: this is the same settings object
    // the window holds, and the window reads these values as it needs them
    // rather than caching them. The close behaviour applies immediately, the
    // warning threshold at the next slow tick, and the sampling interval when
    // that tick next reconsiders it — a few seconds at worst.

    private Forms.ToolStripMenuItem AutoUpdateItem()
    {
        _autoCheckItem = new Forms.ToolStripMenuItem(Strings.Get("S.Tray.AutoCheck"))
        {
            CheckOnClick = true,
            Checked = _settings.AutoCheckForUpdates,
        };
        _autoCheckItem.CheckedChanged += OnAutoCheckToggled;
        return _autoCheckItem;
    }

    private void OnAutoCheckToggled(object? sender, EventArgs e)
    {
        _settings.AutoCheckForUpdates = _autoCheckItem!.Checked;
        if (_autoInstallItem is not null) _autoInstallItem.Enabled = _autoCheckItem.Checked;
        _settings.Save();
        SettingsChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>Install without asking - a step beyond checking, and only meaningful with checking on.</summary>
    private Forms.ToolStripMenuItem AutoInstallItem()
    {
        _autoInstallItem = new Forms.ToolStripMenuItem(Strings.Get("S.Tray.AutoInstall"))
        {
            CheckOnClick = true,
            Checked = _settings.AutoInstallUpdates,
            Enabled = _settings.AutoCheckForUpdates,
        };
        _autoInstallItem.CheckedChanged += OnAutoInstallToggled;
        return _autoInstallItem;
    }

    private void OnAutoInstallToggled(object? sender, EventArgs e)
    {
        _settings.AutoInstallUpdates = _autoInstallItem!.Checked;
        _settings.Save();
        SettingsChanged?.Invoke(this, EventArgs.Empty);
    }

    private Forms.ToolStripMenuItem QuietOnBatteryItem()
    {
        _quietItem = new Forms.ToolStripMenuItem(Strings.Get("S.Tray.OfficeOnBattery"))
        {
            CheckOnClick = true,
            Checked = _settings.QuietOnBattery,
        };
        _quietItem.CheckedChanged += OnQuietToggled;
        return _quietItem;
    }

    private void OnQuietToggled(object? sender, EventArgs e)
    {
        _settings.QuietOnBattery = _quietItem!.Checked;
        _settings.Save();
        SettingsChanged?.Invoke(this, EventArgs.Empty);
    }

    private Forms.ToolStripMenuItem? _overheatItem;

    /// <summary>Re-reads the overheat setting into the menu, for when the window changed it.</summary>
    public void SyncOverheatMenu()
    {
        if (_overheatItem is null) return;
        _overheatItem.CheckedChanged -= OnOverheatToggled;
        _overheatItem.Checked = _settings.OverheatWarningEnabled;
        _overheatItem.CheckedChanged += OnOverheatToggled;
    }

    /// <summary>
    /// On or off only. The two thresholds - one per chip - are sliders on the
    /// System page, where the temperatures they apply to are on screen.
    /// </summary>
    private Forms.ToolStripMenuItem OverheatWarningMenu()
    {
        _overheatItem = new Forms.ToolStripMenuItem(Strings.Get("S.Tray.OverheatWarning"))
        {
            CheckOnClick = true,
            Checked = _settings.OverheatWarningEnabled,
        };
        _overheatItem.CheckedChanged += OnOverheatToggled;
        return _overheatItem;
    }

    private void OnOverheatToggled(object? sender, EventArgs e)
    {
        _settings.OverheatWarningEnabled = _overheatItem!.Checked;
        _settings.Save();
        OverheatSettingChanged?.Invoke(this, EventArgs.Empty);
        SettingsChanged?.Invoke(this, EventArgs.Empty);
    }

    private Forms.ToolStripMenuItem ReadingIntervalMenu()
    {
        var menu = new Forms.ToolStripMenuItem(Strings.Get("S.Tray.ReadEvery"));
        var choices = new (string Label, int Ms)[]
        {
            (Strings.Get("S.Tray.Interval.1"), 1000), (Strings.Get("S.Tray.Interval.2"), 2000),
            (Strings.Get("S.Tray.Interval.5"), 5000), (Strings.Get("S.Tray.Interval.10"), 10000),
        };

        foreach (var (label, ms) in choices)
        {
            var choice = new Forms.ToolStripMenuItem(label)
            {
                Checked = _settings.PollIntervalMs == ms,
            };

            choice.Click += (_, _) =>
            {
                _settings.PollIntervalMs = ms;
                _settings.Save();
                Tick(menu, choice);
                SettingsChanged?.Invoke(this, EventArgs.Empty);
            };

            menu.DropDownItems.Add(choice);
            _intervalItems.Add((choice, ms));
        }

        return menu;
    }

    /// <summary>Leaves exactly one item in a group ticked.</summary>
    private static void Tick(Forms.ToolStripMenuItem group, Forms.ToolStripMenuItem chosen)
    {
        foreach (Forms.ToolStripMenuItem item in group.DropDownItems)
            item.Checked = ReferenceEquals(item, chosen);
    }

    /// <summary>
    /// Updates the tooltip. Windows truncates tray tooltips at 63 characters, so
    /// this stays deliberately terse.
    /// </summary>
    public void UpdateStatus(int cpuC, int gpuC, int cpuRpm)
    {
        if (_disposed) return;
        _icon.Text = Strings.Get("S.Tray.Tooltip", cpuC, gpuC, cpuRpm);
    }

    public void ShowMessage(string title, string body) =>
        _icon.ShowBalloonTip(5000, title, body, Forms.ToolTipIcon.Warning);

    /// <summary>
    /// The tour's last step: a balloon so the person finds the icon, and the
    /// menu opened where the notification area is, so every item can be
    /// read while the card describes it. Windows keeps the icon's exact
    /// position to itself; the corner of the working area is where the
    /// menu would open from it anyway.
    /// </summary>
    public void ShowMenuForTour()
    {
        if (_disposed) return;
        _icon.ShowBalloonTip(8000, Strings.Get("S.Tray.TourBalloonTitle"), Strings.Get("S.Tray.TourBalloonBody"), Forms.ToolTipIcon.Info);
        var area = Forms.Screen.PrimaryScreen?.WorkingArea ?? Forms.SystemInformation.WorkingArea;
        _icon.ContextMenuStrip?.Show(new System.Drawing.Point(area.Right - 8, area.Bottom - 8), Forms.ToolStripDropDownDirection.AboveLeft);
    }

    /// <summary>Brings the window up for something that is not the tray icon - a toast, say.</summary>
    public void ShowWindowFromOutside() => ShowWindow();

    private void ShowWindow()
    {
        _window.Show();
        _window.WindowState = WindowState.Normal;
        _window.Activate();
    }

    private void OnStartupToggled(object? sender, EventArgs e)
    {
        try
        {
            var exe = Environment.ProcessPath
                ?? throw new InvalidOperationException("Could not determine the executable path.");
            StartupRegistration.Set(_startupItem.Checked, exe);
            _settings.StartMinimised = _startupItem.Checked;
            _settings.Save();
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Dialogs.Warn("Nextcalibur", ex.Message);
            _startupItem.CheckedChanged -= OnStartupToggled;
            _startupItem.Checked = StartupRegistration.IsEnabled;
            _startupItem.CheckedChanged += OnStartupToggled;
        }
    }

    private static Icon LoadIcon()
    {
        var stream = Application.GetResourceStream(
            new Uri("pack://application:,,,/Assets/app.ico"))?.Stream;

        // SystemIcons.Application is a shared instance and must not be disposed,
        // but it is only reached when the packed resource is missing.
        return stream is null ? SystemIcons.Application : new Icon(stream);
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _icon.Visible = false;
        _icon.Dispose();
    }
}
