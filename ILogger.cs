namespace ConfluenceSync;

internal interface ILogger
{
    void Info(string message);
    void Warn(string message);
    void Error(string message);
    void Error(Exception ex, string message);
}
