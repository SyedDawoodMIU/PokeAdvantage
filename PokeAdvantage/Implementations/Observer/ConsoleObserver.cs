using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage.Implementation
{

    public class ConsoleObserver : IObserver
    {
        private IErrorHandler _errorHandler;

        public ConsoleObserver()
        {
        }

        public ConsoleObserver(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public void Update(PokemonContext pokemonContext)
        {
            try
            {
                Console.WriteLine($"Pokemon type {pokemonContext.CurrentType} has:");
                Console.WriteLine($"Strengths Against: {string.Join(",", pokemonContext.Pokemon.Strengths)}");
                Console.WriteLine($"Weaknesses Against: {string.Join(",", pokemonContext.Pokemon.Weaknesses)}");
            }
            catch (Exception)
            {
                _errorHandler.HandleError(new Exception("Error updating console"));
            }
        }
    }
}