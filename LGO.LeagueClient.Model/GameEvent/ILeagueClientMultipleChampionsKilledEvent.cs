namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientMultipleChampionsKilledEvent : ILeagueClientKillerEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.MultipleChampionsKilled;
        
        int NumberOfKills { get; }
    }
}