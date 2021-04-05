using System;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal abstract class AbstractMutableGameEvent : ILolClientGameEvent
    {
        [JsonProperty("EventID")]
        public int Id { get; set; }

        [JsonProperty("EventName")]
        [JsonConverter(typeof(GameEventTypeConverter))]
        public LolClientGameEventType Type { get; set; } = LolClientGameEventType.Undefined;
        
        [JsonProperty("EventTime")]
        [JsonConverter(typeof(InGameTimeConverter))]
        public TimeSpan InGameTime { get; set; }
    }
}