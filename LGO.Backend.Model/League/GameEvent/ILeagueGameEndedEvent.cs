using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueGameEndedEvent : ILeagueGameEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.GameEnded;
    }
}