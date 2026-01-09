namespace ConfluenceSync;

internal sealed record AppConfig(
    Uri BaseUri,
    string SpaceKey,
    string RootPageId,
    string OutputDirectory)
{
    public static AppConfig Hardcoded() => new(
        BaseUri: new Uri("https://malvum.atlassian.net/wiki"),
        SpaceKey: "~557058ff7ebda7b4834355a36a176a142ec712",
        RootPageId: "7405569",
        OutputDirectory: "synced");
}
