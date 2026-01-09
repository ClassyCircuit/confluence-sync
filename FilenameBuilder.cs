using System.Text;

namespace ConfluenceSync;

internal sealed class FilenameBuilder
{
    private readonly string _rootTitle;

    public FilenameBuilder(string rootTitle)
    {
        _rootTitle = rootTitle;
    }

    public string BuildMarkdownFilename(ConfluencePage page)
    {
        var parts = new List<string>();

        if (string.Equals(page.Title, _rootTitle, StringComparison.OrdinalIgnoreCase) && page.AncestorTitles.Count == 0)
        {
            parts.Add(_rootTitle);
        }
        else
        {
            parts.Add(_rootTitle);
            foreach (var ancestor in page.AncestorTitles)
            {
                if (string.Equals(ancestor, _rootTitle, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                parts.Add(ancestor);
            }

            parts.Add(page.Title);
        }

        var rawName = string.Join("_", parts);
        return NormalizeFilename(rawName) + ".md";
    }

    private static string NormalizeFilename(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        var sb = new StringBuilder(name.Length);

        foreach (var c in name)
        {
            sb.Append(invalid.Contains(c) ? '-' : c);
        }

        var trimmed = sb.ToString().Trim();
        return string.IsNullOrEmpty(trimmed) ? "untitled" : trimmed;
    }
}
