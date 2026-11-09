using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Payfurl.Sdk.Internal;

/// <summary>Query string parameters as the API's model binding reads them: invariant culture, nulls left out.</summary>
internal static class Query
{
    public static IEnumerable<KeyValuePair<string, string>> Of(params (string Name, object? Value)[] parameters) =>
        parameters
            .Where(p => p.Value != null)
            .Select(p => new KeyValuePair<string, string>(p.Name, Format(p.Value!)));

    public static string Format(object value) => value switch
    {
        string s => s,
        bool b => b ? "true" : "false",
        // Round-trip ISO 8601: Z for UTC, the offset for local times.
        DateTime d => d.ToString("o", CultureInfo.InvariantCulture),
        DateTimeOffset d => d.ToString("o", CultureInfo.InvariantCulture),
        Enum e => EnumName(e),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString() ?? string.Empty,
    };

    public static string ToString(IEnumerable<KeyValuePair<string, string>>? query)
    {
        var builder = new StringBuilder();
        foreach (var parameter in query ?? Enumerable.Empty<KeyValuePair<string, string>>())
        {
            builder.Append(builder.Length == 0 ? '?' : '&')
                .Append(Uri.EscapeDataString(parameter.Key))
                .Append('=')
                .Append(Uri.EscapeDataString(parameter.Value));
        }

        return builder.ToString();
    }

    private static string EnumName(Enum value) =>
        (string)typeof(EnumValues<>).MakeGenericType(value.GetType())
            .GetMethod(nameof(EnumValues<PayfurlEnvironment>.Name))!
            .Invoke(null, new object[] { value })!;
}
