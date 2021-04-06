using System;
using Newtonsoft.Json;

namespace LGO.Backend.Core.Model.Converter
{
    public class ConcreteConverter<TInterface, TImplementation> : JsonConverter<TInterface>
        where TImplementation : TInterface
    {
        public override void WriteJson(JsonWriter writer, TInterface? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override TInterface? ReadJson(JsonReader reader, Type objectType, TInterface? existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<TImplementation>(reader);
        }
    }
}