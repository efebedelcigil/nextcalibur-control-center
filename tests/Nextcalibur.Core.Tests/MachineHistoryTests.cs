using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class MachineHistoryTests
{
    private static readonly DateTime Now = new(2026, 10, 14, 12, 0, 0);

    [Fact]
    public void A_fresh_Windows_with_nothing_on_it()
    {
        var h = new MachineHistory(Now.AddHours(-5), false, false, false, 0, false);
        Assert.Equal(["fresh Windows"], h.Describe(Now));
    }

    [Fact]
    public void A_machine_that_had_Nextcalibur_and_the_vendors_software_before()
    {
        var h = new MachineHistory(Now.AddDays(-200), false, true, true, 3, true);
        Assert.Equal(["vendor Control Center was installed before", "Nextcalibur was installed before", "power-mode fault present"], h.Describe(Now));
    }

    [Fact]
    public void A_machine_used_for_a_while_without_it_with_the_vendors_software_now()
    {
        var h = new MachineHistory(Now.AddDays(-90), true, true, false, 0, false);
        Assert.Equal(["vendor Control Center installed", "used without Nextcalibur until now"], h.Describe(Now));
    }

    [Fact]
    public void An_unknown_install_date_is_not_fresh() =>
        Assert.False(new MachineHistory(null, false, false, false, 0, false).FreshWindows(Now));
}
