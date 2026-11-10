# payFURL .NET SDK

The [payFURL](https://payfurl.com) API for .NET: charges, customers, payment methods, subscriptions, payment links,
batches, transfers, vaults, providers, tokens, disputes, settlements and webhook subscriptions. It covers the
endpoints a secret key can call, plus `GET /info/providers`. The API itself, its fields and its error codes are
documented at [docs.payfurl.com](https://docs.payfurl.com).

What changed in each release: [CHANGELOG.md](https://github.com/payfurl/dotnetsdk/blob/master/CHANGELOG.md).

## Upgrading from 2.x

3.0 is a breaking release: code written for 2.x does not compile against it. Nothing upgrades by itself, since NuGet
keeps a project on the 2.x version it names, but a dependency bot will offer 3.0 as a major update: merge it together
with the migration. [UPGRADE.md](https://github.com/payfurl/dotnetsdk/blob/master/UPGRADE.md) has a step-by-step recipe with before-and-after code, and maps every
2.x call to its replacement.

To stay on 2.x for now, take 2.2.1, which has the last 2.x fixes, and cap the version:

```xml
<PackageReference Include="PayfurlSdk" Version="[2.2.1,3.0)" />
```

## Install

```bash
dotnet add package PayfurlSdk
```

It targets `netstandard2.0` and `net10.0`, so it runs on .NET 10, .NET 8 and the other .NET versions, and on .NET
Framework 4.6.2 or later. Its one dependency is `System.Text.Json`, on `netstandard2.0` only. Compiling against it
needs Visual Studio 2022 17.4 or later, or the .NET 7 SDK or later, with any C# language version (older compilers
cannot read `required` members); a .NET Framework project also references `System.Net.Http`.

## Quick start

```csharp
using Payfurl.Sdk;
using Payfurl.Sdk.Models;

var payfurl = new PayfurlClient(new PayfurlClientOptions
{
    SecretKey = "sectest...-au",
    Environment = PayfurlEnvironment.Sandbox,
});

// A token from the browser SDK or a hosted payment page.
var charge = await payfurl.Charges.CreateWithTokenAsync(new NewChargeToken
{
    Amount = 20.00m,
    Currency = "AUD",
    Token = token,
    Reference = "order-1001",
});

Console.WriteLine($"{charge.ChargeId}: {charge.Status}"); // ...: Success
```

Every call is async, takes an optional `CancellationToken`, and returns the API's response as a model
(`ChargeData`, `CustomerData`, ...). Request models mark the fields the API requires as `required`, so leaving one
out is a compile error.

A client holds one account's key and no other state: make one per account or region, and keep it for the life of
the app.

## Dependency injection

`PayfurlClient` works as a typed client of `IHttpClientFactory`:

```csharp
builder.Services.AddSingleton(new PayfurlClientOptions
{
    SecretKey = builder.Configuration["Payfurl:SecretKey"]!,
    Environment = PayfurlEnvironment.Production,
});
builder.Services.AddHttpClient<PayfurlClient>();
```

Then take a `PayfurlClient` in a constructor. Without an `HttpClient`, a client uses one shared by the process.

## Environments and regions

| `PayfurlEnvironment` | URL | with an `-au` key |
| --- | --- | --- |
| `Production` | https://api.payfurl.com | https://api-au.payfurl.com |
| `Sandbox` | https://sandbox-api.payfurl.com | https://sandbox-api-au.payfurl.com |

The key's last part (`sectest...-au`) is its account's region. `BaseUrl` overrides both, for a proxy say.
`Development` and `Local` are payFURL's own environments.

## Examples

```csharp
// Charges. A card number on your server puts it in PCI DSS scope; a token from the browser SDK keeps it out.
var charge = await payfurl.Charges.CreateWithCardLeastCostAsync(new NewChargeCardLeastCost
{
    Amount = 15m,
    Currency = "AUD",
    PaymentInformation = new CardRequestInformation { CardNumber = "4111111111111111", ExpiryDate = "12/30", Ccv = "123" },
});
await payfurl.Charges.RefundAsync(charge.ChargeId!, amount: 5m, comment: "Returned one item");
var authorised = await payfurl.Charges.CreateWithPaymentMethodAsync(new NewChargePaymentMethod { Amount = 50m, PaymentMethodId = paymentMethodId, Capture = false });
await payfurl.Charges.CaptureAsync(authorised.ChargeId!, amount: 45m);
var page = await payfurl.Charges.SearchAsync(new ChargeSearch { Status = TransactionStatus.Success, Limit = 20 });

// Customers and their payment methods
var customer = await payfurl.Customers.CreateWithTokenAsync(new NewCustomerToken { Token = token, Email = "jo@example.com" });
var paymentMethods = await payfurl.Customers.GetPaymentMethodsAsync(customer.CustomerId!);
await payfurl.Charges.CreateWithCustomerAsync(new NewChargeCustomer { Amount = 10m, CustomerId = customer.CustomerId! });

// Payment methods without a customer
var paymentMethod = await payfurl.PaymentMethods.CreateWithTokenAsync(new NewPaymentMethodToken { Token = token });

// Subscriptions
var subscription = await payfurl.Subscriptions.CreateAsync(new SubscriptionCreate
{
    PaymentMethodId = paymentMethod.PaymentMethodId!,
    Amount = 9.99m,
    Currency = "AUD",
    Interval = SubscriptionInterval.Month,
    Frequency = 1,
});
await payfurl.Subscriptions.UpdateStatusAsync(subscription.SubscriptionId!, SubscriptionStatus.Suspended);

// Payment links
var link = await payfurl.PaymentLinks.CreateAsync(new CreatePaymentLink { Title = "Invoice 1001", Amount = 120m, Currency = "AUD" });

// The rest
await payfurl.Batches.CreateWithPaymentMethodsAsync(new NewTransactionPaymentMethod { Count = 1, Description = "November", Batch = "PaymentMethodId,Amount,Currency\npm_1,10,AUD" });
await payfurl.Transfers.CreateAsync(new NewTransferGroup { ProviderId = providerId, Transfers = new() { new NewTransfer { Account = "acct_1", Amount = 5m } } });
await payfurl.Vaults.CreateAsync(new NewVault { CardNumber = "4111111111111111" });
await payfurl.Providers.UpdateAsync(providerId, new UpdateProvider { Name = "Main gateway" });
await payfurl.Tokens.GetAsync(tokenId);
await payfurl.Disputes.SearchAsync(new DisputeSearch { Status = DisputeStatus.NeedsResponse });
await payfurl.Settlements.SearchAsync(DateTime.Today.AddDays(-7));
await payfurl.WebhookSubscriptions.CreateAsync(new NewWebhookSubscription { Url = "https://example.com/payfurl", Types = new() { WebhookType.Transaction } });
var checkout = await payfurl.Info.GetProvidersAsync(amount: 20m, currency: "AUD");
```

A status the SDK does not know yet (a new charge status, say) reads as `Unknown` instead of failing the call.

## Errors and retries

Every failed call throws a `PayfurlException`:

| Exception | When |
| --- | --- |
| `ApiException` | the API answered with an error: a decline, a server error. It has `ErrorCode`, `HttpStatus`, `IsRetryable`, `Details`, `GatewayCode`, `GatewayMessage`, `ChargeId` and `TraceId` |
| `AuthenticationException` | 401 or 403: a wrong key, or a public key on a secret-key endpoint |
| `NotFoundException` | 404 |
| `InvalidRequestException` | a validation error; `Details` names the fields |
| `PayfurlConnectionException` | no answer: the connection failed |
| `PayfurlTimeoutException` | no answer within `Timeout` (60 seconds by default) |

The middle three are `ApiException`s, and `PayfurlTimeoutException` is a `PayfurlConnectionException`. An empty ID
throws `ArgumentException` before anything is sent.

```csharp
try
{
    await payfurl.Charges.CreateWithTokenAsync(request);
}
catch (ApiException e) when (e.ErrorCode == ErrorCode.InsufficientFunds)
{
    // declined: e.ChargeId is the failed charge
}
catch (PayfurlConnectionException)
{
    // the outcome is unknown: search by reference before charging again
}
```

GETs are retried `MaxRetries` times (2 by default), with a doubling `RetryDelay`, after a connection error, a timeout
or an error the API marks retryable. Nothing else is retried: the API has no idempotency keys, so a retried charge
could charge twice.

## Webhooks

```csharp
var body = await new StreamReader(Request.Body).ReadToEndAsync();
if (!Webhook.Verify(body, Request.Headers["X-Payfurl-Signature"], signingKey))
{
    return Unauthorized();
}

// Or verify and parse in one step; it throws ArgumentException for a bad signature. A transaction webhook's data is a
// ChargeData, a customer's a CustomerData, and so on (webhook.Meta.Type).
var webhook = Webhook.Parse<ChargeData>(body, Request.Headers["X-Payfurl-Signature"], signingKey);
var chargeId = webhook.Data?.ChargeId;
```

`Webhook.Parse` without a type gives the JSON as a `JsonElement`.

The signature is compared in constant time.

## Testing your code

Give the client an `HttpClient` over your own handler, and nothing goes over the network:

```csharp
class FakeHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
        Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("""{"chargeId":"ch_1","status":"SUCCESS"}""", Encoding.UTF8, "application/json"),
        });
}

var payfurl = new PayfurlClient(
    new PayfurlClientOptions { SecretKey = "sectest", Environment = PayfurlEnvironment.Sandbox },
    new HttpClient(new FakeHandler()));
```

The SDK's own tests do the same: [tests/PayfurlSdk.Tests/FakeHandler.cs](https://github.com/payfurl/dotnetsdk/blob/master/tests/PayfurlSdk.Tests/FakeHandler.cs).

## Development

You need [just](https://github.com/casey/just), the .NET 10 SDK, and the .NET 8 runtime, on which the tests run the
`netstandard2.0` build:

```bash
just test   # format check, build, unit tests: what CI runs on every pull request
just format # apply the style
just pack   # the package, into nupkg/
```

The unit tests never call an API. payFURL also runs integration tests against an API built from source before each
release.

## License

MIT
