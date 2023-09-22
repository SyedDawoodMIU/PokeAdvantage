using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage
{
    public interface IObserver
    {
        void Update(PokemonContext pokemonContext);
    }

}
