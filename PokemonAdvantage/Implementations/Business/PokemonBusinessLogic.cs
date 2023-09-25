using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Models;

namespace PokemonAdvantage.Implementation.Business
{
    public class PokemonBusinessLogic : IPokemonBusinessLogic
    {
        private readonly IPokemonStrategy _pokemonStrategy;
        private readonly IErrorHandler _errorHandler;

        public PokemonBusinessLogic(IPokemonStrategy pokemonStrategy, IErrorHandler errorHandler)
        {
            _pokemonStrategy = pokemonStrategy;
            _errorHandler = errorHandler;
        }

        public void ApplyPokemonStrategy(PokemonContext context)
        {
            try
            {
                _pokemonStrategy.Execute(context);
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error applying pokemon strategy"));
            }
        }
    }

}