using System.Net.Http;
namespace PokeAdvantage
{

    public class PokemonApiClient
    {
        private static readonly string _baseUrl = "https://pokeapi.co/api/v2/";

        public static async Task<string> GetPokemonJsonAsync(string pokemonName)
        {
            return await ApiService.Fetch($"{_baseUrl}pokemon/{pokemonName}/");
        }

        public static async Task<string> GetTypeRelationsJsonAsync(string typeName)
        {
            return await ApiService.Fetch($"{_baseUrl}type/{typeName}/");
        }
    }
}
