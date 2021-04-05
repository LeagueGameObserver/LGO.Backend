namespace LGO.LeagueOfLegends.ClientApi.Model.Player
{
    public interface ILolClientScore
    {
        int Kills { get; }
        
        int Deaths { get; }
        
        int Assists { get; }
        
        int MinionKills { get; }
        
        double Vision { get; }
    }
}