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
    }

    /// <summary>Raised when the user chooses Exit.</summary>
    public event EventHandler? ExitRequested;

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
            MessageBox.Show(ex.Message, "Nextcalibur", MessageBoxButton.OK, MessageBoxImage.Warning);
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
