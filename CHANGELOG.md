# Changelog

Every release of the `PayfurlSdk` NuGet package. The format follows
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and versions follow
[Semantic Versioning](https://semver.org/spec/v2.0.0.html). Entries before 2.2.1 were written back from NuGet's
version history and the merged pull requests; only `v1.5` was tagged before 2.2.1.

## [Unreleased]

## [3.0.0] - 2026-11-09

**Breaking.** A new SDK in the same package: a client object instead of static configuration, async only, typed and
nullable. Code written for 2.x does not compile against it. [UPGRADE.md](UPGRADE.md) has a migration recipe with
before-and-after code and maps every 2.x method to its replacement.

Nothing upgrades by itself: a project that names 2.x keeps it. To stay on 2.x for now, take 2.2.1 and cap the version
at `[2.2.1,3.0)`. 2.x gets no new features.

### Added

- `PayfurlClient`: one client per key and environment, with a property per part of the API (`Charges`,
  `Customers`, `PaymentMethods`, ...). `PayfurlClientOptions` sets the key, environment, base URL, timeout, retries
  and retry delay. It takes an `HttpClient`, so it works as an `IHttpClientFactory` typed client and in tests.
- `CancellationToken` on every method, and `ConfigureAwait(false)` throughout.
- Nullable reference types, `required` on the request fields the API requires, and XML docs.
- Exceptions under `PayfurlException`: `ApiException` (HTTP status, `ErrorCode`, the API's `IsRetryable`, type,
  resource, details, gateway code and message, charge ID, trace ID), `AuthenticationException`,
  `NotFoundException`, `InvalidRequestException`, `PayfurlConnectionException` and `PayfurlTimeoutException`.
- Retries: a GET is retried, with backoff, after a connection error, a timeout or a retryable API error. Nothing
  else is retried, since the API has no idempotency keys.
- Enums that read a value the SDK does not know as `Unknown`: `TransactionStatus` (`ChargeData.Status`),
  `SubscriptionStatus` (with the API's `Unknown`), `DisputeStatus`, `DisputeStage` and `PaymentType`.
- `Customers.CreateWithPayToAsync` (`POST /customer/payto`) and `PayfurlClient.GetStatusAsync` (`GET /`).
- `gatewayDelete` on `Customers.DeleteAsync` and `PaymentMethods.DeleteAsync`.
- `BatchId`, `SubscriptionId` and `SortOrder` on `ChargeSearch`.
- Fields the API has and 2.x lacked: `CreateNetworkToken`, `Verify` and `Webhook` on `NewCustomerCard`; `Webhook` on
  `NewCustomerToken`; `CreateNetworkToken`, `FallbackPaymentMethodId`, `Ip`, `Verify` and `Webhook` on
  `NewPaymentMethodCard`; `ExternalThreeDsData` and `ThreeDSServerTransID` on `CardRequestInformation`; `Ip` and
  `IpInfo` on `ChargeData`; `Comment` and `GatewayRefundId` on refunds; the rest of the models brought in line with
  the API's.
- `Webhook.Verify`, and `Webhook.Parse<T>`, which reads a webhook into a `WebhookEvent<T>` (`Meta`, and `Data` as
  the API's model, such as `ChargeData`). `Webhook.Parse` without a type gives the JSON.
- Every request sends `User-Agent: payfurl-dotnet/<version>`.
- SourceLink, a deterministic build, a symbol package (`.snupkg`), the README in the package, and package validation.

### Changed

- Targets `netstandard2.0` and `net10.0` (.NET Framework 4.6.2 or later through `netstandard2.0`).
- `System.Text.Json` instead of Newtonsoft.Json.
- Namespaces `Payfurl.Sdk`, `Payfurl.Sdk.Models` and `Payfurl.Sdk.Resources`; the assembly is `PayfurlSdk.dll`.
- Methods are named for what they do on their part of the API: `Charges.GetAsync` (was `Charge.Single`),
  `Customers.AddPaymentMethodWithCardAsync` (was `CreatePaymentMethodWithCard`), and so on.
- Refunds, captures and subscription status changes take their values as arguments, not request models.
- Response properties are nullable: a field the response leaves out is `null`.
- Requests send camelCase JSON without the fields left unset.
- A webhook with a bad signature still throws `ArgumentException`.

### Removed

- `Config` and its static state, including the process-wide `ServicePointManager` settings (TLS 1.2 only,
  `DefaultConnectionLimit = 9999`).
- The sync methods, which blocked on the async ones (`AsyncHelper.RunSync`).
- `Charge.CreateWithApplePay`: `POST /charge/token/apple` takes only a public key, so it always failed.
- `Info.GetInfo`, `Info.GetProviderInfo`, `Info.GetProviderTokenInfo` and `Info.GetDefaultFallbackProvider`: their
  endpoints are not public.
- The fixed `sdk-version: 4.5.7` header: `Info.GetProvidersAsync` takes the browser SDK version.
- `TokenData.GatewayTokenId`, `TokenData.UserId` and `ChargeData.ThreeDSServerTransID`, which the API never sends.
- `WebhookTools` and the `WebhookTransaction` models: `Webhook` and `WebhookEvent<T>` replace them.
- The `netstandard2.1`, `netcoreapp2.2`, `net451` and `net48` targets, and the `Microsoft.AspNet.WebApi`,
  `Microsoft.NETCore.App`, `System.ValueTuple` and `NETStandard.Library` dependencies.
- The functional tests that called payFURL's development environment: unit tests replace them, and payFURL runs
  the integration tests against an API built from source.

## [2.2.1] - 2026-11-09

### Fixed

- A refund's amount, and the charge search's `AmountGreaterThan` and `AmountLessThan`, are sent in the invariant
  culture. On a comma-decimal culture such as `de-DE` a refund of 10.5 was sent as `amount=10,5`.
- A refund's comment is URL-encoded: an `&` or `#` in it broke the query.
- The search date filters are sent as `yyyy-MM-dd HH:mm:ss` in the invariant culture (most had a stray space,
  `HH: mm:ss`, and all used the current culture's time separator).
- `Customer.GetPaymentMethods` no longer URL-encodes the customer ID twice.
- `ApiException.IsRetryable` is the API's `isRetryable`. It was `true` for every error, declines and validation
  errors included. An error without the API's body is retryable for HTTP 5xx and 429, and after a network error.
- An error response that is not JSON (a load balancer's HTML 502) throws `ApiException`, with the parsing error as
  its inner exception, instead of a `JsonReaderException`. So does a success response that is not JSON.
- `Config.Setup`'s timeout applies to every call made after it. It was read once, when the first call was made,
  and a call made before `Setup` failed with `TypeInitializationException`. The default is 60 seconds.
- `Config.Environment` is set by `Config.Setup`.
- `WebhookTools.DeserializeTransaction` compares signatures in constant time, rejects a missing signature and
  throws `ArgumentException` for an empty signature key (was `NullReferenceException`).
- `WebhookType.Dispute`: dispute webhook subscriptions can be created, and reading one no longer throws.
- The package's release notes link this changelog.

## [2.2.0] - 2026-09-01

### Added

- Disputes: `Dispute.Search` and `Dispute.Single`, and the `InvalidDisputeId` error code (#88).

### Fixed

- A refund with a comment kept its amount; it was sent as a full refund (#89, PAY-5143).

## [2.1.14] - 2026-05-13

- New API endpoints, models and error codes (#84). Published with NuGet trusted publishing (#85, #86).

## [2.1.13] - 2025-08-05

- Metadata on payment links (#83).

## [2.1.12] - 2025-07-04

- Payment links return their URL; payment link helpers; metadata on batches (#78, #80, #81, #82).

## [2.1.11] - 2025-06-13

- Payment links (#75), recurring flags (#77). The US region in sandbox and the JP region in development are gone
  (#74).

## [2.1.10] - 2025-02-12

- Metadata in responses (#68).

## [2.1.9] - 2025-01-15

- Subscriptions: update (#64), pause (#63), webhooks (#67); the subscription ID on charges (#65).

## [2.1.8] - 2024-11-15

- Payment links' display type (#62).

## [2.1.7] - 2024-11-06

- Missing request and response parameters (#60).

## [2.1.6] - 2024-10-02

- First name, last name, email and phone on charges (#59).

## [2.1.5] - 2024-09-18

- Metadata on subscriptions (#58); a fix for a reported issue (#57).

## [2.1.4] - 2024-09-16

- New search parameters (#54, #55) and a missing method (#56).

## [2.1.3] - 2024-08-16

- Update a payment method (#51, #52); error codes synced with the API (#50).

## [2.1.2] - 2024-07-24

- Webhooks on batch transactions (#49).

## [2.1.0] and [2.1.1] - 2024-06-21

- Bank accounts (#40, #41), request models synced with the API (#43, #44, #46, #47).

## 2.0.0 to 2.0.21 - 2021-05-01 to 2024-05-14

- 2.0.21: batches (#38), `POST /payment_method/provider_token` (#39), card details (#36).
- 2.0.20: info methods (#33), 3DS after authorisation (#34).
- 2.0.19: delete a provider (#31), new parameters (#32).
- 2.0.16 to 2.0.18: Visa Installments (#26), descriptors (#27), metadata and fixes (#28, #29, #30).
- 2.0.15: multiple regions (#24, #25).
- 2.0.13: `ErrorCodeType` (#22), `ThreeDsProviderId` (#23); `/payment_method/checkout` removed (#21).
- 2.0.11 and 2.0.12: webhooks (#14), better errors (#18), default payment methods (#16, #17), metadata on
  charges (#19).
- 2.0.9 and 2.0.10: new endpoints (#6), refund comments (#8), initiator (#10), level 2 and 3 data (#11, #12),
  `UpdateProvider` (#13).
- 2.0.0 to 2.0.8: the first 2.x releases, before the pull requests above.

[Unreleased]: https://github.com/payfurl/dotnetsdk/compare/v3.0.0...HEAD
[3.0.0]: https://github.com/payfurl/dotnetsdk/compare/v2.2.1...v3.0.0
[2.2.1]: https://github.com/payfurl/dotnetsdk/releases/tag/v2.2.1
