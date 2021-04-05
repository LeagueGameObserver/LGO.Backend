using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableChampionKilledEvent : AbstractMutableKillerWithAssistersEvent, ILolClientChampionKilledEvent
    {
        [JsonProperty("VictimName")]
        public string VictimSummonerName { get; set; } = string.Empty;
    }
}