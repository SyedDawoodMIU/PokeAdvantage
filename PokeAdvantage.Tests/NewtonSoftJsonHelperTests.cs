using Newtonsoft.Json;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Implementation;

namespace PokeAdvantage.Tests
{
    public class NewtonSoftJsonHelperTests
    {
        [Fact]
        public void Deserialize_ReturnsObject_WhenJsonIsValid()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var jsonHelper = new NewtonSoftJsonHelper(mockErrorHandler.Object);
            var testObject = new { Name = "Pikachu", Type = "Electric" };
            var jsonString = JsonConvert.SerializeObject(testObject);

            // Act
            var result = jsonHelper.Deserialize<dynamic>(jsonString);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pikachu", (string)result.Name);
            Assert.Equal("Electric", (string)result.Type);
        }

        [Fact]
        public void Deserialize_CallsErrorHandler_WhenJsonIsInvalid()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var jsonHelper = new NewtonSoftJsonHelper(mockErrorHandler.Object);
            var invalidJsonString = "Invalid JSON String";

            // Act
            var result = jsonHelper.Deserialize<dynamic>(invalidJsonString);

            // Assert
            mockErrorHandler.Verify(x => x.HandleError(It.IsAny<Exception>()), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public void Deserialize_CallsErrorHandler_WhenJsonIsEmpty()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var jsonHelper = new NewtonSoftJsonHelper(mockErrorHandler.Object);

            // Act
            var result = jsonHelper.Deserialize<dynamic>(string.Empty);

            // Assert
            mockErrorHandler.Verify(x => x.HandleError(It.IsAny<Exception>()), Times.Once);
            Assert.Null(result);
        }
    }
}
