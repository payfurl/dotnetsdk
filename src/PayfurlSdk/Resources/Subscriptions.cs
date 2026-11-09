using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Recurring charges: <c>/subscription</c>.</summary>
public sealed class Subscriptions
{
    private readonly ApiClient _api;

    internal Subscriptions(ApiClient api) => _api = api;

    /// <summary><c>POST /subscription/payment_method</c></summary>
    public Task<Subscription> CreateAsync(SubscriptionCreate subscription, CancellationToken cancellationToken = default) =>
        _api.PostAsync<Subscription>("/subscription/payment_method", subscription, cancellationToken);

    /// <summary><c>GET /subscription/{subscriptionId}</c></summary>
    public Task<Subscription> GetAsync(string subscriptionId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<Subscription>("/subscription/" + ApiClient.Id(subscriptionId, nameof(subscriptionId)), null, cancellationToken);

    /// <summary><c>GET /subscription</c></summary>
    public Task<SubscriptionList> SearchAsync(SubscriptionSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<SubscriptionList>("/subscription", search?.ToQuery(), cancellationToken);

    /// <summary><c>PUT /subscription/{subscriptionId}</c></summary>
    public Task<Subscription> UpdateAsync(string subscriptionId, SubscriptionUpdate subscription, CancellationToken cancellationToken = default) =>
        _api.PutAsync<Subscription>("/subscription/" + ApiClient.Id(subscriptionId, nameof(subscriptionId)), subscription, cancellationToken);

    /// <summary><c>PUT /subscription/{subscriptionId}/status</c>: pause (<see cref="SubscriptionStatus.Suspended"/>), resume (<see cref="SubscriptionStatus.Active"/>) or cancel it.</summary>
    public Task<Subscription> UpdateStatusAsync(string subscriptionId, SubscriptionStatus status, CancellationToken cancellationToken = default) =>
        _api.PutAsync<Subscription>("/subscription/" + ApiClient.Id(subscriptionId, nameof(subscriptionId)) + "/status", new UpdateSubscriptionStatus { Status = status }, cancellationToken);

    /// <summary><c>DELETE /subscription/{subscriptionId}</c>: cancels it.</summary>
    public Task<Subscription> DeleteAsync(string subscriptionId, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<Subscription>("/subscription/" + ApiClient.Id(subscriptionId, nameof(subscriptionId)), null, cancellationToken);
}
