using System;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class InGameTimeConverter : JsonConverter<TimeSpan>
    {
        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue(value.TotalSeconds);
        }

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return TimeSpan.FromSeconds(reader.ReadAsDouble() ?? 0.0d);
        }
    }
}