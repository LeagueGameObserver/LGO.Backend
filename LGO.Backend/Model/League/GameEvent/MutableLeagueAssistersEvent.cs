using System.Collections.Generic;
using System.Linq;

namespace LGO.Backend.Model.League.GameEvent
{
    internal abstract class MutableLeagueAssistersEvent : MutableLeagueGameEvent, ILeagueAssistersEvent
    {
        public IEnumerable<string> AssistersNames { get; set; } = Enumerable.Empty<string>();
    }
}