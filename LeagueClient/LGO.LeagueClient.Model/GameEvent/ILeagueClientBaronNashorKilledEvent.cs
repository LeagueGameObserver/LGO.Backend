namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientBaronNashorKilledEvent : ILeagueClientNeutralObjectiveKilledEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.BaronNashorKilled;
    }
}