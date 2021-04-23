namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientInhibitorAboutToBeReconstructedEvent : ILeagueClientInhibitorEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.InhibitorAboutToBeReconstructed;
    }
}