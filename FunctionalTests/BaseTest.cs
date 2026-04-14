using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using payfurl.sdk;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests;

public class BaseTest
{
    private readonly IConfiguration _configuration;
    public static int TokenNum = 0;
        
    public BaseTest()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables() 
            .Build();

        var secretKey = _configuration["SecretKey"];
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException(
                "Integration tests require a configured SecretKey when RUN_INTEGRATION_TESTS=true.");
        }

        Enum.TryParse(_configuration["Environment"]?.ToUpper() ?? "DEVELOPMENT", out Environment env);
        Config.Setup(secretKey, env);
    }
    
    protected string GetProviderId()
    {
        var providerId = _configuration["ProviderId"];
        if (string.IsNullOrWhiteSpace(providerId))
        {
            throw new InvalidOperationException("Integration tests requiring provider data need ProviderId to be configured.");
        }

        return providerId;
    }
    
    protected string GetPaymentToken()
    {
        var tokens = _configuration.GetSection("Tokens").AsEnumerable().ToArray();
        TokenNum++;
        if (tokens.Count() <= TokenNum)
        {
            throw new InvalidOperationException(
                "Integration tests requiring payment tokens need Tokens to be configured.");
        }

        if (string.IsNullOrWhiteSpace(tokens[TokenNum].Value))
        {
            throw new InvalidOperationException(
                "Integration tests requiring payment tokens need non-empty Tokens values.");
        }

        return tokens[TokenNum].Value;
    }
}
