using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage.Implementation
{

    public class ConsoleObserver : IObserver
    {
        public void Update(PokemonContext pokemonContext)
        {
            Console.WriteLine($"Pokemon type {pokemonContext.CurrentType} has:");
            Console.WriteLine($"Strengths Against: {string.Join(",", pokemonContext.Pokemon.Strengths)}");
            Console.WriteLine($"Weaknesses Against: {string.Join(",", pokemonContext.Pokemon.Weaknesses)}");
        }


    }
}