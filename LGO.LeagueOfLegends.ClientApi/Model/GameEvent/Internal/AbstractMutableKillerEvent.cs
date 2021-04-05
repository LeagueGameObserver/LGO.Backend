using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal abstract class AbstractMutableKillerEvent : AbstractMutableGameEvent, ILolClientKillerEvent
    {
        [JsonProperty("KillerName")]
        public string KillerName { get; set; } = string.Empty;
    }
}