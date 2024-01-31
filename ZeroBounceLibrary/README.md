# ZeroBounceLibrary

## Overview
ZeroBounceLibrary is a C# client library for interfacing with the ZeroBounce API. 
This library provides an easy and efficient way to validate single or batch email addresses and check the credit balance in your ZeroBounce account.

## Features
- Validate single email addresses asynchronously.
- Validate multiple email addresses in a single batch asynchronously.
- Check the available credit balance in your ZeroBounce account.
- Comprehensive error handling for various scenarios such as invalid input, API errors, network issues, and JSON parsing errors.

## Usage

### Setting Up the Client
```csharp
using ZeroBounceLibrary;

var httpClientFactory = ... // Create or get an IHttpClientFactory instance
var client = new ZeroBounceClient(httpClientFactory, "https://api.zerobounce.net/v2");
```

### Validating a Single Email
```csharp
var apiKey = "your_api_key_here";
var email = "test@example.com";
var ipAddress = "192.168.1.1"; // Optional

var validationResult = await client.ValidateEmailAsync(apiKey, email, ipAddress);
Console.WriteLine($"Email: {validationResult.Address}, Status: {validationResult.Status}");
```

### Validating a Batch of Emails
```csharp
var apiKey = "your_api_key_here";
var emailBatchRequest = new List<BatchRequest>
{
    new BatchRequest { EmailAddress = "email1@example.com", IpAddress = "192.168.1.1" },
    // Add more emails as needed
};

var batchResult = await client.ValidateBatchAsync(apiKey, emailBatchRequest);
// Process batchResult as needed
```

### Checking Credit Balance
```csharp
var apiKey = "your_api_key_here";
var credits = await client.GetCreditBalanceAsync(apiKey);
Console.WriteLine($"Credits remaining: {credits}");
```

## Error Handling
The library includes comprehensive error handling. It throws `ZeroBounceException` with descriptive messages in case of various failures such as network issues, API errors, or invalid inputs.