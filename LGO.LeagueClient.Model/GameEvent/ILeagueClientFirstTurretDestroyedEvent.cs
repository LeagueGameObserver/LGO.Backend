namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientFirstTurretDestroyedEvent : ILeagueClientKillerEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.FirstTurretDestroyed;
    }
}