using PokeAdvantage.Models;

namespace PokeAdvantage.Interfaces
{
    public interface IPokemonStrategy
    {
        void Execute(PokemonContext context);
    }

}