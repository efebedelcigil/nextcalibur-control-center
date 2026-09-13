using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Nextcalibur.Core.Dependencies;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// What the application does with an answer from the network that is not
/// the answer it expected. The file at the end of this conversation is
/// downloaded and run as administrator, so every field in it is treated as
/// something a stranger wrote.
/// </summary>
public class HostileAnswerTests
{
    private sealed class Answer : HttpMessageHandler
    {
        private readonly string _body;
        private readonly HttpStatusCode _status;
        public Answer(string body, HttpStatusCode status = HttpStatusCode.OK) { _body = body; _status = status; }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct) =>
            Task.FromResult(new HttpResponseMessage(_status) { Content = new StringContent(_body, Encoding.UTF8, "application/json") });
    }

    private sealed class Probe : Dependency
    {
        public override string Id => "probe";
        public override string Name => "Probe";
        public override string Purpose => "does nothing";
        public override string UninstallKey => "Probe";
        public override string ExpectedSigner => "CN=nobody";
        public override string SilentInstallArguments => "/S";
        public override Version? InstalledVersion() => null;
        public override Task<(Version Version, Uri Download)?> LatestAsync(HttpClient http, CancellationToken ct) =>
            LatestGitHubReleaseAsync(http, "namazso", "PawnIO.Setup", "PawnIO_setup.exe", ct);
    }

    private static async Task<(Version Version, Uri Download)?> Ask(string body, HttpStatusCode status = HttpStatusCode.OK)
    {
        using var http = new HttpClient(new Answer(body, status));
        return await new Probe().LatestAsync(http, CancellationToken.None);
    }

    /// <summary>A release answer with these three fields, encoded properly: the parser is not what is being tested here.</summary>
    private static string Release(string tag, string name, string url) =>
        JsonSerializer.Serialize(new
        {
            tag_name = tag,
            assets = new[] { new { name, browser_download_url = url } },
        });

    private const string Good = "https://github.com/namazso/PawnIO.Setup/releases/download/v3.0/PawnIO_setup.exe";

    [Fact]
    public async Task The_expected_answer_is_accepted()
    {
        var latest = await Ask(Release("v3.0", "PawnIO_setup.exe", Good));
        Assert.NotNull(latest);
        Assert.Equal(new Version(3, 0), latest!.Value.Version);
        Assert.Equal(new Uri(Good), latest.Value.Download);
    }

    /// <summary>
    /// The link is the cheapest field to change in an answer, and it decides
    /// which file is downloaded and run elevated. Anything but an HTTPS
    /// release asset of the repository that was asked about is refused.
    /// </summary>
    [Theory]
    [InlineData("http://github.com/namazso/PawnIO.Setup/releases/download/v3.0/PawnIO_setup.exe")]   // not TLS
    [InlineData("https://github.evil.example/namazso/PawnIO.Setup/releases/download/v3.0/x.exe")]    // not GitHub
    [InlineData("https://github.com.evil.example/namazso/PawnIO.Setup/releases/download/v3.0/x.exe")]
    [InlineData("https://github.com/attacker/repo/releases/download/v3.0/PawnIO_setup.exe")]         // another repository
    [InlineData("https://github.com/namazso/PawnIO.Setup/raw/main/PawnIO_setup.exe")]                // not a release asset
    [InlineData("file:///C:/Windows/System32/calc.exe")]
    [InlineData("\\\\attacker\\share\\PawnIO_setup.exe")]
    [InlineData("javascript:alert(1)")]
    [InlineData("")]
    public async Task A_link_that_is_not_a_release_asset_of_that_repository_is_refused(string url)
    {
        Assert.Null(await Ask(Release("v3.0", "PawnIO_setup.exe", url)));
    }

    /// <summary>GitHub answers a rate limit with a body of a different shape; that is not a crash.</summary>
    [Theory]
    [InlineData("""{"message":"API rate limit exceeded for 1.2.3.4.","documentation_url":"https://docs.github.com"}""")]
    [InlineData("""{"tag_name":null,"assets":[]}""")]
    [InlineData("""{"tag_name":"v3.0"}""")]
    [InlineData("""{"tag_name":"v3.0","assets":{}}""")]
    [InlineData("""{"tag_name":"v3.0","assets":[{"name":"PawnIO_setup.exe"}]}""")]
    [InlineData("""{"tag_name":"v3.0","assets":[{"name":42,"browser_download_url":42}]}""")]
    [InlineData("""{"tag_name":"not a version","assets":[]}""")]
    [InlineData("""{"tag_name":"v99999999999999999999","assets":[]}""")]
    [InlineData("[]")]
    [InlineData("null")]
    [InlineData("\"\"")]
    public async Task An_answer_of_the_wrong_shape_is_a_null_answer_not_an_exception(string body)
    {
        Assert.Null(await Ask(body));
    }

    [Fact]
    public async Task An_error_status_is_a_null_answer()
    {
        Assert.Null(await Ask(Release("v3.0", "PawnIO_setup.exe", Good), HttpStatusCode.Forbidden));
    }

    /// <summary>
    /// A name that is not the asset's is not the asset - including one that
    /// tries to walk out of wherever it might be saved.
    /// </summary>
    [Theory]
    [InlineData("..\\..\\Windows\\System32\\PawnIO_setup.exe")]
    [InlineData("PawnIO_setup.exe.evil")]
    [InlineData("pawnio_setup.exe ")]
    public async Task An_asset_by_another_name_is_not_the_asset(string name)
    {
        Assert.Null(await Ask(Release("v3.0", name, Good)));
    }

    /// <summary>The name is matched without regard to case, as GitHub stores it.</summary>
    [Fact]
    public async Task The_assets_name_is_matched_without_regard_to_case()
    {
        Assert.NotNull(await Ask(Release("v3.0", "PAWNIO_SETUP.EXE", Good)));
    }

    /// <summary>The link check on its own, so every rule it makes is stated once.</summary>
    [Theory]
    [InlineData("https://github.com/o/r/releases/download/v1/a.exe", true)]
    [InlineData("https://GITHUB.COM/o/r/releases/download/v1/a.exe", true)]
    [InlineData("https://github.com/o/r/releases/downloads/v1/a.exe", false)]
    [InlineData("https://github.com/o/r2/releases/download/v1/a.exe", false)]
    [InlineData("https://user:pass@github.com/o/r/releases/download/v1/a.exe", true)]
    [InlineData(null, false)]
    public void The_link_rules(string? link, bool accepted)
    {
        Assert.Equal(accepted, Dependency.IsReleaseAssetOf(link, "o", "r", out _));
    }
}
