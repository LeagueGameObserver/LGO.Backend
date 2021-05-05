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
    internal class LocalLeagueResourceRepository : ILeagueResourceRepository
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalLeagueResourceRepository));

        public MultiComponentVersion GameVersion { get; }
        
        internal ConcurrentDictionary<Guid, MutableLeagueResourceChampion> ChampionById { get; } = new();
        internal ConcurrentDictionary<int, MutableLeagueResourceChampion> ChampionByKey { get; } = new();
        internal ConcurrentDictionary<Guid, MutableLeagueResourceItem> ItemById { get; } = new();
        internal ConcurrentDictionary<int, MutableLeagueResourceItem> ItemByKey { get; } = new();

        internal LocalLeagueResourceRepository(MultiComponentVersion gameVersion, IEnumerable<MutableLeagueResourceChampion> champions, IEnumerable<MutableLeagueResourceItem> items)
        {
            GameVersion = gameVersion;
            InitializeChampionIndexes(champions);
            InitializeItemIndexes(items);
        }

        private void InitializeChampionIndexes(IEnumerable<MutableLeagueResourceChampion> champions)
        {
            Log.Debug("Start creating champion indexes...");
            foreach (var champion in champions)
            {
                if (!ChampionById.TryAdd(champion.Id, champion))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceChampion)} ({champion.Name}) to the {nameof(ChampionById)} index.");
                }
                Log.Debug($"Inserted champion ({champion.Name}) with id {champion.Id} into {nameof(ChampionById)} index.");

                if (!ChampionByKey.TryAdd(champion.Key, champion))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceChampion)} ({champion.Name}) to the {nameof(ChampionByKey)} index.");
                }
                Log.Debug($"Inserted champion ({champion.Name}) with key {champion.Key} into {nameof(ChampionByKey)} index.");
            }
            
            Log.Info($"Done creating champion indexes. {ChampionById.Count} champions inserted.");
        }

        private void InitializeItemIndexes(IEnumerable<MutableLeagueResourceItem> items)
        {
            Log.Debug("Start creating item indexes...");
            foreach (var item in items)
            {
                if (!ItemById.TryAdd(item.Id, item))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceItem)} ({item.Name}) to the {nameof(ItemById)} index.");
                }
                Log.Debug($"Inserted item ({item.Name}) with id {item.Id} into {nameof(ItemById)} index.");
                
                if (!ItemByKey.TryAdd(item.Key, item))
                {
                    throw new ArgumentException($"Unable to add {nameof(MutableLeagueResourceItem)} ({item.Name}) to the {nameof(ItemByKey)} index.");
                }
                Log.Debug($"Inserted item ({item.Name}) with key {item.Key} into {nameof(ItemByKey)} index.");
            }
            
            Log.Info($"Done creating item indexes. {ItemById.Count} items inserted.");
        }

        public Task<IEnumerable<ILeagueResourceChampion>?> ReadAllChampionsAsync()
        {
            Log.Debug("Querying all champions.");
            return Task.FromResult<IEnumerable<ILeagueResourceChampion>?>(ChampionById.Values);
        }

        public Task<ILeagueResourceChampion?> ReadChampionByIdAsync(Guid championId)
        {
            Log.Debug($"Querying champion for id {championId}.");
            if (ChampionById.TryGetValue(championId, out var champion))
            {
                Log.Debug($"Champion for id {championId} ({champion.Name}) found.");
                return Task.FromResult<ILeagueResourceChampion?>(champion);
            }

            Log.Warn($"Unable to find champion for id {championId}. Where did you get that id from?");
            return Task.FromResult<ILeagueResourceChampion?>(null);
        }

        public Task<ILeagueResourceChampion?> ReadChampionByKeyAsync(int championKey)
        {
            Log.Debug($"Querying champion for key {championKey}.");
            if (ChampionByKey.TryGetValue(championKey, out var champion))
            {
                Log.Debug($"Champion for key {championKey} ({champion.Name}) found.");
                return Task.FromResult<ILeagueResourceChampion?>(champion);
            }

            Log.Warn($"Unable to find champion for key {championKey}.");
            return Task.FromResult<ILeagueResourceChampion?>(null);
        }

        public Task<IEnumerable<ILeagueResourceItem>?> ReadAllItemsAsync()
        {
            Log.Debug("Querying all items.");
            return Task.FromResult<IEnumerable<ILeagueResourceItem>?>(ItemById.Values);
        }

        public Task<ILeagueResourceItem?> ReadItemByIdAsync(Guid itemId)
        {
            Log.Debug($"Querying item for id {itemId}.");
            if (ItemById.TryGetValue(itemId, out var item))
            {
                Log.Debug($"Item for id {itemId} ({item.Name}) found.");
                return Task.FromResult<ILeagueResourceItem?>(item);
            }

            Log.Warn($"Unable to find item for id {itemId}. Where did you get that id from?");
            return Task.FromResult<ILeagueResourceItem?>(null);
        }

        public Task<ILeagueResourceItem?> ReadItemByKeyAsync(int itemKey)
        {
            Log.Debug($"Querying item for key {itemKey}.");
            if (ItemByKey.TryGetValue(itemKey, out var item))
            {
                Log.Debug($"Item for key {itemKey} ({item.Name}) found.");
                return Task.FromResult<ILeagueResourceItem?>(item);
            }

            Log.Warn($"Unable to find item for key {itemKey}.");
            return Task.FromResult<ILeagueResourceItem?>(null);
        }
    }
}