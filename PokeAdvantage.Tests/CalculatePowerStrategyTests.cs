using Xunit;
using Moq;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;
using PokeAdvantage.Strategy;

namespace PokeAdvantage.Tests
{
    public class CalculatePowerStrategyTests
    {
        [Fact]
        public void Execute_CallsCalculateDamage_OfDamageCalculator()
        {
            // Arrange
            var mockDamageCalculator = new Mock<IDamageCalculator>();
            var strategy = new CalculatePowerStrategy(mockDamageCalculator.Object);
            var context = new PokemonContext();

            // Act
            strategy.Execute(context);

            // Assert
            mockDamageCalculator.Verify(m => m.CalculateDamage(context), Times.Once);
        }
    }
}
