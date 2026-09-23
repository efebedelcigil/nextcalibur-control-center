using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// The dependencies Windows put on the machine - PawnIO's driver, NVIDIA's
/// nvml.dll - are signed through a catalog, not in the file. A check of the
/// file alone calls them unsigned; these hold the catalog path to its word,
/// on a Windows file every machine has, signed the same way.
/// </summary>
public class SystemFileTrustTests : IDisposable
{
    private static string Cmd => Path.Combine(Environment.SystemDirectory, "cmd.exe");
    private readonly string _folder = Path.Combine(Path.GetTempPath(), "nc-trust-" + Guid.NewGuid().ToString("N"));

    public SystemFileTrustTests() => Directory.CreateDirectory(_folder);

    public void Dispose()
    {
        try { Directory.Delete(_folder, recursive: true); } catch (IOException) { }
    }

    [Fact]
    public void A_catalog_signed_Windows_file_is_trusted()
    {
        Assert.NotNull(Authenticode.CatalogFor(Cmd));
        Assert.True(Authenticode.IsSystemFileSignedBy(Cmd, "O=Microsoft Corporation"));
    }

    [Fact]
    public void The_signer_must_be_the_one_asked_for()
    {
        Assert.False(Authenticode.IsSystemFileSignedBy(Cmd, "O=NVIDIA Corporation"));
    }

    [Fact]
    public void One_changed_byte_and_the_catalog_no_longer_vouches_for_it()
    {
        var copy = Path.Combine(_folder, "cmd.exe");
        File.Copy(Cmd, copy);
        using (var stream = new FileStream(copy, FileMode.Open, FileAccess.ReadWrite))
        {
            stream.Seek(-1, SeekOrigin.End);
            var last = stream.ReadByte();
            stream.Seek(-1, SeekOrigin.End);
            stream.WriteByte((byte)(last ^ 0xFF));
        }

        Assert.Null(Authenticode.CatalogFor(copy));
        Assert.False(Authenticode.IsSystemFileSignedBy(copy, "O=Microsoft Corporation"));
    }

    [Fact]
    public void A_missing_file_is_not_trusted()
    {
        Assert.False(Authenticode.IsSystemFileSignedBy(Path.Combine(_folder, "absent.dll"), "O=Microsoft Corporation"));
    }
}
