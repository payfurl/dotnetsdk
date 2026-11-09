using System.Security.Cryptography;
using System.Text;
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
    public void RefusesToParseAnUnsignedBody() => Assert.Throws<ArgumentException>(() => Webhook.Parse(Body, "AAAA", Key));

    [Fact]
    public void RefusesToParseABodyThatIsNotJson() => Assert.Throws<ArgumentException>(() => Webhook.Parse("not json", Sign("not json", Key), Key));
}
