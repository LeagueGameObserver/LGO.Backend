using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.League.Snapshot.Timer;
using LGO.Backend.Model.League.Enum;
using LGO.Backend.Model.League.GameEvent;

namespace LGO.Backend.League.Snapshot
{
    internal sealed record InternalLeagueGameSnapshot
    {
        public LeagueGameStateType State { get; init; }

        public LeagueGameModeType Mode { get; init; }

        public TimeSpan InGameTime { get; init; }

        public IEnumerable<InternalLeagueTeamSnapshot> Teams { get; init; } = null!;

        public IEnumerable<InternalLeaguePlayerSnapshot> Players { get; init; } = null!;

        public IEnumerable<InternalLeagueTimerSnapshot> Timers { get; init; } = null!;

        public IEnumerable<ILeagueGameEvent> Events { get; init; } = null!;
    }
}