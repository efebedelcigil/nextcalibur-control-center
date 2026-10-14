using System.Diagnostics;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class EfficiencyModeTests
{
    [Fact]
    public void A_process_goes_into_efficiency_mode_and_back()
    {
        // A harmless process of the test's own, ended at the end.
        using var p = Process.Start(new ProcessStartInfo("ping", "-n 20 127.0.0.1") { CreateNoWindow = true, UseShellExecute = false })!;
        try
        {
            Assert.True(EfficiencyMode.Set(p.Id, true));
            p.Refresh();
            Assert.Equal(ProcessPriorityClass.Idle, p.PriorityClass);

            Assert.True(EfficiencyMode.Set(p.Id, false));
            p.Refresh();
            Assert.Equal(ProcessPriorityClass.Normal, p.PriorityClass);
        }
        finally
        {
            p.Kill();
        }
    }

    [Fact]
    public void A_process_that_is_not_there_is_refused() => Assert.False(EfficiencyMode.Set(-1, true));
}
