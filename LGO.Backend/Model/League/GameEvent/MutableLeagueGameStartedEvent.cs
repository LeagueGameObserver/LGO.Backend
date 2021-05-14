using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueGameStartedEvent : MutableLeagueGameEvent, ILeagueGameStartedEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.GameStarted;
    }
}