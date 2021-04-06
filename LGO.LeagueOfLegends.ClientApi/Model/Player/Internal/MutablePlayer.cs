using System;
using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Player.Internal
{
    internal class MutablePlayer : ILolClientPlayer
    {
        [JsonProperty("summonerName")]
        public string SummonerName { get; set; } = string.Empty;

        [JsonProperty("championName")]
        public string ChampionName { get; set; } = string.Empty;

        [JsonProperty("isDead")]
        public bool IsDead { get; set; }

        [JsonProperty("respawnTimer")]
        [JsonConverter(typeof(InGameTimeConverter))]
        public TimeSpan RespawnTime { get; set; } = TimeSpan.Zero;

        [JsonProperty("isBot")]
        public bool IsBot { get; set; }

        [JsonProperty("position")]
        [JsonConverter(typeof(PositionTypeConverter))]
        public LolPositionType Position { get; set; } = LolPositionType.Undefined;

        [JsonProperty("level")]
        public int Level { get; set; }
        
        [JsonProperty("team")]
        [JsonConverter(typeof(TeamTypeConverter))]
        public LolTeamType Team { get; set; } = LolTeamType.Undefined;

        [JsonProperty("items", ItemConverterType = typeof(ConcreteConverter<ILolClientItem, MutableItem>))]
        public IEnumerable<ILolClientItem> Items { get; set; } = Enumerable.Empty<ILolClientItem>();

        [JsonProperty("scores")]
        [JsonConverter(typeof(ConcreteConverter<ILolClientScore, MutableScore>))]
        public ILolClientScore Score { get; set; } = NullScore.Get;
    }
}