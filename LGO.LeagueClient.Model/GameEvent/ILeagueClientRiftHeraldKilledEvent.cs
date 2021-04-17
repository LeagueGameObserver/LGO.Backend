namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientRiftHeraldKilledEvent : ILeagueClientNeutralObjectiveKilledEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.RiftHeraldKilled;
    }
}