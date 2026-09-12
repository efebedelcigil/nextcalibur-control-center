using System.Globalization;
using System.Windows;
using Nextcalibur.Core.Configuration;

namespace Nextcalibur.App;

/// <summary>
/// Every word the application shows, in the chosen language, switched
/// live.
///
/// Two dictionaries, <c>Themes/Strings.en.xaml</c> and
/// <c>Themes/Strings.tr.xaml</c>, hold the same keys. One of them is merged
/// into the application's resources; the markup reads its texts as
/// <c>{DynamicResource}</c> and follows a swap by itself, and the
/// code-behind reads through <see cref="Get"/>. Swapping is the palette's
/// trick: replace the merged dictionary in place. Whatever the code-behind
/// has already put on screen - the tray menu, a subtitle, the tour's card -
/// is re-rendered by its owner on <see cref="Changed"/>.
///
/// A key missing from the chosen language falls back to English; a key
/// missing from both comes back as the key itself between brackets, so a
/// forgotten word is visible rather than blank. Nothing here is invented.
/// </summary>
public static class Strings
{
    private const string EnglishSource = "Themes/Strings.en.xaml";
    private const string TurkishSource = "Themes/Strings.tr.xaml";

    private static ResourceDictionary? _english;

    /// <summary>The language on screen right now.</summary>
    public static UiLanguage Current { get; private set; } = UiLanguage.English;

    /// <summary>Raised after a swap, on the interface thread, for owners of text set from code.</summary>
    public static event EventHandler? Changed;

    /// <summary>
    /// Merges the dictionary for the language, replacing the one there. Safe
    /// to call with the language already applied: nothing happens.
    /// </summary>
    public static void Apply(UiLanguage language)
    {
        var wanted = language == UiLanguage.Turkish ? TurkishSource : EnglishSource;
        var merged = Application.Current.Resources.MergedDictionaries;
        var index = -1;
        for (var i = 0; i < merged.Count; i++)
        {
            var source = merged[i].Source?.OriginalString;
            if (source is null || !source.Contains("Strings.", StringComparison.OrdinalIgnoreCase)) continue;
            if (source.EndsWith(wanted, StringComparison.OrdinalIgnoreCase)) { Current = language; return; }
            index = i;
        }

        var dictionary = new ResourceDictionary { Source = new Uri(wanted, UriKind.Relative) };
        if (index >= 0) merged[index] = dictionary; else merged.Add(dictionary);
        Current = language;
        Changed?.Invoke(null, EventArgs.Empty);
    }

    /// <summary>The text for a key in the current language, with <see cref="string.Format(string, object[])"/> arguments.</summary>
    public static string Get(string key, params object[] args)
    {
        var text = Lookup(key);
        if (args.Length == 0) return text;
        try { return string.Format(CultureInfo.CurrentCulture, text, args); }
        catch (FormatException) { return text; }
    }

    private static string Lookup(string key)
    {
        if (Application.Current?.TryFindResource(key) is string found) return found;

        _english ??= new ResourceDictionary { Source = new Uri(EnglishSource, UriKind.Relative) };
        if (_english[key] is string english) return english;

        return "[" + key + "]";
    }

    /// <summary>
    /// The language to start in: the one chosen, or - the first time - the
    /// one Windows is set to, Turkish for a Turkish Windows and English
    /// for every other.
    /// </summary>
    public static UiLanguage Initial(AppSettings settings)
    {
        if (settings.Language is { } chosen) return chosen;
        return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("tr", StringComparison.OrdinalIgnoreCase)
            ? UiLanguage.Turkish
            : UiLanguage.English;
    }
}
