using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using LGO.Backend.Core.Http;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.Model.Static;
using LGO.LeagueApi.Model.Static.Champion;
using LGO.LeagueApi.Model.Static.Item;
using LGO.LeagueApi.RemoteApiReader.Static.Model.Champion;
using LGO.LeagueApi.RemoteApiReader.Static.Model.Item;
using log4net;

namespace LGO.LeagueApi.RemoteApiReader.Static
{
    public sealed class RemoteLeagueStaticApiReader : ILeagueStaticApiReader
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(RemoteLeagueStaticApiReader));
        private static TimeSpan RequestTimeout { get; } = TimeSpan.FromSeconds(2);

        private static RemoteLeagueStaticApiReader? _instance;

        public static RemoteLeagueStaticApiReader Instance => _instance ??= new RemoteLeagueStaticApiReader();

        private JsonHttpClient Client { get; }

        public RemoteLeagueStaticApiReader(JsonHttpClient client)
        {
            Client = client;
        }

        public RemoteLeagueStaticApiReader()
        {
            Client = new JsonHttpClient(RequestTimeout);
        }

        public async Task<IEnumerable<MultiComponentVersion>?> ReadGameVersionsAsync()
        {
            var versionsAsStrings = await Client.GetAsync<List<string>>("https://ddragon.leagueoflegends.com/api/versions.json");
            if (versionsAsStrings == null)
            {
                return null;
            }

            var versions = new List<MultiComponentVersion>();
            foreach (var versionAsString in versionsAsStrings)
            {
                if (MultiComponentVersion.TryParse(versionAsString, out var version))
                {
                    versions.Add(version);
                }
            }

            return versions;
        }

        public async Task<ILeagueStaticChampionCollection?> ReadAllChampionsAsync(MultiComponentVersion gameVersion, LeagueLocalization localization = LeagueLocalization.EnglishUnitedStates)
        {
            var url = $"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/data/{localization.ToCountryCode()}/champion.json";
            return await Client.GetAsync<MutableLeagueStaticChampionCollection>(url);
        }

        public async Task<ILeagueStaticItemCollection?> ReadAllItemsAsync(MultiComponentVersion gameVersion, LeagueLocalization localization = LeagueLocalization.EnglishUnitedStates)
        {
            var url = $"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/data/{localization.ToCountryCode()}/item.json";
            return await Client.GetAsync<MutableLeagueStaticItemCollection>(url);
        }

        public async Task<FileInfo?> DownloadChampionSplashImageAsync(string championId, FileInfo destination, int skinIndex = 0)
        {
            return await DownloadFileAsync(GetChampionSplashImageUrl(championId, skinIndex), destination);
        }

        public string GetChampionSplashImageUrl(string championId, int skinIndex = 0)
        {
            return $"http://ddragon.leagueoflegends.com/cdn/img/champion/splash/{championId}_{skinIndex}.jpg";
        }

        public async Task<FileInfo?> DownloadChampionLoadingImageAsync(string championId, FileInfo destination, int skinIndex = 0)
        {
            return await DownloadFileAsync(GetChampionLoadingImageUrl(championId, skinIndex), destination);
        }

        public string GetChampionLoadingImageUrl(string championId, int skinIndex = 0)
        {
            return $"http://ddragon.leagueoflegends.com/cdn/img/champion/loading/{championId}_{skinIndex}.jpg";
        }

        public async Task<FileInfo?> DownloadChampionSquareImageAsync(MultiComponentVersion gameVersion, string championId, FileInfo destination)
        {
            return await DownloadFileAsync(GetChampionSquareImageUrl(gameVersion, championId), destination);
        }

        public string GetChampionSquareImageUrl(MultiComponentVersion gameVersion, string championId)
        {
            return $"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/img/champion/{championId}.png";
        }

        public async Task<FileInfo?> DownloadItemSquareImageAsync(MultiComponentVersion gameVersion, string itemId, FileInfo destination)
        {
            return await DownloadFileAsync(GetItemSquareImageUrl(gameVersion, itemId), destination);
        }

        public string GetItemSquareImageUrl(MultiComponentVersion gameVersion, string itemId)
        {
            return $"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/img/item/{itemId}.png";
        }

        public async Task<FileInfo?> DownloadFileAsync(string url, FileInfo destination)
        {
            if (File.Exists(destination.FullName))
            {
                throw new ArgumentException($"{nameof(destination)} (\"{destination.FullName}\") does already exist.");
            }

            if (!Directory.Exists(destination.DirectoryName))
            {
                Directory.CreateDirectory(destination.Directory?.FullName ?? throw new ArgumentException($"Unable to determine directory for {nameof(destination)}."));
            }

            await Task.Run(() =>
                           {
                               try
                               {
                                   using var client = new WebClient();
                                   client.DownloadFile(url, destination.FullName);
                               }
                               catch (Exception exception)
                               {
                                   Log.Error($"Exception while downloading file from \"{url}\".", exception);
                               }
                           });

            return File.Exists(destination.FullName) ? destination : null;
        }
    }
}