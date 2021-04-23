using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal abstract class AbstractMutableLeagueClientKillerEvent : AbstractMutableLeagueClientGameEvent, ILeagueClientKillerEvent
    {
        [JsonProperty("KillerName")]
        public string KillerName { get; set; } = string.Empty;
    }
}