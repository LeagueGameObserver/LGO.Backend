using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableGameEndedEvent : AbstractMutableGameEvent, ILolClientGameEndedEvent
    {
        [JsonProperty("Result")]
        [JsonConverter(typeof(GameResultConverter))]
        public LolGameResult ResultForActivePlayer { get; set; }
    }
}