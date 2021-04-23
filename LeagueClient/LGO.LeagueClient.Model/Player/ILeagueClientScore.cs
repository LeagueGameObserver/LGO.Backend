namespace LGO.LeagueClient.Model.Player
{
    public interface ILeagueClientScore
    {
        int Kills { get; }
        
        int Deaths { get; }
        
        int Assists { get; }
        
        int MinionKills { get; }
        
        double Vision { get; }
    }
}