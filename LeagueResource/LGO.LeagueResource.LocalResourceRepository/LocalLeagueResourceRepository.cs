using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.LeagueResource.LocalResourceRepository.Model.Champion;
using LGO.LeagueResource.LocalResourceRepository.Model.Item;
using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Champion;
using LGO.LeagueResource.Model.Item;
using log4net;

namespace LGO.LeagueResource.LocalResourceRepository
{
    public class LocalLeagueResourceRepository : ILeagueResourceRepository
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalLeagueResourceRepository));

        public MultiComponentVersion GameVersion { get; }

        private ConcurrentDictionary<Guid, MutableLeagueResourceChampion> ChampionById { get; } = new();
        private ConcurrentDictionary<int, MutableLeagueResourceChampion> ChampionByKey { get; } = new();
        private ConcurrentDictionary<Guid, MutableLeagueResourceItem> ItemById { get; } = new();
        private ConcurrentDictionary<int, MutableLeagueResourceItem> ItemByKey { get; } = new();

        internal LocalLeagueResourceRepository(MultiComponentVersion gameVersion, IEnumerable<MutableLeagueResourceChampion> champions, IEnumerable<MutableLeagueResourceItem> items)
        {
            GameVersion = gameVersion;
            InitializeChampionIndexes(champions);
            InitializeItemIndexes(items);
        }

        private void InitializeChampionIndexes(IEnumerable<MutableLeagueResourceChampion> champions)
        {
            foreach (var champion in champions)
            {
                if (!ChampionById.TryAdd(champion.Id, champion))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceChampion)} ({champion.Name}) to the {nameof(ChampionById)} index.");
                }

                if (!ChampionByKey.TryAdd(champion.Key, champion))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceChampion)} ({champion.Name}) to the {nameof(ChampionByKey)} index.");
                }
            }
        }

        private void InitializeItemIndexes(IEnumerable<MutableLeagueResourceItem> items)
        {
            foreach (var item in items)
            {
                if (!ItemById.TryAdd(item.Id, item))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceItem)} ({item.Name}) to the {nameof(ItemById)} index.");
                }
                
                if (!ItemByKey.TryAdd(item.Key, item))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceItem)} ({item.Name}) to the {nameof(ItemByKey)} index.");
                }
            }
        }

        public Task<IEnumerable<ILeagueResourceChampion>?> ReadAllChampionsAsync()
        {
            return Task.FromResult<IEnumerable<ILeagueResourceChampion>?>(ChampionById.Values);
        }

        public Task<ILeagueResourceChampion?> ReadChampionByIdAsync(Guid championId)
        {
            if (ChampionById.TryGetValue(championId, out var champion))
            {
                return Task.FromResult<ILeagueResourceChampion?>(champion);
            }

            return Task.FromResult<ILeagueResourceChampion?>(null);
        }

        public Task<ILeagueResourceChampion?> ReadChampionByKeyAsync(int championKey)
        {
            if (ChampionByKey.TryGetValue(championKey, out var champion))
            {
                return Task.FromResult<ILeagueResourceChampion?>(champion);
            }

            return Task.FromResult<ILeagueResourceChampion?>(null);
        }

        public Task<IEnumerable<ILeagueResourceItem>?> ReadAllItemsAsync()
        {
            return Task.FromResult<IEnumerable<ILeagueResourceItem>?>(ItemById.Values);
        }

        public Task<ILeagueResourceItem?> ReadItemByIdAsync(Guid itemId)
        {
            if (ItemById.TryGetValue(itemId, out var item))
            {
                return Task.FromResult<ILeagueResourceItem?>(item);
            }

            return Task.FromResult<ILeagueResourceItem?>(null);
        }

        public Task<ILeagueResourceItem?> ReadItemByKeyAsync(int itemKey)
        {
            if (ItemByKey.TryGetValue(itemKey, out var item))
            {
                return Task.FromResult<ILeagueResourceItem?>(item);
            }

            return Task.FromResult<ILeagueResourceItem?>(null);
        }
    }
}