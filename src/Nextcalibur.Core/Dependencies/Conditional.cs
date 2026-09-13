using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;

namespace Nextcalibur.Core.Dependencies;

/// <summary>
/// Asking the same question every six hours without downloading the same
/// answer every six hours.
///
/// Every one of these files carries an ETag, so the second request can say
/// "only if it changed" and be answered with 304 and no body at all - a few
/// hundred bytes on the wire instead of kilobytes, and on GitHub's API a 304
/// does not even count against the rate limit. Bodies are remembered so a
/// 304 can be answered from memory; a large one is not, because holding a
/// megabyte and a half of JSON for the life of the process to save a
/// download that happens once per release is the wrong trade.
///
/// This is a background application on a machine somebody may be playing a
/// game on. What it costs the network is part of what it costs them.
/// </summary>
internal static class Conditional
{
    /// <summary>Bodies above this are fetched and used but not remembered.</summary>
    private const int LargestRemembered = 64 * 1024;

    private static readonly ConcurrentDictionary<string, Answer> Remembered = new(StringComparer.Ordinal);

    private sealed record Answer(string ETag, string? Body);

    /// <summary>
    /// The body of the URL, from the network or from memory when the server
    /// says it has not changed. Null when it could not be had.
    /// </summary>
    public static async Task<string?> GetAsync(HttpClient http, string url, CancellationToken ct)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        if (Remembered.TryGetValue(url, out var known) && known.Body is not null)
            request.Headers.TryAddWithoutValidation("If-None-Match", known.ETag);

        using var response = await http.SendAsync(request, HttpCompletionOption.ResponseContentRead, ct);

        if (response.StatusCode == HttpStatusCode.NotModified && known?.Body is { } unchanged)
            return unchanged;

        if (!response.IsSuccessStatusCode) return null;

        var body = await response.Content.ReadAsStringAsync(ct);
        // The whole header value, weak prefix and all: GitHub's API issues
        // W/"..." and a request that drops the prefix is a different tag.
        if (response.Headers.ETag?.ToString() is { Length: > 0 } tag)
            Remembered[url] = new Answer(tag, body.Length <= LargestRemembered ? body : null);

        return body;
    }

    /// <summary>Forgets everything; the tests use it, and nothing else needs to.</summary>
    internal static void Forget() => Remembered.Clear();

    /// <summary>How many answers are being held, and how much they weigh. For the tests and the cost audit.</summary>
    internal static (int Count, int Bytes) Held()
    {
        var count = 0;
        var bytes = 0;
        foreach (var each in Remembered.Values)
        {
            if (each.Body is null) continue;
            count++;
            bytes += each.Body.Length;
        }
        return (count, bytes);
    }
}
