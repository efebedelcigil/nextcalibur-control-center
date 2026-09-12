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
}
