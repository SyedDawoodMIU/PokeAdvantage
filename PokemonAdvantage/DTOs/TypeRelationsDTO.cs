using Newtonsoft.Json;

namespace PokemonAdvantage.DTOs
{

    public class TypeRelationsDTO
    {
        [JsonProperty("damage_relations")]
        public DamageRelationsDTO DamageRelations { get; set; }

    }

}

