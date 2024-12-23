using System;
using Newtonsoft.Json;

namespace payfurl.sdk.Tools
{
    public class EnumToStringConverter<T> : JsonConverter where T : struct, Enum
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            return Enum.Parse(typeof(T), value, true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }
    }
}