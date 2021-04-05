using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal abstract class AbstractMutableKillerWithAssistersEvent : AbstractMutableKillerEvent, ILolClientAssistersEvent
    {
        [JsonProperty("Assisters")]
        public IEnumerable<string> AssistersNames { get; set; } = Enumerable.Empty<string>();
    }
}