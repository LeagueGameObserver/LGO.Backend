namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientFirstChampionKilledEvent : ILeagueClientGameEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.FirstChampionKilled;
        
        string KillerSummonerName { get; }
    }
}