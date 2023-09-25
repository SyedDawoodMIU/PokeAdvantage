using PokemonAdvantage.Models;

namespace PokemonAdvantage.Interfaces
{
    public interface IObserver
    {
        void Update(PokemonContext pokemonContext);
    }

}
