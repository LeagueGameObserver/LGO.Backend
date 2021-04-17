using System;
using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.Player;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.Player
{
    internal class MutableLeagueClientPlayer : ILeagueClientPlayer
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
        public LeaguePositionType Position { get; set; } = LeaguePositionType.Undefined;

        [JsonProperty("level")]
        public int Level { get; set; }
        
        [JsonProperty("team")]
        [JsonConverter(typeof(TeamTypeConverter))]
        public LeagueTeamType Team { get; set; } = LeagueTeamType.Undefined;

        [JsonProperty("items", ItemConverterType = typeof(ConcreteConverter<ILeagueClientItem, MutableLeagueClientItem>))]
        public IEnumerable<ILeagueClientItem> Items { get; set; } = Enumerable.Empty<ILeagueClientItem>();

        [JsonProperty("scores")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueClientScore, MutableLeagueClientScore>))]
        public ILeagueClientScore Score { get; set; } = MutableLeagueClientScore.Null;
    }
}