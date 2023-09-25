
using Newtonsoft.Json;

namespace PokemonAdvantage.DTOs
{
    public class PokemonDTO
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("types")]
        public List<ApiPokemonType> Types { get; set; }

    }

    public class ApiPokemonType
    {
        [JsonProperty("type")]
        public ApiPokemonTypeDetail Type { get; set; }
    }

    public class ApiPokemonTypeDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

    }
}