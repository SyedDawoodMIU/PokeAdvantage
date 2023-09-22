using System.Net.Http;
using PokeAdvantage.Interfaces;
namespace PokeAdvantage
{

    public class PokemonApiClient : IPokemonApiClient
    {

        private static readonly string _baseUrl = "https://pokeapi.co/api/v2/";
        private IApiService _apiService;
        public PokemonApiClient(IApiService apiService)
        {
            _apiService = apiService;
        }


        public async Task<string> GetPokemonAsync(string pokemonName)
        {
            return await _apiService.Fetch($"{_baseUrl}pokemon/{pokemonName}/");
        }

        public async Task<string> GetTypeRelationsAsync(string typeName)
        {
            return await _apiService.Fetch($"{_baseUrl}type/{typeName}/");
        }
    }
}
