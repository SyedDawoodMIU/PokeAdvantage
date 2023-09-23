
using PokeAdvantage.@enum;

namespace PokeAdvantage.Interfaces.Logging
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void SetLogLevel(LogLevel logLevel);
    }
}