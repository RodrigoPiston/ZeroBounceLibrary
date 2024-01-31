using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Threading;
using Moq.Protected;
using ZeroBounceLibrary.Models;

namespace ZeroBounceLibrary.Tests
{
    public class ZeroBounceClientTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly ZeroBounceClient _client;
        private readonly string _baseApiUrl = "https://api.zerobounce.net/v2";

        public ZeroBounceClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _client = new ZeroBounceClient(mockHttpClientFactory.Object,_baseApiUrl);
        }

        [Fact]
        public async Task ValidateEmailAsync_ReturnsValidResult()
        {
            // Arrange
            var apiKey = "test_api_key";
            var email = "test@example.com";
            var jsonResponse = "{\"address\":\"test@example.com\",\"status\":\"valid\"}";
            SetupMockHttpMessageHandler(HttpStatusCode.OK, jsonResponse);

            // Act
            var result = await _client.ValidateEmailAsync(apiKey, email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("valid", result.Status);
        }

        private void SetupMockHttpMessageHandler(HttpStatusCode statusCode, string content)
        {
            _mockHttpMessageHandler.Protected()
                 .Setup<Task<HttpResponseMessage>>(
                     "SendAsync",
                     ItExpr.IsAny<HttpRequestMessage>(),
                     ItExpr.IsAny<CancellationToken>()
                 )
                 .ReturnsAsync(new HttpResponseMessage
                 {
                     StatusCode = statusCode,
                     Content = new StringContent(content)
                 });
        }

        [Fact]
        public async Task ValidateBatchAsync_ReturnsValidResults()
        {
            // Arrange
            var apiKey = "test_api_key";
            var emailBatch = new List<BatchRequest>
            {
                new BatchRequest() { EmailAddress = "rodrigopiston@gmail.com", IpAddress = "1.1.1.1" },
                new BatchRequest() { EmailAddress = "rodrigopiston@gmeil.com", IpAddress = "1.1.1.1" },
                new BatchRequest() { EmailAddress = "rodrigopiston@gmail,com", IpAddress = "1.1.1.1" },
                new BatchRequest() { EmailAddress = "hernestoCastro4124_2@live.com", IpAddress = "1.1.1.1" },
            };
            var jsonResponse = "{\"email_batch\": [{\"address\":\"test@example.com\",\"status\":\"valid\"},{\"address\":\"test2@example.com\",\"status\":\"invalid\"}]}";
            
            SetupMockHttpMessageHandler(HttpStatusCode.OK, jsonResponse);

            // Act
            var result = await _client.ValidateBatchAsync(apiKey, emailBatch);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.EmailBatch.Count);
            // More assertions as needed
        }

        [Fact]
        public async Task GetCreditBalanceAsync_ReturnsCreditBalance()
        {
            // Arrange
            var apiKey = "test_api_key";
            var jsonResponse = "{\"Credits\":100}";
            
            SetupMockHttpMessageHandler(HttpStatusCode.OK, jsonResponse);

            // Act
            var credits = await _client.GetCreditBalanceAsync(apiKey);

            // Assert
            Assert.Equal(100, credits);
        }
    }
}
