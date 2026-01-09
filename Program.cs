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
            if (args.Any(a => string.Equals(a, "--help", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "-h", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("  dotnet run                Sync hardcoded page tree");
                Console.WriteLine("  dotnet run --list-space   List pages in hardcoded space");
                return 0;
            }

            var email = Env.Required("CONFL_EMAIL");
            var apiKey = Env.Required("CONFL_API_KEY");

            using var http = ConfluenceRestClient.CreateHttpClient(config.BaseUri, email, apiKey);
            var client = new ConfluenceRestClient(http, log);

            if (args.Any(a => string.Equals(a, "--list-space", StringComparison.OrdinalIgnoreCase)))
            {
                log.Info($"Listing pages in space '{config.SpaceKey}'...");

                var count = 0;
                await foreach (var dto in client.SearchPagesInSpace(config.SpaceKey, cancellationToken))
                {
                    count++;
                    Console.WriteLine($"{dto.id}\t{dto.title}");
                }

                log.Info($"Listed {count} page(s).");
                return 0;
            }

            log.Info("Starting Confluence sync...");

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
