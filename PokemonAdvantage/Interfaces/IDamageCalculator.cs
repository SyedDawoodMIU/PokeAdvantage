using PokemonAdvantage.Models;

namespace PokemonAdvantage.Interfaces
{
    public interface IDamageCalculator
    {
        void CalculateDamage(PokemonContext pokemonContext);
    }
}