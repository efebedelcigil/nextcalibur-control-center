using System.Text.RegularExpressions;
using System.Xml.Linq;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The keys the code asks for must exist, and the English the library
/// writes beside a key must be the English in the dictionary: a key that
/// is misspelt in one place shows up on screen as the key in brackets, and
/// an English text edited in one place and not the other drifts. Both are
/// read from the source tree, the way <see cref="StringsDictionaryTests"/>
/// reads the dictionaries.
/// </summary>
public class WordsInCodeTests
{
    private static readonly XNamespace X = "http://schemas.microsoft.com/winfx/2006/xaml";

    // Strings.Get("S.Key" ...) in the application; Words.Get("S.Core.Key", "English" ...) in the library.
    private static readonly Regex AppKey = new(@"Strings\.Get\(\s*""(S\.[A-Za-z0-9.]+)""", RegexOptions.Compiled);
    private static readonly Regex CoreKey = new(@"Words\.Get\(\s*""(S\.Core\.[A-Za-z0-9.]+)"",\s*((?:""(?:[^""\\]|\\.)*""\s*\+?\s*)+)", RegexOptions.Compiled);
    private static readonly Regex Literal = new(@"""((?:[^""\\]|\\.)*)""", RegexOptions.Compiled);

    private static string Source()
    {
        var folder = AppContext.BaseDirectory;
        for (var i = 0; i < 8 && folder is not null; i++)
        {
            var candidate = Path.Combine(folder, "src");
            if (Directory.Exists(Path.Combine(candidate, "Nextcalibur.App", "Themes"))) return candidate;
            folder = Path.GetDirectoryName(folder);
        }
        throw new DirectoryNotFoundException("The source tree was not found above the test output.");
    }

    private static Dictionary<string, string> Dictionary(string file)
    {
        var doc = XDocument.Load(Path.Combine(Source(), "Nextcalibur.App", "Themes", file));
        return doc.Root!.Elements()
            .Where(e => e.Attribute(X + "Key") is not null)
            .ToDictionary(e => e.Attribute(X + "Key")!.Value, e => e.Value);
    }

    private static IEnumerable<string> CSharpFiles(string project) =>
        Directory.EnumerateFiles(Path.Combine(Source(), project), "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar)
                     && !f.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar));

    /// <summary>The C# literal(s) joined by + as the string they make.</summary>
    private static string Unescape(string literals) =>
        string.Concat(Literal.Matches(literals).Select(m => Regex.Unescape(m.Groups[1].Value)));

    [Fact]
    public void Every_key_the_application_asks_for_exists()
    {
        var en = Dictionary("Strings.en.xaml");
        var missing = new List<string>();
        foreach (var file in CSharpFiles("Nextcalibur.App"))
            foreach (Match m in AppKey.Matches(File.ReadAllText(file)))
                if (!en.ContainsKey(m.Groups[1].Value))
                    missing.Add($"{Path.GetFileName(file)}: {m.Groups[1].Value}");

        Assert.True(missing.Count == 0, "Keys asked for but not in Strings.en.xaml: " + string.Join(", ", missing));
    }

    [Fact]
    public void Every_word_the_library_says_is_in_both_dictionaries_and_the_English_matches()
    {
        var en = Dictionary("Strings.en.xaml");
        var tr = Dictionary("Strings.tr.xaml");
        var problems = new List<string>();
        var seen = 0;
        foreach (var file in CSharpFiles("Nextcalibur.Core"))
            foreach (Match m in CoreKey.Matches(File.ReadAllText(file)))
            {
                seen++;
                var key = m.Groups[1].Value;
                var english = Unescape(m.Groups[2].Value);
                if (!en.TryGetValue(key, out var inDictionary)) problems.Add($"{key}: not in Strings.en.xaml");
                else if (inDictionary != english) problems.Add($"{key}: the code says \"{english}\" and Strings.en.xaml says \"{inDictionary}\"");
                if (!tr.ContainsKey(key)) problems.Add($"{key}: not in Strings.tr.xaml");
            }

        Assert.True(seen > 50, "The library's Words.Get calls were not found; the pattern needs looking at.");
        Assert.True(problems.Count == 0, string.Join(Environment.NewLine, problems));
    }
}
