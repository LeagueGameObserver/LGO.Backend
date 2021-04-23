namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientFirstMinionWaveSpawnedEvent : ILeagueClientGameEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.FirstMinionWaveSpawned;
    }
}