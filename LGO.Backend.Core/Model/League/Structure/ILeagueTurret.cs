using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Core.Model.League.Structure
{
    public interface ILeagueTurret
    {
        LeagueTurretTierType Tier { get; }
        
        LeagueTeamType Team { get; }
    }
}