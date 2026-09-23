using Nextcalibur.Core.Configuration;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// A file changed outside the application must not change the application:
/// the store notices, reports, and the last good copy stands. Run in a
/// folder of the tests' own, without the ownership changes an unelevated
/// run cannot make - the permissions are checked on the machine, not here.
/// </summary>
// With the log's tests: a report here is a line in the log they count.
[Collection("log")]
public class ProtectedStoreTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "nc-store-" + Guid.NewGuid().ToString("N"));
    private readonly List<string> _reported = new();

    public ProtectedStoreTests()
    {
        ProtectedStore.UseForTests(_root);
        ProtectedStore.Tampered += OnTampered;
    }

    private void OnTampered(string name) => _reported.Add(name);

    public void Dispose()
    {
        ProtectedStore.Tampered -= OnTampered;
        try { Directory.Delete(_root, recursive: true); } catch (IOException) { }
    }

    private static string PathOf(string name) => Path.Combine(ProtectedStore.Folder, name);

    [Fact]
    public void What_was_written_is_read_back()
    {
        Assert.True(ProtectedStore.Write("a.json", "{\"x\":1}"));
        Assert.Equal("{\"x\":1}", ProtectedStore.Read("a.json"));
        Assert.Empty(_reported);
    }

    [Fact]
    public void An_edited_file_is_reported_and_the_last_good_copy_stands()
    {
        ProtectedStore.Write("a.json", "{\"x\":1}");
        File.WriteAllText(PathOf("a.json"), "{\"x\":999}");

        Assert.Equal("{\"x\":1}", ProtectedStore.Read("a.json"));
        Assert.Contains("a.json", _reported);
        Assert.Equal("{\"x\":1}", File.ReadAllText(PathOf("a.json")));   // put back
    }

    [Fact]
    public void A_deleted_file_is_put_back()
    {
        ProtectedStore.Write("a.json", "{\"x\":1}");
        File.Delete(PathOf("a.json"));

        Assert.Equal("{\"x\":1}", ProtectedStore.Read("a.json"));
        Assert.True(File.Exists(PathOf("a.json")));
    }

    [Fact]
    public void Editing_the_file_and_its_good_copy_together_yields_nothing_rather_than_the_edit()
    {
        ProtectedStore.Write("a.json", "{\"x\":1}");
        File.WriteAllText(PathOf("a.json"), "{\"x\":999}");
        File.WriteAllText(PathOf("a.json.good"), "{\"x\":999}");

        Assert.Null(ProtectedStore.Read("a.json"));
        Assert.True(ProtectedStore.IsRecorded("a.json"));   // so callers do not fall back elsewhere
    }

    [Fact]
    public void A_file_the_application_never_wrote_is_not_read()
    {
        Directory.CreateDirectory(ProtectedStore.Folder);
        File.WriteAllText(PathOf("planted.json"), "{}");

        Assert.Null(ProtectedStore.Read("planted.json"));
        Assert.Contains("planted.json", _reported);
        Assert.False(File.Exists(PathOf("planted.json")));
    }

    [Fact]
    public void The_timer_puts_back_a_file_changed_while_running()
    {
        ProtectedStore.Write("a.json", "{\"x\":1}");
        File.WriteAllText(PathOf("a.json"), "{\"x\":999}");

        Assert.Contains("a.json", ProtectedStore.Verify());
        Assert.Equal("{\"x\":1}", File.ReadAllText(PathOf("a.json")));
        Assert.Empty(ProtectedStore.Verify());   // and then it is quiet
    }

    [Fact]
    public void Rewriting_the_record_is_noticed_by_the_timer()
    {
        ProtectedStore.Write("a.json", "{\"x\":1}");
        File.WriteAllText(PathOf("a.json"), "{\"x\":999}");
        File.WriteAllText(PathOf("integrity.json"), $$"""{"a.json":"{{ProtectedStore.Hash("{\"x\":999}")}}"}""");

        Assert.Contains("a.json", ProtectedStore.Verify());
        Assert.Equal("{\"x\":1}", File.ReadAllText(PathOf("a.json")));
    }

    [Fact]
    public void Settings_changed_outside_do_not_reach_the_application()
    {
        new AppSettings { CpuWarningTemperatureC = 90 }.Save();
        var text = File.ReadAllText(PathOf(AppSettings.FileName)).Replace("\"CpuWarningTemperatureC\": 90", "\"CpuWarningTemperatureC\": 99");
        File.WriteAllText(PathOf(AppSettings.FileName), text);

        Assert.Equal(90, AppSettings.Load().CpuWarningTemperatureC);
        Assert.Contains(AppSettings.FileName, _reported);
    }
}
