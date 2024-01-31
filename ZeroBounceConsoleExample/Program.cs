using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

using Microsoft.Extensions.DependencyInjection;

using ZeroBounceLibrary;
using ZeroBounceLibrary.Models;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        var clientFactory = serviceProvider.GetService<IHttpClientFactory>();

        var apiKey = "9ae26048c69d4f40b5401599ad8d5a34";
        var baseApiUrl = "https://api.zerobounce.net/v2";
        var email = "rodrigopiston@gmail.com";

        var client = new ZeroBounceClient(clientFactory,baseApiUrl);
        Console.WriteLine($"__________SINGLE EMAIL VALIDATION__________");

        try
        {
            var validationResult = await client.ValidateEmailAsync(apiKey, email);
            Console.WriteLine(validationResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.WriteLine($"\n");
        Console.WriteLine($"__________BATCH EMAIL VALIDATION__________");
        try
        {
            var emailBatch = new List<BatchRequest>
            {
                new() { EmailAddress = "rodrigopiston@gmail.com", IpAddress = "1.1.1.1" },
                new() { EmailAddress = "rodrigopiston@gmeil.com", IpAddress = "1.1.1.1" },
                new() { EmailAddress = "rodrigopiston@gmail,com", IpAddress = "1.1.1.1" },
                new() { EmailAddress = "hernestoCastro4124_2@live.com", IpAddress = "1.1.1.1" },
            };

            var validationResult = await client.ValidateBatchAsync(apiKey, emailBatch);
            Console.WriteLine(validationResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.WriteLine($"\n");
        Console.WriteLine($"__________GET CREDIT BALANCE__________");
        try
        {
            var validationResult = await client.GetCreditBalanceAsync(apiKey);
            Console.WriteLine(validationResult);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadLine();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
    }
}
