using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableFirstChampionKilledEvent : AbstractMutableGameEvent, ILolClientFirstChampionKilledEvent
    {
        [JsonProperty("Recipient")]
        public string KillerSummonerName { get; set; } = string.Empty;
    }
}