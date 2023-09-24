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
                (string strengths, string weaknesses) = FormatPowers(pokemonContext);

                Console.WriteLine("\n");
                Console.WriteLine($"Pokemon type {pokemonContext.CurrentType} has:");
                Console.WriteLine($"Strengths Against: {strengths}");
                Console.WriteLine($"Weaknesses Against: {weaknesses}");
                Console.WriteLine("\n");
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error updating console"));
            }
        }

        private static (string, string) FormatPowers(PokemonContext pokemonContext)
        {
            string strengths;
            if (pokemonContext.Pokemon.Strengths != null && pokemonContext.Pokemon.Strengths.Count > 0)
            {
                strengths = string.Join(", ", pokemonContext.Pokemon.Strengths);
            }
            else
            {
                strengths = "No strengths";
            }

            string weaknesses;
            if (pokemonContext.Pokemon.Weaknesses != null && pokemonContext.Pokemon.Weaknesses.Count > 0)
            {
                weaknesses = string.Join(",", pokemonContext.Pokemon.Weaknesses);
            }
            else
            {
                weaknesses = "No weaknesses";
            }

            return (strengths, weaknesses);
        }
    }
}