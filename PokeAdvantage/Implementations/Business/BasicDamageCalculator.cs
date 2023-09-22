using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage.Implementation.Business
{
    public class BasicDamageCalculator : IDamageCalculator
    {
        public void CalculateDamage(PokemonContext pokemonContext)
        {

            DamageRelations damageRelations = pokemonContext.TypeRelations.DamageRelations;
            pokemonContext.Pokemon.Strengths = DetermineStrengths(damageRelations);
            pokemonContext.Pokemon.Weaknesses = DetermineWeaknesses(damageRelations);
        }

        private static List<string> DetermineStrengths(DamageRelations typeRelations)
        {
            List<string> strengths = new();
            strengths.AddRange(typeRelations.DoubleDamageTo.Select(x => x.Name));
            strengths.AddRange(typeRelations.NoDamageFrom.Select(x => x.Name));
            strengths.AddRange(typeRelations.HalfDamageFrom.Select(x => x.Name));
            return strengths.Distinct().ToList();
        }

        private static List<string> DetermineWeaknesses(DamageRelations typeRelations)
        {
            List<string> weaknesses = new();
            weaknesses.AddRange(typeRelations.NoDamageTo.Select(x => x.Name));
            weaknesses.AddRange(typeRelations.HalfDamageTo.Select(x => x.Name));
            weaknesses.AddRange(typeRelations.DoubleDamageFrom.Select(x => x.Name));
            return weaknesses.Distinct().ToList();
        }
    }

}