namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientFirstMinionWaveSpawnedEvent : ILolClientGameEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.FirstMinionWaveSpawned;
    }
}