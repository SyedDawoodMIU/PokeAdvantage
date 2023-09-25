using Xunit;
using Moq;
using PokemonAdvantage.Interfaces;
using PokemonAdvantage.DTOs;
using PokemonAdvantage.Implementation.Business;
using System.Threading.Tasks;
using System;

namespace PokemonAdvantage.Tests
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
                         .ReturnsAsync("{\"name\":\"fire\"}");
            mockJsonHelper.Setup(helper => helper.Deserialize<PokemonDTO>(It.IsAny<string>()))
                          .Returns(new PokemonDTO { Name = "fire" });

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchPokemonData("fire");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("fire", result.Name);
        }

        [Fact]
        public async Task FetchPokemonData_ShouldHandleError_WhenApiCallFails()
        {
            // Arrange
            var mockApiClient = new Mock<IPokemonApiClient>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var mockJsonHelper = new Mock<IJsonHelper>();

            mockApiClient.Setup(api => api.GetPokemonAsync(It.IsAny<string>()))
                         .ThrowsAsync(new Exception("api call failed"));
            mockErrorHandler.Setup(handler => handler.HandleError(It.IsAny<Exception>()));

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchPokemonData("fire");

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
                         .ReturnsAsync("{\"damage_relations\":\"fire\"}");
            mockJsonHelper.Setup(helper => helper.Deserialize<TypeRelationsDTO>(It.IsAny<string>()))
                          .Returns(new TypeRelationsDTO()
                          {
                              DamageRelations = new DamageRelationsDTO()
                              {
                                  DoubleDamageFrom = new List<DamageTypeDTO>(){
                                        new DamageTypeDTO(){
                                            Name = "fire"
                                        }
                                    }
                              }
                          });

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchTypeRelationsAsync("fire");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("fire", result.DamageRelations.DoubleDamageFrom.First().Name);
        }

        [Fact]

        public async Task FetchTypeRelationsAsync_ShouldHandleError_WhenApiCallFails()
        {
            // Arrange
            var mockApiClient = new Mock<IPokemonApiClient>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var mockJsonHelper = new Mock<IJsonHelper>();

            mockApiClient.Setup(api => api.GetPokemonAsync(It.IsAny<string>()))
                         .ThrowsAsync(new Exception("api call failed"));
            mockErrorHandler.Setup(handler => handler.HandleError(It.IsAny<Exception>()));

            var manager = new PokemonApiManager(mockApiClient.Object, mockErrorHandler.Object, mockJsonHelper.Object);

            // Act
            var result = await manager.FetchTypeRelationsAsync("fire");

            // Assert
            Assert.Null(result);
            mockErrorHandler.Verify(handler => handler.HandleError(It.IsAny<Exception>()), Times.Once);
        }


    }
}
