# Upgrading from 2.x to 3.0

3.0 is a new SDK in the same NuGet package, `PayfurlSdk`. Code that uses the SDK changes, but each 2.x call has a
direct replacement (below). 2.x stays on NuGet: `PayfurlSdk` 2.2.1 has the 2.x fixes, and pinning `2.*` keeps
installing it until you move.

```bash
dotnet add package PayfurlSdk --version 3.0.0
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
| `WebhookTools.DeserializeTransaction(body, signature, key)` returning `WebhookTransaction` | `Webhook.Parse(body, signature, key)` returning the JSON as a `JsonElement`; it still throws `ArgumentException` for a bad signature |
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
