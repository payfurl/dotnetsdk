using System.Security.Cryptography;
using System.Text;
using Payfurl.Sdk.Models;
using Xunit;

namespace Payfurl.Sdk.Tests;

/// <summary>Signatures as services/webhooks-sender/src/WebhooksSender/Tools/SignatureGenerator.cs makes them.</summary>
public class WebhookTests
{
    private const string Key = "signing-key";
    private const string Body = """{"data":{"chargeId":"ch1","status":"SUCCESS"},"meta":{"type":"Transaction"}}""";

    private static string Sign(string body, string key) =>
        Convert.ToBase64String(HMACSHA256.HashData(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(body)));

    [Fact]
    public void VerifiesTheSignature() => Assert.True(Webhook.Verify(Body, Sign(Body, Key), Key));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("AAAA")]
    public void RejectsAMissingOrWrongSignature(string? signature) => Assert.False(Webhook.Verify(Body, signature, Key));

    [Fact]
    public void RejectsAChangedBody() => Assert.False(Webhook.Verify(Body + " ", Sign(Body, Key), Key));

    [Fact]
    public void RejectsAnotherKey() => Assert.False(Webhook.Verify(Body, Sign(Body, "other"), Key));

    [Fact]
    public void RejectsAnEmptyKey() => Assert.False(Webhook.Verify(Body, Sign(Body, ""), ""));

    [Fact]
    public void ParsesASignedBody()
    {
        var webhook = Webhook.Parse(Body, Sign(Body, Key), Key);

        Assert.Equal("ch1", webhook.GetProperty("data").GetProperty("chargeId").GetString());
        Assert.Equal("Transaction", webhook.GetProperty("meta").GetProperty("type").GetString());
    }

    [Fact]
    public void ParsesATransactionWebhookAsAChargeData()
    {
        const string body = """
            {"meta":{"messageId":"m1","timestamp":"2026-11-09T01:02:03Z","type":"Transaction","eventType":"transaction.created"},
             "data":{"chargeId":"ch1","amount":10.5,"status":"SUCCESS","somethingNew":1}}
            """;

        var webhook = Webhook.Parse<ChargeData>(body, Sign(body, Key), Key);

        Assert.Equal("m1", webhook.Meta!.MessageId);
        Assert.Equal(WebhookType.Transaction, webhook.Meta.Type);
        Assert.Equal("transaction.created", webhook.Meta.EventType);
        Assert.Equal("ch1", webhook.Data!.ChargeId);
        Assert.Equal(10.5m, webhook.Data.Amount);
        Assert.Equal(TransactionStatus.Success, webhook.Data.Status);
    }

    [Fact]
    public void RefusesToParseAnUnsignedBodyAsAModel() => Assert.Throws<ArgumentException>(() => Webhook.Parse<ChargeData>(Body, "AAAA", Key));

    [Fact]
    public void RefusesToParseAnUnsignedBody() => Assert.Throws<ArgumentException>(() => Webhook.Parse(Body, "AAAA", Key));

    [Fact]
    public void RefusesToParseABodyThatIsNotJson() => Assert.Throws<ArgumentException>(() => Webhook.Parse("not json", Sign("not json", Key), Key));
}
