using System.Globalization;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class StorageUseTests
{
    [Fact]
    public void An_empty_total_reports_zero_rather_than_dividing_by_it()
    {
        Assert.Equal(0, new StorageUse(0, 0).Percent);
        Assert.Equal(0, new StorageUse(1024, 0).Percent);
    }

    [Fact]
    public void Percent_is_used_over_total()
    {
        Assert.Equal(50, new StorageUse(500, 1000).Percent);
        Assert.Equal(100, new StorageUse(1000, 1000).Percent);
    }

    [Fact]
    public void Describe_reads_as_used_over_total_in_gigabytes()
    {
        // The separator follows the machine's locale on purpose, so this pins
        // the culture rather than the punctuation.
        var previous = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        try
        {
            const ulong gb = 1024UL * 1024 * 1024;
            Assert.Equal("12.0/32.0GB", new StorageUse(12 * gb, 32 * gb).Describe());
            Assert.Equal("0.5/1.0GB", new StorageUse(gb / 2, gb).Describe());

            // Gigabytes as the machine counts them, not as a disk is sold: a
            // 1 TB drive reads as 931 GB, which is what the interface shows.
            Assert.Equal("0.0/931.3GB", new StorageUse(0, 1_000_000_000_000).Describe());
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }
}

/// <summary>
/// Device names are read from the machine at runtime — no model is written into
/// this repository, which is public. What can be tested is the tidying, which is
/// string handling and fails silently when it is wrong.
/// </summary>
public class ProcessorNameTests
{
    [Theory]
    [InlineData("Intel(R) Core(TM) i7-12650H CPU @ 2.30GHz", "Intel Core i7-12650H")]
    [InlineData("Intel(R) Core(TM) i7-12650H", "Intel Core i7-12650H")]
    [InlineData("AMD Ryzen 7 7840HS w/ Radeon 780M Graphics", "AMD Ryzen 7 7840HS w/ Radeon 780M Graphics")]
    [InlineData("NVIDIA GeForce RTX 4050 Laptop GPU", "NVIDIA GeForce RTX 4050 Laptop GPU")]
    public void Trademark_noise_and_the_clock_speed_are_removed(string raw, string expected) =>
        Assert.Equal(expected, SystemInfo.Tidy(raw));

    [Fact]
    public void Runs_of_spaces_left_behind_are_collapsed()
    {
        // Removing "(R)" from "Intel(R) Core" leaves "Intel Core"; removing it
        // from "Intel (R) Core" would leave two spaces.
        Assert.Equal("Intel Core i5", SystemInfo.Tidy("Intel (R)  Core (TM)  i5"));
    }

    [Fact]
    public void A_name_that_is_only_a_marker_is_not_emptied()
    {
        // " CPU " at position zero is not a suffix marker, and cutting there
        // would leave nothing at all.
        Assert.Equal("CPU 1234", SystemInfo.Tidy("CPU 1234"));
    }

    [Fact]
    public void An_ordinary_name_is_left_alone() =>
        Assert.Equal("Apple M3 Pro", SystemInfo.Tidy("Apple M3 Pro"));
}
