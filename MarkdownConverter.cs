using ReverseMarkdown;

namespace ConfluenceSync;

internal sealed class MarkdownConverter
{
    private readonly Converter _converter;
    private readonly ILogger? _log;

    public MarkdownConverter(ILogger? log = null)
    {
        _log = log;
        _converter = new Converter(new Config
        {
            UnknownTags = Config.UnknownTagsOption.Bypass,
            GithubFlavored = true,
            RemoveComments = true
        });
    }

    public string ConvertStorageHtml(string html)
    {
        try
        {
            return _converter.Convert(html ?? string.Empty);
        }
        catch (Exception ex)
        {
            _log?.Error(ex, "Markdown conversion failed.");
            throw;
        }
    }
}
