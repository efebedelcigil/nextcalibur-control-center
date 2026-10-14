using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class ProgramRulesTests
{
    private static readonly DateTime T0 = new(2026, 10, 12, 11, 0, 0, DateTimeKind.Utc);

    private static Dictionary<string, DateTime> Running(params (string Name, int Minute)[] p) =>
        p.ToDictionary(x => x.Name, x => T0.AddMinutes(x.Minute), StringComparer.OrdinalIgnoreCase);

    [Theory]
    [InlineData("MATLAB.exe", "MATLAB")]
    [InlineData("  AddOnInstaller  ", "AddOnInstaller")]
    [InlineData("steam", "steam")]
    [InlineData("Some Game (x64)", "Some Game (x64)")]
    public void Names_are_normalised(string input, string expected) => Assert.Equal(expected, ProgramRules.Normalise(input));

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("a' OR ProcessName LIKE '%")]
    [InlineData("C:\\Program Files\\x.exe")]
    [InlineData("name\"quote")]
    public void Names_that_could_be_anything_else_are_refused(string input) => Assert.Null(ProgramRules.Normalise(input));

    [Fact]
    public void Nothing_running_means_no_rule() =>
        Assert.Null(ProgramRules.Winning([new ProgramRule { Process = "MATLAB" }], Running(), null));

    [Fact]
    public void A_running_program_brings_its_rule_case_aside()
    {
        var rule = new ProgramRule { Process = "matlab", Mode = SystemMode.Office };
        Assert.Same(rule, ProgramRules.Winning([rule], Running(("MATLAB", 0)), null));
    }

    [Fact]
    public void The_program_started_last_wins()
    {
        var matlab = new ProgramRule { Process = "MATLAB", Mode = SystemMode.Office };
        var game = new ProgramRule { Process = "game", Mode = SystemMode.Performance };
        Assert.Same(game, ProgramRules.Winning([matlab, game], Running(("MATLAB", 0), ("game", 5)), null));
        Assert.Same(matlab, ProgramRules.Winning([matlab, game], Running(("MATLAB", 9), ("game", 5)), null));
    }

    [Fact]
    public void A_rule_switched_off_does_nothing() =>
        Assert.Null(ProgramRules.Winning([new ProgramRule { Process = "MATLAB", Enabled = false }], Running(("MATLAB", 0)), null));

    [Fact]
    public void On_battery_no_rule_for_a_faster_mode_than_the_batterys_applies()
    {
        var game = new ProgramRule { Process = "game", Mode = SystemMode.Gaming };
        var matlab = new ProgramRule { Process = "MATLAB", Mode = SystemMode.Office };
        var office = new ProgramRule { Process = "word", Mode = SystemMode.Balanced };
        Assert.Null(ProgramRules.Winning([game], Running(("game", 0)), batteryCeiling: SystemMode.Office));
        Assert.Same(matlab, ProgramRules.Winning([game, matlab], Running(("game", 5), ("MATLAB", 0)), batteryCeiling: SystemMode.Office));
        Assert.Null(ProgramRules.Winning([office], Running(("word", 0)), batteryCeiling: SystemMode.Office));
        Assert.Same(office, ProgramRules.Winning([office], Running(("word", 0)), batteryCeiling: SystemMode.Balanced));
    }
}
