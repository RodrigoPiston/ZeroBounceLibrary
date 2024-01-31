using System;
using System.Net.Http;
using System.Threading.Tasks;
using ZeroBounceLibrary.Models;
using System.Text.Json;
using ZeroBounceLibrary.Exceptions;
using System.Collections.Generic;
using ZeroBounceLibrary.Utilities;

namespace ZeroBounceLibrary
{
    public class ZeroBounceClient
    {
        private readonly string _baseApiUrl;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ZeroBounceClient(IHttpClientFactory httpClientFactory, string baseApiUrl)
        {
            _baseApiUrl = baseApiUrl ?? "https://api.zerobounce.net/v2";
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Converters = { new NullToNoneConverter() }
            };
        }


        /// <summary>
        /// Validates a single email address asynchronously.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <param name="ipAddress">Optional IP address for email validation.</param>
        /// <returns>The validation result of the email.</returns>
        /// <exception cref="ZeroBounceException">Thrown when there is an error in API communication.</exception>
        public async Task<EmailValidationResult> ValidateEmailAsync(string apiKey, string email, string ipAddress = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API key is required.", nameof(apiKey));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            var url = $"{_baseApiUrl}/validate?api_key={apiKey}&email={Uri.EscapeDataString(email)}" +
                      $"{(ipAddress != null ? $"&ip_address={Uri.EscapeDataString(ipAddress)}" : string.Empty)}";

            try
            {
                var responseContent = await GetAsync(url);
                var validationResult = JsonSerializer.Deserialize<EmailValidationResult>(responseContent, _jsonSerializerOptions);

                if (validationResult == null)
                {
                    throw new ZeroBounceException("Received null response from the API.");
                }

                return validationResult;
            }
            catch (HttpRequestException ex)
            {
                throw new ZeroBounceException("Network error occurred while validating email.", ex);
            }
            catch (JsonException ex)
            {
                throw new ZeroBounceException("Error parsing the JSON response.", ex);
            }
        }

        /// <summary>
        /// Validates multiple email addresses in a single batch asynchronously.
        /// </summary>
        /// <param name="emails">List of email addresses to validate.</param>
        /// <param name="ipAddresses">Optional corresponding list of IP addresses.</param>
        /// <returns>The batch validation results.</returns>
        /// <exception cref="ZeroBounceException">Thrown when there is an error in API communication.</exception>
        public async Task<BatchValidationResult> ValidateBatchAsync(string apiKey, List<BatchRequest> emailBatchRequest)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API key is required.", nameof(apiKey));
            if (emailBatchRequest == null || emailBatchRequest.Count == 0)
                throw new ArgumentException("Email batch cannot be empty.", nameof(emailBatchRequest));

            var url = $"{_baseApiUrl}/validateBatch";
            var contentStr = JsonSerializer.Serialize(new { api_key = apiKey, email_batch = emailBatchRequest }, _jsonSerializerOptions);
            var content = new StringContent(JsonSerializer.Serialize(new { api_key = apiKey,email_batch = emailBatchRequest },_jsonSerializerOptions), System.Text.Encoding.UTF8, "application/json");

            try
            {
                var responseContent = await PostAsync(url, content);
                var batchValidationResult = JsonSerializer.Deserialize<BatchValidationResult>(responseContent, _jsonSerializerOptions);
                if (batchValidationResult == null)
                {
                    throw new ZeroBounceException("Received null response from the API.");
                }

                return batchValidationResult;
            }
            catch (HttpRequestException ex)
            {
                throw new ZeroBounceException("Network error occurred while validating batch emails.", ex);
            }
            catch (JsonException ex)
            {
                throw new ZeroBounceException("Error parsing the JSON response.", ex);
            }
        }



        /// <summary>
        /// Gets the credit balance available in the ZeroBounce account.
        /// </summary>
        /// <returns>The number of credits remaining.</returns>
        /// <exception cref="ZeroBounceException">Thrown when there is an error in API communication.</exception>
        public async Task<int> GetCreditBalanceAsync(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API key is required.", nameof(apiKey));

            var url = $"{_baseApiUrl}/getcredits?api_key={apiKey}";

            try
            {
                var responseContent = await GetAsync(url);
                var creditBalance = JsonSerializer.Deserialize<CreditBalanceResult>(responseContent, _jsonSerializerOptions);
                if (creditBalance == null)
                {
                    throw new ZeroBounceException("Received null response from the API.");
                }

                return int.Parse(creditBalance.Credits);
            }
            catch (HttpRequestException ex)
            {
                throw new ZeroBounceException("Network error occurred while getting credit balance.", ex);
            }
            catch (JsonException ex)
            {
                throw new ZeroBounceException("Error parsing the JSON response.", ex);
            }
        }

        private async Task<string> GetAsync(string uri)
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> PostAsync(string uri, HttpContent content)
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
