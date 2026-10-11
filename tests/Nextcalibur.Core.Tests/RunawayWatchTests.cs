using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class RunawayWatchTests
{
    private static readonly DateTime T0 = new(2026, 10, 11, 12, 0, 0, DateTimeKind.Utc);
    private static readonly HashSet<string> None = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Feeds one process using a given number of cores each minute; returns what was reported.</summary>
    private static List<Runaway> Feed(RunawayWatch w, double[] coresPerMinute, int pid = 100, string name = "loop",
        int foreground = 1, ISet<string>? skip = null)
    {
        var found = new List<Runaway>();
        double cpu = 0;
        found.AddRange(w.SampleCpu(Snap(pid, name, cpu), T0, foreground, skip ?? None));
        for (var i = 0; i < coresPerMinute.Length; i++)
        {
            cpu += coresPerMinute[i] * 60_000;
            found.AddRange(w.SampleCpu(Snap(pid, name, cpu), T0.AddMinutes(i + 1), foreground, skip ?? None));
        }
        return found;
    }

    private static Dictionary<(int, long), (string, double)> Snap(int pid, string name, double cpu) =>
        new() { [(pid, 1L)] = (name, cpu) };

    [Fact]
    public void A_flat_loop_on_one_core_is_named_after_fifteen_minutes()
    {
        var found = Feed(new RunawayWatch(), Enumerable.Repeat(1.0, 16).ToArray());
        var one = Assert.Single(found);
        Assert.Equal("loop", one.Name);
        Assert.InRange(one.Cores, 0.99, 1.01);
        Assert.False(one.OnGpu);
    }

    [Fact]
    public void Fourteen_minutes_is_not_enough() =>
        Assert.Empty(Feed(new RunawayWatch(), Enumerable.Repeat(1.0, 14).ToArray()));

    [Fact]
    public void Work_that_rises_and_falls_is_not_a_loop() =>
        Assert.Empty(Feed(new RunawayWatch(), [4, 1, 6, 1.2, 5, 1, 7, 1, 3, 1, 6, 1, 4, 1, 5, 1, 6, 1, 4, 1]));

    [Fact]
    public void A_quiet_minute_breaks_the_run() =>
        Assert.Empty(Feed(new RunawayWatch(), [1, 1, 1, 1, 1, 1, 1, 1, 0.2, 1, 1, 1, 1, 1, 1, 1, 1]));

    [Fact]
    public void The_program_in_front_is_left_alone() =>
        Assert.Empty(Feed(new RunawayWatch(), Enumerable.Repeat(1.0, 20).ToArray(), pid: 100, foreground: 100));

    [Fact]
    public void Programs_handled_elsewhere_are_left_to_it() =>
        Assert.Empty(Feed(new RunawayWatch(), Enumerable.Repeat(1.0, 20).ToArray(), name: "TextInputHost",
            skip: new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "textinputhost" }));

    [Fact]
    public void Named_once_not_every_minute() =>
        Assert.Single(Feed(new RunawayWatch(), Enumerable.Repeat(2.0, 60).ToArray()));

    [Fact]
    public void Sleep_does_not_count()
    {
        var w = new RunawayWatch();
        double cpu = 0;
        w.SampleCpu(Snap(100, "loop", cpu), T0, 1, None);
        for (var i = 1; i <= 10; i++) { cpu += 60_000; w.SampleCpu(Snap(100, "loop", cpu), T0.AddMinutes(i), 1, None); }
        // An hour asleep, then five more minutes: ten plus five is not fifteen unbroken.
        cpu += 60_000;
        Assert.Empty(w.SampleCpu(Snap(100, "loop", cpu), T0.AddMinutes(70), 1, None));
        for (var i = 71; i <= 75; i++) { cpu += 60_000; Assert.Empty(w.SampleCpu(Snap(100, "loop", cpu), T0.AddMinutes(i), 1, None)); }
    }

    [Fact]
    public void A_busy_card_with_nothing_in_front_is_named()
    {
        var w = new RunawayWatch();
        var found = new List<Runaway>();
        for (var i = 0; i <= 16; i++)
            found.AddRange(w.SampleGpu(70, fullScreen: false, "explorer", T0.AddMinutes(i), () => ["wallpaper64"]));
        var one = Assert.Single(found);
        Assert.Equal("wallpaper64", one.Name);
        Assert.True(one.OnGpu);
    }

    [Fact]
    public void A_card_busy_for_the_program_in_front_or_a_game_is_working()
    {
        var w = new RunawayWatch();
        for (var i = 0; i <= 30; i++)
        {
            Assert.Empty(w.SampleGpu(90, fullScreen: false, "blender", T0.AddMinutes(i), () => ["blender"]));
            Assert.Empty(w.SampleGpu(90, fullScreen: true, null, T0.AddMinutes(i).AddSeconds(1), () => ["game"]));
        }
    }
}
