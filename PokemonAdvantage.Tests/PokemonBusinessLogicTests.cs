using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Models;
using PokemonAdvantage.Implementation.Business;

namespace PokemonAdvantage.Tests
{
    public class PokemonBusinessLogicTests
    {
        [Fact]
        public void ApplyPokemonStrategy_ShouldExecuteStrategy()
        {
            // Arrange
            var mockPokemonStrategy = new Mock<IPokemonStrategy>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var pokemonBusinessLogic = new PokemonBusinessLogic(mockPokemonStrategy.Object, mockErrorHandler.Object);
            var pokemonContext = new PokemonContext();

            // Act
            pokemonBusinessLogic.ApplyPokemonStrategy(pokemonContext);

            // Assert
            mockPokemonStrategy.Verify(strategy => strategy.Execute(pokemonContext), Times.Once);
        }

        [Fact]
        public void ApplyPokemonStrategy_ShouldHandleError_WhenExceptionOccurs()
        {
            // Arrange
            var mockPokemonStrategy = new Mock<IPokemonStrategy>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var pokemonBusinessLogic = new PokemonBusinessLogic(mockPokemonStrategy.Object, mockErrorHandler.Object);

            // Exception when the execute method is called
            mockPokemonStrategy.Setup(strategy => strategy.Execute(It.IsAny<PokemonContext>())).Throws(new Exception("An error occurred"));

            // Act
            pokemonBusinessLogic.ApplyPokemonStrategy(new PokemonContext());

            // Assert
            mockErrorHandler.Verify(handler => handler.HandleError(It.IsAny<Exception>()), Times.Once);
        }
    }
}
