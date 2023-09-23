using Xunit;
using Moq;
using PokeAdvantage.Interfaces;
using PokeAdvantage.DTOs;
using PokeAdvantage.Implementation.Business;
using System.Threading.Tasks;
using System;

namespace PokeAdvantage.Tests
{
    public class PokemonApiManagerTests
    {
        [Fact]
        public async Task FetchPokemonData_ShouldReturnPokemonDTO_WhenApiCallSucceeds()
        {
            // Arrange
            var mockApiClient = new Mock<IPokemonApiClient>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var mockJsonHelper = new Mock<IJsonHelper>();

            mockApiClient.Setup(api => api.GetPokemonAsync(It.IsAny<string>()))
                         .ReturnsAsync("{\"name\":\"pikachu\"}");
            mockJsonHelper.Setup(helper => helper.Deserialize<PokemonDTO>(It.IsAny<string>()))
                          .Returns(new PokemonDTO { Name = "Pikachu" });

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchPokemonData("pikachu");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pikachu", result.Name);
        }

        [Fact]
        public async Task FetchPokemonData_ShouldHandleError_WhenApiCallFails()
        {
            // Arrange
            var mockApiClient = new Mock<IPokemonApiClient>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var mockJsonHelper = new Mock<IJsonHelper>();

            mockApiClient.Setup(api => api.GetPokemonAsync(It.IsAny<string>()))
                         .ThrowsAsync(new Exception("API call failed"));
            mockErrorHandler.Setup(handler => handler.HandleError(It.IsAny<Exception>()));

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchPokemonData("pikachu");

            // Assert
            Assert.Null(result);
            mockErrorHandler.Verify(handler => handler.HandleError(It.IsAny<Exception>()), Times.Once);
        }

        [Fact]
        public async Task FetchTypeRelationsAsync_ShouldReturnTypeRelationsDTO_WhenApiCallSucceeds()
        {
            // Arrange
            var mockApiClient = new Mock<IPokemonApiClient>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var mockJsonHelper = new Mock<IJsonHelper>();

            mockApiClient.Setup(api => api.GetTypeRelationsAsync(It.IsAny<string>()))
                         .ReturnsAsync("{\"damage_relations\":\"Fire\"}");
            mockJsonHelper.Setup(helper => helper.Deserialize<TypeRelationsDTO>(It.IsAny<string>()))
                          .Returns(new TypeRelationsDTO
                          {
                              DamageRelations = new DamageRelationsDTO
                              {
                                  DoubleDamageFrom = new List<DamageTypeDTO> { new DamageTypeDTO { Name = "Fire" } }
                              }

                          });

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchTypeRelationsAsync("Fire");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Fire", result.DamageRelations.DoubleDamageFrom.First().Name);
        }

        [Fact]

        public async Task FetchTypeRelationsAsync_ShouldHandleError_WhenApiCallFails()
        {
            // Arrange
            var mockApiClient = new Mock<IPokemonApiClient>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var mockJsonHelper = new Mock<IJsonHelper>();

            mockApiClient.Setup(api => api.GetPokemonAsync(It.IsAny<string>()))
                         .ThrowsAsync(new Exception("API call failed"));
            mockErrorHandler.Setup(handler => handler.HandleError(It.IsAny<Exception>()));

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchTypeRelationsAsync("pikachu");

            // Assert
            Assert.Null(result);
            mockErrorHandler.Verify(handler => handler.HandleError(It.IsAny<Exception>()), Times.Once);
        }


    }
}
