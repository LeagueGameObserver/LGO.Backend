using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableMultipleChampionsKilledEvent : AbstractMutableKillerEvent, ILolClientMultipleChampionsKilledEvent
    {
        [JsonProperty("KillStreak")]
        public int NumberOfKills { get; set; }
    }
}