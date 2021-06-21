using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League
{
    public interface ILeagueGameSummary
    {
        Guid Id { get; }
        
        LeagueGameStateType State { get; }
        
        LeagueGameModeType Mode { get; }
        
        double InGameTimeInSeconds { get; }
        
        IEnumerable<string> SummonerNamesBlueTeam { get; }
        
        IEnumerable<string> SummonerNamesRedSide { get; }
    }
}