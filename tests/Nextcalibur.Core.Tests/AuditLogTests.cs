using System.Text.Json;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>The audit file keeps every entry, in order, as JSON, and a copy changed from outside does not stand.</summary>
[Collection("log")]
public class AuditLogTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "nc-audit-" + Guid.NewGuid().ToString("N"));

    public AuditLogTests() => ProtectedStore.UseForTests(_root);

    public void Dispose()
    {
        try { Directory.Delete(_root, recursive: true); } catch (IOException) { }
    }

    [Fact]
    public void Entries_are_kept_in_order_with_their_time_and_details()
    {
        AuditLog.Record("asked", new Dictionary<string, object?> { ["change"] = "ndu", ["answer"] = "yes", ["restorePointTicked"] = true });
        AuditLog.Record("restore-point", new Dictionary<string, object?> { ["change"] = "ndu", ["created"] = true, ["sequence"] = 42L });

        var entries = AuditLog.Entries();
        Assert.Equal(2, entries.Count);

        using var first = JsonDocument.Parse(entries[0]);
        Assert.Equal("asked", first.RootElement.GetProperty("action").GetString());
        Assert.True(first.RootElement.GetProperty("restorePointTicked").GetBoolean());
        Assert.True(DateTimeOffset.TryParse(first.RootElement.GetProperty("at").GetString(), out _));
        Assert.EndsWith("Z", first.RootElement.GetProperty("utc").GetString());

        using var second = JsonDocument.Parse(entries[1]);
        Assert.Equal(42, second.RootElement.GetProperty("sequence").GetInt64());
    }

    [Fact]
    public void A_file_rewritten_from_outside_is_replaced_by_the_last_good_copy()
    {
        AuditLog.Record("asked", new Dictionary<string, object?> { ["change"] = "gpu-firmware-mode", ["answer"] = "yes" });
        File.WriteAllText(Path.Combine(ProtectedStore.Folder, AuditLog.FileName), "{\"action\":\"nothing happened\"}\n");

        var entries = AuditLog.Entries();
        Assert.Single(entries);
        Assert.Contains("gpu-firmware-mode", entries[0]);
    }
}
