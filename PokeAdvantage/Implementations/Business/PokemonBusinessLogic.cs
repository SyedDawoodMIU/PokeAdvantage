using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage.Implementation.Business
{
    public class PokemonBusinessLogic : IPokemonBusinessLogic
    {
        private readonly IPokemonStrategy _pokemonStrategy;

        public PokemonBusinessLogic(IPokemonStrategy pokemonStrategy)
        {
            _pokemonStrategy = pokemonStrategy;
        }

        public void ApplyPokemonStrategy(PokemonContext context)
        {
            _pokemonStrategy.Execute(context);
        }
    }

}