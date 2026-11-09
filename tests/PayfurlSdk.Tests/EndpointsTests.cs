using System.Text.Json;
using Payfurl.Sdk.Models;
using Xunit;

namespace Payfurl.Sdk.Tests;

/// <summary>Every endpoint's method, path, query and body: the whole public API the SDK covers.</summary>
public class EndpointsTests
{
    private static readonly CardRequestInformation Card = new() { CardNumber = "4111111111111111", ExpiryDate = "12/30", Ccv = "123" };
    private static readonly NewBankPayment Bank = new() { BankCode = "123-456", AccountNumber = "12345678", AccountName = "Test" };
    private static readonly NewPayToAgreement PayTo = new()
    {
        PayerName = "Test",
        PayerPayIdDetails = new PayIdDetails { PayId = "test@example.com", PayIdType = "EMAIL" },
        Description = "Test",
        MaximumAmount = 100,
        ProviderId = "p1",
    };

    // name, call, method, path and query, body (null: none)
    private static readonly (string Name, Func<PayfurlClient, Task> Call, string Method, string Path, string? Body)[] Cases =
    {
        ("status", c => c.GetStatusAsync(), "GET", "/", null),
        ("version", c => c.GetVersionAsync(), "GET", "/version", null),

        ("charges.card", c => c.Charges.CreateWithCardAsync(new NewChargeCard { Amount = 10.5m, ProviderId = "p1", Currency = "AUD", PaymentInformation = Card, Capture = false }), "POST", "/charge/card",
            """{"amount":10.5,"providerId":"p1","currency":"AUD","paymentInformation":{"cardNumber":"4111111111111111","expiryDate":"12/30","ccv":"123"},"capture":false}"""),
        ("charges.leastCost", c => c.Charges.CreateWithCardLeastCostAsync(new NewChargeCardLeastCost { Amount = 1m, Initiator = Initiator.Merchant }), "POST", "/charge/card/least_cost", """{"amount":1,"initiator":"MERCHANT"}"""),
        ("charges.customer", c => c.Charges.CreateWithCustomerAsync(new NewChargeCustomer { Amount = 1m, CustomerId = "cu1" }), "POST", "/charge/customer", """{"amount":1,"customerId":"cu1"}"""),
        ("charges.paymentMethod", c => c.Charges.CreateWithPaymentMethodAsync(new NewChargePaymentMethod { Amount = 1m, PaymentMethodId = "pm1" }), "POST", "/charge/payment_method", """{"amount":1,"paymentMethodId":"pm1"}"""),
        ("charges.token", c => c.Charges.CreateWithTokenAsync(new NewChargeToken { Amount = 1m, Token = "t1", Metadata = new() { ["a"] = "b" } }), "POST", "/charge/token", """{"amount":1,"token":"t1","metadata":{"a":"b"}}"""),
        ("charges.bankAccount", c => c.Charges.CreateWithBankAccountAsync(new NewChargeBankPayment { Amount = 1m, ProviderId = "p1", BankPaymentInformation = Bank }), "POST", "/charge/bank_account",
            """{"amount":1,"providerId":"p1","bankPaymentInformation":{"bankCode":"123-456","accountNumber":"12345678","accountName":"Test"}}"""),
        ("charges.networkToken", c => c.Charges.CreateWithNetworkTokenAsync(new NewChargeNetworkToken { NetworkTokenId = "n1", Amount = 1m, ProviderId = "p1" }), "POST", "/charge/network_token", """{"networkTokenId":"n1","amount":1,"providerId":"p1"}"""),
        ("charges.get", c => c.Charges.GetAsync("ch 1"), "GET", "/charge/ch%201", null),
        ("charges.search", c => c.Charges.SearchAsync(new ChargeSearch { Status = TransactionStatus.PartialRefund, BatchId = "b1", SubscriptionId = "s1", SortOrder = "asc", Limit = 5 }), "GET",
            "/charge?status=PARTIAL_REFUND&batchId=b1&subscriptionId=s1&sortOrder=asc&limit=5", null),
        ("charges.searchAll", c => c.Charges.SearchAsync(), "GET", "/charge", null),
        ("charges.refund", c => c.Charges.RefundAsync("ch1", 10.5m, "a&b #1"), "DELETE", "/charge/ch1?amount=10.5&comment=a%26b%20%231", null),
        ("charges.refundAll", c => c.Charges.RefundAsync("ch1"), "DELETE", "/charge/ch1", null),
        ("charges.capture", c => c.Charges.CaptureAsync("ch1", 5m), "POST", "/charge/ch1/capture", """{"amount":5}"""),
        ("charges.captureAll", c => c.Charges.CaptureAsync("ch1"), "POST", "/charge/ch1/capture", "{}"),
        ("charges.void", c => c.Charges.VoidAsync("ch1"), "DELETE", "/charge/ch1/void", null),

        ("customers.card", c => c.Customers.CreateWithCardAsync(new NewCustomerCard { ProviderId = "p1", PaymentInformation = Card, CreateNetworkToken = true, Verify = false }), "POST", "/customer/card",
            """{"providerId":"p1","paymentInformation":{"cardNumber":"4111111111111111","expiryDate":"12/30","ccv":"123"},"createNetworkToken":true,"verify":false}"""),
        ("customers.token", c => c.Customers.CreateWithTokenAsync(new NewCustomerToken { Token = "t1", Webhook = new WebhookConfig { Url = "https://example.com" } }), "POST", "/customer/token", """{"token":"t1","webhook":{"url":"https://example.com"}}"""),
        ("customers.payTo", c => c.Customers.CreateWithPayToAsync(new NewCustomerPayTo { PayToAgreement = PayTo }), "POST", "/customer/payto",
            """{"payToAgreement":{"payerName":"Test","payerPayIdDetails":{"payId":"test@example.com","payIdType":"EMAIL"},"description":"Test","maximumAmount":100,"providerId":"p1"}}"""),
        ("customers.providerToken", c => c.Customers.CreateWithProviderTokenAsync(new NewCustomerProviderToken { ProviderId = "p1", ProviderToken = "x" }), "POST", "/customer/provider_token", """{"providerId":"p1","providerToken":"x"}"""),
        ("customers.singleUseToken", c => c.Customers.CreateWithSingleUseTokenAsync(new SingleUseToken { ProviderId = "p1", ProviderToken = "x" }), "POST", "/customer/provider_single_use_token", """{"providerId":"p1","providerToken":"x"}"""),
        ("customers.bankAccount", c => c.Customers.CreateWithBankAccountAsync(new NewCustomerBankPayment { ProviderId = "p1", BankPaymentInformation = Bank }), "POST", "/customer/bank_account",
            """{"providerId":"p1","bankPaymentInformation":{"bankCode":"123-456","accountNumber":"12345678","accountName":"Test"}}"""),
        ("customers.get", c => c.Customers.GetAsync("cu1"), "GET", "/customer/cu1", null),
        ("customers.search", c => c.Customers.SearchAsync(new CustomerSearch { Email = "a+b@example.com", Skip = 10 }), "GET", "/customer?email=a%2Bb%40example.com&skip=10", null),
        ("customers.update", c => c.Customers.UpdateAsync("cu1", new UpdateCustomer { Phone = "+61400000000" }), "PUT", "/customer/cu1", """{"phone":"+61400000000"}"""),
        ("customers.delete", c => c.Customers.DeleteAsync("cu1", gatewayDelete: true), "DELETE", "/customer/cu1?gatewayDelete=true", null),
        ("customers.paymentMethods", c => c.Customers.GetPaymentMethodsAsync("cu1"), "GET", "/customer/cu1/payment_method", null),
        ("customers.addCard", c => c.Customers.AddPaymentMethodWithCardAsync("cu1", new NewPaymentMethodCard { ProviderId = "p1", PaymentInformation = Card, FallbackPaymentMethodId = "pm0" }), "POST", "/customer/cu1/payment_method/card",
            """{"providerId":"p1","paymentInformation":{"cardNumber":"4111111111111111","expiryDate":"12/30","ccv":"123"},"fallbackPaymentMethodId":"pm0"}"""),
        ("customers.addToken", c => c.Customers.AddPaymentMethodWithTokenAsync("cu1", new NewPaymentMethodToken { Token = "t1", SetDefault = true }), "POST", "/customer/cu1/payment_method/token", """{"token":"t1","setDefault":true}"""),
        ("customers.addPayTo", c => c.Customers.AddPaymentMethodWithPayToAsync("cu1", PayTo), "POST", "/customer/cu1/payment_method/payto",
            """{"payerName":"Test","payerPayIdDetails":{"payId":"test@example.com","payIdType":"EMAIL"},"description":"Test","maximumAmount":100,"providerId":"p1"}"""),
        ("customers.addBankAccount", c => c.Customers.AddPaymentMethodWithBankAccountAsync("cu1", new NewPaymentMethodBankPayment { ProviderId = "p1", BankPaymentInformation = Bank }), "POST", "/customer/cu1/bank_account",
            """{"providerId":"p1","bankPaymentInformation":{"bankCode":"123-456","accountNumber":"12345678","accountName":"Test"}}"""),

        ("paymentMethods.card", c => c.PaymentMethods.CreateWithCardAsync(new NewPaymentMethodCard { ProviderId = "p1", PaymentInformation = Card }), "POST", "/payment_method/card",
            """{"providerId":"p1","paymentInformation":{"cardNumber":"4111111111111111","expiryDate":"12/30","ccv":"123"}}"""),
        ("paymentMethods.vault", c => c.PaymentMethods.CreateWithVaultAsync(new NewPaymentMethodVault { ProviderId = "p1", PaymentMethodId = "pm1" }), "POST", "/payment_method/vault", """{"providerId":"p1","paymentMethodId":"pm1"}"""),
        ("paymentMethods.payTo", c => c.PaymentMethods.CreateWithPayToAsync(PayTo), "POST", "/payment_method/payto",
            """{"payerName":"Test","payerPayIdDetails":{"payId":"test@example.com","payIdType":"EMAIL"},"description":"Test","maximumAmount":100,"providerId":"p1"}"""),
        ("paymentMethods.token", c => c.PaymentMethods.CreateWithTokenAsync(new NewPaymentMethodToken { Token = "t1" }), "POST", "/payment_method/token", """{"token":"t1"}"""),
        ("paymentMethods.singleUseToken", c => c.PaymentMethods.CreateWithSingleUseTokenAsync(new SingleUseToken { ProviderId = "p1", ProviderToken = "x" }), "POST", "/payment_method/provider_single_use_token", """{"providerId":"p1","providerToken":"x"}"""),
        ("paymentMethods.providerToken", c => c.PaymentMethods.CreateWithProviderTokenAsync(new NewProviderToken { ProviderId = "p1", ProviderToken = "x" }), "POST", "/payment_method/provider_token", """{"providerId":"p1","providerToken":"x"}"""),
        ("paymentMethods.bankAccount", c => c.PaymentMethods.CreateWithBankAccountAsync(new NewPaymentMethodBankPayment { ProviderId = "p1", BankPaymentInformation = Bank }), "POST", "/payment_method/bank_account",
            """{"providerId":"p1","bankPaymentInformation":{"bankCode":"123-456","accountNumber":"12345678","accountName":"Test"}}"""),
        ("paymentMethods.get", c => c.PaymentMethods.GetAsync("pm1"), "GET", "/payment_method/pm1", null),
        ("paymentMethods.search", c => c.PaymentMethods.SearchAsync(new PaymentMethodSearch { PaymentType = PaymentType.ClickToPay }), "GET", "/payment_method?paymentType=CLICK2PAY", null),
        ("paymentMethods.update", c => c.PaymentMethods.UpdateAsync("pm1", new UpdatePaymentMethod { Card = new UpdatePaymentMethodCardRequestInformation { ExpiryDate = "01/31" } }), "PUT", "/payment_method/pm1", """{"card":{"expiryDate":"01/31"}}"""),
        ("paymentMethods.delete", c => c.PaymentMethods.DeleteAsync("pm1"), "DELETE", "/payment_method/pm1", null),

        ("batches.create", c => c.Batches.CreateWithPaymentMethodsAsync(new NewTransactionPaymentMethod { Count = 1, Description = "d", Batch = "PaymentMethodId,Amount\npm1,1" }), "POST", "/batch/transaction/payment_method",
            """{"count":1,"description":"d","batch":"PaymentMethodId,Amount\npm1,1"}"""),
        ("batches.get", c => c.Batches.GetAsync("b1"), "GET", "/batch/b1", null),
        ("batches.status", c => c.Batches.GetStatusAsync("b1"), "GET", "/batch/b1/status", null),
        ("batches.search", c => c.Batches.SearchAsync(new BatchSearch { Description = "d" }), "GET", "/batch?description=d", null),

        ("disputes.get", c => c.Disputes.GetAsync("d1"), "GET", "/dispute/d1", null),
        ("disputes.search", c => c.Disputes.SearchAsync(new DisputeSearch { Status = DisputeStatus.NeedsResponse, Stage = DisputeStage.Chargeback }), "GET", "/dispute?status=NEEDS_RESPONSE&stage=CHARGEBACK", null),

        ("info.providers", c => c.Info.GetProvidersAsync(10.5m, "AUD", cardProviderId: "p1"), "GET", "/info/providers?amount=10.5&currency=AUD&cardProviderId=p1", null),

        ("paymentLinks.create", c => c.PaymentLinks.CreateAsync(new CreatePaymentLink { Title = "T", Amount = 1m, Currency = "AUD" }), "POST", "/payment_link", """{"title":"T","amount":1,"currency":"AUD"}"""),
        // The browser SDK's names, which the payment link passes to its drop-in; azupay is no PaymentType.
        ("paymentLinks.create with type names", c => c.PaymentLinks.CreateAsync(new CreatePaymentLink { Title = "T", AllowedPaymentTypeNames = new() { "card", "azupay" } }), "POST", "/payment_link", """{"title":"T","allowedPaymentTypes":["card","azupay"]}"""),
#pragma warning disable CS0618 // The obsolete PaymentType view still writes the names.
        ("paymentLinks.create with PaymentType", c => c.PaymentLinks.CreateAsync(new CreatePaymentLink { Title = "T", AllowedPaymentTypes = new() { PaymentType.Card, PaymentType.PayPal } }), "POST", "/payment_link", """{"title":"T","allowedPaymentTypes":["CARD","PAYPAL"]}"""),
#pragma warning restore CS0618
        ("paymentLinks.get", c => c.PaymentLinks.GetAsync("l1"), "GET", "/payment_link/l1", null),
        ("paymentLinks.search", c => c.PaymentLinks.SearchAsync(new PaymentLinkSearch { Limit = 1 }), "GET", "/payment_link?limit=1", null),

        ("providers.create", c => c.Providers.CreateAsync(new NewProvider { Type = "DUMMY", Name = "n", Environment = "TEST", AuthenticationParameters = new() { ["k"] = "v" }, Currency = "AUD" }), "POST", "/provider",
            """{"type":"DUMMY","name":"n","environment":"TEST","authenticationParameters":{"k":"v"},"currency":"AUD"}"""),
        ("providers.update", c => c.Providers.UpdateAsync("p1", new UpdateProvider { Name = "m" }), "PUT", "/provider/p1", """{"name":"m"}"""),
        ("providers.delete", c => c.Providers.DeleteAsync("p1"), "DELETE", "/provider/p1", null),

        ("settlements.search", c => c.Settlements.SearchAsync(new DateTime(2026, 1, 2, 23, 0, 0), new DateTime(2026, 2, 3)), "GET", "/settlements?startDate=2026-01-02&endDate=2026-02-03", null),

        ("subscriptions.create", c => c.Subscriptions.CreateAsync(new SubscriptionCreate { PaymentMethodId = "pm1", Amount = 1m, Currency = "AUD", Interval = SubscriptionInterval.Month, Frequency = 1 }), "POST", "/subscription/payment_method",
            """{"paymentMethodId":"pm1","amount":1,"currency":"AUD","interval":"Month","frequency":1}"""),
        ("subscriptions.get", c => c.Subscriptions.GetAsync("s1"), "GET", "/subscription/s1", null),
        ("subscriptions.search", c => c.Subscriptions.SearchAsync(new SubscriptionSearch { Status = SubscriptionStatus.Suspended, AmountGreaterThan = 1.5m }), "GET", "/subscription?amountGreaterThan=1.5&status=Suspended", null),
        ("subscriptions.update", c => c.Subscriptions.UpdateAsync("s1", new SubscriptionUpdate { Amount = 2m, Currency = "AUD", Interval = SubscriptionInterval.Week, Frequency = 2 }), "PUT", "/subscription/s1",
            """{"amount":2,"currency":"AUD","interval":"Week","frequency":2}"""),
        ("subscriptions.status", c => c.Subscriptions.UpdateStatusAsync("s1", SubscriptionStatus.Active), "PUT", "/subscription/s1/status", """{"status":"Active"}"""),
        ("subscriptions.delete", c => c.Subscriptions.DeleteAsync("s1"), "DELETE", "/subscription/s1", null),

        ("tokens.get", c => c.Tokens.GetAsync("t1"), "GET", "/token/t1", null),
        ("tokens.search", c => c.Tokens.SearchAsync(new TokenSearch { ProviderId = "p1" }), "GET", "/token?providerId=p1", null),

        ("transfers.create", c => c.Transfers.CreateAsync(new NewTransferGroup { ProviderId = "p1", GroupReference = "g", Transfers = new() { new NewTransfer { Account = "a", Amount = 1m } } }), "POST", "/transfer",
            """{"providerId":"p1","groupReference":"g","transfers":[{"account":"a","amount":1}]}"""),
        // The API doesn't require a group reference.
        ("transfers.create without a group reference", c => c.Transfers.CreateAsync(new NewTransferGroup { ProviderId = "p1", ChargeId = "ch1", Transfers = new() { new NewTransfer { Account = "a", Amount = 1m } } }), "POST", "/transfer",
            """{"providerId":"p1","transfers":[{"account":"a","amount":1}],"chargeId":"ch1"}"""),
        ("transfers.get", c => c.Transfers.GetAsync("tr1"), "GET", "/transfer/tr1", null),
        ("transfers.search", c => c.Transfers.SearchAsync(new TransferSearch { Reference = "g" }), "GET", "/transfer?reference=g", null),

        ("vaults.create", c => c.Vaults.CreateAsync(new NewVault { CardNumber = "4111111111111111" }), "POST", "/vault", """{"cardNumber":"4111111111111111"}"""),
        ("vaults.get", c => c.Vaults.GetAsync("v1"), "GET", "/vault/v1", null),
        ("vaults.delete", c => c.Vaults.DeleteAsync("v1"), "DELETE", "/vault/v1", null),

        ("webhookSubscriptions.create", c => c.WebhookSubscriptions.CreateAsync(new NewWebhookSubscription { Url = "https://example.com", Types = new() { WebhookType.Transaction, WebhookType.Dispute } }), "POST", "/webhook/subscription",
            """{"url":"https://example.com","types":["Transaction","Dispute"]}"""),
        ("webhookSubscriptions.get", c => c.WebhookSubscriptions.GetAsync("w1"), "GET", "/webhook/subscription/w1", null),
        ("webhookSubscriptions.search", c => c.WebhookSubscriptions.SearchAsync(new WebhookSubscriptionSearch { Type = WebhookType.Dispute, SortBy = "-dateAdded" }), "GET", "/webhook/subscription?type=Dispute&sortBy=-dateAdded", null),
        ("webhookSubscriptions.delete", c => c.WebhookSubscriptions.DeleteAsync("w1"), "DELETE", "/webhook/subscription/w1", null),
    };

    public static TheoryData<string> Names() => new(Cases.Select(c => c.Name));

    [Theory]
    [MemberData(nameof(Names))]
    public async Task SendsTheRequest(string name)
    {
        var expected = Cases.Single(c => c.Name == name);
        var (client, handler) = Fake.Client();
        // A list for the endpoints that answer one.
        if (expected.Path.EndsWith("/payment_method", StringComparison.Ordinal) && expected.Path.StartsWith("/customer/", StringComparison.Ordinal)
            || expected.Path.StartsWith("/settlements", StringComparison.Ordinal) || name.StartsWith("transfers.create", StringComparison.Ordinal))
        {
            handler.Respond(200, "[]");
        }

        await expected.Call(client);

        var request = Assert.Single(handler.Requests);
        Assert.Equal(expected.Method, request.Method);
        Assert.Equal("https://sandbox-api-au.payfurl.com" + expected.Path, request.Uri.AbsoluteUri);
        if (expected.Body == null)
        {
            Assert.Null(request.Body);
        }
        else
        {
            Assert.Equal("application/json", request.ContentType);
            Assert.Equal(Normalise(expected.Body), Normalise(request.Body!));
        }
    }

    [Fact]
    public void EveryResourceMethodIsCovered()
    {
        var methods = typeof(PayfurlClient).GetProperties()
            .Where(p => p.PropertyType.Namespace == "Payfurl.Sdk.Resources")
            .SelectMany(p => p.PropertyType.GetMethods().Where(m => m.DeclaringType == p.PropertyType).Select(m => p.Name + "." + m.Name))
            .ToList();
        // One case or more per method: the cases are named resource.something, so count per resource.
        foreach (var resource in methods.GroupBy(m => m.Split('.')[0]))
        {
            var prefix = char.ToLowerInvariant(resource.Key[0]) + resource.Key.Substring(1) + ".";
            Assert.True(Cases.Count(c => c.Name.StartsWith(prefix, StringComparison.Ordinal)) >= resource.Count(), $"{resource.Key} has {resource.Count()} methods but fewer cases");
        }
    }

    private static string Normalise(string json) => JsonSerializer.Serialize(JsonDocument.Parse(json).RootElement);
}
