using System;

namespace LGO.LeagueResource.Model.Champion
{
    public interface ILeagueResourceChampion
    {
        Guid Id { get; }
        
        int Key { get; }
        
        string Name { get; }
        
        ILeagueResourceChampionImages Images { get; }
    }
}