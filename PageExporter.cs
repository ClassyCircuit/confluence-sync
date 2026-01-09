using System.Text;

namespace ConfluenceSync;

internal sealed class PageExporter
{
    private readonly AppConfig _config;
    private readonly MarkdownConverter _markdown;
    private readonly ILogger _log;

    public PageExporter(AppConfig config, MarkdownConverter markdown, ILogger log)
    {
        _config = config;
        _markdown = markdown;
        _log = log;
    }

    public async Task ExportAll(
        IReadOnlyList<ConfluencePage> pages,
        string rootTitle,
        CancellationToken cancellationToken)
    {
        _log.Info($"Ensuring output directory exists: '{_config.OutputDirectory}'");
        Directory.CreateDirectory(_config.OutputDirectory);

        var filenameBuilder = new FilenameBuilder(rootTitle);

        foreach (var page in pages)
        {
            var filename = filenameBuilder.BuildMarkdownFilename(page);
            var filePath = Path.Combine(_config.OutputDirectory, filename);

            try
            {
                _log.Info($"Converting page '{page.Title}' ({page.Id}) -> {filename}");

                var markdown = BuildMarkdown(page);
                await File.WriteAllTextAsync(filePath, markdown, Encoding.UTF8, cancellationToken);

                _log.Info($"Wrote {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed exporting page '{page.Title}' ({page.Id}) to '{filePath}'.", ex);
            }
        }
    }

    private string BuildMarkdown(ConfluencePage page)
    {
        var header = new StringBuilder();
        header.AppendLine($"# {page.Title}");
        header.AppendLine();
        header.AppendLine($"Confluence: {_config.BaseUri}/pages/viewpage.action?pageId={page.Id}");
        header.AppendLine();

        var body = _markdown.ConvertStorageHtml(page.StorageHtml);

        return header + body.Trim() + "\n";
    }
}
