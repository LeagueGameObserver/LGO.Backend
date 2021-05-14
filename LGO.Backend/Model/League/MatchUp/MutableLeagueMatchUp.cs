using System;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp
{
    internal abstract class MutableLeagueMatchUp : ILeagueMatchUp
    {
        public Guid Id { get; set; } = Guid.Empty;
        public ILeagueGoldOwner BlueSideCompetitor { get; set; } = NullLeagueGoldOwner.Instance;
        public ILeagueGoldOwner RedSideCompetitor { get; set; } = NullLeagueGoldOwner.Instance;
        public int GoldDifference { get; set; } = 0;
        public LeagueTeamType WinningTeam { get; set; } = LeagueTeamType.Undefined;
    }
}