using PokeAdvantage.Interfaces;
namespace PokeAdvantage
{

    public class PokemonApiClient : IPokemonApiClient
    {

        private readonly IErrorHandler _errorHandler;
        private static readonly string _baseUrl = "https://pokeapi.co/api/v2/";
        private IApiService _apiService;
        public PokemonApiClient(IApiService apiService, IErrorHandler errorHandler)
        {
            _apiService = apiService;
            _errorHandler = errorHandler;
        }


        public async Task<string> GetPokemonAsync(string pokemonName)
        {
            try
            {
                return await _apiService.Fetch($"{_baseUrl}pokemon/{pokemonName}/");
            }
            catch (Exception)
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
            catch (Exception)
            {
                _errorHandler.HandleError(new Exception("Error fetching Type data from API"));
                return default!;
            }
        }
    }
}
