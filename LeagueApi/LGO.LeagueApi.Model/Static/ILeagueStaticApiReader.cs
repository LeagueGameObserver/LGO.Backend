using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.Model.Static.Champion;
using LGO.LeagueApi.Model.Static.Item;

namespace LGO.LeagueApi.Model.Static
{
    public interface ILeagueStaticApiReader
    {
        const string ChampionSplashImageFileExtension = ".jpg";
        const string ChampionLoadingImageFileExtension = ".jpg";
        const string ChampionSquareImageFileExtension = ".png";
        const string ItemSquareImageFileExtension = ".png";
        
        Task<IEnumerable<MultiComponentVersion>?> ReadGameVersionsAsync();

        Task<ILeagueStaticChampionCollection?> ReadAllChampionsAsync(MultiComponentVersion gameVersion, LeagueLocalizationType localizationType = LeagueLocalizationType.EnglishUnitedStates);

        Task<ILeagueStaticItemCollection?> ReadAllItemsAsync(MultiComponentVersion gameVersion, LeagueLocalizationType localizationType = LeagueLocalizationType.EnglishUnitedStates);
        
        Task<FileInfo?> DownloadChampionSplashImageAsync(string championId, FileInfo destination, int skinIndex = 0);
        
        Uri GetChampionSplashImageUrl(string championId, int skinIndex = 0);

        Task<FileInfo?> DownloadChampionLoadingImageAsync(string championId, FileInfo destination, int skinIndex = 0);
        
        Uri GetChampionLoadingImageUrl(string championId, int skinIndex = 0);

        Task<FileInfo?> DownloadChampionSquareImageAsync(MultiComponentVersion gameVersion, string championId, FileInfo destination);
        
        Uri GetChampionSquareImageUrl(MultiComponentVersion gameVersion, string championId);
        
        Task<FileInfo?> DownloadItemSquareImageAsync(MultiComponentVersion gameVersion, string itemId, FileInfo destination);
        
        Uri GetItemSquareImageUrl(MultiComponentVersion gameVersion, string itemId);

        Task<FileInfo?> DownloadFileAsync(Uri url, FileInfo destination);
    }
}