using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal abstract class MutableLeagueGameEvent : ILeagueGameEvent
    {
        public abstract LeagueGameEventType Type { get; }
        public double InGameTimeInSeconds { get; set; } = 0.0;
    }
}