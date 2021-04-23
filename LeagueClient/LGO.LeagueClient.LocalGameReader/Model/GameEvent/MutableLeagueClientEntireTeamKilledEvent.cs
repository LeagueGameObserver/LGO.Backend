using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientEntireTeamKilledEvent : AbstractMutableLeagueClientGameEvent, ILeagueClientEntireTeamKilledEvent
    {
        [JsonProperty("Acer")]
        public string KillerSummonerName { get; set; } = string.Empty;

        [JsonProperty("AcingTeam")]
        [JsonConverter(typeof(TeamTypeConverter))]
        public LeagueTeamType KillerTeam { get; set; } = LeagueTeamType.Undefined;
    }
}