using System.Diagnostics;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class TopProcessesTests
{
    [Fact]
    public void A_busy_process_is_named_with_its_share()
    {
        using var stop = new CancellationTokenSource();
        var spinner = new Thread(() => { while (!stop.IsCancellationRequested) { } }) { IsBackground = true };
        spinner.Start();
        try
        {
            var loads = TopProcesses.Measure(count: 50, window: TimeSpan.FromMilliseconds(800));
            var me = Process.GetCurrentProcess().ProcessName;
            var mine = loads.FirstOrDefault(l => l.Name.Equals(me, StringComparison.OrdinalIgnoreCase));

            Assert.NotNull(mine.Name);
            // One core of the whole processor, with room for a busy test machine.
            Assert.InRange(mine.Percent, 100.0 / Environment.ProcessorCount * 0.5, 100);
            Assert.Equal(loads.OrderByDescending(l => l.Percent).Select(l => l.Name), loads.Select(l => l.Name));
        }
        finally
        {
            stop.Cancel();
            spinner.Join();
        }
    }

    [Fact]
    public void Describes_the_busiest_first_and_nothing_as_null()
    {
        Assert.Null(TopProcesses.Describe([]));
        Assert.Equal("openscad 62%, chrome 9%",
            TopProcesses.Describe([new ProcessLoad("openscad", 62.4), new ProcessLoad("chrome", 9.2)]));
    }
}
