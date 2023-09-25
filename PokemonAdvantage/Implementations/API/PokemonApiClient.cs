using Microsoft.Extensions.Options;
using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Models;
namespace PokemonAdvantage
{

    public class PokemonApiClient : IPokemonApiClient
    {

        private readonly IErrorHandler _errorHandler;
        private readonly string _baseUrl;
        private IApiService _apiService;
        public PokemonApiClient(IApiService apiService, IErrorHandler errorHandler, IOptions<PokemonApiSettings> settings)
        {
            _apiService = apiService;
            _errorHandler = errorHandler;
            _baseUrl = settings.Value.BaseUrl;
        }


        public async Task<string> GetPokemonAsync(string pokemonName)
        {
            try
            {
                return await _apiService.Fetch($"{_baseUrl}pokemon/{pokemonName}/");
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error fetching Pokemon data from API"));
                return default!;
            }
        }

        public async Task<string> GetTypeRelationsAsync(string typeName)
        {
            try
            {
                return await _apiService.Fetch($"{_baseUrl}type/{typeName}/");
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error fetching Type data from API"));
                return default!;
            }
        }
    }
}
