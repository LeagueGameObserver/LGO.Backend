using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.League.Snapshot
{
    internal sealed record InternalLeaguePlayerSnapshot : InternalLeagueGoldOwnerSnapshot
    {
        public string SummonerName { get; init; } = null!;

        public string ChampionName { get; init; } = null!;

        public LeagueTeamType Team { get; init; }
        
        public bool IsDead { get; init; }
        
        public TimeSpan RespawnTime { get; init; }

        public IEnumerable<InternalLeagueItemSnapshot> Items { get; init; } = null!;
    }
}