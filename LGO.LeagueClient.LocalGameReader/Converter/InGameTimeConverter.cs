using System;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class InGameTimeConverter : ReadonlyJsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return TimeSpan.FromSeconds((double) (reader.Value ?? 0.0d));
        }
    }
}