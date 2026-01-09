namespace ConfluenceSync;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var config = AppConfig.Hardcoded();
        var cancellationToken = CancellationToken.None;
        var log = new ConsoleLogger();

        try
        {
            log.Info("Starting Confluence sync...");

            var email = Env.Required("confl_email");
            var apiKey = Env.Required("confl_api_key");

            using var http = ConfluenceRestClient.CreateHttpClient(config.BaseUri, email, apiKey);
            var client = new ConfluenceRestClient(http, log);

            var walker = new PageTreeWalker(client, log);
            var pages = await walker.FetchTree(config.RootPageId, cancellationToken);

            var root = pages.First(p => p.Id == config.RootPageId);
            if (!string.Equals(root.SpaceKey, config.SpaceKey, StringComparison.OrdinalIgnoreCase))
            {
                Console.Error.WriteLine(
                    $"Root page space mismatch. Expected '{config.SpaceKey}', got '{root.SpaceKey}'.");
                return 2;
            }

            log.Info($"Space verified: '{root.SpaceKey}'");
            log.Info($"Fetched {pages.Count} page(s). Exporting...");

            var exporter = new PageExporter(config, new MarkdownConverter(log), log);
            await exporter.ExportAll(pages, root.Title, cancellationToken);

            log.Info("Done.");
            return 0;
        }
        catch (Exception ex)
        {
            log.Error(ex, "Sync failed.");
            return 1;
        }
    }
}
