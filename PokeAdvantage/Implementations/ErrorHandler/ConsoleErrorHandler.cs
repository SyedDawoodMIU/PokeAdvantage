using PokeAdvantage.Interfaces;
using PokeAdvantage.Interfaces.Logging;

namespace PokeAdvantage.Implementation.ErrorHandler
{
    public class ConsoleErrorHandler : IErrorHandler
    {
        private readonly ILogger _logger;

        public ConsoleErrorHandler(ILogger logger)
        {
            _logger = logger;
        }
        public void HandleError(Exception ex)
        {
            if (ex is null)
            {
                _logger.LogError("The exception is null");
                return;
            }
            _logger.LogError($"An error occurred: {ex.Message}");
        }
    }
}