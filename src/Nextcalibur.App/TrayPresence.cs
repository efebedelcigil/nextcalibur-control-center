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
    private readonly Forms.ToolStripMenuItem _startupItem;
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

        _startupItem = new Forms.ToolStripMenuItem("Start with Windows")
        {
            CheckOnClick = true,
            Checked = StartupRegistration.IsEnabled,
        };
        _startupItem.CheckedChanged += OnStartupToggled;

        var menu = new Forms.ContextMenuStrip();
        menu.Items.Add("Open Nextcalibur", null, (_, _) => ShowWindow());
        menu.Items.Add(new Forms.ToolStripSeparator());
        menu.Items.Add(_startupItem);
        menu.Items.Add(QuietOnBatteryItem());
        menu.Items.Add(OverheatWarningMenu());
        menu.Items.Add(ReadingIntervalMenu());
        menu.Items.Add(new Forms.ToolStripSeparator());
        menu.Items.Add(AutoUpdateItem());
        menu.Items.Add("Check for updates now", null, (_, _) => CheckForUpdatesRequested?.Invoke(this, EventArgs.Empty));
        menu.Items.Add("Open the log folder", null, (_, _) =>
        {
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("explorer.exe", Log.Folder) { UseShellExecute = true }); }
            catch (Exception ex) when (ex is System.ComponentModel.Win32Exception or InvalidOperationException) { }
        });
        menu.Items.Add(new Forms.ToolStripSeparator());
        menu.Items.Add("Exit", null, (_, _) => ExitRequested?.Invoke(this, EventArgs.Empty));

        _icon = new Forms.NotifyIcon
        {
            Icon = LoadIcon(),
            Visible = true,
            Text = "Nextcalibur Control Center",
            ContextMenuStrip = menu,
        };
        _icon.DoubleClick += (_, _) => ShowWindow();
        _icon.BalloonTipClicked += (_, _) => { ShowWindow(); BalloonClicked?.Invoke(this, EventArgs.Empty); };
    }

    /// <summary>Raised when the user chooses Exit.</summary>
    public event EventHandler? ExitRequested;

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
        var item = new Forms.ToolStripMenuItem("Check for updates automatically")
        {
            CheckOnClick = true,
            Checked = _settings.AutoCheckForUpdates,
        };

        item.CheckedChanged += (_, _) =>
        {
            _settings.AutoCheckForUpdates = item.Checked;
            _settings.Save();
        };

        return item;
    }

    private Forms.ToolStripMenuItem QuietOnBatteryItem()
    {
        var item = new Forms.ToolStripMenuItem("Office mode on battery")
        {
            CheckOnClick = true,
            Checked = _settings.QuietOnBattery,
        };

        item.CheckedChanged += (_, _) =>
        {
            _settings.QuietOnBattery = item.Checked;
            _settings.Save();
        };

        return item;
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
        _overheatItem = new Forms.ToolStripMenuItem("Warn when the CPU or GPU runs hot")
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
    }

    private Forms.ToolStripMenuItem ReadingIntervalMenu()
    {
        var menu = new Forms.ToolStripMenuItem("Read the sensors every");
        var choices = new (string Label, int Ms)[]
        {
            ("Second", 1000), ("2 seconds", 2000), ("5 seconds", 5000), ("10 seconds", 10000),
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
            };

            menu.DropDownItems.Add(choice);
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
        _icon.Text = $"CPU {cpuC}°C · GPU {gpuC}°C · fan {cpuRpm} rpm";
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
        _icon.ShowBalloonTip(8000, "This is Nextcalibur's tray icon", "Right-click it for the menu; double-click to open the window.", Forms.ToolTipIcon.Info);
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
