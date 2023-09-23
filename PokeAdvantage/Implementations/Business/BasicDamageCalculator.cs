using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage.Implementation.Business
{
    public class BasicDamageCalculator : IDamageCalculator
    {

        private IErrorHandler _errorHandler;
        public BasicDamageCalculator(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public void CalculateDamage(PokemonContext pokemonContext)
        {
            try
            {

                DamageRelations damageRelations = pokemonContext.TypeRelations.DamageRelations;
                pokemonContext.Pokemon.Strengths = DetermineStrengths(damageRelations);
                pokemonContext.Pokemon.Weaknesses = DetermineWeaknesses(damageRelations);
            }
            catch (Exception ex)
            {

                _errorHandler.HandleError(new Exception("Error calculating damage"));

            }
        }

        public List<string> DetermineStrengths(DamageRelations typeRelations)
        {
            try
            {
                List<string> strengths = new();
                strengths.AddRange(typeRelations.DoubleDamageTo.Select(x => x.Name));
                strengths.AddRange(typeRelations.NoDamageFrom.Select(x => x.Name));
                strengths.AddRange(typeRelations.HalfDamageFrom.Select(x => x.Name));
                return strengths.Distinct().ToList();
            }
            catch (Exception ex)
            {

                _errorHandler.HandleError(new Exception("Error calculating strengths"));
                return default!;

            }
        }

        public List<string> DetermineWeaknesses(DamageRelations typeRelations)
        {
            try
            {
                List<string> weaknesses = new();
                weaknesses.AddRange(typeRelations.NoDamageTo.Select(x => x.Name));
                weaknesses.AddRange(typeRelations.HalfDamageTo.Select(x => x.Name));
                weaknesses.AddRange(typeRelations.DoubleDamageFrom.Select(x => x.Name));
                return weaknesses.Distinct().ToList();
            }
            catch (Exception ex)
            {

                _errorHandler.HandleError(new Exception("Error calculating weaknesses"));
                return default!;
            }
        }
    }

}