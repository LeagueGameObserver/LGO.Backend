using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.League.Snapshot
{
    internal sealed record InternalLeagueTeamSnapshot : InternalLeagueGoldOwnerSnapshot
    {
        public LeagueTeamType Side { get; init; }

        public IEnumerable<LeagueDragonType> DragonsKilled { get; init; } = null!;

        public int NumberOfRiftHeraldsKilled { get; init; }

        public int NumberOfBaronNashorsKilled { get; init; }

        public IEnumerable<LeagueTurretTierType> TurretsDestroyed { get; init; } = null!;

        public IEnumerable<LeagueInhibitorTierType> InhibitorsDestroyed { get; init; } = null!;
    }
}