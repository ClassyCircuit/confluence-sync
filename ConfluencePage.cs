namespace ConfluenceSync;

internal sealed record ConfluencePage(
    string Id,
    string Title,
    string SpaceKey,
    string StorageHtml,
    IReadOnlyList<string> AncestorTitles);
