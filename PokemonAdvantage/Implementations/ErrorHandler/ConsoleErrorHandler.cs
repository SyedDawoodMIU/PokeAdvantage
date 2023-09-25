using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Interfaces.Logging;

namespace PokemonAdvantage.Implementation.ErrorHandler
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