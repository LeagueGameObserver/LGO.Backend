using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.Backend.Core.Model.LeagueOfLegends.Structure;

namespace LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal
{
    internal class MutableInhibitor : ILolInhibitor
    {
        public LolInhibitorTierType Tier { get; set; } = LolInhibitorTierType.Undefined;
        public LolTeamType Team { get; set; } = LolTeamType.Undefined;
    }
}