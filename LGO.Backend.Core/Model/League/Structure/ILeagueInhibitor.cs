using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Core.Model.League.Structure
{
    public interface ILeagueInhibitor
    {
        LeagueInhibitorTierType Tier { get; }
        
        LeagueTeamType Team { get; }
    }
}