using System;
using Newtonsoft.Json;

namespace LGO.Backend.Core.Model.Converter
{
    public sealed class MultiComponentVersionConverter : JsonConverter<MultiComponentVersion>
    {
        public override void WriteJson(JsonWriter writer, MultiComponentVersion? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }

        public override MultiComponentVersion? ReadJson(JsonReader reader, Type objectType, MultiComponentVersion? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var valueAsString = reader.ReadAsString();
            if (string.IsNullOrEmpty(valueAsString))
            {
                return null;
            }
            
            return MultiComponentVersion.Parse(valueAsString);
        }
    }
}