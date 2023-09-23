using PokeAdvantage.Implementation.ErrorHandler;
using System;
using PokeAdvantage.Interfaces.Logging;

namespace PokeAdvantage.Tests
{
    public class ConsoleErrorHandlerTests
    {
        private readonly Mock<ILogger> mockLogger;
        private readonly ConsoleErrorHandler consoleErrorHandler;


        public ConsoleErrorHandlerTests()
        {
            mockLogger = new Mock<ILogger>();
            consoleErrorHandler = new ConsoleErrorHandler(mockLogger.Object);
        }

        [Fact]
        public void HandleError_ShouldLogErrorMessage()
        {
            // Arrange
            var exception = new Exception("sample error");

            // Act
            consoleErrorHandler.HandleError(exception);

            // Assert
            mockLogger.Verify(m => m.LogError("An error occurred: sample error"), Times.Once);
        }

        [Fact]
        public void HandleError_ShouldLogIfExceptionIsNull()
        {
            // Arrange
            Exception exception = null;

            // Act
            consoleErrorHandler.HandleError(exception);

            // Assert
            mockLogger.Verify(m => m.LogError("The exception is null"), Times.Once);
        }
    }
}
