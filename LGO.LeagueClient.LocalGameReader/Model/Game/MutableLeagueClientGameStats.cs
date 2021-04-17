using System;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.Game;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.Game
{
    internal class MutableLeagueClientGameStats : ILeagueClientGameStats
    {
        [JsonProperty("gameTime")]
        [JsonConverter(typeof(InGameTimeConverter))]
        public TimeSpan InGameTime { get; set; } = TimeSpan.Zero;
        
        [JsonProperty("gameMode")]
        [JsonConverter(typeof(GameModeTypeConverter))]
        public LeagueGameModeType GameMode { get; set; } = LeagueGameModeType.Undefined;

        [JsonProperty("mapName")]
        [JsonConverter(typeof(MapConverter))]
        public LeagueMap Map { get; set; } = LeagueMap.Undefined;

        public static ILeagueClientGameStats Null => NullGameStats.Get;
    }
}