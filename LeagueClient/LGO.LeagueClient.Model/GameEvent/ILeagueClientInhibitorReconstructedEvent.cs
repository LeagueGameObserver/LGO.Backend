namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientInhibitorReconstructedEvent : ILeagueClientInhibitorEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.InhibitorReconstructed;
    }
}