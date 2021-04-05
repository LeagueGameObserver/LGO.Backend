using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableEntireTeamKilledEvent : AbstractMutableGameEvent, ILolClientEntireTeamKilledEvent
    {
        [JsonProperty("Acer")]
        public string KillerSummonerName { get; set; } = string.Empty;

        [JsonProperty("AcingTeam")]
        [JsonConverter(typeof(TeamTypeConverter))]
        public LolTeamType KillerTeam { get; set; } = LolTeamType.Undefined;
    }
}