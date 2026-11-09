using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Payfurl.Sdk.Internal;

/// <summary>The API's JSON: camelCase names, nulls left out of requests, enums as their names.</summary>
internal static class Json
{
    public static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new EnumConverterFactory() },
    };
}

/// <summary>
/// Enums as the API writes them (<see cref="EnumMemberAttribute"/>'s value, else the name). A value the SDK does not
/// know reads as the enum's 0, <c>Unknown</c>, instead of failing the whole response; a number reads as itself.
/// </summary>
internal sealed class EnumConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsEnum;

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        (JsonConverter)Activator.CreateInstance(typeof(EnumConverter<>).MakeGenericType(typeToConvert))!;
}

internal sealed class EnumConverter<T> : JsonConverter<T>
    where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out var number))
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            return EnumValues<T>.Parse(reader.GetString());
        }

        throw new JsonException($"Expected a string or a number for {typeof(T).Name}, got {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
        writer.WriteStringValue(EnumValues<T>.Name(value));
}

/// <summary>An enum's names as the API writes them, in both directions.</summary>
internal static class EnumValues<T>
    where T : struct, Enum
{
    private static readonly Dictionary<T, string> Names = typeof(T)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .ToDictionary(f => (T)f.GetValue(null)!, f => f.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? f.Name);

    private static readonly Dictionary<string, T> Values = Names
        .GroupBy(n => n.Value, StringComparer.OrdinalIgnoreCase)
        .ToDictionary(g => g.Key, g => g.First().Key, StringComparer.OrdinalIgnoreCase);

    public static string Name(T value) =>
        Names.TryGetValue(value, out var name) ? name : Convert.ToInt64(value).ToString(System.Globalization.CultureInfo.InvariantCulture);

    public static T Parse(string? name)
    {
        if (name != null && Values.TryGetValue(name, out var value))
        {
            return value;
        }

        // The names too, so CARD and Card both read.
        return name != null && Enum.TryParse<T>(name, true, out var parsed) && !char.IsDigit(name[0]) ? parsed : default;
    }
}
