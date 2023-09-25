using PokemonAdvantage.Models;

namespace PokemonAdvantage.Interfaces
{
    public interface IPokemonStrategy
    {
        void Execute(PokemonContext context);
    }

}