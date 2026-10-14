using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class FirmwareChangeTests
{
    [Fact]
    public void A_field_the_last_scan_did_not_have_is_not_a_change() =>
        Assert.False(MachineScan.FirmwareChanged(
            new MachineScanRecord { BiosVersion = "QQ141", BiosDate = null, EcRelease = "" },
            new MachineScanRecord { BiosVersion = "QQ141", BiosDate = "06/27/2024", EcRelease = "" }));

    [Fact]
    public void A_new_BIOS_is_a_change() =>
        Assert.True(MachineScan.FirmwareChanged(
            new MachineScanRecord { BiosVersion = "QQ141", BiosDate = "06/27/2024" },
            new MachineScanRecord { BiosVersion = "QQ150", BiosDate = "01/10/2026" }));
}
