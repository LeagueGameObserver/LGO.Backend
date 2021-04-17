using System;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp
{
    public interface ILeagueMatchUp
    {
        Guid Id { get; }
        
        ILeagueGoldOwner BlueSideCompetitor { get; }
        
        ILeagueGoldOwner RedSideCompetitor { get; }
        
        int GoldDifference { get; }
        
        LeagueTeamType WinningTeam { get; }
    }
}