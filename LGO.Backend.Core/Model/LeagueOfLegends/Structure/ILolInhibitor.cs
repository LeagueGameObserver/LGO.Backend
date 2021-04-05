using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.Backend.Core.Model.LeagueOfLegends.Structure
{
    public interface ILolInhibitor
    {
        LolInhibitorTierType Tier { get; }
        
        LolTeamType Team { get; }
    }
}