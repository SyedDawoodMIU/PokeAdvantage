using PokeAdvantage.Models;

namespace PokeAdvantage.Interfaces
{
    public interface IObserver
    {
        void Update(PokemonContext pokemonContext);
    }

}
