using System;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal abstract class ReadonlyJsonConverter<T> : JsonConverter<T>
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}