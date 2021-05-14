using System;
using System.Collections.Generic;
using LGO.Backend.Model.Retrieval;
using LGO.Backend.Model.Retrieval.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LGO.Backend.Converter
{
    internal sealed class RetrievalConfigurationConverter : JsonConverter<ILgoDataRetrievalConfiguration>
    {
        private static Dictionary<LgoDataRetrievalConfigurationType, Func<ILgoDataRetrievalConfiguration>> RetrievalConfigurationFactory { get; }

        static RetrievalConfigurationConverter()
        {
            RetrievalConfigurationFactory = new Dictionary<LgoDataRetrievalConfigurationType, Func<ILgoDataRetrievalConfiguration>>
                                            {
                                                {LgoDataRetrievalConfigurationType.LeagueChampion, () => new MutableLeagueChampionRetrievalConfiguration()},
                                                {LgoDataRetrievalConfigurationType.LeagueGame, () => new MutableLeagueGameRetrievalConfiguration()},
                                                {LgoDataRetrievalConfigurationType.LeagueItem, () => new MutableLeagueItemRetrievalConfiguration()},
                                                {LgoDataRetrievalConfigurationType.LeaguePlayer, () => new MutableLeaguePlayerRetrievalConfiguration()},
                                                {LgoDataRetrievalConfigurationType.LeagueTeam, () => new MutableLeagueTeamRetrievalConfiguration()},
                                                {LgoDataRetrievalConfigurationType.LeagueTimer, () => new MutableLeagueTimerRetrievalConfiguration()},
                                                {LgoDataRetrievalConfigurationType.LeaguePowerPlayTimer, () => new MutableLeaguePowerPlayRetrievalConfiguration()}
                                            };
        }

        public override void WriteJson(JsonWriter writer, ILgoDataRetrievalConfiguration? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override ILgoDataRetrievalConfiguration? ReadJson(JsonReader reader, Type objectType, ILgoDataRetrievalConfiguration? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // taken from https://blog.mbwarez.dk/deserializing-different-types-based-on-properties-with-newtonsoft-json/
            var jsonObject = JToken.ReadFrom(reader);

            var typeReader = jsonObject[nameof(ILgoDataRetrievalConfiguration.Type)]!.CreateReader();
            if (!typeReader.Read())
            {
                throw new Exception($"Unable to read the {nameof(ILgoDataRetrievalConfiguration)}.");
            }

            var type = serializer.Deserialize<LgoDataRetrievalConfigurationType>(reader);

            if (!RetrievalConfigurationFactory.TryGetValue(type, out var factory))
            {
                throw new Exception($"Unknown {nameof(ILgoDataRetrievalConfiguration)}: {type}!");
            }

            var result = factory.Invoke();
            serializer.Populate(jsonObject.CreateReader(), result);
            return result;
        }
    }
}