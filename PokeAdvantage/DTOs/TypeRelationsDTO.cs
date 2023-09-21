using Newtonsoft.Json;

namespace PokeAdvantage.DTOs
{

    public class DamageTypeDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class DamageRelationsDTO
    {
        [JsonProperty("double_damage_from")]
        public List<DamageTypeDTO> DoubleDamageFrom { get; set; }

        [JsonProperty("double_damage_to")]
        public List<DamageTypeDTO> DoubleDamageTo { get; set; }

        [JsonProperty("half_damage_from")]
        public List<DamageTypeDTO> HalfDamageFrom { get; set; }

        [JsonProperty("half_damage_to")]
        public List<DamageTypeDTO> HalfDamageTo { get; set; }

        [JsonProperty("no_damage_from")]
        public List<DamageTypeDTO> NoDamageFrom { get; set; }

        [JsonProperty("no_damage_to")]
        public List<DamageTypeDTO> NoDamageTo { get; set; }
    }

    public class TypeRelationsDTO
    {
        [JsonProperty("damage_relations")]
        public DamageRelationsDTO DamageRelations { get; set; }
    }

}

