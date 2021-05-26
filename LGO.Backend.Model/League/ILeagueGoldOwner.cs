using System;

namespace LGO.Backend.Model.League
{
    public interface ILeagueGoldOwner
    {
        Guid Id { get; }
        
        int TotalGoldOwned { get; }
        
        int UnspentKills { get; }
        
        int UnspentAssists { get; }
        
        int UnspentDeaths { get; }
        
        int TotalKills { get; }
        
        int TotalAssists { get; }
        
        int TotalDeaths { get; }
    }
}