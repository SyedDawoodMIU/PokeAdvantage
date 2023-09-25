using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Models;

namespace PokemonAdvantage.Strategy
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