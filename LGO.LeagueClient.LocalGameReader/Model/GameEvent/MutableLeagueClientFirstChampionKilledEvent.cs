using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientFirstChampionKilledEvent : AbstractMutableLeagueClientGameEvent, ILeagueClientFirstChampionKilledEvent
    {
        [JsonProperty("Recipient")]
        public string KillerSummonerName { get; set; } = string.Empty;
    }
}