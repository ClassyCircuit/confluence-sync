namespace ConfluenceSync;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var config = AppConfig.Hardcoded();
        var cancellationToken = CancellationToken.None;

        try
        {
            var email = Env.Required("confl_email");
            var apiKey = Env.Required("confl_api_key");

            using var http = ConfluenceRestClient.CreateHttpClient(config.BaseUri, email, apiKey);
            var client = new ConfluenceRestClient(http);

            var walker = new PageTreeWalker(client);
            var pages = await walker.FetchTree(config.RootPageId, cancellationToken);

            var root = pages.First(p => p.Id == config.RootPageId);
            if (!string.Equals(root.SpaceKey, config.SpaceKey, StringComparison.OrdinalIgnoreCase))
            {
                Console.Error.WriteLine(
                    $"Root page space mismatch. Expected '{config.SpaceKey}', got '{root.SpaceKey}'.");
                return 2;
            }

            Console.WriteLine($"Fetched {pages.Count} page(s). Converting to Markdown...");

            var exporter = new PageExporter(config, new MarkdownConverter());
            await exporter.ExportAll(pages, root.Title, cancellationToken);

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return 1;
        }
    }
}
