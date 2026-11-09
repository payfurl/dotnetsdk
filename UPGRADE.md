# Upgrading from 2.x to 3.0

3.0 is a breaking release: a new SDK in the same NuGet package, `PayfurlSdk`. Code written for 2.x does not compile
against it, but each 2.x call has a direct replacement. The [recipe](#migration-recipe) below takes an integration
across in one pass; the sections after it list every change.

## Staying on 2.x until you move

Nothing upgrades by itself. NuGet restores the lowest version a project's reference allows, so a project that names
2.x keeps 2.x. Two things do move it: someone choosing 3.0 (the Visual Studio package manager, `dotnet add package`),
and a dependency bot (Dependabot, Renovate) opening a major-version update. Merge that update together with the
migration, not on its own: on its own it does not build.

To make sure, cap the version:

```xml
<PackageReference Include="PayfurlSdk" Version="[2.2.1,3.0)" />
```

2.2.1 has the last 2.x fixes: refund amounts on comma-decimal cultures, URL-encoded refund comments, the API's
`IsRetryable`, timeouts, constant-time webhook signatures and dispute webhooks ([CHANGELOG.md](CHANGELOG.md)). Take it
whatever you decide about 3.0. 2.x gets no new features: new API fields and endpoints go into 3.x.

## Migration recipe

1. **Install 3.0** and build: every 2.x call is now a compile error, which makes the list of places to change.

   ```bash
   dotnet add package PayfurlSdk --version 3.0.0
   ```

2. **Replace the namespaces.** `using payfurl.sdk;` and `using payfurl.sdk.Models;` become `using Payfurl.Sdk;` and
   `using Payfurl.Sdk.Models;`. Drop `using payfurl.sdk.Models.Subscriptions;` and `using payfurl.sdk.Tools;`. 2.x's
   `Environment` no longer hides `System.Environment`.

3. **Replace `Config.Setup` with one client per account**, made once. In an ASP.NET Core app, register it:

   ```csharp
   builder.Services.AddSingleton(new PayfurlClientOptions
   {
       SecretKey = builder.Configuration["Payfurl:SecretKey"]!,
       Environment = PayfurlEnvironment.Sandbox, // was Environment.SANDBOX
       Timeout = TimeSpan.FromSeconds(60),        // was Config.Setup's timeoutMilliseconds
   });
   builder.Services.AddHttpClient<PayfurlClient>();
   ```

   and take `PayfurlClient` in the constructors that did `new Charge()`, `new Customer()` and so on. Elsewhere, keep
   one `new PayfurlClient(options)` in a static field or your container. Don't make one per request: it is cheap,
   but it is meant to live as long as the app.

4. **Make the callers async.** `new Charge().CreateWithToken(x)` becomes `await payfurl.Charges.CreateWithTokenAsync(x)`,
   and each method's name is in [Every method](#every-method). Pass your request's `CancellationToken` where you
   have one. Code that cannot be async yet can call `.GetAwaiter().GetResult()`, as 2.x did inside every sync method.

5. **Fill in the required fields.** The compiler names each request model field the API requires and the code did
   not set. Set it: the API would have rejected the request anyway.

6. **Compare statuses as enums.** `charge.Status == "SUCCESS"` becomes `charge.Status == TransactionStatus.Success`;
   the same for `SubscriptionStatus`, `DisputeStatus`, `DisputeStage` and `PaymentType`. A value the SDK does not
   know yet is `Unknown`, so a `switch` needs a branch for it.

7. **Handle nulls in responses.** Every response property is nullable: `charge.Amount` is a `decimal?`. Where 2.x
   gave `0`, `false` or `DateTime.MinValue` for a missing field, use `?? 0m`, `== true`, or `.HasValue`.

8. **Catch the new exceptions.** `catch (ApiException e)` still catches every API error. `e.Code` is `e.ErrorCode`
   and `e.ErrorData.X` is `e.X`. Add `catch (PayfurlConnectionException)` where a 2.x timeout surfaced as an
   `ApiException` with HTTP 408, and stop retrying charges yourself on `IsRetryable` alone: it now says what the API
   says, and a charge whose outcome is unknown should be searched for by reference first.

9. **Move the webhook handler** to `Webhook.Parse<ChargeData>` (or `Webhook.Verify` and your own parsing).

10. **Run your tests.** Tests that mocked the 2.x interfaces (`ICharge`, `ICustomer`, ...) can give the client an
    `HttpClient` over a fake handler instead (README, *Testing your code*).

### Before and after

A typical 2.x integration: charge a token, refund part of it, save a card, and take the webhook.

```csharp
// 2.x
using payfurl.sdk;
using payfurl.sdk.Models;

Config.Setup(secretKey, Environment.SANDBOX);

var charge = new Charge().CreateWithToken(new NewChargeToken { Amount = 20, Currency = "AUD", Token = token, Reference = orderId });
if (charge.Status != "SUCCESS") { /* ... */ }

new Charge().Refund(new NewRefund { ChargeId = charge.ChargeId, RefundAmount = 5, Comment = "Damaged" });

var customer = new Customer().CreateWithToken(new NewCustomerToken { Token = token, Email = email });
var paymentMethodId = customer.DefaultPaymentMethod.PaymentMethodId;

try
{
    new Charge().CreateWithPaymentMethod(new NewChargePaymentMethod { Amount = 10, PaymentMethodId = paymentMethodId });
}
catch (ApiException e) when (e.Code == ErrorCodeType.InsufficientFunds)
{
    // declined
}

var webhook = WebhookTools.DeserializeTransaction(body, signatureHeader, signingKey);
var webhookChargeId = webhook.Data.ChargeId;
```

```csharp
// 3.0
using Payfurl.Sdk;
using Payfurl.Sdk.Models;

var payfurl = new PayfurlClient(new PayfurlClientOptions { SecretKey = secretKey, Environment = PayfurlEnvironment.Sandbox });

var charge = await payfurl.Charges.CreateWithTokenAsync(new NewChargeToken { Amount = 20m, Currency = "AUD", Token = token, Reference = orderId });
if (charge.Status != TransactionStatus.Success) { /* ... */ }

await payfurl.Charges.RefundAsync(charge.ChargeId!, amount: 5m, comment: "Damaged");

var customer = await payfurl.Customers.CreateWithTokenAsync(new NewCustomerToken { Token = token, Email = email });
var paymentMethodId = customer.DefaultPaymentMethod!.PaymentMethodId!;

try
{
    await payfurl.Charges.CreateWithPaymentMethodAsync(new NewChargePaymentMethod { Amount = 10m, PaymentMethodId = paymentMethodId });
}
catch (ApiException e) when (e.ErrorCode == ErrorCode.InsufficientFunds)
{
    // declined
}

var webhook = Webhook.Parse<ChargeData>(body, signatureHeader, signingKey);
var webhookChargeId = webhook.Data!.ChargeId;
```

## Frameworks

3.0 targets `netstandard2.0` and `net10.0`. .NET Framework apps need 4.6.2 or later (2.x also built for 4.5.1 and
4.8); .NET (Core) apps need a version that supports .NET Standard 2.0, which is all of them since 2.0. Newtonsoft.Json
and `Microsoft.AspNet.WebApi` are no longer dependencies: the SDK uses `System.Text.Json`.

## A client replaces `Config`

2.x was configured once per process, and each part of the API was created with `new`:

```csharp
using payfurl.sdk;
using payfurl.sdk.Models;

Config.Setup(secretKey, Environment.SANDBOX);
var charge = new Charge().CreateWithToken(new NewChargeToken { Amount = 20, Currency = "AUD", Token = token });
```

3.0 has a client per key. An app with several payFURL accounts makes one client for each:

```csharp
using Payfurl.Sdk;
using Payfurl.Sdk.Models;

var payfurl = new PayfurlClient(new PayfurlClientOptions { SecretKey = secretKey, Environment = PayfurlEnvironment.Sandbox });
var charge = await payfurl.Charges.CreateWithTokenAsync(new NewChargeToken { Amount = 20m, Currency = "AUD", Token = token });
```

| 2.x | 3.0 |
| --- | --- |
| namespaces `payfurl.sdk`, `payfurl.sdk.Models`, `payfurl.sdk.Models.Subscriptions` | `Payfurl.Sdk`, `Payfurl.Sdk.Models`, `Payfurl.Sdk.Resources` |
| `Config.Setup(key, environment)` | `new PayfurlClient(new PayfurlClientOptions { SecretKey = key, Environment = environment })` |
| `Config.Setup(..., timeoutMilliseconds)` | `PayfurlClientOptions.Timeout`, a `TimeSpan` |
| `Environment.LOCAL`, `DEVELOPMENT`, `SANDBOX`, `PRODUCTION`, `PROD` | `PayfurlEnvironment.Local`, `Development`, `Sandbox`, `Production` |
| `Config.BaseUrl` (read only) | `PayfurlClientOptions.BaseUrl` points any environment elsewhere |
| a static `HttpClient`, TLS 1.2 forced and `DefaultConnectionLimit = 9999` for the whole process | your `HttpClient` (`new PayfurlClient(options, httpClient)`, or `AddHttpClient<PayfurlClient>()`), or one the SDK shares; no process-wide settings |

## Async only

Every method is async and takes an optional `CancellationToken`. The sync methods are gone: they blocked on the
async ones (`AsyncHelper.RunSync`), which can deadlock and holds a thread for each call. Code that cannot be async
can call `.GetAwaiter().GetResult()` itself, knowing the cost.

## Models

The request models are named as in 2.x, after the API's own models, and the fields the API requires are `required`
properties: code that leaves one out no longer compiles. The response models' properties are all nullable: a field
the response leaves out is `null`, where 2.x gave `0`, `false` or `DateTime.MinValue`.

| 2.x | 3.0 |
| --- | --- |
| `ChargeData.Status` and the other statuses as `string` | `TransactionStatus`, `SubscriptionStatus`, `DisputeStatus`, `DisputeStage` enums; a value the SDK does not know yet is `Unknown` |
| `PaymentData.Type` and the search's `PaymentType` as `string` | `PaymentType` |
| `NewRefund` | `Charges.RefundAsync(chargeId, amount, comment)` |
| `NewChargeCapture` | `Charges.CaptureAsync(chargeId, amount)` |
| `UpdateSubscriptionStatus` | `Subscriptions.UpdateStatusAsync(subscriptionId, status)` |
| `NewCustomerSingleUseToken`, `NewPaymentMethodSingleUseToken` | `SingleUseToken`, the API's name |
| `PaymentLinkData`, `SearchPaymentLink`, `SearchPaymentLinkResult` | `PaymentLink`, `PaymentLinkSearch`, `PaymentLinkList` |
| `WebhookSubscriptionData`, `WebhookSubscriptionSearchResults` | `WebhookSubscription`, `WebhookSubscriptionList` |
| `DisputeData` | `Dispute` |
| `TokenDataList` | `TokenList` |
| `ErrorCodeType` | `ErrorCode`, in `Payfurl.Sdk` |
| `TokenData.GatewayTokenId`, `TokenData.UserId`, `ChargeData.ThreeDSServerTransID` | gone: the API never sends them |
| `SubscriptionSearch.Sort` and `WebhookSubscriptionSearch.Sort`, of a `SortBy` enum | `SortBy`, a string, as the API takes it (`"-dateAdded"`) |

New fields the API takes or returns: `CreateNetworkToken`, `Verify` and `Webhook` on `NewCustomerCard`;
`CreateNetworkToken`, `FallbackPaymentMethodId`, `Ip`, `Verify` and `Webhook` on `NewPaymentMethodCard`;
`ExternalThreeDsData` and `ThreeDSServerTransID` on `CardRequestInformation`; `Ip` and `IpInfo` on `ChargeData`;
`Comment` on a charge's refunds; `BatchId`, `SubscriptionId` and `SortOrder` on `ChargeSearch`; and more, listed in
the changelog.

Requests send camelCase JSON and leave out the fields you did not set, so the API applies its own defaults.

## Errors

| 2.x | 3.0 |
| --- | --- |
| `ApiException` with `ErrorData`, `Code` and `IsRetryable` (always `true`) | `ApiException` with `HttpStatus`, `ErrorCode`, `IsRetryable` (the API's), `Type`, `Resource`, `Details`, `GatewayCode`, `GatewayMessage`, `ChargeId`, `TraceId` |
| `ApiException` with HTTP 408 for a timeout | `PayfurlTimeoutException` |
| a connection error as `ApiException` | `PayfurlConnectionException` |
| none | `AuthenticationException` (401, 403), `NotFoundException` (404), `InvalidRequestException` (a validation error), all `ApiException`s |
| `JsonReaderException` for a body that is not JSON | `ApiException`, with the parsing error as its inner exception |

Catch `PayfurlException` for everything. 3.0 retries GETs itself (`MaxRetries`, 2 by default); it never retries
anything else.

## Webhooks

| 2.x | 3.0 |
| --- | --- |
| `WebhookTools.DeserializeTransaction(body, signature, key)` returning `WebhookTransaction` | `Webhook.Parse<ChargeData>(body, signature, key)` returning a `WebhookEvent<ChargeData>` (`Meta`, `Data`); it still throws `ArgumentException` for a bad signature |
| `WebhookTransaction.Data`, `WebhookMeta` | `WebhookEvent<T>.Data` (the API's own model: `ChargeData`, `CustomerData`, ...), `WebhookMeta` with `Type` as a `WebhookType` |
| none | `Webhook.Parse(body, signature, key)`, the JSON as a `JsonElement` |
| none | `Webhook.Verify(body, signature, key)`, a `bool` |

## Every method

`payfurl` is a `PayfurlClient`. Each 3.0 method ends in `Async` and returns a `Task`.

| 2.x | 3.0 |
| --- | --- |
| `new Charge().CreateWithCard(c)` | `payfurl.Charges.CreateWithCardAsync(c)` |
| `CreateWithCardLeastCost`, `CreateWithCustomer`, `CreateWithPaymentMethod`, `CreateWithToken`, `CreateWithNetworkToken`, `CreateWithBankAccount` | the same names on `payfurl.Charges`, with `Async` |
| `Charge.CreateWithApplePay(c, providerId)` | gone: `POST /charge/token/apple` takes only a public key, so it always failed |
| `Charge.Refund(new NewRefund { ChargeId, RefundAmount, Comment })` | `payfurl.Charges.RefundAsync(chargeId, amount, comment)` |
| `Charge.Capture(chargeId, new NewChargeCapture { Amount })` | `payfurl.Charges.CaptureAsync(chargeId, amount)` |
| `Charge.Void(chargeId)` | `payfurl.Charges.VoidAsync(chargeId)` |
| `Charge.Single(chargeId)` | `payfurl.Charges.GetAsync(chargeId)` |
| `Charge.Search(search)` | `payfurl.Charges.SearchAsync(search)` |
| `new Customer().CreateWithCard`, `CreateWithToken`, `CreateWithProviderToken`, `CreateWithBankAccount` | the same names on `payfurl.Customers` |
| `Customer.CreateWithSingleUseToken(t)` | `payfurl.Customers.CreateWithSingleUseTokenAsync(SingleUseToken)` |
| none | `payfurl.Customers.CreateWithPayToAsync(NewCustomerPayTo)` |
| `Customer.CreatePaymentMethodWithCard`, `...WithToken`, `...WithPayTo`, `...WithBankAccount` | `payfurl.Customers.AddPaymentMethodWithCardAsync`, `...WithTokenAsync`, `...WithPayToAsync`, `...WithBankAccountAsync` |
| `Customer.GetPaymentMethods(customerId)` | `payfurl.Customers.GetPaymentMethodsAsync(customerId)` |
| `Customer.Single`, `Search`, `Update`, `Delete` | `payfurl.Customers.GetAsync`, `SearchAsync`, `UpdateAsync`, `DeleteAsync` (with `gatewayDelete`) |
| `new PaymentMethod().CreatePaymentMethodWithCard`, `...WithVault`, `...WithPayto`, `...WithSingleUseToken`, `...WithBankAccount` | `payfurl.PaymentMethods.CreateWithCardAsync`, `CreateWithVaultAsync`, `CreateWithPayToAsync`, `CreateWithSingleUseTokenAsync`, `CreateWithBankAccountAsync` |
| `PaymentMethod.CreateWithProviderToken`, `CreateWithToken` | `payfurl.PaymentMethods.CreateWithProviderTokenAsync`, `CreateWithTokenAsync` |
| `PaymentMethod.Single`, `Search`, `UpdatePaymentMethod`, `Delete` | `payfurl.PaymentMethods.GetAsync`, `SearchAsync`, `UpdateAsync`, `DeleteAsync` (with `gatewayDelete`) |
| `new Subscription().CreateSubscription`, `GetSubscription`, `SearchSubscription`, `UpdateSubscription`, `DeleteSubscription` | `payfurl.Subscriptions.CreateAsync`, `GetAsync`, `SearchAsync`, `UpdateAsync`, `DeleteAsync` |
| `Subscription.UpdateSubscriptionStatus(id, new UpdateSubscriptionStatus { Status })` | `payfurl.Subscriptions.UpdateStatusAsync(id, status)` |
| `new Batch().CreateTransactionWithPaymentMethod`, `GetBatch`, `GetBatchStatus`, `SearchBatch` | `payfurl.Batches.CreateWithPaymentMethodsAsync`, `GetAsync`, `GetStatusAsync`, `SearchAsync` |
| `new PaymentLink().Create`, `Single`, `Search` | `payfurl.PaymentLinks.CreateAsync`, `GetAsync`, `SearchAsync` |
| `new WebhookSubscription().CreateWebhookSubscription`, `GetWebhookSubscription`, `SearchWebhookSubscription`, `DeleteWebhookSubscription` | `payfurl.WebhookSubscriptions.CreateAsync`, `GetAsync`, `SearchAsync`, `DeleteAsync` |
| `new Dispute().Single`, `Search` | `payfurl.Disputes.GetAsync`, `SearchAsync` |
| `new Transfer().Create`, `Single`, `Search` | `payfurl.Transfers.CreateAsync`, `GetAsync`, `SearchAsync` |
| `new Vault().Create`, `Single`, `Delete` | `payfurl.Vaults.CreateAsync`, `GetAsync`, `DeleteAsync` |
| `new Provider().Create`, `Update`, `Delete` | `payfurl.Providers.CreateAsync`, `UpdateAsync`, `DeleteAsync` |
| `new Token().Single`, `Search` | `payfurl.Tokens.GetAsync`, `SearchAsync` |
| `new Settlement().GetSettlements(start, end)` | `payfurl.Settlements.SearchAsync(start, end)` |
| `new Version().Get()` | `payfurl.GetVersionAsync()` |
| none | `payfurl.GetStatusAsync()` (`GET /`) |
| `new Info().GetProvidersInfo(amount, currency, cardProviderId)` | `payfurl.Info.GetProvidersAsync(amount, currency, browserSdkVersion, cardProviderId)` |
| `Info.GetInfo()`, `GetProviderInfo()`, `GetProviderTokenInfo()`, `GetDefaultFallbackProvider()` | gone: their endpoints are not public |

`Info.GetProvidersAsync` no longer sends `sdk-version: 4.5.7`. Pass the browser SDK version your checkout runs as
`browserSdkVersion`: the API lists PayPal only for version 4 or later. Every request now sends
`User-Agent: payfurl-dotnet/<version>`.
