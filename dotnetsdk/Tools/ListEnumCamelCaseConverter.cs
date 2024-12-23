using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace payfurl.sdk.Tools
{
    public class ListEnumCamelCaseConverter<T> : JsonConverter where T : struct, Enum
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = (List<T>)value;
            writer.WriteStartArray();

            foreach (var item in list)
            {
                var camelCaseValue = char.ToLowerInvariant(item.ToString()[0]) + item.ToString().Substring(1);
                writer.WriteValue(camelCaseValue);
            }

            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var list = new List<T>();
            var array = JArray.Load(reader);

            foreach (var token in array)
            {
                var value = token.ToString();
                if (Enum.TryParse<T>(value, true, out var result))
                {
                    list.Add(result);
                }
                else
                {
                    throw new JsonException($"Unable to convert \"{value}\" to Enum \"{typeof(T)}\".");
                }
            }

            return list;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<T>);
        }
    }
}