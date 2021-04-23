using LGO.Backend.Core.Model.League.Structure;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientInhibitorAboutToBeReconstructedEvent : AbstractMutableLeagueClientGameEvent, ILeagueClientInhibitorAboutToBeReconstructedEvent
    {
        [JsonProperty("InhibRespawningSoon")]
        [JsonConverter(typeof(InhibitorConverter))]
        public ILeagueInhibitor Inhibitor { get; set; } = MutableLeagueInhibitor.Null;
    }
}