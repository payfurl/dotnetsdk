using Payfurl.Sdk.Models;
using Xunit;

namespace Payfurl.Sdk.Tests;

/// <summary>Responses read into the models, as the API writes them.</summary>
public class ModelsTests
{
    [Fact]
    public async Task ReadsACharge()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(200, """
            {
              "chargeId": "ch1", "providerChargeId": "pc1", "amount": 10.5, "providerId": "p1", "reference": "r",
              "paymentInformation": {"paymentMethodId": null, "type": "CARD", "card": {"cardNumber": "4111********1111", "expiryDate": "12/30", "type": "VISA"}},
              "status": "PARTIAL_REFUND", "dateAdded": "2026-11-09T01:02:03.456Z", "refundedAmount": 5,
              "currency": "AUD", "refunds": [{"amount": 5, "dateAdded": "2026-11-09T02:00:00Z", "comment": "partial"}],
              "failedAttempts": [], "threeDsVerified": false, "initiator": "MERCHANT", "ip": "203.0.113.1",
              "metadata": {"order": "42"}, "subscriptionId": null, "somethingNew": {"a": 1}
            }
            """);

        var charge = await client.Charges.GetAsync("ch1");

        Assert.Equal("ch1", charge.ChargeId);
        Assert.Equal(10.5m, charge.Amount);
        Assert.Equal(TransactionStatus.PartialRefund, charge.Status);
        Assert.Equal(PaymentType.Card, charge.PaymentInformation!.Type);
        Assert.Equal("4111********1111", charge.PaymentInformation.Card!.CardNumber);
        Assert.Equal(new DateTime(2026, 11, 9, 1, 2, 3, 456, DateTimeKind.Utc), charge.DateAdded);
        Assert.Equal("partial", Assert.Single(charge.Refunds!).Comment);
        Assert.Equal("42", charge.Metadata!["order"]);
        Assert.Equal("203.0.113.1", charge.Ip);
        Assert.Null(charge.SubscriptionId);
    }

    [Fact]
    public async Task APaymentLinkKeepsItsPaymentTypesAsSent()
    {
        // As the dashboard saves them: the browser SDK's names.
        var (client, handler) = Fake.Client();
        handler.Respond(200, """{"paymentLinkId":"l1","allowedPaymentTypes":["card","azupay","PAYPAL"]}""");

        var link = await client.PaymentLinks.GetAsync("l1");

        Assert.Equal(new List<string> { "card", "azupay", "PAYPAL" }, link.AllowedPaymentTypeNames);
#pragma warning disable CS0618 // The obsolete PaymentType view: a name no case matches is Unknown.
        Assert.Equal(new List<PaymentType> { PaymentType.Card, PaymentType.Unknown, PaymentType.PayPal }, link.AllowedPaymentTypes);
#pragma warning restore CS0618
    }

    [Fact]
    public async Task AStatusTheSdkDoesNotKnowIsUnknown()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(200, """{"chargeId":"ch1","status":"SOMETHING_NEW","paymentInformation":{"type":"NEWTYPE"}}""");

        var charge = await client.Charges.GetAsync("ch1");

        Assert.Equal(TransactionStatus.Unknown, charge.Status);
        Assert.Equal(PaymentType.Unknown, charge.PaymentInformation!.Type);
    }

    [Theory]
    [InlineData("Suspended", SubscriptionStatus.Suspended)]
    [InlineData("Unknown", SubscriptionStatus.Unknown)]
    [InlineData("Paused", SubscriptionStatus.Unknown)]
    public async Task ReadsASubscriptionsStatus(string status, SubscriptionStatus expected)
    {
        var (client, handler) = Fake.Client();
        handler.Respond(200, $$"""{"subscriptionId":"s1","status":"{{status}}","interval":"Month","frequency":1,"amount":9.99}""");

        var subscription = await client.Subscriptions.GetAsync("s1");

        Assert.Equal(expected, subscription.Status);
        Assert.Equal(SubscriptionInterval.Month, subscription.Interval);
    }

    [Fact]
    public async Task ReadsADispute()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(200, """{"disputeId":"d1","status":"NEEDS_RESPONSE","stage":"CHARGEBACK","amount":20}""");

        var dispute = await client.Disputes.GetAsync("d1");

        Assert.Equal(DisputeStatus.NeedsResponse, dispute.Status);
        Assert.Equal(DisputeStage.Chargeback, dispute.Stage);
    }

    [Fact]
    public async Task ReadsAList()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(200, """{"charges":[{"chargeId":"ch1"},{"chargeId":"ch2"}],"limit":2,"skip":0,"count":10}""");

        var list = await client.Charges.SearchAsync();

        Assert.Equal(new[] { "ch1", "ch2" }, list.Charges!.Select(c => c.ChargeId));
        Assert.Equal(10, list.Count);
    }

    [Fact]
    public async Task ReadsACustomersPaymentMethods()
    {
        var (client, handler) = Fake.Client();
        handler.Respond(200, """[{"paymentMethodId":"pm1","type":"CARD"},{"paymentMethodId":"pm2","type":"PAYTO"}]""");

        var paymentMethods = await client.Customers.GetPaymentMethodsAsync("cu1");

        Assert.Equal(new PaymentType?[] { PaymentType.Card, PaymentType.PayTo }, paymentMethods.Select(p => p.Type));
    }
}
