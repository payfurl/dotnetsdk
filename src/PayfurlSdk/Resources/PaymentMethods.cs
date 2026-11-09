using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Payment methods without a customer: <c>/payment_method</c>.</summary>
public sealed class PaymentMethods
{
    private readonly ApiClient _api;

    internal PaymentMethods(ApiClient api) => _api = api;

    /// <summary><c>POST /payment_method/card</c></summary>
    public Task<PaymentMethodData> CreateWithCardAsync(NewPaymentMethodCard paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/payment_method/card", paymentMethod, cancellationToken);

    /// <summary><c>POST /payment_method/vault</c>: a card from another payment method's vaulted card, on another provider.</summary>
    public Task<PaymentMethodData> CreateWithVaultAsync(NewPaymentMethodVault paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/payment_method/vault", paymentMethod, cancellationToken);

    /// <summary><c>POST /payment_method/payto</c></summary>
    public Task<PaymentMethodData> CreateWithPayToAsync(NewPayToAgreement agreement, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/payment_method/payto", agreement, cancellationToken);

    /// <summary><c>POST /payment_method/token</c></summary>
    public Task<PaymentMethodData> CreateWithTokenAsync(NewPaymentMethodToken paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/payment_method/token", paymentMethod, cancellationToken);

    /// <summary><c>POST /payment_method/provider_single_use_token</c></summary>
    public Task<PaymentMethodData> CreateWithSingleUseTokenAsync(SingleUseToken paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/payment_method/provider_single_use_token", paymentMethod, cancellationToken);

    /// <summary><c>POST /payment_method/provider_token</c></summary>
    public Task<PaymentMethodData> CreateWithProviderTokenAsync(NewProviderToken paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/payment_method/provider_token", paymentMethod, cancellationToken);

    /// <summary><c>POST /payment_method/bank_account</c></summary>
    public Task<PaymentMethodData> CreateWithBankAccountAsync(NewPaymentMethodBankPayment paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentMethodData>("/payment_method/bank_account", paymentMethod, cancellationToken);

    /// <summary><c>GET /payment_method/{paymentMethodId}</c></summary>
    public Task<PaymentMethodData> GetAsync(string paymentMethodId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<PaymentMethodData>("/payment_method/" + ApiClient.Id(paymentMethodId, nameof(paymentMethodId)), null, cancellationToken);

    /// <summary><c>GET /payment_method</c></summary>
    public Task<PaymentMethodList> SearchAsync(PaymentMethodSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<PaymentMethodList>("/payment_method", search?.ToQuery(), cancellationToken);

    /// <summary><c>PUT /payment_method/{paymentMethodId}</c>: a card's expiry date, cardholder or number, and the metadata.</summary>
    public Task<PaymentMethodData> UpdateAsync(string paymentMethodId, UpdatePaymentMethod paymentMethod, CancellationToken cancellationToken = default) =>
        _api.PutAsync<PaymentMethodData>("/payment_method/" + ApiClient.Id(paymentMethodId, nameof(paymentMethodId)), paymentMethod, cancellationToken);

    /// <summary><c>DELETE /payment_method/{paymentMethodId}</c>, and with <paramref name="gatewayDelete"/> at the gateway too.</summary>
    public Task<PaymentMethodData> DeleteAsync(string paymentMethodId, bool? gatewayDelete = null, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<PaymentMethodData>("/payment_method/" + ApiClient.Id(paymentMethodId, nameof(paymentMethodId)), Query.Of(("gatewayDelete", gatewayDelete)), cancellationToken);
}
