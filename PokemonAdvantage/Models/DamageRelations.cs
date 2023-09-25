namespace PokemonAdvantage.Models
{


    public class DamageRelations
    {
        private DamageRelations(List<Damage> doubleDamageTo, List<Damage> doubleDamageFrom, List<Damage> halfDamageTo, List<Damage> halfDamageFrom, List<Damage> noDamageTo, List<Damage> noDamageFrom)
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

        public class DamageRelationsBuilder
        {
            private List<Damage> _doubleDamageFrom = new();
            private List<Damage> _doubleDamageTo = new();
            private List<Damage> _halfDamageFrom = new();
            private List<Damage> _halfDamageTo = new();
            private List<Damage> _noDamageFrom = new();
            private List<Damage> _noDamageTo = new();

            public DamageRelationsBuilder WithDoubleDamageFrom(List<Damage> damage)
            {
                _doubleDamageFrom = damage;
                return this;
            }

            public DamageRelationsBuilder WithDoubleDamageTo(List<Damage> damage)
            {
                _doubleDamageTo = damage;
                return this;
            }

            public DamageRelationsBuilder WithHalfDamageFrom(List<Damage> damage)
            {
                _halfDamageFrom = damage;
                return this;
            }

            public DamageRelationsBuilder WithHalfDamageTo(List<Damage> damage)
            {
                _halfDamageTo = damage;
                return this;
            }

            public DamageRelationsBuilder WithNoDamageFrom(List<Damage> damage)
            {
                _noDamageFrom = damage;
                return this;
            }

            public DamageRelationsBuilder WithNoDamageTo(List<Damage> damage)
            {
                _noDamageTo = damage;
                return this;
            }

            public DamageRelations Build()
            {
                return new DamageRelations(
                    _doubleDamageTo,
                    _doubleDamageFrom,
                    _halfDamageTo,
                    _halfDamageFrom,
                    _noDamageTo,
                    _noDamageFrom
                );
            }
        }
    }
}
