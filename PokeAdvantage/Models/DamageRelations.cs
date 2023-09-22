
namespace PokeAdvantage.Models
{
    public class DamageRelations
    {
        public DamageRelations(List<Damage> doubleDamageTo, List<Damage> doubleDamageFrom, List<Damage> halfDamageTo, List<Damage> halfDamageFrom, List<Damage> noDamageTo, List<Damage> noDamageFrom)
        {
            DoubleDamageTo = doubleDamageTo;
            DoubleDamageFrom = doubleDamageFrom;
            HalfDamageTo = halfDamageTo;
            HalfDamageFrom = halfDamageFrom;
            NoDamageTo = noDamageTo;
            NoDamageFrom = noDamageFrom;
        }

        public List<Damage> DoubleDamageFrom { get; set; }
        public List<Damage> DoubleDamageTo { get; set; }
        public List<Damage> HalfDamageFrom { get; set; }
        public List<Damage> HalfDamageTo { get; set; }
        public List<Damage> NoDamageFrom { get; set; }
        public List<Damage> NoDamageTo { get; set; }

    }

}

