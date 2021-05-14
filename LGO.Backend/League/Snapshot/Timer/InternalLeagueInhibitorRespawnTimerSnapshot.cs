using LGO.Backend.Core.Model.League.Structure;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.League.Snapshot.Timer
{
    internal sealed record InternalLeagueInhibitorRespawnTimerSnapshot : InternalLeagueTimerSnapshot
    {
        public override LeagueTimerType Type
        {
            get => LeagueTimerType.InhibitorRespawn;
            init { }
        }

        public ILeagueInhibitor Inhibitor { get; init; } = null!;
    }
}