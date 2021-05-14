using System.Collections.Generic;
using System.Linq;

namespace LGO.Backend.Model.League.GameEvent
{
    internal abstract class MutableLeagueKillerWithAssistersEvent : MutableLeagueGameEvent, ILeagueKillerEvent, ILeagueAssistersEvent
    {
        public string KillerName { get; set; } = string.Empty;
        public IEnumerable<string> AssistersNames { get; set; } = Enumerable.Empty<string>();
    }
}