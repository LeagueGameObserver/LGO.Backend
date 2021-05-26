using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.Team
{
    public interface ILeagueTeam : ILeagueGoldOwner
    {
        LeagueTeamType? Side { get; }

        IEnumerable<LeagueDragonType>? DragonsKilled { get; }

        int? NumberOfRiftHeraldsKilled { get; }

        int? NumberOfBaronNashorsKilled { get; }

        IEnumerable<LeagueTurretTierType>? TurretsDestroyed { get; }

        IEnumerable<LeagueInhibitorTierType>? InhibitorsDestroyed { get; }
    }
}