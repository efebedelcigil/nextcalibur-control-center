using System.Windows;
using System.Windows.Controls;

namespace Nextcalibur.App;

/// <summary>
/// Words the code put on screen, said again when the language changes - and
/// nothing else (the owner's rule of 15 October 2026: "dil değişimi sadece
/// UI'ı etkilemeli").
///
/// The markup's own words follow the dictionary by themselves. What the code
/// builds - a mode's name in a row, "10 min" in a list, a button's tooltip -
/// used to be built again on a language change: settings reloaded, rows
/// rebuilt, the NVIDIA driver and the hardware read again, handlers firing on
/// the way. Now each such word is bound here to the function that makes it,
/// and a language change only runs those functions and writes the results
/// into the same elements. No row is rebuilt, no setting reloaded, nothing
/// read from the machine, no handler run.
/// </summary>
internal static class Localized
{
    private static readonly List<(WeakReference<DependencyObject> Target, DependencyProperty Property, Func<object?> Value)> Bound = [];
    private static readonly List<WeakReference<ItemsControl>> Lists = [];
    private static int _added;

    /// <summary>True while the words are being said again: a handler that sees it does nothing.</summary>
    public static bool Refreshing { get; private set; }

    /// <summary>Sets a property from a function now, and again at every language change.</summary>
    public static T Bind<T>(T target, DependencyProperty property, Func<object?> value) where T : DependencyObject
    {
        target.SetValue(property, value());
        // One binding per element and property: a row refreshed in place binds again.
        Bound.RemoveAll(b => b.Property == property && b.Target.TryGetTarget(out var t) && ReferenceEquals(t, target));
        // Rows are built again as Windows changes under them: what they left is let go now and then.
        if (++_added % 500 == 0) Bound.RemoveAll(b => !b.Target.TryGetTarget(out _));
        Bound.Add((new WeakReference<DependencyObject>(target), property, value));
        return target;
    }

    /// <summary>A TextBlock's text, bound.</summary>
    public static TextBlock Text(TextBlock block, Func<string> text) => Bind(block, TextBlock.TextProperty, text);

    /// <summary>A Content (button, list item, check box), bound.</summary>
    public static T Content<T>(T control, Func<object?> content) where T : ContentControl => Bind(control, ContentControl.ContentProperty, content);

    /// <summary>A tooltip, bound.</summary>
    public static T Tip<T>(T element, Func<object?> tip) where T : FrameworkElement => Bind(element, FrameworkElement.ToolTipProperty, tip);

    /// <summary>A list whose items say themselves (ToString): their text is redrawn, the items and the choice stay.</summary>
    public static T Items<T>(T list) where T : ItemsControl
    {
        if (Lists.Any(l => l.TryGetTarget(out var c) && ReferenceEquals(c, list))) return list;
        if (Lists.Count % 100 == 99) Lists.RemoveAll(l => !l.TryGetTarget(out _));
        Lists.Add(new WeakReference<ItemsControl>(list));
        return list;
    }

    /// <summary>The language changed: every bound word said again, in place.</summary>
    public static void Refresh()
    {
        Refreshing = true;
        try
        {
            Bound.RemoveAll(b => !b.Target.TryGetTarget(out _));
            foreach (var (target, property, value) in Bound.ToList())
                if (target.TryGetTarget(out var element)) element.SetValue(property, value());

            Lists.RemoveAll(l => !l.TryGetTarget(out _));
            foreach (var list in Lists.ToList())
            {
                if (!list.TryGetTarget(out var control)) continue;
                control.Items.Refresh();
                // A closed combo box shows a copy of its choice taken when it was chosen:
                // chosen again, the same item, so the copy is in the new language too.
                if (control is System.Windows.Controls.ComboBox { SelectedIndex: >= 0 } combo)
                {
                    var index = combo.SelectedIndex;
                    combo.SelectedIndex = -1;
                    combo.SelectedIndex = index;
                }
            }
        }
        finally
        {
            Refreshing = false;
        }
    }

    /// <summary>For the log: how many words and lists are bound now.</summary>
    public static string Count => $"{Bound.Count} words, {Lists.Count} lists";
}
