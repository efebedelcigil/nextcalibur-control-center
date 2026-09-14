using System.Xml.Linq;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The two language dictionaries must carry the same keys, key for key:
/// a word missing from one language shows up on screen as its key in
/// brackets. Read as XML from the source tree, since the test project has
/// no WPF.
/// </summary>
public class StringsDictionaryTests
{
    private static readonly XNamespace X = "http://schemas.microsoft.com/winfx/2006/xaml";

    private static string ThemesFolder()
    {
        var folder = AppContext.BaseDirectory;
        for (var i = 0; i < 8 && folder is not null; i++)
        {
            var candidate = Path.Combine(folder, "src", "Nextcalibur.App", "Themes");
            if (Directory.Exists(candidate)) return candidate;
            folder = Path.GetDirectoryName(folder);
        }
        throw new DirectoryNotFoundException("The Themes folder was not found above the test output.");
    }

    /// <summary>
    /// A key defined twice does not fail the build - the compiler is happy,
    /// and the window dies at load with a XamlParseException that names a
    /// line number in a generated file. It happened on 14 September 2026:
    /// a new S.Fault.Title collided with the crash dialogue's, and the
    /// application would not start at all. The dictionaries are read here
    /// as text for exactly that reason.
    /// </summary>
    [Theory]
    [InlineData("Strings.en.xaml")]
    [InlineData("Strings.tr.xaml")]
    public void No_key_is_defined_twice(string file)
    {
        var doc = XDocument.Load(Path.Combine(ThemesFolder(), file));
        var duplicates = doc.Root!.Elements()
            .Select(e => e.Attribute(X + "Key")?.Value)
            .Where(key => key is not null)
            .GroupBy(key => key!, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        Assert.True(duplicates.Count == 0, $"{file} defines these more than once: {string.Join(", ", duplicates)}");
    }

    private static Dictionary<string, string> Keys(string file)
    {
        var doc = XDocument.Load(Path.Combine(ThemesFolder(), file));
        return doc.Root!.Elements()
            .Where(e => e.Attribute(X + "Key") is not null)
            .ToDictionary(e => e.Attribute(X + "Key")!.Value, e => e.Value);
    }

    [Fact]
    public void English_and_Turkish_have_the_same_keys()
    {
        var en = Keys("Strings.en.xaml");
        var tr = Keys("Strings.tr.xaml");

        var missingInTurkish = en.Keys.Except(tr.Keys).OrderBy(k => k).ToList();
        var missingInEnglish = tr.Keys.Except(en.Keys).OrderBy(k => k).ToList();

        Assert.True(missingInTurkish.Count == 0, "Missing in Strings.tr.xaml: " + string.Join(", ", missingInTurkish));
        Assert.True(missingInEnglish.Count == 0, "Missing in Strings.en.xaml: " + string.Join(", ", missingInEnglish));
    }

    [Fact]
    public void Every_placeholder_appears_in_both_languages()
    {
        var en = Keys("Strings.en.xaml");
        var tr = Keys("Strings.tr.xaml");

        foreach (var (key, english) in en)
        {
            if (!tr.TryGetValue(key, out var turkish)) continue;
            for (var i = 0; i < 4; i++)
            {
                var placeholder = "{" + i + "}";
                Assert.True(english.Contains(placeholder) == turkish.Contains(placeholder),
                    $"{key}: placeholder {placeholder} is in one language and not the other");
            }
        }
    }

    [Fact]
    public void No_text_is_empty()
    {
        foreach (var file in new[] { "Strings.en.xaml", "Strings.tr.xaml" })
            foreach (var (key, text) in Keys(file))
                Assert.False(string.IsNullOrWhiteSpace(text), $"{file}: {key} is empty");
    }

    [Fact]
    public void Heat_while_away_formats_correctly()
    {
        var en = Keys("Strings.en.xaml");
        var tr = Keys("Strings.tr.xaml");

        var enText = string.Format(en["S.Heat.WhileAway"], 97, en["S.Heat.ChipCpu"]);
        Assert.Equal("While you were away the processor reached 97 °C", enText);

        var trText = string.Format(tr["S.Heat.WhileAway"], 97, tr["S.Heat.ChipCpu"]);
        Assert.Equal("Siz yokken işlemci 97 °C'ye ulaştı", trText);

        var enGpu = string.Format(en["S.Heat.WhileAway"], 88, en["S.Heat.ChipGpu"]);
        Assert.Equal("While you were away the graphics card reached 88 °C", enGpu);

        var trGpu = string.Format(tr["S.Heat.WhileAway"], 88, tr["S.Heat.ChipGpu"]);
        Assert.Equal("Siz yokken ekran kartı 88 °C'ye ulaştı", trGpu);
    }
}
