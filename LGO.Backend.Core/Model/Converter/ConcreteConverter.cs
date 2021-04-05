using System;
using Newtonsoft.Json;

namespace LGO.Backend.Core.Model.Converter
{
    public class ConcreteConverter<T> : JsonConverter<T>
    {
        public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }
    }
}