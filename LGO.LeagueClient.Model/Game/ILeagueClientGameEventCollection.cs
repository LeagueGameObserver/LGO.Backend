using System.Collections.Generic;
using LGO.LeagueClient.Model.GameEvent;

namespace LGO.LeagueClient.Model.Game
{
    public interface ILeagueClientGameEventCollection
    {
        IEnumerable<ILeagueClientGameEvent> Events { get; }
    }
}