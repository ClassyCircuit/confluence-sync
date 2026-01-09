using ReverseMarkdown;

namespace ConfluenceSync;

internal sealed class MarkdownConverter
{
    private readonly Converter _converter;

    public MarkdownConverter()
    {
        _converter = new Converter(new Config
        {
            UnknownTags = Config.UnknownTagsOption.Bypass,
            GithubFlavored = true,
            RemoveComments = true
        });
    }

    public string ConvertStorageHtml(string html)
    {
        return _converter.Convert(html ?? string.Empty);
    }
}
