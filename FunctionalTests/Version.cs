using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests;

public class Version : BaseTest
{
    [Fact]
    public void GetVersion()
    {
        var svc = new payfurl.sdk.Version();
        var result = svc.Get();

        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.Version));
        Assert.NotEqual(default(DateTime), result.Released);
        Assert.False(string.IsNullOrWhiteSpace(result.Region));
    }

    [Fact]
    public async Task GetVersionAsync()
    {
        var svc = new payfurl.sdk.Version();
        var result = await svc.GetAsync();

        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.Version));
        Assert.NotEqual(default(DateTime), result.Released);
        Assert.False(string.IsNullOrWhiteSpace(result.Region));
    }
}
