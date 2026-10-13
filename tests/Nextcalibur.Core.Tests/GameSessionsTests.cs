using Nextcalibur.Core.Power;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class GameSessionsTests
{
    private static readonly TimeSpan Long = TimeSpan.FromMinutes(20);

    [Theory]
    [InlineData("brave")]
    [InlineData("chrome.exe")]
    [InlineData("VLC")]
    [InlineData("POWERPNT")]
    [InlineData("Nextcalibur")]
    public void Browsers_players_and_presentations_are_not_games(string name) => Assert.True(GameSessions.IsNotAGame(name));

    [Theory]
    [InlineData("eldenring")]
    [InlineData("cs2")]
    public void Anything_else_may_be(string name) => Assert.False(GameSessions.IsNotAGame(name));

    [Fact]
    public void A_game_played_in_office_is_asked_about() =>
        Assert.True(GameSessions.ShouldAsk("cs2", Long, SystemMode.Office, [], []));

    [Fact]
    public void A_game_played_in_gaming_is_not() =>
        Assert.False(GameSessions.ShouldAsk("cs2", Long, SystemMode.Gaming, [], []));

    [Fact]
    public void A_glance_is_not() =>
        Assert.False(GameSessions.ShouldAsk("cs2", TimeSpan.FromMinutes(1), SystemMode.Office, [], []));

    [Fact]
    public void A_browser_is_never_asked_about() =>
        Assert.False(GameSessions.ShouldAsk("firefox", Long, SystemMode.Office, [], []));

    [Fact]
    public void A_program_with_a_rule_is_not_asked_about_even_switched_off() =>
        Assert.False(GameSessions.ShouldAsk("cs2", Long, SystemMode.Office,
            [new ProgramRule { Process = "CS2", Mode = SystemMode.Office, Enabled = false }], []));

    [Fact]
    public void An_answered_program_is_not_asked_again() =>
        Assert.False(GameSessions.ShouldAsk("cs2", Long, SystemMode.Office, [], ["CS2"]));

    [Fact]
    public void The_mode_most_looked_at_counts()
    {
        Assert.Equal(SystemMode.Office, GameSessions.MostlyIn(new Dictionary<SystemMode, int> { [SystemMode.Office] = 5, [SystemMode.Gaming] = 2 }));
        Assert.Null(GameSessions.MostlyIn(new Dictionary<SystemMode, int>()));
    }
}
