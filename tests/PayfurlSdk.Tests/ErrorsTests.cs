using System.Net.Http;
using Payfurl.Sdk.Models;
using Xunit;

namespace Payfurl.Sdk.Tests;

/// <summary>The API's errors as exceptions, and which requests are retried.</summary>
public class ErrorsTests
{
    private static readonly NewChargeToken Charge = new() { Amount = 13m, Token = "t" };

    [Fact]
    public async Task ADeclineIsAnApiExceptionWithTheApisFields()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(400, """
            {"message":"Insufficient funds","code":15,"isRetryable":false,"type":"https://docs.payfurl.com/errorcodes#15",
             "resource":"charge","gatewayCode":"51","gatewayMessage":"Do not honour","chargeId":"ch1","traceId":"abc","details":{}}
            """);

        var e = await Assert.ThrowsAsync<ApiException>(() => client.Charges.CreateWithTokenAsync(Charge));

        Assert.Equal(typeof(ApiException), e.GetType());
        Assert.Equal("Insufficient funds", e.Message);
        Assert.Equal(400, e.HttpStatus);
        Assert.Equal(ErrorCode.InsufficientFunds, e.ErrorCode);
        Assert.False(e.IsRetryable);
        Assert.Equal("https://docs.payfurl.com/errorcodes#15", e.Type);
        Assert.Equal("charge", e.Resource);
        Assert.Equal("51", e.GatewayCode);
        Assert.Equal("Do not honour", e.GatewayMessage);
        Assert.Equal("ch1", e.ChargeId);
        Assert.Equal("abc", e.TraceId);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task AValidationErrorIsAnInvalidRequestExceptionWithItsDetails()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(400, """{"message":"Validation error","code":90,"details":{"Amount":"The Amount field is required."}}""");

        var e = await Assert.ThrowsAsync<InvalidRequestException>(() => client.Charges.CreateWithTokenAsync(Charge));

        Assert.Equal("The Amount field is required.", e.Details["Amount"]);
    }

    [Theory]
    [InlineData(401)]
    [InlineData(403)]
    public async Task AWrongKeyIsAnAuthenticationException(int status)
    {
        var (client, handler) = Fake.Client();
        handler.Respond(status, """{"message":"Invalid authentication","code":40}""");

        var e = await Assert.ThrowsAsync<AuthenticationException>(() => client.GetVersionAsync());

        Assert.Equal(ErrorCode.InvalidAuthentication, e.ErrorCode);
    }

    [Fact]
    public async Task AMissingResourceIsANotFoundException()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(404, """{"message":"Not found","code":50,"isRetryable":false}""");

        await Assert.ThrowsAsync<NotFoundException>(() => client.Charges.GetAsync("nope"));

        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task ABodyThatIsNotJsonIsAnApiException()
    {
        var (client, handler) = Fake.Client(configure: o => o.MaxRetries = 0);
        handler.Respond(502, "<html><body>Bad Gateway</body></html>", "text/html");

        var e = await Assert.ThrowsAsync<ApiException>(() => client.Charges.GetAsync("ch1"));

        Assert.Equal(502, e.HttpStatus);
        Assert.True(e.IsRetryable);
        Assert.Equal(ErrorCode.UnknownError, e.ErrorCode);
        Assert.Contains("Bad Gateway", e.Message);
    }

    [Fact]
    public async Task APlainMessageIsTheExceptionsMessage()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(400, "\"Amount must be positive\"");

        var e = await Assert.ThrowsAsync<ApiException>(() => client.Charges.CreateWithTokenAsync(Charge));

        Assert.Equal("Amount must be positive", e.Message);
        Assert.False(e.IsRetryable);
    }

    [Fact]
    public async Task ASuccessThatIsNotJsonIsAnApiExceptionWithTheParseError()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(200, "<html></html>", "text/html");

        var e = await Assert.ThrowsAsync<ApiException>(() => client.Charges.GetAsync("ch1"));

        Assert.IsAssignableFrom<System.Text.Json.JsonException>(e.InnerException);
    }

    [Fact]
    public async Task AGetIsRetriedAfterARetryableError()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(503, """{"message":"Busy","code":37,"isRetryable":true}""")
            .Throw(new HttpRequestException("Connection reset"))
            .Respond(200, """{"chargeId":"ch1"}""");

        var charge = await client.Charges.GetAsync("ch1");

        Assert.Equal("ch1", charge.ChargeId);
        Assert.Equal(3, handler.Requests.Count);
    }

    [Fact]
    public async Task AGetIsRetriedMaxRetriesTimes()
    {
        var (client, handler) = Fake.Client(configure: o => o.MaxRetries = 1);
        handler.Respond(503, "{}").Respond(503, "{}").Respond(200, "{}");

        await Assert.ThrowsAsync<ApiException>(() => client.Charges.GetAsync("ch1"));

        Assert.Equal(2, handler.Requests.Count);
    }

    [Fact]
    public async Task AGetIsNotRetriedAfterAnErrorTheApiSaysIsFinal()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(500, """{"message":"No","code":1,"isRetryable":false}""");

        await Assert.ThrowsAsync<ApiException>(() => client.Charges.GetAsync("ch1"));

        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task AChargeIsNeverRetried()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(503, """{"message":"Busy","code":37,"isRetryable":true}""");

        var e = await Assert.ThrowsAsync<ApiException>(() => client.Charges.CreateWithTokenAsync(Charge));

        Assert.True(e.IsRetryable);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task ARefundIsNeverRetried()
    {
        var (client, handler) = Fake.Client();
        handler.Throw(new HttpRequestException("Connection reset"));

        await Assert.ThrowsAsync<PayfurlConnectionException>(() => client.Charges.RefundAsync("ch1"));

        Assert.Single(handler.Requests);
    }
}
