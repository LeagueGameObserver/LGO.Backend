using System;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.League.Snapshot.Timer
{
    internal abstract record InternalLeagueTimerSnapshot
    {
        public Guid Id { get; init; }
        
        public abstract LeagueTimerType Type { get; init; }
        
        public TimeSpan InGameStartTime { get; init; }
        
        public TimeSpan InGameEndTime { get; init; }
        
        public TimeSpan RemainingTime { get; init; }
    }
}