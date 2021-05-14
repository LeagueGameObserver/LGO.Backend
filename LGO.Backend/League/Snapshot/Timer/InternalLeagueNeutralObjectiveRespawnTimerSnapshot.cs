using System;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.League.Snapshot.Timer
{
    internal sealed record InternalLeagueNeutralObjectiveRespawnTimerSnapshot : InternalLeagueTimerSnapshot
    {
        public override LeagueTimerType Type { get; init; } = LeagueTimerType.Undefined;
    }
}