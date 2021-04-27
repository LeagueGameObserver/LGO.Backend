using System;
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
        
        Task<IEnumerable<ILeagueResourceChampion>?> ReadAllChampionsAsync();

        Task<ILeagueResourceChampion?> ReadChampionByIdAsync(Guid championId);

        Task<ILeagueResourceChampion?> ReadChampionByKeyAsync(int championKey);

        Task<IEnumerable<ILeagueResourceItem>?> ReadAllItemsAsync();

        Task<ILeagueResourceItem?> ReadItemByIdAsync(Guid itemId);

        Task<ILeagueResourceItem?> ReadItemByKeyAsync(int itemKey);
    }
}