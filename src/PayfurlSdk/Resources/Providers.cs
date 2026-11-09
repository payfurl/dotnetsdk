using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>The account's gateways: <c>/provider</c>.</summary>
public sealed class Providers
{
    private readonly ApiClient _api;

    internal Providers(ApiClient api) => _api = api;

    /// <summary><c>POST /provider</c></summary>
    public Task<Provider> CreateAsync(NewProvider provider, CancellationToken cancellationToken = default) =>
        _api.PostAsync<Provider>("/provider", provider, cancellationToken);

    /// <summary><c>PUT /provider/{providerId}</c></summary>
    public Task<Provider> UpdateAsync(string providerId, UpdateProvider provider, CancellationToken cancellationToken = default) =>
        _api.PutAsync<Provider>("/provider/" + ApiClient.Id(providerId, nameof(providerId)), provider, cancellationToken);

    /// <summary><c>DELETE /provider/{providerId}</c>: archives the provider.</summary>
    public Task<Provider> DeleteAsync(string providerId, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<Provider>("/provider/" + ApiClient.Id(providerId, nameof(providerId)), null, cancellationToken);
}
