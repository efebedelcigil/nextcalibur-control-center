using System.Security.Cryptography.X509Certificates;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The signature check is what stands between a download and an elevated
/// run. It must pass a file Windows signed, refuse one nobody signed, and
/// match the signer's name as a whole component rather than a substring.
/// </summary>
public class AuthenticodeTests
{
    private static string System32(string name) => Path.Combine(Environment.SystemDirectory, name);

    [Fact]
    public void A_file_Windows_signed_is_valid_and_is_Microsofts()
    {
        var file = System32("kernel32.dll");
        Assert.True(Authenticode.SignatureIsValid(file), "kernel32.dll should carry a valid signature (catalogue-signed files are not, but kernel32 is signed in-file)");
        Assert.True(Authenticode.IsSignedBy(file, "CN=Microsoft Windows"));
        Assert.False(Authenticode.IsSignedBy(file, "CN=namazso.eu"));
    }

    [Fact]
    public void A_file_nobody_signed_is_refused()
    {
        var file = Path.GetTempFileName();
        try
        {
            File.WriteAllBytes(file, new byte[] { 0x4D, 0x5A, 0, 0 });
            Assert.False(Authenticode.SignatureIsValid(file));
            Assert.False(Authenticode.IsSignedBy(file, "CN=Microsoft Windows"));
        }
        finally
        {
            File.Delete(file);
        }
    }

    [Fact]
    public void A_missing_file_is_refused()
    {
        Assert.False(Authenticode.SignatureIsValid(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".exe")));
    }

    [Fact]
    public void The_subject_component_must_match_whole()
    {
        using var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(System32("kernel32.dll")));
        Assert.True(Authenticode.HasSubjectComponent(certificate, "CN=Microsoft Windows"));
        Assert.False(Authenticode.HasSubjectComponent(certificate, "CN=Microsoft"));
        Assert.False(Authenticode.HasSubjectComponent(certificate, "Microsoft Windows"));
    }
}
