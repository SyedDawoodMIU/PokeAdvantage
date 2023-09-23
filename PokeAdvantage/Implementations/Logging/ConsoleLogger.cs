using PokeAdvantage.@enum;
using PokeAdvantage.Interfaces.Logging;

namespace PokeAdvantage.Implementations.Logging
{
    public class ConsoleLogger : ILogger
    {
        private LogLevel _logLevel;

        public void SetLogLevel(LogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public void LogInformation(string message)
        {
            if (_logLevel >= LogLevel.Information)
            {
                Console.WriteLine($"[INFO] {message}");
            }
        }

        public void LogWarning(string message)
        {
            if (_logLevel >= LogLevel.Warning)
            {
                Console.WriteLine($"[WARNING] {message}");
            }
        }

        public void LogError(string message)
        {
            if (_logLevel >= LogLevel.Error)
            {
                Console.WriteLine($"[ERROR] {message}");
            }
        }
    }
}