using System.Windows;

namespace Nextcalibur.App;

/// <summary>
/// Every dialogue the application raises, through one door.
///
/// Three things the owner asked for: no sound, nothing else clickable while
/// one is up, and the application's own look rather than Windows'. All three
/// are here now: with the window on screen the box is drawn inside it, over
/// a shade that takes every click; otherwise it is Windows' box, silent.
///
/// The sound is the icon. <see cref="MessageBoxImage"/> values other than
/// <c>None</c> play the system's alert sounds; none of these pass one. The
/// modality is the owner: a box owned by the main window blocks that window
/// until it is answered. Boxes raised before the window exists - the first-run
/// repair, the uninstall question - have no owner to block and are modal by
/// being the only thing on screen.
/// </summary>
public static class Dialogs
{
    /// <summary>The main window, once there is one, so every box can be owned by it.</summary>
    public static Window? Owner { get; set; }

    /// <summary>Something happened and the person should know.</summary>
    public static void Tell(string title, string body) =>
        Show(title, body, MessageBoxButton.OK, MessageBoxResult.OK);

    /// <summary>Something went wrong, or is about to, and the person should know.</summary>
    public static void Warn(string title, string body) =>
        Show(title, body, MessageBoxButton.OK, MessageBoxResult.OK);

    /// <summary>A yes-or-no question. Escape and closing the box mean the default.</summary>
    /// <param name="defaultNo">
    /// Whether "No" is the safe answer. True for anything that changes the
    /// machine - an unanswered question must not do the thing.
    /// </param>
    public static bool Ask(string title, string body, bool defaultNo = true)
    {
        var answer = Show(title, body, MessageBoxButton.YesNo,
            defaultNo ? MessageBoxResult.No : MessageBoxResult.Yes);
        return answer == MessageBoxResult.Yes;
    }

    /// <summary>An OK-or-cancel confirmation. Cancel is the default.</summary>
    public static bool Confirm(string title, string body)
    {
        var answer = Show(title, body, MessageBoxButton.OKCancel, MessageBoxResult.Cancel);
        return answer == MessageBoxResult.OK;
    }

    private static MessageBoxResult Show(string title, string body, MessageBoxButton buttons, MessageBoxResult fallback)
    {
        // The owner may be on another thread's dispatcher when this is called
        // from the tray; marshal rather than assume.
        var owner = Owner;
        if (owner is not null && !owner.Dispatcher.CheckAccess())
            return owner.Dispatcher.Invoke(() => Show(title, body, buttons, fallback));

        // The application's own look, when there is a window to draw it in.
        // Before the window exists, or while it is in the tray, Windows' box
        // is still the honest choice: a dialogue nobody can see is no dialogue.
        if (owner is MainWindow { IsVisible: true } main && main.WindowState != WindowState.Minimized)
            return main.ShowOverlayDialog(title, body, buttons, fallback);

        return owner is not null && owner.IsVisible
            ? MessageBox.Show(owner, body, title, buttons, MessageBoxImage.None, fallback)
            : MessageBox.Show(body, title, buttons, MessageBoxImage.None, fallback);
    }
}
