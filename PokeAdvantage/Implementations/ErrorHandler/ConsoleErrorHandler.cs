using PokeAdvantage.Interfaces;

namespace PokeAdvantage.Implementation.ErrorHandler
{
    public class ConsoleErrorHandler : IErrorHandler
    {
        public void HandleError(Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}