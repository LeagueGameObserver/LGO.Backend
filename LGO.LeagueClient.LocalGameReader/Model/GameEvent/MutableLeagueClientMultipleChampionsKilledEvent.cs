using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientMultipleChampionsKilledEvent : AbstractMutableLeagueClientKillerEvent, ILeagueClientMultipleChampionsKilledEvent
    {
        [JsonProperty("KillStreak")]
        public int NumberOfKills { get; set; }
    }
}