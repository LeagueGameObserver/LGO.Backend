using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal abstract class AbstractMutableNeutralObjectiveKilledEvent : AbstractMutableKillerWithAssistersEvent, ILolClientNeutralObjectiveKilledEvent
    {
        [JsonProperty("Stolen")]
        public bool HasBeenStolen { get; set; }
    }
}