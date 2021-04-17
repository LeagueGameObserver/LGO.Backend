namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientGameStartedEvent : ILeagueClientGameEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.GameStarted;
    }
}