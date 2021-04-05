using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.Backend.Core.Model.LeagueOfLegends.Structure;

namespace LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal
{
    internal sealed class NullTurret : ILolTurret
    {
        public LolTurretTierType Tier => LolTurretTierType.Undefined;
        public LolTeamType Team => LolTeamType.Undefined;

        private static NullTurret? _instance;

        public static NullTurret Get => _instance ??= new NullTurret();
        
        private NullTurret() { }
    }
}