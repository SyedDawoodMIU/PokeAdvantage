
namespace PokemonAdvantage.Models
{

    public class TypeRelations
    {
        public TypeRelations(DamageRelations damageRelations)
        {
            DamageRelations = damageRelations;
        }

        public DamageRelations DamageRelations { get; set; }

    }

}

