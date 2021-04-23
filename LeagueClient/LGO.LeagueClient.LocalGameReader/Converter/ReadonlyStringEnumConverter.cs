using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal abstract class ReadonlyStringEnumConverter<T> : ReadonlyJsonConverter<T> where T : Enum
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;

        private T ErrorValue { get; }
        private Dictionary<string, T> Mapping { get; } = new();

        protected ReadonlyStringEnumConverter(T errorValue, params (string, T)[] validValues)
        {
            ErrorValue = errorValue;
            foreach (var (key, value) in validValues)
            {
                Mapping.Add(key.ToLower(), value);
            }
        }

        public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is not string valueAsString || !Mapping.TryGetValue(valueAsString.ToLower(), out var result))
            {
                return ErrorValue;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}