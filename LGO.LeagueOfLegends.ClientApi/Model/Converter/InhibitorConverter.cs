using System;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.Backend.Core.Model.LeagueOfLegends.Structure;
using LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class InhibitorConverter : JsonConverter<ILolInhibitor>
    {
        private const string StringPrefix = "Barracks";
        private const string TokenSeparator = "_";

        private static BidirectionalStringMapping<LolTeamType> TeamMapping { get; } = new((LolTeamType.Undefined, "UNDEFINED"),
                                                                                          (LolTeamType.Blue, "T1"),
                                                                                          (LolTeamType.Red, "T2"));

        private static BidirectionalStringMapping<LolInhibitorTierType> TierMapping { get; } = new((LolInhibitorTierType.Undefined, "UNDEFINED"),
                                                                                                   (LolInhibitorTierType.Top, "L1"),
                                                                                                   (LolInhibitorTierType.Middle, "C1"),
                                                                                                   (LolInhibitorTierType.Bottom, "R1"));

        public override void WriteJson(JsonWriter writer, ILolInhibitor? value, JsonSerializer serializer)
        {
            writer.WriteValue(string.Join(TokenSeparator,
                                          StringPrefix,
                                          TeamMapping.Get(value?.Team ?? LolTeamType.Undefined),
                                          TierMapping.Get(value?.Tier ?? LolInhibitorTierType.Undefined)));
        }

        public override ILolInhibitor? ReadJson(JsonReader reader, Type objectType, ILolInhibitor? existingValue, bool hasExistingValue,
                                                JsonSerializer serializer)
        {
            var value = reader.Value as string;
            var tokens = value?.Split(TokenSeparator) ?? Array.Empty<string>();
            var team = tokens.Length == 3 ? TeamMapping.Get(tokens[1]) : LolTeamType.Undefined;
            var tier = tokens.Length == 3 ? TierMapping.Get(tokens[2]) : LolInhibitorTierType.Undefined;
            return new MutableInhibitor
                   {
                       Team = team,
                       Tier = tier,
                   };
        }
    }
}