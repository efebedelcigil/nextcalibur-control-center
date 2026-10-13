using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class CoolingPolicyTests
{
    private static readonly DateTime T0 = new(2026, 10, 11, 12, 0, 0, DateTimeKind.Utc);

    /// <summary>Readings every 30 s of the processor against a 90 °C line; the card is cold.</summary>
    private static List<CoolingStep> Run(CoolingPolicy p, params int[] cpu) =>
        cpu.Select((t, i) => p.Next(t, 90, 50, 85, T0.AddSeconds(30 * i))).ToList();

    [Fact]
    public void Starts_on_the_way_up_before_the_line()
    {
        // 4 °C a minute towards a 90 °C line: on two readings in a row it would
        // cross within two minutes, and it starts there - well short of 90.
        int[] temps = [70, 72, 74, 76, 78, 80, 82, 84, 86, 88, 90];
        var steps = Run(new CoolingPolicy(), temps);
        var start = steps.IndexOf(CoolingStep.Start);
        Assert.True(start >= 0, "never started");
        Assert.True(temps[start] <= 84, $"started at {temps[start]} °C");
    }

    [Fact]
    public void Warm_and_steady_is_left_alone() =>
        Assert.DoesNotContain(CoolingStep.Start, Run(new CoolingPolicy(), 82, 83, 82, 83, 82, 83, 82, 83, 82, 83));

    [Fact]
    public void Rising_slowly_far_from_the_line_is_left_alone() =>
        Assert.DoesNotContain(CoolingStep.Start, Run(new CoolingPolicy(), 50, 51, 52, 53, 54, 55, 56, 57));

    [Fact]
    public void Over_the_line_starts_on_the_second_reading()
    {
        var steps = Run(new CoolingPolicy(), 91, 91);
        Assert.Equal([CoolingStep.Nothing, CoolingStep.Start], steps);
    }

    [Fact]
    public void A_burst_read_every_five_seconds_is_not_enough()
    {
        // The window reads every five seconds; a start-up climb from 68 to
        // 82 in twenty seconds, then settling, must not start anything.
        var p = new CoolingPolicy();
        int[] temps = [68, 72, 76, 80, 82, 81, 79, 77, 75, 74];
        var steps = temps.Select((t, i) => p.Next(t, 90, 50, 85, T0.AddSeconds(5 * i))).ToList();
        Assert.DoesNotContain(CoolingStep.Start, steps);
    }

    [Fact]
    public void One_spike_is_not_enough() =>
        Assert.DoesNotContain(CoolingStep.Start, Run(new CoolingPolicy(), 70, 93, 70, 70, 93, 70));

    [Fact]
    public void A_hot_card_counts_as_much_as_a_hot_processor()
    {
        var p = new CoolingPolicy();
        Assert.Equal(CoolingStep.Nothing, p.Next(60, 90, 86, 85, T0));
        Assert.Equal(CoolingStep.Start, p.Next(60, 90, 86, 85, T0.AddSeconds(30)));
    }

    [Fact]
    public void Stops_only_well_below_and_not_rising_for_five_minutes()
    {
        var p = new CoolingPolicy { Engaged = true };
        var steps = Run(p, 80, 76, 74, 72, 72, 72, 72, 72, 72, 72, 72, 72, 72, 72, 72);
        var stop = steps.IndexOf(CoolingStep.Stop);
        Assert.True(stop >= 12, $"stopped at reading {stop}");    // 74 at reading 2, five minutes on is reading 12
    }

    [Fact]
    public void Sleep_between_readings_starts_everything_again()
    {
        var p = new CoolingPolicy();
        Assert.Equal(CoolingStep.Nothing, p.Next(91, 90, 50, 85, T0));
        Assert.Equal(CoolingStep.Nothing, p.Next(91, 90, 50, 85, T0.AddMinutes(40)));
        Assert.Equal(CoolingStep.Start, p.Next(91, 90, 50, 85, T0.AddMinutes(41)));
    }

    [Theory]
    [InlineData("99,99", 99, 99)]
    [InlineData("70,100", 70, 100)]
    [InlineData("0,180", 0, 180)]
    public void Markers_read_back(string text, int ac, int dc) =>
        Assert.Equal(new PlanTuning.AcDc(ac, dc), PlanTuning.ParseMarker(text));

    [Theory]
    [InlineData("")]
    [InlineData("99")]
    [InlineData("86401,5")]
    [InlineData("-1,5")]
    [InlineData("a,b")]
    public void Odd_markers_are_refused(string text) => Assert.Null(PlanTuning.ParseMarker(text));
}
