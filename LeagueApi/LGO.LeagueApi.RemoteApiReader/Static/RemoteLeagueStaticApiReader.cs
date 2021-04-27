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

        private static BidirectionalStringMapping<LeagueLocalization> LocalizationTranslations { get; } = new((LeagueLocalization.Undefined, "Undefined"),
                                                                                                              (LeagueLocalization.Czech, "cs_CZ"),
                                                                                                              (LeagueLocalization.Greek, "el_GR"),
                                                                                                              (LeagueLocalization.Polish, "pl_PL"),
                                                                                                              (LeagueLocalization.Romanian, "ro_RO"),
                                                                                                              (LeagueLocalization.Hungarian, "hu_HU"),
                                                                                                              (LeagueLocalization.EnglishUnitedKingdom, "en_GB"),
                                                                                                              (LeagueLocalization.German, "de_DE"),
                                                                                                              (LeagueLocalization.SpanishSpain, "es_ES"),
                                                                                                              (LeagueLocalization.Italian, "it_IT"),
                                                                                                              (LeagueLocalization.French, "fr_FR"),
                                                                                                              (LeagueLocalization.Japanese, "ja_JP"),
                                                                                                              (LeagueLocalization.Korean, "ko_KR"),
                                                                                                              (LeagueLocalization.SpanishMexico, "es_MX"),
                                                                                                              (LeagueLocalization.SpanishArgentina, "es_AR"),
                                                                                                              (LeagueLocalization.PortugueseBrazil, "pt_BR"),
                                                                                                              (LeagueLocalization.EnglishUnitedStates, "en_US"),
                                                                                                              (LeagueLocalization.EnglishAustralia, "en_AU"),
                                                                                                              (LeagueLocalization.Russian, "ru_RU"),
                                                                                                              (LeagueLocalization.Turkish, "tr_TR"),
                                                                                                              (LeagueLocalization.Malay, "ms_MY"),
                                                                                                              (LeagueLocalization.EnglishPhilippines, "en_PH"),
                                                                                                              (LeagueLocalization.EnglishSingapore, "en_SG"),
                                                                                                              (LeagueLocalization.Thai, "th_TH"),
                                                                                                              (LeagueLocalization.Vietnamese, "vn_VN"),
                                                                                                              (LeagueLocalization.Indonesian, "id_ID"),
                                                                                                              (LeagueLocalization.ChineseMalaysia, "zh_MY"),
                                                                                                              (LeagueLocalization.ChineseChina, "zh_CN"),
                                                                                                              (LeagueLocalization.ChineseTaiwan, "zh_TW"));

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
            var url = $"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/data/{LocalizationTranslations.Get(localization)}/champion.json";
            return await Client.GetAsync<MutableLeagueStaticChampionCollection>(url);
        }

        public async Task<ILeagueStaticItemCollection?> ReadAllItemsAsync(MultiComponentVersion gameVersion, LeagueLocalization localization = LeagueLocalization.EnglishUnitedStates)
        {
            var url = $"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/data/{LocalizationTranslations.Get(localization)}/item.json";
            return await Client.GetAsync<MutableLeagueStaticItemCollection>(url);
        }

        public async Task<FileInfo?> DownloadChampionSplashImageAsync(string championId, FileInfo destination, int skinIndex = 0)
        {
            return await DownloadFileAsync($"http://ddragon.leagueoflegends.com/cdn/img/champion/splash/{championId}_{skinIndex}.jpg", destination);
        }
        
        public async Task<FileInfo?> DownloadChampionLoadingImageAsync(string championId, FileInfo destination, int skinIndex = 0)
        {
            return await DownloadFileAsync($"http://ddragon.leagueoflegends.com/cdn/img/champion/loading/{championId}_{skinIndex}.jpg", destination);
        }

        public async Task<FileInfo?> DownloadChampionSquareImageAsync(MultiComponentVersion gameVersion, string championId, FileInfo destination)
        {
            return await DownloadFileAsync($"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/img/champion/{championId}.png", destination);
        }

        public async Task<FileInfo?> DownloadItemSquareImageAsync(MultiComponentVersion gameVersion, string itemId, FileInfo destination)
        {
            return await DownloadFileAsync($"http://ddragon.leagueoflegends.com/cdn/{gameVersion}/img/item/{itemId}.png", destination);
        }

        private static async Task<FileInfo?> DownloadFileAsync(string url, FileInfo destination)
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