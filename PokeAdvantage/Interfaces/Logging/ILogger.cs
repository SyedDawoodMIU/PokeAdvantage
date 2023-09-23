
using PokeAdvantage.@enum;

public interface ILogger
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
    void SetLogLevel(LogLevel logLevel);
}