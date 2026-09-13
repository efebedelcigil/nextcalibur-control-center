using System.Security.Cryptography.X509Certificates;
using Nextcalibur.Core.Security;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// The signature check decides whether a downloaded file is run as
/// administrator, so it is tested by attacking it rather than by using it.
///
/// The attack these prove closed: a file that carries somebody else's
/// signature block. Reading the certificate out of a file and building its
/// chain - which is what this project did until 13 September 2026 - says
/// who signed *something*, not that they signed *this*. Windows'
/// WinVerifyTrust hashes the file and compares.
/// </summary>
public class SignatureForgeryTests
{
    private static string SignedByMicrosoft => Path.Combine(Environment.SystemDirectory, "kernel32.dll");

    private static string CopyToTemp(string source)
    {
        var copy = Path.Combine(Path.GetTempPath(), "nc-test-" + Guid.NewGuid().ToString("N") + ".exe");
        File.Copy(source, copy);
        return copy;
    }

    /// <summary>
    /// A signed file with one byte of its code changed: the certificate is
    /// still readable and still Microsoft's, and the old check would have
    /// passed it. The hash no longer matches, and the check now refuses.
    /// </summary>
    [Fact]
    public void A_signed_file_that_has_been_edited_is_refused()
    {
        var file = CopyToTemp(SignedByMicrosoft);
        try
        {
            Assert.True(Authenticode.SignatureIsValid(file), "the untouched copy should verify");

            // Somewhere in the middle, well away from the signature block at
            // the end - the code an attacker would be replacing.
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = stream.Length / 2;
                var b = stream.ReadByte();
                stream.Position = stream.Length / 2;
                stream.WriteByte((byte)(b ^ 0xFF));
            }

            // What the old check looked at is unchanged: the certificate is
            // there, it is Microsoft's, and its chain builds.
            using var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(file));
            Assert.Contains("Microsoft", certificate.Subject);
            using var chain = new X509Chain();
            Assert.True(chain.Build(certificate), "the certificate itself is genuine - that was never the question");

            // What the file actually is has changed, and that is the question.
            Assert.False(Authenticode.SignatureIsValid(file), "an edited file must not verify");
            Assert.False(Authenticode.IsSignedBy(file, "CN=Microsoft Windows"));
        }
        finally
        {
            File.Delete(file);
        }
    }

    /// <summary>
    /// The signature block of a signed file, appended to a file of one's own.
    /// This is the transplant an attacker would try: the certificate reads as
    /// Microsoft's, the file is theirs.
    /// </summary>
    [Fact]
    public void A_borrowed_signature_block_is_refused()
    {
        var donor = File.ReadAllBytes(SignedByMicrosoft);
        var file = Path.Combine(Path.GetTempPath(), "nc-test-" + Guid.NewGuid().ToString("N") + ".exe");
        try
        {
            // A different file that ends with the donor's signature bytes.
            var mine = new byte[donor.Length];
            Array.Copy(donor, mine, donor.Length);
            for (var i = 0x1000; i < 0x2000 && i < mine.Length; i++) mine[i] ^= 0x5A;
            File.WriteAllBytes(file, mine);

            Assert.False(Authenticode.SignatureIsValid(file));
            Assert.False(Authenticode.IsSignedBy(file, "CN=Microsoft Windows"));
        }
        finally
        {
            File.Delete(file);
        }
    }

    /// <summary>
    /// A signer's name is matched as a whole component of the subject, so
    /// "CN=namazso.eu.attacker.example" - which a certificate authority will
    /// happily issue to whoever owns that domain - is not "CN=namazso.eu".
    /// </summary>
    [Theory]
    [InlineData("CN=namazso.eu", "CN=namazso.eu", true)]
    [InlineData("CN=namazso.eu.attacker.example", "CN=namazso.eu", false)]
    [InlineData("CN=notnamazso.eu", "CN=namazso.eu", false)]
    [InlineData("CN=namazso.eu, O=Somebody", "CN=namazso.eu", true)]
    [InlineData("CN=NAMAZSO.EU", "CN=namazso.eu", true)]
    public void The_signers_name_is_a_whole_component(string subject, string expected, bool matches)
    {
        using var certificate = SelfSigned(subject);
        Assert.Equal(matches, Authenticode.HasSubjectComponent(certificate, expected));
    }

    /// <summary>A self-signed certificate with any name at all: what an attacker makes in a minute.</summary>
    private static X509Certificate2 SelfSigned(string subject)
    {
        using var key = System.Security.Cryptography.RSA.Create(2048);
        var request = new CertificateRequest(subject, key, System.Security.Cryptography.HashAlgorithmName.SHA256,
            System.Security.Cryptography.RSASignaturePadding.Pkcs1);
        return request.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddDays(1));
    }

    /// <summary>A file with no signature at all, and a file that is not there.</summary>
    [Fact]
    public void An_unsigned_or_missing_file_is_refused()
    {
        var file = Path.Combine(Path.GetTempPath(), "nc-test-" + Guid.NewGuid().ToString("N") + ".exe");
        File.WriteAllBytes(file, new byte[] { 0x4D, 0x5A, 0x90, 0x00 });
        try
        {
            Assert.False(Authenticode.SignatureIsValid(file));
            Assert.False(Authenticode.IsSignedBy(file, "CN=namazso.eu"));
        }
        finally
        {
            File.Delete(file);
        }

        Assert.False(Authenticode.SignatureIsValid(file));
        Assert.False(Authenticode.IsSignedBy(file, "CN=namazso.eu"));
    }
}
