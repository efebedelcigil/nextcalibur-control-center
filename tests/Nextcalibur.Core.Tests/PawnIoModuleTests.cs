using System.Reflection;
using System.Security.Cryptography;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>The PawnIO modules shipped are byte for byte the ones released (PawnIO.Modules 0.2.11).</summary>
public class PawnIoModuleTests
{
    [Theory]
    [InlineData("Nextcalibur.Core.Resources.PawnIO.IntelMSR.bin", "D6ED85D65AB17A22F813EF98207D6D537155EE2DED5976A21CB48413C9B92E5F")]
    [InlineData("Nextcalibur.Core.Resources.PawnIO.LpcACPIEC.bin", "C38FD116E7AFF4D1FDB0A494E296BE0A6708E5A22FC72F14587442FB7F8F7906")]
    public void Modules_are_the_released_ones(string resource, string sha256)
    {
        using var stream = typeof(FanTurboBit).Assembly.GetManifestResourceStream(resource);
        Assert.NotNull(stream);
        Assert.Equal(sha256, Convert.ToHexString(SHA256.HashData(stream!)));
    }
}
