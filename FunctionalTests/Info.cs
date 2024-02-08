using System;
using System.Collections.Generic;
using payfurl.sdk.Models;
using Xunit;

namespace FunctionalTests;

public class Info : BaseTest
{
    [Fact]
    public void GetInfo()
    {
        var svc = new payfurl.sdk.Info();
        var result = svc.GetInfo();

        Assert.NotNull(result.Countries);
        Assert.NotNull(result.Currencies);
        Assert.NotNull(result.Timezones);
    }

    [Fact]
    public void GetProvidersInfo()
    {
        var svc = new payfurl.sdk.Info();
        var result = svc.GetProvidersInfo(10, "AUD");

        Assert.NotNull(result);
        Assert.True(result.HasCardProviders);
    }
    
    [Fact]
    public void GetProviderInfo()
    {
        var providerService = new payfurl.sdk.Provider();
        var provider = providerService.Create(CreateNewProvider());

        var svc = new payfurl.sdk.Info();
        var result = svc.GetProviderInfo(provider.ProviderId);

        Assert.NotNull(result);
    }

    [Fact]
    public void GetProviderTokenInfo()
    {
        var tokenId = GetPaymentToken();

        var svc = new payfurl.sdk.Info();
        var result = svc.GetProviderTokenInfo(tokenId);

        Assert.NotNull(result);
    }

    [Fact]
    public void GetDefaultFallbackProvider()
    {
        var svc = new payfurl.sdk.Info();
        var result = svc.GetDefaultFallbackProvider();

        Assert.NotNull(result);
    }
    
    private NewProvider CreateNewProvider()
    {
        return new NewProvider
        {
            Type = "dummy",
            Name = Guid.NewGuid().ToString(),
            Environment = "SANDBOX",
            Currency = "AUD",
            AuthenticationParameters = new Dictionary<string, string>()
            {
                ["MinMilliseconds"] = "1",
                ["MaxMilliseconds"] = "10"
            }
        };
    }
}