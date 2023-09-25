using Microsoft.Extensions.Options;
using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Models;

namespace PokemonAdvantage.Tests
{
    public class PokemonApiClientTests
    {

        private readonly Mock<IOptions<PokemonApiSettings>> options;

        public PokemonApiClientTests()
        {
            var settings = new PokemonApiSettings { BaseUrl = "https://fakeapi.com/" };
            options = new Mock<IOptions<PokemonApiSettings>>();
            options.Setup(o => o.Value).Returns(settings);

        }
        [Fact]
        public async Task GetPokemonAsync_ShouldReturnData_WhenApiServiceWorks()
        {
            // Arrange
            var mockApiService = new Mock<IApiService>();
            var mockErrorHandler = new Mock<IErrorHandler>();
            var settings = new PokemonApiSettings { BaseUrl = "https://fakeapi.com/" };

            mockApiService.Setup(api => api.Fetch(It.IsAny<string>()))
                          .ReturnsAsync("fake pokemon data");

            var client = new PokemonApiClient(mockApiService.Object, mockErrorHandler.Object, options.Object);

            // Act
            var result = await client.GetPokemonAsync("charizard");

            // Assert
            Assert.Equal("fake pokemon data", result);
        }

        [Fact]
        public async Task GetPokemonAsync_ShouldHandleError_WhenApiServiceThrowsException()
        {
            // Arrange
            var mockApiService = new Mock<IApiService>();
            var mockErrorHandler = new Mock<IErrorHandler>();

            mockApiService.Setup(api => api.Fetch(It.IsAny<string>()))
                          .ThrowsAsync(new Exception("API Error"));

            mockErrorHandler.Setup(handler => handler.HandleError(It.IsAny<Exception>()));

            var client = new PokemonApiClient(mockApiService.Object, mockErrorHandler.Object, options.Object);

            // Act
            var result = await client.GetPokemonAsync("charizard");

            // Assert
            Assert.Null(result);
            mockErrorHandler.Verify(handler => handler.HandleError(It.IsAny<Exception>()), Times.Once);
        }

        [Fact]
        public async Task GetTypeRelationsAsync_ShouldReturnData_WhenApiServiceWorks()
        {
            // Arrange
            var mockApiService = new Mock<IApiService>();
            var mockErrorHandler = new Mock<IErrorHandler>();

            mockApiService.Setup(api => api.Fetch(It.IsAny<string>()))
                          .ReturnsAsync("fake type relations data");

            var client = new PokemonApiClient(mockApiService.Object, mockErrorHandler.Object, options.Object);

            // Act
            var result = await client.GetTypeRelationsAsync("fire");

            // Assert
            Assert.Equal("fake type relations data", result);
        }

        [Fact]
        public async Task GetTypeRelationsAsync_ShouldHandleError_WhenApiServiceThrowsException()
        {
            // Arrange
            var mockApiService = new Mock<IApiService>();
            var mockErrorHandler = new Mock<IErrorHandler>();

            mockApiService.Setup(api => api.Fetch(It.IsAny<string>()))
                          .ThrowsAsync(new Exception("API Error"));

            mockErrorHandler.Setup(handler => handler.HandleError(It.IsAny<Exception>()));

            var client = new PokemonApiClient(mockApiService.Object, mockErrorHandler.Object, options.Object);

            // Act
            var result = await client.GetTypeRelationsAsync("fire");

            // Assert
            Assert.Null(result);
            mockErrorHandler.Verify(handler => handler.HandleError(It.IsAny<Exception>()), Times.Once);
        }
    }
}
