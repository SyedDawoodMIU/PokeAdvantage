using Newtonsoft.Json;

namespace PokeAdvantage.DTOs
{

    public class TypeRelationsDTO
    {
        [JsonProperty("damage_relations")]
        public DamageRelationsDTO DamageRelations { get; set; }

    }

}

