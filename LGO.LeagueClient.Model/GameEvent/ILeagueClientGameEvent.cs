using System;

namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientGameEvent
    {
        int Id { get; }
        
        LeagueClientGameEventType Type { get; }
        
        TimeSpan InGameTime { get; }
    }
}