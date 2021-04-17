using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.MatchUp;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeaguePowerPlayTimer : ILeagueTimer
    {
        LeagueTeamType Team { get; }
        
        bool IsActive { get; }
        
        IEnumerable<ILeaguePowerPlayMatchUp> MatchUps { get; }
    }
}