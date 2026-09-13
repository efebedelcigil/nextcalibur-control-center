using System.Globalization;

namespace Nextcalibur.Core;

/// <summary>
/// The words this library says to a person - a verdict's reasons, a switch's
/// summary, a dependency's name - in the language the application has
/// chosen.
///
/// The library has no dictionaries of its own. Every text is written here
/// in English, keyed, and the application installs a <see cref="Resolver"/>
/// that answers from its dictionaries (<c>Strings.en.xaml</c> and
/// <c>Strings.tr.xaml</c>, which carry every key used here; a test holds
/// the English in the dictionary to the English written here). Without a
/// resolver - the tests, a tool - the English is what comes back.
/// </summary>
public static class Words
{
    /// <summary>Answers a key with the text in the chosen language, or null when it has none.</summary>
    public static Func<string, string?>? Resolver { get; set; }

    /// <summary>The text for a key: the resolver's answer, or the English written at the call.</summary>
    public static string Get(string key, string english, params object[] args)
    {
        var text = Resolver?.Invoke(key) ?? english;
        if (args.Length == 0) return text;
        try { return string.Format(CultureInfo.CurrentCulture, text, args); }
        catch (FormatException) { return text; }
    }
}
