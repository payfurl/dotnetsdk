using System;

namespace Payfurl.Sdk.Models;

/// <summary>
/// A webhook's body (services/webhooks-sender's <c>JsonRequestEnvelopeService</c>): what happened, in
/// <see cref="Meta"/>, and the object it happened to, in <see cref="Data"/>: a <see cref="ChargeData"/> for a
/// <see cref="WebhookType.Transaction"/> webhook, a <see cref="CustomerData"/> for a customer, and so on.
/// </summary>
/// <typeparam name="T">The model of <c>data</c>.</typeparam>
public sealed class WebhookEvent<T>
    where T : class
{
    /// <summary>The message ID, time, type and event type.</summary>
    public WebhookMeta? Meta { get; set; }

    /// <summary>The object the event is about.</summary>
    public T? Data { get; set; }
}

/// <summary>A webhook's <c>meta</c>.</summary>
public sealed class WebhookMeta
{
    /// <summary>The message's ID: the same for every retry of one message.</summary>
    public string? MessageId { get; set; }

    /// <summary>When the message was sent.</summary>
    public DateTime? Timestamp { get; set; }

    /// <summary>What the webhook is about, which says what <c>data</c> is.</summary>
    public WebhookType? Type { get; set; }

    /// <summary>What happened, such as <c>transaction.created</c> or <c>subscription.status.changed</c>.</summary>
    public string? EventType { get; set; }
}
