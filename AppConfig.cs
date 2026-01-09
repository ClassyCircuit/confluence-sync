namespace ConfluenceSync;

internal sealed record AppConfig(
    Uri BaseUri,
    string SpaceKey,
    string RootPageId,
    string OutputDirectory)
{
    public static AppConfig Hardcoded() => new(
        BaseUri: new Uri("https://malvum.atlassian.net/wiki"),
        SpaceKey: "codingmuch",
        RootPageId: "7405569",
        OutputDirectory: "synced");
}
