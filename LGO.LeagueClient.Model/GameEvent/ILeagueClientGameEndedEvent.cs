using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientGameEndedEvent : ILeagueClientGameEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.GameEnded;
        
        LeagueGameResult ResultForActivePlayer { get; }
    }
}