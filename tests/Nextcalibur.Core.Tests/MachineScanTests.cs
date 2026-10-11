using System.Text.Json;
using Nextcalibur.Core.Hardware;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The machine's verdict is kept, and a change between two scans is kept
/// with it. Whatever this machine's own verdict is - the test does not care -
/// it is the change that is checked.
/// </summary>
[Collection("log")]
public class MachineScanTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "nc-scan-" + Guid.NewGuid().ToString("N"));

    public MachineScanTests() => ProtectedStore.UseForTests(_root);

    public void Dispose()
    {
        try { Directory.Delete(_root, recursive: true); } catch (IOException) { }
    }

    [Fact]
    public void A_scan_is_kept_and_a_change_of_verdict_is_remembered()
    {
        var first = MachineScan.RunAndRecord("test", "0.0.0");
        var kept = MachineScan.Load();
        Assert.NotNull(kept);
        Assert.Equal(first.Level, kept!.Level);
        Assert.Equal("test", kept.Trigger);
        Assert.Null(kept.PreviousLevel);

        // As if the last scan had found something else.
        var other = first.Level == SupportLevel.Supported ? SupportLevel.Unsupported : SupportLevel.Supported;
        kept.Level = other;
        File.WriteAllText(MachineScan.RecordPath, JsonSerializer.Serialize(kept));

        MachineScan.RunAndRecord("test", "0.0.0");
        var after = MachineScan.Load()!;
        Assert.Equal(first.Level, after.Level);
        Assert.Equal(other, after.PreviousLevel);
        Assert.NotNull(after.LevelChangedAtUtc);
    }

    [Fact]
    public void A_damaged_record_is_no_record()
    {
        Directory.CreateDirectory(ProtectedStore.Root);
        File.WriteAllText(MachineScan.RecordPath, "{ not json");
        Assert.Null(MachineScan.Load());
    }
}
