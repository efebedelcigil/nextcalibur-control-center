using System.Reflection;
using System.Security.Cryptography;
using Nextcalibur.Core.Hardware;

using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>The PawnIO module shipped is byte for byte the one released (PawnIO.Modules 0.2.11).</summary>
public class PawnIoModuleTests
{
    [Theory]
    [InlineData("Nextcalibur.Core.Resources.PawnIO.IntelMSR.bin", "D6ED85D65AB17A22F813EF98207D6D537155EE2DED5976A21CB48413C9B92E5F")]
    public void Modules_are_the_released_ones(string resource, string sha256)
    {
        using var stream = typeof(CpuPowerReader).Assembly.GetManifestResourceStream(resource);
        Assert.NotNull(stream);
        Assert.Equal(sha256, Convert.ToHexString(SHA256.HashData(stream!)));
    }
}
