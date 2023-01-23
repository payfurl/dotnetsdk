using System;
using System.Collections.Generic;
using payfurl.sdk;
using payfurl.sdk.Models;
using Xunit;
using Environment = payfurl.sdk.Environment;

namespace FunctionalTests;

public class Provider
{
    public Provider()
    {
        Config.Setup("sectestab8c10faebf84918758da45df1530a590dc295513b", Environment.LOCAL);
    }
    
    [Fact]
    public void CreateProvider()
    {
        var provider = CreateNewProvider();

        var svc = new payfurl.sdk.Provider();
        var result = svc.Create(provider);

        Assert.NotNull(result.ProviderId);
    }

    [Fact]
    public void UpdateProvider()
    {
        var provider = CreateNewProvider();

        var svc = new payfurl.sdk.Provider();
        var result = svc.Create(provider);

        var newName = Guid.NewGuid().ToString();
        var updateProvider = new UpdateProvider()
        {
            Name = newName,
            Currency = "AUD",
            AuthenticationParameters = new Dictionary<string, string>()
            {
                ["MinMilliseconds"] = "1",
                ["MaxMilliseconds"] = "10"
            }
        };

        result = svc.Update(result.ProviderId, updateProvider);

        Assert.Equal(result.Name, newName);
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