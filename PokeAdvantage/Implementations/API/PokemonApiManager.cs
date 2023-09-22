using Newtonsoft.Json;
using PokeAdvantage.DTOs;

namespace PokeAdvantage.Implementation.Business
{
    public class PokemonApiManager : IPokemonApiManager
    {
        private readonly IPokemonApiClient _pokemonApiClient;

        public PokemonApiManager(IPokemonApiClient pokemonApiClient)
        {
            _pokemonApiClient = pokemonApiClient;
        }

        public async Task<PokemonDTO?> FetchPokemonData(string pokemonName)
        {
            string jsonResponse = await _pokemonApiClient.GetPokemonAsync(pokemonName);
            return JsonConvert.DeserializeObject<PokemonDTO>(jsonResponse);
        }

        public async Task<TypeRelationsDTO> FetchTypeRelationsAsync(string type)
        {
            string jsonResponse = await _pokemonApiClient.GetTypeRelationsAsync(type);
            return JsonConvert.DeserializeObject<TypeRelationsDTO>(jsonResponse);
        }
    }

}