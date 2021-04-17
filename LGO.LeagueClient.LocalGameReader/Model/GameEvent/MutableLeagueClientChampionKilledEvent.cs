using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientChampionKilledEvent : AbstractMutableLeagueClientKillerWithAssistersEvent, ILeagueClientChampionKilledEvent
    {
        [JsonProperty("VictimName")]
        public string VictimSummonerName { get; set; } = string.Empty;
    }
}