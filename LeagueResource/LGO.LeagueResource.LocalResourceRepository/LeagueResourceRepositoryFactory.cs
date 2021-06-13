using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.Model.Static;
using LGO.LeagueResource.LocalResourceRepository.Model.Champion;
using LGO.LeagueResource.LocalResourceRepository.Model.Item;
using LGO.LeagueResource.Model;
using log4net;

namespace LGO.LeagueResource.LocalResourceRepository
{
    public static class LeagueResourceRepositoryFactory
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LeagueResourceRepositoryFactory));

        public static async Task<ILeagueResourceRepository> Create(ILeagueStaticApiReader staticApiReader,
                                                                   MultiComponentVersion gameVersion,
                                                                   LeagueLocalization localization = LeagueLocalization.EnglishUnitedStates)
        {
            Log.Info($"Creating new {nameof(LocalLeagueResourceRepository)} instance for game version {gameVersion} and localization {localization}.");
            var champions = await ReadChampionsAsync(staticApiReader, gameVersion, localization);
            var items = await ReadItemsAsync(staticApiReader, gameVersion, localization);

            return new LocalLeagueResourceRepository(gameVersion, champions, items);
        }

        private static async Task<IEnumerable<MutableLeagueResourceChampion>> ReadChampionsAsync(ILeagueStaticApiReader staticApiReader,
                                                                                                 MultiComponentVersion gameVersion,
                                                                                                 LeagueLocalization localization)
        {
            var staticChampions = await staticApiReader.ReadAllChampionsAsync(gameVersion, localization);
            if (staticChampions == null)
            {
                throw new Exception($"Unable to read all static champions from the given {nameof(ILeagueStaticApiReader)}.");
            }

            return from staticChampion in staticChampions.Entries.Values
                   let loadingImage = staticApiReader.GetChampionLoadingImageUrl(staticChampion.Id)
                   let splashImage = staticApiReader.GetChampionSplashImageUrl(staticChampion.Id)
                   let squareImage = staticApiReader.GetChampionSquareImageUrl(gameVersion, staticChampion.Id)
                   select new MutableLeagueResourceChampion
                          {
                              Key = staticChampion.Key,
                              Name = staticChampion.Name,
                              Images = new MutableLeagueResourceChampionImages
                                       {
                                           LoadingImage = loadingImage,
                                           SplashImage = splashImage,
                                           SquareImage = squareImage,
                                       }
                          };
        }

        private static async Task<IEnumerable<MutableLeagueResourceItem>> ReadItemsAsync(ILeagueStaticApiReader staticApiReader,
                                                                                         MultiComponentVersion gameVersion,
                                                                                         LeagueLocalization localization)
        {
            var staticItems = await staticApiReader.ReadAllItemsAsync(gameVersion, localization);
            if (staticItems == null)
            {
                throw new Exception($"Unable to read all static items from the given {nameof(ILeagueStaticApiReader)}.");
            }

            var resourceItems = new List<MutableLeagueResourceItem>();
            foreach (var (staticItemId, staticItem) in staticItems.Entries)
            {
                var squareImage = staticApiReader.GetItemSquareImageUrl(gameVersion, staticItemId);
                var resourceItem = new MutableLeagueResourceItem
                                   {
                                       Key = int.Parse(staticItemId),
                                       Name = staticItem.Name,
                                       IsPurchasable = staticItem.Costs.IsPurchasable,
                                       Costs = new MutableLeagueResourceItemCosts
                                               {
                                                   TotalCosts = staticItem.Costs.TotalCosts,
                                                   RecipeCosts = staticItem.Costs.RecipeCosts,
                                                   SellWorth = staticItem.Costs.SellWorth
                                               },
                                       Images = new MutableLeagueResourceItemImages {SquareImage = squareImage}
                                   };
                resourceItems.Add(resourceItem);
            }

            return resourceItems;
        }
    }
}