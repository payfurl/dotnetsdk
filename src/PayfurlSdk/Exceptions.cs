using System;
using System.Collections.Generic;

namespace Payfurl.Sdk;

/// <summary>
/// Every error this SDK throws for a failed request. Catch a subclass for the case: <see cref="ApiException"/> when
/// the API answered with an error, <see cref="PayfurlConnectionException"/> when it did not answer.
/// </summary>
public abstract class PayfurlException : Exception
{
    /// <summary>Creates the exception.</summary>
    protected PayfurlException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// The API answered with an error (services/api/src/api/Models/Error.cs): a declined charge, a server error, or
/// anything without a subclass of its own.
/// </summary>
public class ApiException : PayfurlException
{
    /// <summary>Creates the exception from the API's error response.</summary>
    public ApiException(
        string message,
        int httpStatus,
        ErrorCode errorCode = ErrorCode.UnknownError,
        bool isRetryable = false,
        string? type = null,
        string? resource = null,
        IReadOnlyDictionary<string, string>? details = null,
        string? gatewayCode = null,
        string? gatewayMessage = null,
        string? chargeId = null,
        string? traceId = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        HttpStatus = httpStatus;
        ErrorCode = errorCode;
        IsRetryable = isRetryable;
        Type = type;
        Resource = resource;
        Details = details ?? new Dictionary<string, string>();
        GatewayCode = gatewayCode;
        GatewayMessage = gatewayMessage;
        ChargeId = chargeId;
        TraceId = traceId;
    }

    /// <summary>The response's HTTP status.</summary>
    public int HttpStatus { get; }

    /// <summary>The payFURL error code, <see cref="ErrorCode.UnknownError"/> when the response has none.</summary>
    public ErrorCode ErrorCode { get; }

    /// <summary>
    /// Whether the same request may succeed later: the API's <c>isRetryable</c>, or for a response without it, an
    /// HTTP 5xx or 429.
    /// </summary>
    public bool IsRetryable { get; }

    /// <summary>The URL documenting the error code.</summary>
    public string? Type { get; }

    /// <summary>The resource the error is about, such as <c>charge</c>.</summary>
    public string? Resource { get; }

    /// <summary>A validation error's messages, by field.</summary>
    public IReadOnlyDictionary<string, string> Details { get; }

    /// <summary>The gateway's own code for a decline.</summary>
    public string? GatewayCode { get; }

    /// <summary>The gateway's own message for a decline.</summary>
    public string? GatewayMessage { get; }

    /// <summary>The failed charge's ID, which <see cref="Resources.Charges.GetAsync"/> reads.</summary>
    public string? ChargeId { get; }

    /// <summary>The request's trace ID, for payFURL support.</summary>
    public string? TraceId { get; }
}

/// <summary>The key is missing, wrong, or not allowed to call the endpoint (HTTP 401 or 403).</summary>
public sealed class AuthenticationException : ApiException
{
    /// <inheritdoc cref="ApiException(string, int, ErrorCode, bool, string, string, IReadOnlyDictionary{string, string}, string, string, string, string, Exception)"/>
    public AuthenticationException(string message, int httpStatus, ErrorCode errorCode = ErrorCode.UnknownError, bool isRetryable = false, string? type = null, string? resource = null, IReadOnlyDictionary<string, string>? details = null, string? gatewayCode = null, string? gatewayMessage = null, string? chargeId = null, string? traceId = null)
        : base(message, httpStatus, errorCode, isRetryable, type, resource, details, gatewayCode, gatewayMessage, chargeId, traceId)
    {
    }
}

/// <summary>The resource does not exist (HTTP 404).</summary>
public sealed class NotFoundException : ApiException
{
    /// <inheritdoc cref="ApiException(string, int, ErrorCode, bool, string, string, IReadOnlyDictionary{string, string}, string, string, string, string, Exception)"/>
    public NotFoundException(string message, int httpStatus, ErrorCode errorCode = ErrorCode.UnknownError, bool isRetryable = false, string? type = null, string? resource = null, IReadOnlyDictionary<string, string>? details = null, string? gatewayCode = null, string? gatewayMessage = null, string? chargeId = null, string? traceId = null)
        : base(message, httpStatus, errorCode, isRetryable, type, resource, details, gatewayCode, gatewayMessage, chargeId, traceId)
    {
    }
}

/// <summary>The API rejected the request's fields (<see cref="ErrorCode.ValidationError"/>): <see cref="ApiException.Details"/> says which.</summary>
public sealed class InvalidRequestException : ApiException
{
    /// <inheritdoc cref="ApiException(string, int, ErrorCode, bool, string, string, IReadOnlyDictionary{string, string}, string, string, string, string, Exception)"/>
    public InvalidRequestException(string message, int httpStatus, ErrorCode errorCode = ErrorCode.ValidationError, bool isRetryable = false, string? type = null, string? resource = null, IReadOnlyDictionary<string, string>? details = null, string? gatewayCode = null, string? gatewayMessage = null, string? chargeId = null, string? traceId = null)
        : base(message, httpStatus, errorCode, isRetryable, type, resource, details, gatewayCode, gatewayMessage, chargeId, traceId)
    {
    }
}

/// <summary>
/// The API did not answer: the connection failed or was cut. A charge may or may not have been made, so look for it
/// (<see cref="Resources.Charges.SearchAsync"/> by reference) before charging again.
/// </summary>
public class PayfurlConnectionException : PayfurlException
{
    /// <summary>Creates the exception.</summary>
    public PayfurlConnectionException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}

/// <summary>The API did not answer within <see cref="PayfurlClientOptions.Timeout"/>.</summary>
public sealed class PayfurlTimeoutException : PayfurlConnectionException
{
    /// <summary>Creates the exception.</summary>
    public PayfurlTimeoutException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
