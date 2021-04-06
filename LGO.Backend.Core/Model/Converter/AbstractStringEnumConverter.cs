using System;
using Newtonsoft.Json;

namespace LGO.Backend.Core.Model.Converter
{
    public abstract class AbstractStringEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : Enum
    {
        private BidirectionalStringMapping<TEnum> Mapping { get; }

        public AbstractStringEnumConverter(ValueTuple<TEnum, string> errorValue, params ValueTuple<TEnum, string>[] validValues)
        {
            Mapping = new BidirectionalStringMapping<TEnum>(errorValue, validValues);
        }

        public override TEnum? ReadJson(JsonReader reader, Type objectType, TEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Mapping.Get(reader.Value as string);
        }

        public override void WriteJson(JsonWriter writer, TEnum? value, JsonSerializer serializer)
        {
            writer.WriteValue(Mapping.Get(value));
        }
    }
}