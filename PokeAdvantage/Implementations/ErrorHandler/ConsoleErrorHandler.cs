using PokeAdvantage.Interfaces;

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
            _logger.LogError($"An error occurred: {ex.Message}");
        }
    }
}