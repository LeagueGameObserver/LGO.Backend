using LGO.Backend.Core.Model.LeagueOfLegends.Structure;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableInhibitorDestroyedEvent : AbstractMutableKillerWithAssistersEvent, ILolClientInhibitorDestroyedEvent
    {
        [JsonProperty("InhibKilled")]
        [JsonConverter(typeof(InhibitorConverter))]
        public ILolInhibitor Inhibitor { get; set; } = NullInhibitor.Get;
    }
}