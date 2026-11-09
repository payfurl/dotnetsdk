using System.Globalization;
using System.Net.Http;
using Payfurl.Sdk.Models;
using Xunit;

namespace Payfurl.Sdk.Tests;

public class ClientTests
{
    [Theory]
    [InlineData(PayfurlEnvironment.Local, "sectest1", "https://localhost:5001/version")]
    [InlineData(PayfurlEnvironment.Local, "sectest1-au", "https://localhost:5001/version")]
    [InlineData(PayfurlEnvironment.Development, "sectest1", "https://develop-api.payfurl.com/version")]
    [InlineData(PayfurlEnvironment.Development, "sectest1-au", "https://develop-api-au.payfurl.com/version")]
    [InlineData(PayfurlEnvironment.Sandbox, "sectest1", "https://sandbox-api.payfurl.com/version")]
    [InlineData(PayfurlEnvironment.Sandbox, "sectest1-AU", "https://sandbox-api-au.payfurl.com/version")]
    [InlineData(PayfurlEnvironment.Sandbox, "sectest1-us", "https://sandbox-api.payfurl.com/version")]
    [InlineData(PayfurlEnvironment.Production, "seclive1", "https://api.payfurl.com/version")]
    [InlineData(PayfurlEnvironment.Production, "seclive1-au", "https://api-au.payfurl.com/version")]
    public async Task PicksTheEnvironmentsAndRegionsUrl(PayfurlEnvironment environment, string key, string url)
    {
        var (client, handler) = Fake.Client(key, environment);

        await client.GetVersionAsync();

        Assert.Equal(url, handler.Last.Uri.AbsoluteUri);
    }

    [Fact]
    public async Task BaseUrlOverridesTheEnvironment()
    {
        var (client, handler) = Fake.Client(configure: o => o.BaseUrl = "http://api:5000/");

        await client.Charges.GetAsync("ch1");

        Assert.Equal("http://api:5000/charge/ch1", handler.Last.Uri.AbsoluteUri);
    }

    [Fact]
    public async Task SendsTheSecretKeyAndUserAgent()
    {
        var (client, handler) = Fake.Client("sectest1-au");

        await client.GetVersionAsync();

        Assert.Equal("sectest1-au", handler.Last.Headers["x-secretkey"]);
        Assert.False(handler.Last.Headers.ContainsKey("x-publickey"));
        Assert.Equal("payfurl-dotnet/" + PayfurlClient.Version, handler.Last.Headers["User-Agent"]);
        Assert.Matches(@"^\d+\.\d+\.\d+", PayfurlClient.Version);
        Assert.DoesNotContain("+", PayfurlClient.Version);
    }

    [Fact]
    public async Task SendsAPublicKeyAsOne()
    {
        var (client, handler) = Fake.Client("pubtest1-au");

        await client.Info.GetProvidersAsync(browserSdkVersion: "4.6.0");

        Assert.Equal("pubtest1-au", handler.Last.Headers["x-publickey"]);
        Assert.False(handler.Last.Headers.ContainsKey("x-secretkey"));
        Assert.Equal("4.6.0", handler.Last.Headers["sdk-version"]);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void RejectsAnEmptyKey(string key)
    {
        Assert.Throws<ArgumentException>(() => new PayfurlClient(new PayfurlClientOptions { SecretKey = key, Environment = PayfurlEnvironment.Sandbox }));
    }

    [Fact]
    public void RejectsABaseUrlThatIsNotAUrl()
    {
        Assert.Throws<ArgumentException>(() => new PayfurlClient(new PayfurlClientOptions { SecretKey = "sec1", Environment = PayfurlEnvironment.Sandbox, BaseUrl = "api:5000" }));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task RejectsAnEmptyIdBeforeSending(string id)
    {
        var (client, handler) = Fake.Client();

        await Assert.ThrowsAsync<ArgumentException>(() => client.Charges.GetAsync(id));

        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task SendsAmountsAndDatesInTheInvariantCulture()
    {
        var culture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo("de-DE");
        try
        {
            var (client, handler) = Fake.Client();

            await client.Charges.RefundAsync("ch1", 10.5m);
            Assert.Equal("/charge/ch1?amount=10.5", handler.Last.PathAndQuery);

            await client.Charges.SearchAsync(new ChargeSearch
            {
                AmountGreaterThan = 1234.56m,
                AddedAfter = new DateTime(2026, 11, 9, 13, 5, 0, DateTimeKind.Utc),
            });
            Assert.Equal("/charge?amountGreaterThan=1234.56&addedAfter=2026-11-09T13%3A05%3A00.0000000Z", handler.Last.PathAndQuery);

            await client.Charges.CreateWithTokenAsync(new NewChargeToken { Amount = 10.5m, Token = "t" });
            Assert.Contains("\"amount\":10.5", handler.Last.Body);
        }
        finally
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = culture;
        }
    }

    [Fact]
    public async Task TimesOutAfterTheTimeout()
    {
        var (client, handler) = Fake.Client(configure: o =>
        {
            o.Timeout = TimeSpan.FromMilliseconds(50);
            o.MaxRetries = 0;
        });
        handler.Hang();

        var e = await Assert.ThrowsAsync<PayfurlTimeoutException>(() => client.Charges.GetAsync("ch1"));

        Assert.IsAssignableFrom<PayfurlConnectionException>(e);
    }

    [Fact]
    public async Task ACancelledCallIsCancelledNotATimeout()
    {
        var (client, handler) = Fake.Client();
        handler.Hang();
        using var cancellation = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => client.Charges.GetAsync("ch1", cancellation.Token));
    }

    [Fact]
    public async Task AConnectionErrorIsAConnectionException()
    {
        var (client, handler) = Fake.Client(configure: o => o.MaxRetries = 0);
        handler.Throw(new HttpRequestException("Connection refused"));

        var e = await Assert.ThrowsAsync<PayfurlConnectionException>(() => client.Charges.CreateWithTokenAsync(new NewChargeToken { Amount = 1m, Token = "t" }));

        Assert.IsType<HttpRequestException>(e.InnerException);
    }

    [Fact]
    public async Task WorksWithoutAnHttpClient()
    {
        // The shared client; nothing listens on this port, so the call fails to connect rather than hanging.
        var client = new PayfurlClient(new PayfurlClientOptions
        {
            SecretKey = "sec1",
            Environment = PayfurlEnvironment.Local,
            BaseUrl = "http://127.0.0.1:9",
            MaxRetries = 0,
            Timeout = TimeSpan.FromSeconds(5),
        });

        await Assert.ThrowsAnyAsync<PayfurlConnectionException>(() => client.GetVersionAsync());
    }
}
