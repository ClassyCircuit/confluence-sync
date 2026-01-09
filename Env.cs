namespace ConfluenceSync;

internal static class Env
{
    public static string Required(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Missing required env var: {name}");
        }

        return value;
    }
}
