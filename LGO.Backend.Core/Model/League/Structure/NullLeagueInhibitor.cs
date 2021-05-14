using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Core.Model.League.Structure
{
    public sealed class NullLeagueInhibitor : ILeagueInhibitor
    {
        public LeagueInhibitorTierType Tier => LeagueInhibitorTierType.Undefined;
        public LeagueTeamType Team => LeagueTeamType.Undefined;

        private static NullLeagueInhibitor? _instance;

        public static NullLeagueInhibitor Instance => _instance ??= new NullLeagueInhibitor();
        
        private NullLeagueInhibitor() { }
    }
}