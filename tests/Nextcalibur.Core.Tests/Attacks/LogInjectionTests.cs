using System.Text.RegularExpressions;
using Nextcalibur.Core.Configuration;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// The log is what a person attaches to a bug report and what this project
/// reads to decide what happened on a machine. Things from outside are
/// written into it - an exception's text, a server's message, a path - and
/// a newline in one of them would let it write entries of its own: any
/// timestamp, any level, any wording.
///
/// These write into a folder of their own, never the machine's own log.
/// </summary>
[Collection("log")]
public class LogInjectionTests : IDisposable
{
    private readonly string _folder = Path.Combine(Path.GetTempPath(), "nc-log-" + Guid.NewGuid().ToString("N"));

    public LogInjectionTests()
    {
        Log.WriteTo(_folder);
        Log.Start("Nextcalibur.Tests", "0");
    }

    public void Dispose()
    {
        try { Directory.Delete(_folder, recursive: true); } catch (IOException) { }
        GC.SuppressFinalize(this);
    }

    private static readonly Regex Entry = new(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3} (INFO |WARN |ERROR) \[\s*\d+\] ");

    private string[] LinesWrittenBy(Action write)
    {
        var file = Path.Combine(_folder, $"nextcalibur-{DateTime.Now:yyyyMMdd}.log");
        var before = File.Exists(file) ? File.ReadAllLines(file).Length : 0;
        write();
        return File.ReadAllLines(file).Skip(before).ToArray();
    }

    /// <summary>
    /// The attack: a message that ends the line and starts one of its own,
    /// so that the log says the application started elevated at a time it
    /// did not.
    /// </summary>
    [Fact]
    public void A_message_cannot_write_an_entry_of_its_own()
    {
        var forged = "download failed\r\n2026-01-01 00:00:00.000 INFO  [  1] start: Nextcalibur 9.9.9; elevated True";
        var written = LinesWrittenBy(() => Log.Warn("update", forged));

        // One entry, and it is the one this application wrote: the forged
        // text is still there to read, on that line, where it says nothing
        // about what happened.
        Assert.Single(written);
        Assert.Equal("WARN", Entry.Match(written[0]).Groups[1].Value.Trim());
        Assert.Contains("update: download failed", written[0]);
        Assert.StartsWith(DateTime.Now.ToString("yyyy-MM-dd"), written[0]);
    }

    [Theory]
    [InlineData("a\nb")]
    [InlineData("a\rb")]
    [InlineData("a\r\nb")]
    [InlineData("a\u0085b")]
    [InlineData("a\u2028b")]
    [InlineData("a\tb")]
    [InlineData("a\0b")]
    [InlineData("a\u001b[2Jb")]
    public void No_control_character_survives_into_the_file(string message)
    {
        var written = LinesWrittenBy(() => Log.Info("test", message));

        Assert.Single(written);
        foreach (var c in written[0])
            Assert.False(char.IsControl(c), $"a control character (U+{(int)c:X4}) reached the log");
    }

    [Fact]
    public void A_category_cannot_forge_a_line_either()
    {
        var written = LinesWrittenBy(() => Log.Info("test\r\n2026-01-01 00:00:00.000 ERROR [  1] x", "message"));
        Assert.Single(written);
    }

    [Fact]
    public void An_endless_message_is_cut()
    {
        var written = LinesWrittenBy(() => Log.Info("test", new string('x', 100_000)));

        Assert.Single(written);
        Assert.True(written[0].Length < 5000, $"the line was {written[0].Length} characters");
    }

    /// <summary>
    /// A stack trace is the one thing worth several lines, so it is written
    /// as several entries rather than as one line with newlines in it: every
    /// line in the file still begins with a timestamp and a level.
    /// </summary>
    [Fact]
    public void An_exceptions_stack_is_written_as_entries_of_its_own()
    {
        Exception caught;
        try { throw new InvalidOperationException("no\r\nnewlines\r\nhere"); }
        catch (Exception ex) { caught = ex; }

        var written = LinesWrittenBy(() => Log.Error("test", "while testing", caught));

        Assert.NotEmpty(written);
        foreach (var line in written)
        {
            Assert.Matches(Entry, line);
            foreach (var c in line) Assert.False(char.IsControl(c));
        }
    }

    /// <summary>The ordinary case still reads as it always did.</summary>
    [Fact]
    public void An_ordinary_message_is_written_whole()
    {
        var written = LinesWrittenBy(() => Log.Info("start", "Nextcalibur 0.5.4; pid 1; elevated True"));

        Assert.Single(written);
        Assert.Matches(Entry, written[0]);
        Assert.EndsWith("start: Nextcalibur 0.5.4; pid 1; elevated True", written[0]);
    }
}
