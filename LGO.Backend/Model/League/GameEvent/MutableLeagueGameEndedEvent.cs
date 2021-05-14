using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueGameEndedEvent : MutableLeagueGameEvent, ILeagueGameEndedEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.GameEnded;
    }
}