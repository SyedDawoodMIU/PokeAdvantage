using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Models;
using PokemonAdvantage.Implementation.Business;
using static PokemonAdvantage.Models.DamageRelations;

namespace PokemonAdvantage.Tests
{
    public class BasicDamageCalculatorTests
    {
        [Fact]
        public void CalculateDamage_ShouldPopulateStrengthsAndWeaknesses()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var damageCalculator = new BasicDamageCalculator(mockErrorHandler.Object);
            var pokemonContext = CreatePokemonContext();

            // Act
            damageCalculator.CalculateDamage(pokemonContext);

            // Assert
            Assert.Contains("ghost", pokemonContext.Pokemon.Strengths);
            Assert.Contains("ground", pokemonContext.Pokemon.Strengths);
            Assert.Contains("ice", pokemonContext.Pokemon.Strengths);
            Assert.Contains("water", pokemonContext.Pokemon.Weaknesses);
            Assert.Contains("electric", pokemonContext.Pokemon.Weaknesses);
            Assert.Contains("steel", pokemonContext.Pokemon.Weaknesses);
        }

        [Fact]
        public void CalculateDamage_ShouldHandleError_WhenExceptionOccurs()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var damageCalculator = new BasicDamageCalculator(mockErrorHandler.Object);
            PokemonContext pokemonContext = null;

            // Act
            damageCalculator.CalculateDamage(pokemonContext);

            // Assert
            mockErrorHandler.Verify(handler => handler.HandleError(It.IsAny<Exception>()), Times.Once);
        }

        [Fact]
        public void DetermineStrengths_ShouldReturnStrengthsBasedOnDamageRelations()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var damageCalculator = new BasicDamageCalculator(mockErrorHandler.Object);
            var damageRelations = CreatePokemonContext().TypeRelations.DamageRelations;

            // Act
            var strengths = damageCalculator.DetermineStrengths(damageRelations);

            // Assert
            Assert.Equal(3, strengths.Count);
            Assert.Contains("ground", strengths);
            Assert.Contains("ghost", strengths);
            Assert.Contains("ice", strengths);
        }

        [Fact]
        public void DetermineWeaknesses_ShouldReturnWeaknessesBasedOnDamageRelations()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var damageCalculator = new BasicDamageCalculator(mockErrorHandler.Object);
            var damageRelations = CreatePokemonContext().TypeRelations.DamageRelations;

            // Act
            var weaknesses = damageCalculator.DetermineWeaknesses(damageRelations);

            // Assert
            Assert.Equal(3, weaknesses.Count);
            Assert.Contains("electric", weaknesses);
            Assert.Contains("water", weaknesses);
            Assert.Contains("steel", weaknesses);
        }

        private static PokemonContext CreatePokemonContext()
        {
            return new PokemonContext
            {
                TypeRelations = new TypeRelations
                (new DamageRelationsBuilder()
                       .WithDoubleDamageTo(new List<Damage> { new("ground", "url") })
                       .WithDoubleDamageFrom(new List<Damage> { new("electric", "url") })
                       .WithHalfDamageTo(new List<Damage> { new("water", "url") })
                       .WithHalfDamageFrom(new List<Damage> { new("ghost", "url") })
                       .WithNoDamageTo(new List<Damage> { new("steel", "url") })
                       .WithNoDamageFrom(new List<Damage> { new("ice", "url") })
                       .Build()
                ),
                Pokemon = new Pokemon("charizard", new List<string>() { "fire", "flying" })
            };
        }
    }
}
