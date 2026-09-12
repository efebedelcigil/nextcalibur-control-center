using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The cheap process detector answers the same question the expensive one
/// did. The test runner itself is the process that is certainly running.
/// </summary>
public class ProcessPresenceTests
{
    private static string Self => System.IO.Path.GetFileName(Environment.ProcessPath!);

    [Fact]
    public void Finds_a_process_that_is_running()
    {
        var presence = new ProcessPresence(Self);
        Assert.True(presence.AnyRunning());
    }

    [Fact]
    public void Does_not_find_one_that_is_not()
    {
        var presence = new ProcessPresence("nextcalibur-no-such-process-7f3a.exe");
        Assert.False(presence.AnyRunning());
    }

    [Fact]
    public void The_answer_is_stable_across_looks()
    {
        var presence = new ProcessPresence("nextcalibur-no-such-process-7f3a.exe", Self);
        for (var i = 0; i < 5; i++)
            Assert.True(presence.AnyRunning());
    }

    [Fact]
    public void Names_are_compared_without_regard_to_case()
    {
        var presence = new ProcessPresence(Self.ToUpperInvariant());
        Assert.True(presence.AnyRunning());
    }
}
