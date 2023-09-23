using Newtonsoft.Json;
using PokeAdvantage.DTOs;
using PokeAdvantage.Interfaces;

namespace PokeAdvantage.Implementation.Business
{
    public class PokemonApiManager : IPokemonApiManager
    {
        private readonly IPokemonApiClient _pokemonApiClient;
        private readonly IErrorHandler _errorHandler;
        private readonly IJsonHelper _jsonHelper;

        public PokemonApiManager(IPokemonApiClient pokemonApiClient, IErrorHandler errorHandler, IJsonHelper jsonHelper)
        {
            _pokemonApiClient = pokemonApiClient;
            _errorHandler = errorHandler;
            _jsonHelper = jsonHelper;


        }

        public async Task<PokemonDTO?> FetchPokemonData(string pokemonName)
        {

            try
            {
                string jsonResponse = await _pokemonApiClient.GetPokemonAsync(pokemonName);
                return _jsonHelper.Deserialize<PokemonDTO>(jsonResponse);
            }
            catch (Exception)
            {

                _errorHandler.HandleError(new Exception("Error fetching Pokemon data from API"));
                return null;
            }

        }

        public async Task<TypeRelationsDTO> FetchTypeRelationsAsync(string type)
        {
            try
            {
                string jsonResponse = await _pokemonApiClient.GetTypeRelationsAsync(type);
                return _jsonHelper.Deserialize<TypeRelationsDTO>(jsonResponse);
            }
            catch (Exception)
            {

                _errorHandler.HandleError(new Exception("Error fetching Type data from API"));
                return null!;
            }
        }
    }

}