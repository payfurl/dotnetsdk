using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Charges: <c>/charge</c>.</summary>
public sealed class Charges
{
    private readonly ApiClient _api;

    internal Charges(ApiClient api) => _api = api;

    /// <summary><c>POST /charge/card</c>: a card, on the provider named.</summary>
    public Task<ChargeData> CreateWithCardAsync(NewChargeCard charge, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/card", charge, cancellationToken);

    /// <summary><c>POST /charge/card/least_cost</c>: a card, on the account's cheapest provider for it.</summary>
    public Task<ChargeData> CreateWithCardLeastCostAsync(NewChargeCardLeastCost charge, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/card/least_cost", charge, cancellationToken);

    /// <summary><c>POST /charge/customer</c>: a customer's default payment method.</summary>
    public Task<ChargeData> CreateWithCustomerAsync(NewChargeCustomer charge, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/customer", charge, cancellationToken);

    /// <summary><c>POST /charge/payment_method</c>: a saved payment method.</summary>
    public Task<ChargeData> CreateWithPaymentMethodAsync(NewChargePaymentMethod charge, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/payment_method", charge, cancellationToken);

    /// <summary><c>POST /charge/token</c>: a token from the browser SDK or a hosted page.</summary>
    public Task<ChargeData> CreateWithTokenAsync(NewChargeToken charge, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/token", charge, cancellationToken);

    /// <summary><c>POST /charge/bank_account</c>: a direct debit from a bank account.</summary>
    public Task<ChargeData> CreateWithBankAccountAsync(NewChargeBankPayment charge, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/bank_account", charge, cancellationToken);

    /// <summary><c>POST /charge/network_token</c>: a network token.</summary>
    public Task<ChargeData> CreateWithNetworkTokenAsync(NewChargeNetworkToken charge, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/network_token", charge, cancellationToken);

    /// <summary><c>GET /charge/{chargeId}</c></summary>
    public Task<ChargeData> GetAsync(string chargeId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<ChargeData>("/charge/" + ApiClient.Id(chargeId, nameof(chargeId)), null, cancellationToken);

    /// <summary><c>GET /charge</c>: <c>null</c> lists the most recent charges.</summary>
    public Task<ChargeList> SearchAsync(ChargeSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<ChargeList>("/charge", search?.ToQuery(), cancellationToken);

    /// <summary><c>DELETE /charge/{chargeId}</c>: refunds the charge, all of it when <paramref name="amount"/> is <c>null</c>.</summary>
    public Task<ChargeData> RefundAsync(string chargeId, decimal? amount = null, string? comment = null, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<ChargeData>("/charge/" + ApiClient.Id(chargeId, nameof(chargeId)), Query.Of(("amount", amount), ("comment", comment)), cancellationToken);

    /// <summary><c>POST /charge/{chargeId}/capture</c>: captures an authorised charge (<c>Capture = false</c>), all of it when <paramref name="amount"/> is <c>null</c>.</summary>
    public Task<ChargeData> CaptureAsync(string chargeId, decimal? amount = null, CancellationToken cancellationToken = default) =>
        _api.PostAsync<ChargeData>("/charge/" + ApiClient.Id(chargeId, nameof(chargeId)) + "/capture", new CaptureBody { Amount = amount }, cancellationToken);

    /// <summary><c>DELETE /charge/{chargeId}/void</c>: cancels an authorised charge.</summary>
    public Task<ChargeData> VoidAsync(string chargeId, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<ChargeData>("/charge/" + ApiClient.Id(chargeId, nameof(chargeId)) + "/void", null, cancellationToken);

    private sealed class CaptureBody
    {
        public decimal? Amount { get; set; }
    }
}
