using System;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.League.Snapshot.MatchUp
{
    internal record InternalLeagueMatchUpSnapshot
    {
        public Guid Id { get; init; }

        public InternalLeagueGoldOwnerSnapshot GoldOwnerBlueSide { get; init; } = null!;

        public InternalLeagueGoldOwnerSnapshot GoldOwnerRedSide { get; init; } = null!;

        public LeagueTeamType WinningSide => GoldOwnerBlueSide.TotalGoldOwned >= GoldOwnerRedSide.TotalGoldOwned ? LeagueTeamType.Blue : LeagueTeamType.Red;

        public int GoldDifference => Math.Abs(GoldOwnerBlueSide.TotalGoldOwned - GoldOwnerRedSide.TotalGoldOwned);
    }
}