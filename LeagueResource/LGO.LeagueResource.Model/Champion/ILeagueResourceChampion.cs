using System.Collections.Generic;

namespace LGO.LeagueResource.Model.Champion
{
    public interface ILeagueResourceChampion
    {
        int Id { get; }
        
        string Name { get; }
        
        ILeagueResourceChampionImages Images { get; }
        
        IEnumerable<ILeagueResourceChampionSkin> Skins { get; }
    }
}