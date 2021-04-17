using System;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal abstract class AbstractMutableLeagueClientGameEvent : ILeagueClientGameEvent
    {
        [JsonProperty("EventID")]
        public int Id { get; set; }

        [JsonProperty("EventName")]
        [JsonConverter(typeof(GameEventTypeConverter))]
        public LeagueClientGameEventType Type { get; set; } = LeagueClientGameEventType.Undefined;
        
        [JsonProperty("EventTime")]
        [JsonConverter(typeof(InGameTimeConverter))]
        public TimeSpan InGameTime { get; set; }
    }
}