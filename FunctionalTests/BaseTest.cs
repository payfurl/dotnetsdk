using System;
using Microsoft.Extensions.Configuration;
using payfurl.sdk;
using Xunit.Abstractions;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests;

public class BaseTest
{
    protected readonly ITestOutputHelper output;
    private readonly IConfiguration _configuration;
        
    public BaseTest(ITestOutputHelper output)
    {
        this.output = output;

        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables() 
            .Build();

        Enum.TryParse(_configuration["Environment"]?.ToUpper() ?? "DEVELOPMENT", out Environment env);
        Config.Setup(_configuration["SecretKey"], env);
    }
    
    protected string GetProviderId()
    {
        return _configuration["ProviderId"];
    }

    protected string GetPayToProviderId()
    {
        return _configuration["PayToProviderId"];
    }

    protected string GetPaymentToken()
    {
        return _configuration["Token"];
    }
}