using System.Text.RegularExpressions;
using Nextcalibur.Core.Configuration;
using Xunit;

namespace Nextcalibur.Core.Tests;

[Collection("log")]
public class CrashGuardTests : IDisposable
{
    private readonly string _folder = Path.Combine(Path.GetTempPath(), "nc-guard-" + Guid.NewGuid().ToString("N"));
    private string Marker => Path.Combine(_folder, "session.json");

    public CrashGuardTests()
    {
        Directory.CreateDirectory(_folder);
        Log.WriteTo(Path.Combine(_folder, "logs"));
        CrashGuard.UseMarker(Marker);
    }

    public void Dispose()
    {
        try { Directory.Delete(_folder, recursive: true); } catch (IOException) { }
        GC.SuppressFinalize(this);
    }

    private static readonly DateTimeOffset T0 = new(2026, 10, 14, 12, 0, 0, TimeSpan.FromHours(3));

    [Fact]
    public void A_clean_exit_leaves_nothing_and_the_next_start_is_normal()
    {
        CrashGuard.Begin("1", T0);
        Assert.True(File.Exists(Marker));
        CrashGuard.End();
        Assert.False(File.Exists(Marker));

        CrashGuard.UseMarker(Marker);
        CrashGuard.Begin("1", T0.AddMinutes(1));
        Assert.Null(CrashGuard.UncleanPrevious);
        Assert.False(CrashGuard.SafeMode);
    }

    [Fact]
    public void One_crash_is_reported_but_the_start_is_normal()
    {
        CrashGuard.Begin("1", T0);
        CrashGuard.RecordFault(new InvalidOperationException("boom"));
        // the process dies: no End()
        CrashGuard.UseMarker(Marker);
        CrashGuard.Begin("1", T0.AddMinutes(1));
        Assert.NotNull(CrashGuard.UncleanPrevious);
        Assert.Contains("boom", CrashGuard.UncleanPrevious!.LastFault);
        Assert.False(CrashGuard.SafeMode);
    }

    [Fact]
    public void Two_early_crashes_in_a_row_make_a_safe_start()
    {
        CrashGuard.Begin("1", T0);                      // run 1 dies at once
        CrashGuard.UseMarker(Marker);
        CrashGuard.Begin("1", T0.AddMinutes(1));        // run 2 sees one early end, dies at once
        Assert.False(CrashGuard.SafeMode);
        CrashGuard.UseMarker(Marker);
        CrashGuard.Begin("1", T0.AddMinutes(2));        // run 3 sees two
        Assert.True(CrashGuard.SafeMode);
    }

    [Fact]
    public void A_run_that_lived_past_the_early_window_clears_the_history()
    {
        CrashGuard.Begin("1", T0);
        CrashGuard.UseMarker(Marker);
        CrashGuard.Begin("1", T0.AddMinutes(1));
        CrashGuard.Heartbeat(T0.AddMinutes(30));        // run 2 was fine for half an hour, then was killed
        CrashGuard.UseMarker(Marker);
        CrashGuard.Begin("1", T0.AddMinutes(31));
        Assert.NotNull(CrashGuard.UncleanPrevious);
        Assert.False(CrashGuard.SafeMode);
    }

    [Fact]
    public void A_damaged_marker_is_not_a_crash()
    {
        File.WriteAllText(Marker, "{ not json");
        CrashGuard.UseMarker(Marker);
        CrashGuard.Begin("1", T0);
        Assert.Null(CrashGuard.UncleanPrevious);
    }

    [Fact]
    public void Lines_follow_the_standard_shape_and_errors_carry_their_causes()
    {
        Log.Error("test", "outer failed", new InvalidOperationException("outer", new IOException("inner cause")));
        Log.Info("test", "fields", ("mode", "Gaming"), ("value", 4.5), ("path", @"C:\a b"));
        var file = Path.Combine(_folder, "logs", $"nextcalibur-{DateTime.Now:yyyyMMdd}.log");
        var lines = File.ReadAllLines(file);
        var shape = new Regex(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{3}[+-]\d{2}:\d{2} (DEBUG|INFO |WARN |ERROR|FATAL) \[\d+:\s*\d+\] [a-z-]+: ");
        Assert.All(lines, l => Assert.Matches(shape, l));
        Assert.Contains(lines, l => l.Contains("System.InvalidOperationException (0x") && l.Contains("outer"));
        Assert.Contains(lines, l => l.Contains("caused by System.IO.IOException") && l.Contains("inner cause"));
        Assert.Contains(lines, l => l.EndsWith("fields mode=Gaming value=4.5 path=\"C:\\a b\""));
    }
}
