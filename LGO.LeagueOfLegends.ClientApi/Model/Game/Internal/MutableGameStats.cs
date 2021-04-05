using System;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game.Internal
{
    internal class MutableGameStats : ILolClientGameStats
    {
        [JsonProperty("gameTime")]
        [JsonConverter(typeof(InGameTimeConverter))]
        public TimeSpan InGameTime { get; set; } = TimeSpan.Zero;
        
        [JsonProperty("gameMode")]
        [JsonConverter(typeof(GameModeTypeConverter))]
        public LolGameModeType GameMode { get; set; } = LolGameModeType.Undefined;
    }
}