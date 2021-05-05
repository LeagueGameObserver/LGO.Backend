using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.Model.Static;
using LGO.LeagueApi.Model.Static.Champion;
using LGO.LeagueResource.LocalResourceRepository.Model.Champion;
using LGO.LeagueResource.LocalResourceRepository.Model.ImageReader;
using LGO.LeagueResource.LocalResourceRepository.Model.Item;
using LGO.LeagueResource.Model;
using log4net;

namespace LGO.LeagueResource.LocalResourceRepository
{
    public static class LeagueResourceRepositoryFactory
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LeagueResourceRepositoryFactory));

        private const string RootDirectory = nameof(LocalLeagueResourceRepository);

        public static async Task<ILeagueResourceRepository> LoadOrCreate(ILeagueStaticApiReader staticApiReader,
                                                                         MultiComponentVersion gameVersion,
                                                                         LeagueLocalization localization = LeagueLocalization.EnglishUnitedStates)
        {
            var rootDirectory = GetOrCreateRootDirectory(gameVersion);
            if (Directory.Exists(rootDirectory.FullName))
            {
                Log.Info($"Reusing existing {nameof(LocalLeagueResourceRepository)} (Game Version = {gameVersion}) from \"{rootDirectory.FullName}\".");
                Directory.Delete(rootDirectory.FullName, true);
            }
            else
            {
                Log.Info($"Creating new {nameof(LocalLeagueResourceRepository)} instance for game version {gameVersion} and localization {localization}.");
            }

            var champions = await ReadChampionsAsync(staticApiReader, gameVersion, localization);
            var items = await ReadItemsAsync(staticApiReader, gameVersion, localization);

            return new LocalLeagueResourceRepository(gameVersion, champions, items);
        }

        private static async Task<IEnumerable<MutableLeagueResourceChampion>> ReadChampionsAsync(ILeagueStaticApiReader staticApiReader,
                                                                                                 MultiComponentVersion gameVersion,
                                                                                                 LeagueLocalization localization)
        {
            var championImageDirectory = new DirectoryInfo($"{GetOrCreateRootDirectory(gameVersion)}/Images/Champions");
            if (!Directory.Exists(championImageDirectory.FullName))
            {
                Directory.CreateDirectory(championImageDirectory.FullName);
            }

            var staticChampions = await staticApiReader.ReadAllChampionsAsync(gameVersion, localization);
            if (staticChampions == null)
            {
                throw new Exception($"Unable to read all static champions from the given {nameof(ILeagueStaticApiReader)}.");
            }

            return from staticChampion in staticChampions.Entries.Values
                   let loadingImage = CreateChampionLoadingImageDescriptor(staticApiReader, championImageDirectory, staticChampion)
                   let splashImage = CreateChampionSplashImageDescriptor(staticApiReader, championImageDirectory, staticChampion)
                   let squareImage = CreateChampionSquareImageDescriptor(staticApiReader, championImageDirectory, gameVersion, staticChampion)
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
            var itemImageDirectory = new DirectoryInfo($"{GetOrCreateRootDirectory(gameVersion)}/Images/Items");
            if (!Directory.Exists(itemImageDirectory.FullName))
            {
                Directory.CreateDirectory(itemImageDirectory.FullName);
            }

            var staticItems = await staticApiReader.ReadAllItemsAsync(gameVersion, localization);
            if (staticItems == null)
            {
                throw new Exception($"Unable to read all static items from the given {nameof(ILeagueStaticApiReader)}.");
            }

            var resourceItems = new List<MutableLeagueResourceItem>();
            foreach (var (staticItemId, staticItem) in staticItems.Entries)
            {
                var squareImage = CreateItemSquareImageDescriptor(staticApiReader, itemImageDirectory, gameVersion, staticItemId);
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

        internal static DirectoryInfo GetOrCreateRootDirectory(MultiComponentVersion gameVersion)
        {
            var dirInfo = new DirectoryInfo($"./{RootDirectory}/{gameVersion}");
            if (!Directory.Exists(dirInfo.FullName))
            {
                Directory.CreateDirectory(dirInfo.FullName);
            }

            return dirInfo;
        }

        private static IImageReader CreateChampionLoadingImageDescriptor(ILeagueStaticApiReader staticApiReader,
                                                                         DirectoryInfo championImageDirectory,
                                                                         ILeagueStaticChampion staticChampion)
        {
            var localFile = $"{championImageDirectory}/Loading/{staticChampion.Key}{ILeagueStaticApiReader.ChampionLoadingImageFileExtension}";
            var url = staticApiReader.GetChampionLoadingImageUrl(staticChampion.Id, 0);
            return RemoteImageReader.Of(new RemoteImageData
                                        {
                                            DownloadUrl = url,
                                            RelativeFilePath = localFile
                                        },
                                        staticApiReader.DownloadFileAsync);
        }

        private static IImageReader CreateChampionSplashImageDescriptor(ILeagueStaticApiReader staticApiReader,
                                                                        DirectoryInfo championImageDirectory,
                                                                        ILeagueStaticChampion staticChampion)
        {
            var localFile = $"{championImageDirectory}/Splash/{staticChampion.Key}{ILeagueStaticApiReader.ChampionSplashImageFileExtension}";
            var url = staticApiReader.GetChampionSplashImageUrl(staticChampion.Id, 0);
            return RemoteImageReader.Of(new RemoteImageData
                                        {
                                            DownloadUrl = url,
                                            RelativeFilePath = localFile
                                        },
                                        staticApiReader.DownloadFileAsync);
        }

        private static IImageReader CreateChampionSquareImageDescriptor(ILeagueStaticApiReader staticApiReader,
                                                                        DirectoryInfo championImageDirectory,
                                                                        MultiComponentVersion gameVersion,
                                                                        ILeagueStaticChampion staticChampion)
        {
            var localFile = $"{championImageDirectory}/Square/{staticChampion.Key}{ILeagueStaticApiReader.ChampionSplashImageFileExtension}";
            var url = staticApiReader.GetChampionSquareImageUrl(gameVersion, staticChampion.Id);
            return RemoteImageReader.Of(new RemoteImageData
                                        {
                                            DownloadUrl = url,
                                            RelativeFilePath = localFile
                                        },
                                        staticApiReader.DownloadFileAsync);
        }

        private static IImageReader CreateItemSquareImageDescriptor(ILeagueStaticApiReader staticApiReader,
                                                                    DirectoryInfo itemImageDirectory,
                                                                    MultiComponentVersion gameVersion,
                                                                    string staticItemId)
        {
            var localFile = $"{itemImageDirectory.FullName}/Square/{staticItemId}{ILeagueStaticApiReader.ItemSquareImageFileExtension}";
            var url = staticApiReader.GetItemSquareImageUrl(gameVersion, staticItemId);
            return RemoteImageReader.Of(new RemoteImageData
                                        {
                                            DownloadUrl = url,
                                            RelativeFilePath = localFile
                                        },
                                        staticApiReader.DownloadFileAsync);
        }
    }
}