using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueGameStartedEvent : ILeagueGameEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.GameStarted;
    }
}