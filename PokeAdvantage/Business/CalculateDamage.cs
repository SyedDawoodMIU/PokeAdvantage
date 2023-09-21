using PokeAdvantage.DTOs;
using PokeAdvantage.Models;

namespace PokeAdvantage.Business
{
    public class CalculateDamage
    {
        public static List<string> DetermineStrengths(TypeRelations typeRelations)
        {
            List<string> strengths = new();
            strengths.AddRange(typeRelations.DoubleDamageTo.Select(x => x.Name));
            strengths.AddRange(typeRelations.NoDamageFrom.Select(x => x.Name));
            strengths.AddRange(typeRelations.HalfDamageFrom.Select(x => x.Name));
            return strengths.Distinct().ToList();
        }

        public static List<string> DetermineWeaknesses(TypeRelations typeRelations)
        {
            List<string> weaknesses = new();
            weaknesses.AddRange(typeRelations.NoDamageTo.Select(x => x.Name));
            weaknesses.AddRange(typeRelations.HalfDamageTo.Select(x => x.Name));
            weaknesses.AddRange(typeRelations.DoubleDamageFrom.Select(x => x.Name));
            return weaknesses.Distinct().ToList();
        }
    }
}