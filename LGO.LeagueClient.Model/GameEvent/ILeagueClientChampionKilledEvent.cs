namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientChampionKilledEvent : ILeagueClientKillerEvent, ILeagueClientAssistersEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.ChampionKilled;
        
        string VictimSummonerName { get; }
    }
}