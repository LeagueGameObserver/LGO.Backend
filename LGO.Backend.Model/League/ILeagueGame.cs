using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;
using LGO.Backend.Model.League.GameEvent;
using LGO.Backend.Model.League.MatchUp;
using LGO.Backend.Model.League.Player;
using LGO.Backend.Model.League.Team;
using LGO.Backend.Model.League.Timer;

namespace LGO.Backend.Model.League
{
    public interface ILeagueGame
    {
        Guid Id { get; }
        
        LeagueGameStateType State { get; }
        
        LeagueGameModeType Mode { get; }
        
        double InGameTimeInSeconds { get; }
        
        IEnumerable<ILeagueTeam> Teams { get; }
        
        IEnumerable<ILeaguePlayer> Players { get; }
        
        IEnumerable<ILeagueMatchUp> MatchUps { get; }
        
        IEnumerable<ILeagueTimer> Timers { get; }
        
        IEnumerable<ILeagueGameEvent> Events { get; }
        
        IEnumerable<ILeagueGameEvent> EventsSinceLastUpdate { get; }
    }
}