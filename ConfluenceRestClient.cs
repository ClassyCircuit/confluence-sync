using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Flurl;

namespace ConfluenceSync;

internal sealed class ConfluenceRestClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private const int DefaultPageLimit = 50;

    private readonly HttpClient _http;
    private readonly ILogger _log;

    public ConfluenceRestClient(HttpClient http, ILogger log)
    {
        _http = http;
        _log = log;
    }

    public static HttpClient CreateHttpClient(Uri baseUri, string email, string apiKey)
    {
        var http = new HttpClient
        {
            BaseAddress = baseUri
        };

        var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{apiKey}"));
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return http;
    }

    public async Task<ContentDto> GetPageById(string pageId, CancellationToken cancellationToken)
    {
        var relative = new Url("rest/api/content")
            .AppendPathSegment(pageId)
            .SetQueryParam("expand", "space,body.storage")
            .ToString();

        _log.Info($"GET /{relative}");

        try
        {
            return await GetJson<ContentDto>(relative, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to fetch page '{pageId}'.", ex);
        }
    }

    public async IAsyncEnumerable<ContentDto> GetChildPages(
        string parentId,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var start = 0;
        const int limit = DefaultPageLimit;

        while (true)
        {
            var relative = new Url("rest/api/content")
                .AppendPathSegment(parentId)
                .AppendPathSegment("child/page")
                .SetQueryParam("start", start)
                .SetQueryParam("limit", limit)
                .SetQueryParam("expand", "space,body.storage")
                .ToString();

            _log.Info($"GET /{relative}");

            ContentListDto list;
            try
            {
                list = await GetJson<ContentListDto>(relative, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to fetch children of page '{parentId}' (start={start}, limit={limit}).", ex);
            }

            if (list.results is null || list.results.Count == 0)
            {
                yield break;
            }

            foreach (var item in list.results)
            {
                yield return item;
            }

            start += list.results.Count;
            if (list.results.Count < limit)
            {
                yield break;
            }
        }
    }

    public async IAsyncEnumerable<ContentDto> SearchPagesInSpace(
        string spaceKey,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var start = 0;
        const int limit = DefaultPageLimit;

        // CQL: https://developer.atlassian.com/cloud/confluence/advanced-searching-using-cql/
        // Note: space keys can contain '~', so always quote.
        var cql = $"space=\"{spaceKey}\" and type=page order by title";

        while (true)
        {
            var relative = new Url("rest/api/content/search")
                .SetQueryParam("cql", cql)
                .SetQueryParam("start", start)
                .SetQueryParam("limit", limit)
                .ToString();

            _log.Info($"GET /{relative}");

            ContentListDto list;
            try
            {
                list = await GetJson<ContentListDto>(relative, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to search pages in space '{spaceKey}'.", ex);
            }

            if (list.results is null || list.results.Count == 0)
            {
                yield break;
            }

            foreach (var item in list.results)
            {
                yield return item;
            }

            start += list.results.Count;
            if (list.results.Count < limit)
            {
                yield break;
            }
        }
    }

    private async Task<T> GetJson<T>(string relativeUrl, CancellationToken cancellationToken)
    {
        using var resp = await _http.GetAsync(relativeUrl, cancellationToken);
        _log.Info($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase} <- /{relativeUrl}");

        var body = await resp.Content.ReadAsStringAsync(cancellationToken);

        if (!resp.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase} while calling /{relativeUrl}\n{body}");
        }

        return JsonSerializer.Deserialize<T>(body, JsonOptions)
               ?? throw new InvalidOperationException($"Invalid JSON from {relativeUrl}.");
    }

    internal sealed class ContentDto
    {
        public string id { get; set; } = "";
        public string title { get; set; } = "";
        public SpaceDto? space { get; set; }
        public BodyDto? body { get; set; }
    }

    internal sealed class SpaceDto
    {
        public string key { get; set; } = "";
    }

    internal sealed class BodyDto
    {
        public StorageDto? storage { get; set; }
    }

    internal sealed class StorageDto
    {
        public string value { get; set; } = "";
    }

    internal sealed class ContentListDto
    {
        public List<ContentDto>? results { get; set; }
        public int? start { get; set; }
        public int? limit { get; set; }
        public int? size { get; set; }
    }
}
