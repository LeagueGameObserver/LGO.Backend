using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.Backend.Core.Model.LeagueOfLegends.Structure;

namespace LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal
{
    internal sealed class NullInhibitor : ILolInhibitor
    {
        public LolInhibitorTierType Tier => LolInhibitorTierType.Undefined;
        public LolTeamType Team => LolTeamType.Undefined;

        private static NullInhibitor? _instance;

        public static NullInhibitor Get => _instance ??= new NullInhibitor();
        
        private NullInhibitor() { }
    }
}