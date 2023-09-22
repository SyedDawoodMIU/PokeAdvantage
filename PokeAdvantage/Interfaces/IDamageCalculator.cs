using PokeAdvantage.Models;

namespace PokeAdvantage.Interfaces
{
    public interface IDamageCalculator
    {
        void CalculateDamage(PokemonContext pokemonContext);
    }
}