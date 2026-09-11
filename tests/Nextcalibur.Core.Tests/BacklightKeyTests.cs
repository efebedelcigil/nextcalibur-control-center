using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class BacklightKeyTests
{
    [Theory]
    [InlineData(0, LedBrightness.Off)]
    [InlineData(1, LedBrightness.Half)]
    [InlineData(2, LedBrightness.Full)]
    public void The_first_byte_is_the_level_measured(byte first, LedBrightness expected) =>
        Assert.Equal(expected, BacklightKeyWatcher.Parse(first));

    [Fact]
    public void Anything_else_is_not_guessed_at() => Assert.Null(BacklightKeyWatcher.Parse(3));
}
