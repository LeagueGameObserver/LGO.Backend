using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.Team
{
    internal sealed class MutableLeagueTeam : MutableLeagueGoldOwner, ILeagueTeam
    {
        public Guid Id { get; set; } = Guid.Empty;
        public LeagueTeamType? Side { get; set; } = null;
        public IEnumerable<LeagueDragonType>? DragonsKilled { get; set; } = null;
        public int? NumberOfRiftHeraldsKilled { get; set; } = null;
        public int? NumberOfBaronNashorsKilled { get; set; } = null;
        public IEnumerable<LeagueTurretTierType>? TurretsDestroyed { get; set; } = null;
        public IEnumerable<LeagueInhibitorTierType>? InhibitorsDestroyed { get; set; } = null;
    }
}