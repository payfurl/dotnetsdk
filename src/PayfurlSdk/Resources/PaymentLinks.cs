using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Hosted payment pages: <c>/payment_link</c>.</summary>
public sealed class PaymentLinks
{
    private readonly ApiClient _api;

    internal PaymentLinks(ApiClient api) => _api = api;

    /// <summary><c>POST /payment_link</c></summary>
    public Task<PaymentLink> CreateAsync(CreatePaymentLink paymentLink, CancellationToken cancellationToken = default) =>
        _api.PostAsync<PaymentLink>("/payment_link", paymentLink, cancellationToken);

    /// <summary><c>GET /payment_link/{paymentLinkId}</c></summary>
    public Task<PaymentLink> GetAsync(string paymentLinkId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<PaymentLink>("/payment_link/" + ApiClient.Id(paymentLinkId, nameof(paymentLinkId)), null, cancellationToken);

    /// <summary><c>GET /payment_link</c></summary>
    public Task<PaymentLinkList> SearchAsync(PaymentLinkSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<PaymentLinkList>("/payment_link", search?.ToQuery(), cancellationToken);
}
