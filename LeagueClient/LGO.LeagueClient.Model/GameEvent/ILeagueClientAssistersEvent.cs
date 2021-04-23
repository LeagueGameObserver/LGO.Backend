using System.Collections.Generic;

namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientAssistersEvent : ILeagueClientGameEvent
    {
        IEnumerable<string> AssistersNames { get; }
    }
}