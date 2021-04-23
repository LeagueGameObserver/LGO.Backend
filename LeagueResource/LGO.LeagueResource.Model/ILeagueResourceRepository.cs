using System.Collections.Generic;
using System.Threading.Tasks;
using LGO.LeagueResource.Model.Champion;
using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.Model
{
    public interface ILeagueResourceRepository
    {
        Task<ILeagueResourceChampion?> GetChampionAsync(int id);
        
        Task<ILeagueResourceChampion?> GetChampionAsync(string name);

        Task<IEnumerable<ILeagueResourceChampion>> GetChampionsAsync(params int[] ids);
        
        Task<IEnumerable<ILeagueResourceChampion>> GetChampionsAsync(params string[] names);

        Task<ILeagueResourceItem?> GetItemAsync(int id);
        
        Task<ILeagueResourceItem?> GetItemAsync(string name);

        Task<IEnumerable<ILeagueResourceItem>> GetItemsAsync(params int[] ids);
        
        Task<IEnumerable<ILeagueResourceItem>> GetItemsAsync(params string[] names);
    }
}