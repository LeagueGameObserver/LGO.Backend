namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientInhibitorDestroyedEvent : ILeagueClientKillerEvent, ILeagueClientAssistersEvent, ILeagueClientInhibitorEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.InhibitorDestroyed;
    }
}