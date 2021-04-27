using System.Collections.Generic;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.LeagueResource.Model.Champion;
using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.Model
{
    public interface ILeagueResourceRepository
    {
        MultiComponentVersion GameVersion { get; }
        
        Task<ILeagueResourceChampion?> GetChampionAsync(int id);
        
        Task<ILeagueResourceChampion?> GetChampionAsync(string id);

        Task<IEnumerable<ILeagueResourceChampion>> GetChampionsAsync(params int[] ids);
        
        Task<IEnumerable<ILeagueResourceChampion>> GetChampionsAsync(params string[] ids);

        Task<ILeagueResourceItem?> GetItemAsync(int id);

        Task<IEnumerable<ILeagueResourceItem>> GetItemsAsync(params int[] ids);
    }
}