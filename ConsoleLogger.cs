namespace ConfluenceSync;

internal sealed class ConsoleLogger : ILogger
{
    private readonly object _lock = new();

    public void Info(string message) => Write("INFO", message);

    public void Warn(string message) => Write("WARN", message);

    public void Error(string message) => Write("ERROR", message);

    public void Error(Exception ex, string message)
    {
        Write("ERROR", message);
        Write("ERROR", ex.ToString());
    }

    private void Write(string level, string message)
    {
        lock (_lock)
        {
            Console.WriteLine($"{DateTimeOffset.UtcNow:O} [{level}] {message}");
        }
    }
}
