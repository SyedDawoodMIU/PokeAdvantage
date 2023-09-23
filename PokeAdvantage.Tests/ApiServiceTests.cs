using Xunit;
using Moq;
using PokeAdvantage.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Threading;

namespace PokeAdvantage.Tests
{
    public class ApiServiceTests
    {
        [Fact]
        public async Task Fetch_ShouldReturnData_WhenHttpRequestSucceeds()
        {
            // Arrange
            var mockErrorHandler = new Mock<IErrorHandler>();
            var mockSingletonHttpClient = new Mock<ISingletonHttpClient>();

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Fake API Data")
                
            });

            mockSingletonHttpClient.Setup(client => client.Instance).Returns(new HttpClient(fakeHttpMessageHandler));

            var service = new ApiService(mockErrorHandler.Object, mockSingletonHttpClient.Object);

            // Act
            var result = await service.Fetch("https://fakeapi.com/pokemon/charizard");

            // Assert
            Assert.Equal("Fake API Data", result);
        }

        public class FakeHttpMessageHandler : HttpMessageHandler
        {
            private HttpResponseMessage response;

            public FakeHttpMessageHandler(HttpResponseMessage response)
            {
                this.response = response;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(response);
            }
        }
    }
}
