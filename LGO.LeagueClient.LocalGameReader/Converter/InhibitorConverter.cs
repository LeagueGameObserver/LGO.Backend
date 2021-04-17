using System;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Core.Model.League.Structure;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class InhibitorConverter : JsonConverter<ILeagueInhibitor>
    {
        private const string StringPrefix = "Barracks";
        private const string TokenSeparator = "_";

        private static BidirectionalStringMapping<LeagueTeamType> TeamMapping { get; } = new((LeagueTeamType.Undefined, "UNDEFINED"),
                                                                                          (LeagueTeamType.Blue, "T1"),
                                                                                          (LeagueTeamType.Red, "T2"));

        private static BidirectionalStringMapping<LeagueInhibitorTierType> TierMapping { get; } = new((LeagueInhibitorTierType.Undefined, "UNDEFINED"),
                                                                                                   (LeagueInhibitorTierType.Top, "L1"),
                                                                                                   (LeagueInhibitorTierType.Middle, "C1"),
                                                                                                   (LeagueInhibitorTierType.Bottom, "R1"));

        public override void WriteJson(JsonWriter writer, ILeagueInhibitor? value, JsonSerializer serializer)
        {
            writer.WriteValue(string.Join(TokenSeparator,
                                          StringPrefix,
                                          TeamMapping.Get(value?.Team ?? LeagueTeamType.Undefined),
                                          TierMapping.Get(value?.Tier ?? LeagueInhibitorTierType.Undefined)));
        }

        public override ILeagueInhibitor? ReadJson(JsonReader reader, Type objectType, ILeagueInhibitor? existingValue, bool hasExistingValue,
                                                JsonSerializer serializer)
        {
            var value = reader.Value as string;
            var tokens = value?.Split(TokenSeparator) ?? Array.Empty<string>();
            var team = tokens.Length == 3 ? TeamMapping.Get(tokens[1]) : LeagueTeamType.Undefined;
            var tier = tokens.Length == 3 ? TierMapping.Get(tokens[2]) : LeagueInhibitorTierType.Undefined;
            return new MutableLeagueInhibitor
                   {
                       Team = team,
                       Tier = tier,
                   };
        }
    }
}