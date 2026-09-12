using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Nextcalibur.App;

/// <summary>
/// A Windows notification with a button on it.
///
/// The tray's balloon cannot carry a button; a toast can. An unpackaged
/// application may raise toasts once a Start-menu shortcut carries its
/// AppUserModelID - which <see cref="AppIdentity"/> arranges - and while the
/// application is running its buttons come back as an event in-process.
/// This application is always running (it lives in the tray), so nothing
/// more is registered on the machine: no COM activator, no class keys,
/// nothing for the uninstall to take back. Where a toast cannot be shown -
/// a build with no shortcut, a machine with notifications off - the caller
/// falls back to the balloon.
/// </summary>
public static class Toasts
{
    /// <summary>
    /// Shows a toast with one button. Returns false when it could not be
    /// shown, so the caller can use the balloon instead.
    /// </summary>
    /// <param name="onButton">Called on the calling thread's dispatcher when the button is pressed.</param>
    public static bool TryShow(string title, string body, string buttonText, Action onButton)
    {
        try
        {
            var xml = new XmlDocument();
            xml.LoadXml($"""
                <toast>
                  <visual>
                    <binding template="ToastGeneric">
                      <text>{Escape(title)}</text>
                      <text>{Escape(body)}</text>
                    </binding>
                  </visual>
                  <actions>
                    <action content="{Escape(buttonText)}" arguments="button" activationType="foreground"/>
                  </actions>
                </toast>
                """);

            var toast = new ToastNotification(xml);
            var dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            toast.Activated += (_, args) =>
            {
                // A click on the body activates with empty arguments; the
                // button with its own. Both mean "show me", so both count.
                dispatcher.BeginInvoke(onButton);
            };

            ToastNotificationManager.CreateToastNotifier(AppIdentity.Id).Show(toast);
            return true;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            return false;
        }
    }

    private static string Escape(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
}
