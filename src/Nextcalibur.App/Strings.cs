using System.Globalization;
using System.Windows;
using Nextcalibur.Core;
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
    // The chosen language's dictionary, loaded directly: it answers before
    // the Application exists (the uninstall hook, the first-run repair) and
    // whenever the application's resources are not the place to look.
    private static ResourceDictionary? _active;

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
        if (Current != language || _active is null)
        {
            try { _active = Load(wanted); }
            catch (Exception ex) when (Application.Current is null)
            {
                // Before the Application exists the pack scheme can refuse;
                // English from the code's own fallbacks is better than no start.
                Log.Warn("language", "The dictionary could not be loaded this early: " + ex.Message);
                _active = null;
            }
        }
        Current = language;
        if (Application.Current is null) return;   // before the window: Get still answers from _active

        var merged = Application.Current.Resources.MergedDictionaries;
        var index = -1;
        for (var i = 0; i < merged.Count; i++)
        {
            var source = merged[i].Source?.OriginalString;
            if (source is null || !source.Contains("Strings.", StringComparison.OrdinalIgnoreCase)) continue;
            if (source.EndsWith(wanted, StringComparison.OrdinalIgnoreCase)) return;
            index = i;
        }

        var dictionary = Load(wanted);
        if (index >= 0) merged[index] = dictionary; else merged.Add(dictionary);
        Changed?.Invoke(null, EventArgs.Empty);
    }

    // An absolute pack URI: the relative form needs an Application to resolve
    // against, and the uninstall hook runs before there is one.
    private static ResourceDictionary Load(string source)
    {
        // The pack scheme is registered by the Application's constructor;
        // before that, touching PackUriHelper registers it.
        _ = System.IO.Packaging.PackUriHelper.UriSchemePack;
        return new ResourceDictionary { Source = new Uri("pack://application:,,,/" + source, UriKind.Absolute) };
    }

    /// <summary>The text for a key in the current language, with <see cref="string.Format(string, object[])"/> arguments.</summary>
    public static string Get(string key, params object[] args)
    {
        var text = Lookup(key);
        if (args.Length == 0) return text;
        try { return string.Format(CultureInfo.CurrentCulture, text, args); }
        catch (FormatException) { return text; }
    }

    /// <summary>The text for a key, or null when neither dictionary has it.</summary>
    public static string? TryGet(string key)
    {
        if (_active?[key] is string active) return active;
        if (Application.Current?.TryFindResource(key) is string found) return found;

        _english ??= Load(EnglishSource);
        return _english[key] as string;
    }

    private static string Lookup(string key) => TryGet(key) ?? "[" + key + "]";

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
