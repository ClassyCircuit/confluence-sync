using System.Text;

namespace ConfluenceSync;

internal sealed class PageExporter
{
    private readonly AppConfig _config;
    private readonly MarkdownConverter _markdown;

    public PageExporter(AppConfig config, MarkdownConverter markdown)
    {
        _config = config;
        _markdown = markdown;
    }

    public async Task ExportAll(
        IReadOnlyList<ConfluencePage> pages,
        string rootTitle,
        CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_config.OutputDirectory);

        var filenameBuilder = new FilenameBuilder(rootTitle);

        foreach (var page in pages)
        {
            var markdown = BuildMarkdown(page);
            var filename = filenameBuilder.BuildMarkdownFilename(page);
            var filePath = Path.Combine(_config.OutputDirectory, filename);

            await File.WriteAllTextAsync(filePath, markdown, Encoding.UTF8, cancellationToken);
            Console.WriteLine($"Wrote {filePath}");
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
