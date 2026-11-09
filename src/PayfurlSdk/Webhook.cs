using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Payfurl.Sdk.Internal;
using Payfurl.Sdk.Models;

namespace Payfurl.Sdk;

/// <summary>
/// Checks that a webhook came from payFURL: its <c>X-Payfurl-Signature</c> header is the HMAC-SHA256 of the body,
/// in Base64, with the subscription's signing key (services/webhooks-sender/src/WebhooksSender/Tools/SignatureGenerator.cs).
/// </summary>
public static class Webhook
{
    /// <summary>Whether <paramref name="signature"/> is the body's, compared in constant time.</summary>
    /// <param name="body">The request body, exactly as received.</param>
    /// <param name="signature">The <c>X-Payfurl-Signature</c> header.</param>
    /// <param name="signingKey">The webhook subscription's signing key.</param>
    public static bool Verify(string body, string? signature, string signingKey)
    {
        if (body == null || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(signingKey))
        {
            return false;
        }

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(signingKey));
        var expected = Encoding.ASCII.GetBytes(Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(body))));
        var actual = Encoding.ASCII.GetBytes(signature);
#if NET
        return CryptographicOperations.FixedTimeEquals(expected, actual);
#else
        if (expected.Length != actual.Length)
        {
            return false;
        }

        var difference = 0;
        for (var i = 0; i < expected.Length; i++)
        {
            difference |= expected[i] ^ actual[i];
        }

        return difference == 0;
#endif
    }

    /// <summary>
    /// The webhook once its signature checks out, with <c>data</c> read as <typeparamref name="T"/>: <see cref="ChargeData"/>
    /// for a transaction webhook, <see cref="CustomerData"/> for a customer, and so on (<see cref="WebhookMeta.Type"/>).
    /// </summary>
    /// <exception cref="ArgumentException">When the signature does not match the body, or the body is not the expected JSON.</exception>
    public static WebhookEvent<T> Parse<T>(string body, string? signature, string signingKey)
        where T : class
    {
        if (!Verify(body, signature, signingKey))
        {
            throw new ArgumentException("The webhook's signature does not match its body: it is not from payFURL", nameof(signature));
        }

        try
        {
            return JsonSerializer.Deserialize<WebhookEvent<T>>(body, Json.Options)
                ?? throw new ArgumentException("The webhook's body is null", nameof(body));
        }
        catch (JsonException e)
        {
            throw new ArgumentException("The webhook's body is not the expected JSON", nameof(body), e);
        }
    }

    /// <summary>The webhook's JSON (<c>data</c> and <c>meta</c>) once its signature checks out.</summary>
    /// <exception cref="ArgumentException">When the signature does not match the body, or the body is not JSON.</exception>
    public static JsonElement Parse(string body, string? signature, string signingKey)
    {
        if (!Verify(body, signature, signingKey))
        {
            throw new ArgumentException("The webhook's signature does not match its body: it is not from payFURL", nameof(signature));
        }

        try
        {
            using var document = JsonDocument.Parse(body);
            return document.RootElement.Clone();
        }
        catch (JsonException e)
        {
            throw new ArgumentException("The webhook's body is not JSON", nameof(body), e);
        }
    }
}
