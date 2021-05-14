using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    internal abstract class MutableLeagueTimer : ILeagueTimer
    {
        public abstract LeagueTimerType Type { get; }
        public double RemainingTimeInSeconds { get; set; } = 0.0d;
        public double? InGameStartTimeInSeconds { get; set; } = null;
        public double? InGameEndTimeInSeconds { get; set; } = null;
    }
}