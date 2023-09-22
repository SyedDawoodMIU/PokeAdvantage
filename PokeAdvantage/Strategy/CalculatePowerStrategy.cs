using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage.Strategy
{
    public class CalculatePowerStrategy : IPokemonStrategy
    {
         private readonly IDamageCalculator _damageCalculator;

        public CalculatePowerStrategy(IDamageCalculator damageCalculator)
        {
            _damageCalculator = damageCalculator;
        }

        public void Execute(PokemonContext context)
        {
             _damageCalculator.CalculateDamage(context);
        }
    }
}