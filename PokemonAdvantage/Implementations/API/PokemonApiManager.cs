using Newtonsoft.Json;
using PokemonAdvantage.DTOs;
using PokemonAdvantage.Interfaces;

namespace PokemonAdvantage.Implementation.Business
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

            string jsonResponse;
            try
            {
                jsonResponse = await _pokemonApiClient.GetPokemonAsync(pokemonName);
                if (jsonResponse != null)
                {
                    return _jsonHelper.Deserialize<PokemonDTO>(jsonResponse);
                }
                else
                {
                    _errorHandler.HandleError(new Exception("Empty response from API"));
                    return default!;
                }
            }
            catch (Exception ex)
            {

                _errorHandler.HandleError(new Exception("Error fetching Pokemon data from API"));
                return default!;
            }

        }

        public async Task<TypeRelationsDTO> FetchTypeRelationsAsync(string type)
        {
            string jsonResponse;
            try
            {
                jsonResponse = await _pokemonApiClient.GetTypeRelationsAsync(type);
                if (jsonResponse != null)
                {
                    return _jsonHelper.Deserialize<TypeRelationsDTO>(jsonResponse);
                }
                else
                {
                    _errorHandler.HandleError(new Exception("Empty response from API"));
                    return default!;
                }
            }
            catch (Exception ex)
            {

                _errorHandler.HandleError(new Exception("Error fetching Type data from API"));
                return null!;
            }
        }
    }

}