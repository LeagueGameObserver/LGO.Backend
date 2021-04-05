using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.Backend.Core.Model.LeagueOfLegends.Structure;

namespace LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal
{
    internal class MutableTurret : ILolTurret
    {
        public LolTurretTierType Tier { get; set; } = LolTurretTierType.Undefined;
        public LolTeamType Team { get; set; } = LolTeamType.Undefined;
    }
}