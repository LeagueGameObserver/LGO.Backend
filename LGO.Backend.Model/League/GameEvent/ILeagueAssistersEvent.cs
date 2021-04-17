using System.Collections.Generic;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueAssistersEvent : ILeagueGameEvent
    {
        IEnumerable<string> AssistersNames { get; }
    }
}