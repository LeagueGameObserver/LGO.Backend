using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Core.Model.League.Structure
{
    public sealed class MutableLeagueInhibitor : ILeagueInhibitor
    {
        public LeagueInhibitorTierType Tier { get; set; } = LeagueInhibitorTierType.Undefined;
        public LeagueTeamType Team { get; set; } = LeagueTeamType.Undefined;
    }
}