using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>What a checkout can show: <c>/info/providers</c>, the one endpoint a public key can call.</summary>
public sealed class Info
{
    private readonly ApiClient _api;

    internal Info(ApiClient api) => _api = api;

    /// <summary><c>GET /info/providers</c>: the payment methods a checkout of this amount and currency can show. <paramref name="browserSdkVersion"/> is the browser SDK version the checkout runs, sent as <c>sdk-version</c>: the API lists PayPal only for version 4 or later.</summary>
    public Task<InfoProviders> GetProvidersAsync(decimal? amount = null, string? currency = null, string? browserSdkVersion = null, string? cardProviderId = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<InfoProviders>("/info/providers", Query.Of(("amount", amount), ("currency", currency), ("cardProviderId", cardProviderId)), cancellationToken, browserSdkVersion == null ? null : new Dictionary<string, string> { ["sdk-version"] = browserSdkVersion });
}
