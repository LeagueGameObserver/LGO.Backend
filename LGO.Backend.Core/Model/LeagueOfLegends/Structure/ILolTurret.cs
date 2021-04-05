using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.Backend.Core.Model.LeagueOfLegends.Structure
{
    public interface ILolTurret
    {
        LolTurretTierType Tier { get; }
        
        LolTeamType Team { get; }
    }
}