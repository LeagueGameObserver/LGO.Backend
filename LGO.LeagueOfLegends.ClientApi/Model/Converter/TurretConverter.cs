using System;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.Backend.Core.Model.LeagueOfLegends.Structure;
using LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class TurretConverter : JsonConverter<ILolTurret>
    {
        private const string StringPrefix = "Turret";
        private const string StringPostfix = "A";
        private const string TokenSeparator = "_";

        private static BidirectionalStringMapping<LolTeamType> TeamMapping { get; } = new(
            (LolTeamType.Undefined, "UNDEFINED"),
            (LolTeamType.Blue, "T1"),
            (LolTeamType.Red, "T2"));

        private static BidirectionalStringMapping<LolTurretTierType> TurretTierMapping { get; } = new(
            (LolTurretTierType.Undefined, "UNDEFINED"),
            (LolTurretTierType.TopOuter, $"L{TokenSeparator}03"),
            (LolTurretTierType.TopInner, $"L{TokenSeparator}02"),
            (LolTurretTierType.TopInhibitor, $"L{TokenSeparator}01"),
            (LolTurretTierType.MiddleOuter, $"C{TokenSeparator}05"),
            (LolTurretTierType.MiddleInner, $"C{TokenSeparator}04"),
            (LolTurretTierType.MiddleInhibitor, $"C{TokenSeparator}03"),
            (LolTurretTierType.NexusBottom, $"C{TokenSeparator}02"),
            (LolTurretTierType.NexusTop, $"C{TokenSeparator}01"),
            (LolTurretTierType.BottomOuter, $"R{TokenSeparator}03"),
            (LolTurretTierType.BottomInner, $"R{TokenSeparator}02"),
            (LolTurretTierType.BottomInhibitor, $"R{TokenSeparator}01"));

        public override void WriteJson(JsonWriter writer, ILolTurret? value, JsonSerializer serializer)
        {
            writer.WriteValue(string.Join(TokenSeparator,
                StringPrefix,
                TeamMapping.Get(value?.Team ?? LolTeamType.Undefined),
                TurretTierMapping.Get(value?.Tier ?? LolTurretTierType.Undefined),
                StringPostfix));
        }

        public override ILolTurret? ReadJson(JsonReader reader, Type objectType, ILolTurret? existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            var tokens = value?.Split(TokenSeparator) ?? Array.Empty<string>();

            var team = tokens.Length == 5 ? TeamMapping.Get(tokens[1]) : LolTeamType.Undefined;
            var tier = tokens.Length == 5
                ? TurretTierMapping.Get(string.Join(TokenSeparator, tokens[2], tokens[3]))
                : LolTurretTierType.Undefined;
            return new MutableTurret
            {
                Team = team,
                Tier = tier,
            };
        }
    }
}