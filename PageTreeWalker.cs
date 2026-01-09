namespace ConfluenceSync;

internal sealed class PageTreeWalker
{
    private readonly ConfluenceRestClient _client;
    private readonly ILogger _log;

    public PageTreeWalker(ConfluenceRestClient client, ILogger log)
    {
        _client = client;
        _log = log;
    }

    public async Task<IReadOnlyList<ConfluencePage>> FetchTree(string rootPageId, CancellationToken cancellationToken)
    {
        _log.Info($"Fetching root page {rootPageId}...");

        var rootDto = await _client.GetPageById(rootPageId, cancellationToken);
        var root = ToPage(rootDto, ancestorTitles: Array.Empty<string>());

        _log.Info($"Root: '{root.Title}' ({root.Id})");

        var results = new List<ConfluencePage> { root };
        var stack = new Stack<ConfluencePage>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            _log.Info($"Listing children of '{current.Title}' ({current.Id})");

            await foreach (var childDto in _client.GetChildPages(current.Id, cancellationToken))
            {
                var childAncestors = current.AncestorTitles.Concat(new[] { current.Title }).ToArray();
                var child = ToPage(childDto, childAncestors);

                results.Add(child);
                stack.Push(child);

                _log.Info($"Discovered: '{child.Title}' ({child.Id})");
            }
        }

        _log.Info($"Total pages discovered: {results.Count}");
        return results;
    }

    private static ConfluencePage ToPage(ConfluenceRestClient.ContentDto dto, IReadOnlyList<string> ancestorTitles)
    {
        return new ConfluencePage(
            Id: dto.id,
            Title: dto.title,
            SpaceKey: dto.space?.key ?? "",
            StorageHtml: dto.body?.storage?.value ?? "",
            AncestorTitles: ancestorTitles);
    }
}
