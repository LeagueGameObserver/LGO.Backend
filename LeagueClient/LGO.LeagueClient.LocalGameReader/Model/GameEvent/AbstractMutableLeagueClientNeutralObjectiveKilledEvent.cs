using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal abstract class AbstractMutableLeagueClientNeutralObjectiveKilledEvent : AbstractMutableLeagueClientKillerWithAssistersEvent, ILeagueClientNeutralObjectiveKilledEvent
    {
        [JsonProperty("Stolen")]
        public bool HasBeenStolen { get; set; }
    }
}