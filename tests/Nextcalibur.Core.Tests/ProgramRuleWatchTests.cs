using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class ProgramRuleWatchTests
{
    private static readonly HashSet<string> Names = new(["cs2", "League of Legends", "VALORANT-Win64-Shipping", "AddOnInstaller"], StringComparer.OrdinalIgnoreCase);

    [Theory]
    [InlineData("cs2.exe", true)]
    [InlineData("CS2.EXE", true)]
    [InlineData("League of Legends.exe", true)]    // never arrives whole from the trace, but would match
    [InlineData("League of Legen", true)]          // as the kernel's trace has it
    [InlineData("VALORANT-Win64-", true)]
    [InlineData("AddOnInstaller.", true)]
    [InlineData("chrome.exe", false)]
    [InlineData("cs.exe", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void A_start_is_matched_by_name_or_by_the_kernel_s_cut_name(string? image, bool watched)
    {
        Assert.Equal(watched, ProgramRules.IsWatched(Names, image));
    }

    [Fact]
    public void A_cut_name_that_starts_no_rule_is_not_matched() =>
        Assert.False(ProgramRules.IsWatched(Names, "SomethingElse12"));
}
