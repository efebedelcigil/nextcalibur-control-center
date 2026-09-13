using System.Net;
using System.Net.Http;
using System.Text;
using Nextcalibur.Core.Dependencies;
using Xunit;

namespace Nextcalibur.Core.Tests.Attacks;

/// <summary>
/// What a check costs the network. This application sits in the tray on a
/// machine somebody plays games on, so "how many bytes, how often" is a
/// promise it makes rather than an afterthought - and a promise nobody
/// measures is a promise nobody keeps.
/// </summary>
public class NetworkCostTests
{
    /// <summary>A server that counts what it is asked for and answers with an ETag.</summary>
    private sealed class Counting : HttpMessageHandler
    {
        private readonly string _body;
        public int Requests { get; private set; }
        public int Conditional { get; private set; }
        public long BytesSent { get; private set; }

        public Counting(string body) => _body = body;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            Requests++;
            if (request.Headers.TryGetValues("If-None-Match", out var tags) && tags.Contains("\"v1\""))
            {
                Conditional++;
                var notModified = new HttpResponseMessage(HttpStatusCode.NotModified);
                notModified.Headers.ETag = new System.Net.Http.Headers.EntityTagHeaderValue("\"v1\"");
                return Task.FromResult(notModified);
            }

            BytesSent += Encoding.UTF8.GetByteCount(_body);
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(_body, Encoding.UTF8, "application/json") };
            response.Headers.ETag = new System.Net.Http.Headers.EntityTagHeaderValue("\"v1\"");
            return Task.FromResult(response);
        }
    }

    private const string Url = "https://example.invalid/index.json";

    [Fact]
    public async Task An_answer_that_has_not_changed_is_not_downloaded_twice()
    {
        Conditional.Forget();
        var server = new Counting(new string('x', 4096));
        using var http = new HttpClient(server);

        var first = await Conditional.GetAsync(http, Url, CancellationToken.None);
        var second = await Conditional.GetAsync(http, Url, CancellationToken.None);
        var third = await Conditional.GetAsync(http, Url, CancellationToken.None);

        Assert.Equal(first, second);
        Assert.Equal(first, third);
        Assert.Equal(3, server.Requests);
        Assert.Equal(2, server.Conditional);
        Assert.Equal(4096, server.BytesSent);   // the body crossed the wire once
    }

    /// <summary>
    /// A large answer is used but not remembered: holding a megabyte and a
    /// half for the life of the process to save a download that happens once
    /// per release is the wrong way round.
    /// </summary>
    [Fact]
    public async Task A_large_answer_is_not_held_in_memory()
    {
        Conditional.Forget();
        var big = new string('y', 200_000);
        using var http = new HttpClient(new Counting(big));

        Assert.Equal(big, await Conditional.GetAsync(http, Url, CancellationToken.None));

        var (count, bytes) = Conditional.Held();
        Assert.Equal(0, count);
        Assert.Equal(0, bytes);
    }

    [Fact]
    public async Task A_small_answer_is_held_and_it_is_small()
    {
        Conditional.Forget();
        using var http = new HttpClient(new Counting(new string('z', 7000)));

        await Conditional.GetAsync(http, Url, CancellationToken.None);

        var (count, bytes) = Conditional.Held();
        Assert.Equal(1, count);
        Assert.InRange(bytes, 1, 64 * 1024);
    }

    /// <summary>An error is not remembered as an answer, or the next check would be answered with nothing.</summary>
    [Fact]
    public async Task A_failure_is_not_remembered()
    {
        Conditional.Forget();
        using var http = new HttpClient(new Refusing());

        Assert.Null(await Conditional.GetAsync(http, Url, CancellationToken.None));
        Assert.Equal(0, Conditional.Held().Count);
    }

    private sealed class Refusing : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
    }

    /// <summary>
    /// A check never reads the runtime's expensive index: a day of checking
    /// costs a few kilobytes, and the 310 kB that carries the hashes is
    /// read once, when somebody has said yes to the offer.
    /// </summary>
    [Fact]
    public async Task A_check_never_reads_the_large_release_index()
    {
        Conditional.Forget();
        var counting = new CountingByUrl();
        using var http = new HttpClient(counting);
        var dotnet = new DotNetRuntimeDependency();

        for (var i = 0; i < 4; i++)
        {
            var found = await dotnet.LatestAsync(http, CancellationToken.None);
            Assert.NotNull(found);
            Assert.Equal(new Version(8, 99, 99), found!.Version);
            Assert.Null(found.Sha512);   // not known yet, and not worth 310 kB to know
        }

        Assert.Equal(4, counting.Hits("releases-index.json"));
        Assert.Equal(0, counting.Hits("8.0/releases.json"));
    }

    /// <summary>And the hash is fetched when it is about to be used.</summary>
    [Fact]
    public async Task The_hash_is_fetched_when_the_download_is_about_to_happen()
    {
        Conditional.Forget();
        var counting = new CountingByUrl();
        using var http = new HttpClient(counting);
        var dotnet = new DotNetRuntimeDependency();

        var found = await dotnet.LatestAsync(http, CancellationToken.None);
        var resolved = await dotnet.ResolveBeforeDownloadAsync(http, found!, CancellationToken.None);

        Assert.Equal(1, counting.Hits("8.0/releases.json"));
        Assert.Equal("ab", resolved.Sha512);
        Assert.Equal(found!.Version, resolved.Version);
    }

    /// <summary>Answers both .NET indexes, and counts which was asked for.</summary>
    private sealed class CountingByUrl : HttpMessageHandler
    {
        private readonly Dictionary<string, int> _hits = new(StringComparer.Ordinal);

        public int Hits(string ending) => _hits.TryGetValue(ending, out var n) ? n : 0;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            var url = request.RequestUri!.ToString();
            var key = url.EndsWith("releases-index.json", StringComparison.Ordinal) ? "releases-index.json" : "8.0/releases.json";
            _hits[key] = Hits(key) + 1;

            // A version far above anything installed, so the check always
            // wants the large index and the caching is what stops it.
            var body = key == "releases-index.json"
                ? """{"releases-index":[{"channel-version":"8.0","latest-release":"8.99.99","support-phase":"active","eol-date":"2030-01-01"}]}"""
                : """{"releases":[{"windowsdesktop":{"version":"8.99.99","files":[{"name":"windowsdesktop-runtime-win-x64.exe","url":"https://builds.dotnet.microsoft.com/dotnet/WindowsDesktop/8.99.99/windowsdesktop-runtime-8.99.99-win-x64.exe","hash":"ab"}]}}]}""";

            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(body, Encoding.UTF8, "application/json") };
            return Task.FromResult(response);
        }
    }
}
