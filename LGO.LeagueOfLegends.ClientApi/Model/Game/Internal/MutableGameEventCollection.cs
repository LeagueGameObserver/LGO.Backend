using System.Collections.Generic;
using System.Linq;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using LGO.LeagueOfLegends.ClientApi.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game.Internal
{
    internal class MutableGameEventCollection : ILolClientGameEventCollection
    {
        [JsonProperty("Events", ItemConverterType = typeof(GameEventConverter))]
        public IEnumerable<ILolClientGameEvent> Events { get; set; } = Enumerable.Empty<ILolClientGameEvent>();
    }
}