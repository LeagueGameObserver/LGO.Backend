using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.League.Snapshot.Timer
{
    internal sealed record InternalLeaguePowerPlayTimerSnapshot : InternalLeagueTimerSnapshot
    {
        public override LeagueTimerType Type { get; init; }
        
        public LeagueTeamType Team { get; init; }

        public IEnumerable<string> BuffedPlayers { get; init; } = null!;

        public bool IsActive => BuffedPlayers.Any();
    }
}