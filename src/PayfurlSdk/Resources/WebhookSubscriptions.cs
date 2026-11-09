using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk.Resources;

/// <summary>Where payFURL sends webhooks: <c>/webhook/subscription</c>.</summary>
public sealed class WebhookSubscriptions
{
    private readonly ApiClient _api;

    internal WebhookSubscriptions(ApiClient api) => _api = api;

    /// <summary><c>POST /webhook/subscription</c></summary>
    public Task<WebhookSubscription> CreateAsync(NewWebhookSubscription subscription, CancellationToken cancellationToken = default) =>
        _api.PostAsync<WebhookSubscription>("/webhook/subscription", subscription, cancellationToken);

    /// <summary><c>GET /webhook/subscription/{webhookSubscriptionId}</c></summary>
    public Task<WebhookSubscription> GetAsync(string webhookSubscriptionId, CancellationToken cancellationToken = default) =>
        _api.GetAsync<WebhookSubscription>("/webhook/subscription/" + ApiClient.Id(webhookSubscriptionId, nameof(webhookSubscriptionId)), null, cancellationToken);

    /// <summary><c>GET /webhook/subscription</c></summary>
    public Task<WebhookSubscriptionList> SearchAsync(WebhookSubscriptionSearch? search = null, CancellationToken cancellationToken = default) =>
        _api.GetAsync<WebhookSubscriptionList>("/webhook/subscription", search?.ToQuery(), cancellationToken);

    /// <summary><c>DELETE /webhook/subscription/{webhookSubscriptionId}</c></summary>
    public Task<WebhookSubscription> DeleteAsync(string webhookSubscriptionId, CancellationToken cancellationToken = default) =>
        _api.DeleteAsync<WebhookSubscription>("/webhook/subscription/" + ApiClient.Id(webhookSubscriptionId, nameof(webhookSubscriptionId)), null, cancellationToken);
}
