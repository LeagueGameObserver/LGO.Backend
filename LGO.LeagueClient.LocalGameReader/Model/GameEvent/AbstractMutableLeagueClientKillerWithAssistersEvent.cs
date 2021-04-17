using System.Collections.Generic;
using System.Linq;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal abstract class AbstractMutableLeagueClientKillerWithAssistersEvent : AbstractMutableLeagueClientKillerEvent, ILeagueClientAssistersEvent
    {
        [JsonProperty("Assisters")]
        public IEnumerable<string> AssistersNames { get; set; } = Enumerable.Empty<string>();
    }
}