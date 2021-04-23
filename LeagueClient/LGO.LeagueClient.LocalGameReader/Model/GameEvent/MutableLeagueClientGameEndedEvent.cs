using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientGameEndedEvent : AbstractMutableLeagueClientGameEvent, ILeagueClientGameEndedEvent
    {
        [JsonProperty("Result")]
        [JsonConverter(typeof(GameResultConverter))]
        public LeagueGameResult ResultForActivePlayer { get; set; }
    }
}