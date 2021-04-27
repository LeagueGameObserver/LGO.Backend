using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.LeagueApi.Model.Static;
using LGO.LeagueApi.RemoteApiReader.Static;
using NUnit.Framework;

// ReSharper disable PossibleMultipleEnumeration

namespace LGO.LeagueApi.RemoteApiReader.Test.Static
{
    [TestFixture]
    public class RemoteLeagueStaticApiReaderTest
    {
        private const string ImageDirectory = "./Images";

        [SetUp]
        public void BeforeTest()
        {
            if (Directory.Exists(ImageDirectory))
            {
                Directory.Delete(ImageDirectory, true);
            }
        }
        
        [Test]
        public async Task TestReadGameVersions()
        {
            var reader = new RemoteLeagueStaticApiReader();
            var availableVersions = await reader.ReadGameVersionsAsync();
            
            Assert.IsNotNull(availableVersions);
            Assert.IsTrue(availableVersions.Any());
            Assert.That(availableVersions, Is.Ordered.Descending);
            CollectionAssert.Contains(availableVersions, new MultiComponentVersion(11, 8, 1)); // latest version as of when writing this test (23.04.2021)
        }

        [Test]
        public async Task TestReadAllChampions()
        {
            var reader = new RemoteLeagueStaticApiReader();
            var version = new MultiComponentVersion(11, 8, 1);

            var champions = await reader.ReadAllChampionsAsync(version);
            
            Assert.IsNotNull(champions);
            Assert.AreEqual(155, champions.Entries.Count);
            
            Assert.IsTrue(champions.Entries.TryGetValue("AurelionSol", out var aurelionSol));
            Assert.IsNotNull(aurelionSol);
            Assert.AreEqual("AurelionSol", aurelionSol.Id);
            Assert.AreEqual("Aurelion Sol", aurelionSol.Name);
            Assert.AreEqual(136, aurelionSol.Key);
        }

        [Test]
        public async Task ReadAllItems()
        {
            var reader = new RemoteLeagueStaticApiReader();
            var version = new MultiComponentVersion(11, 8, 1);

            var items = await reader.ReadAllItemsAsync(version);
            
            Assert.IsNotNull(items);
            Assert.AreEqual(199, items.Entries.Count);
            
            Assert.IsTrue(items.Entries.TryGetValue("1001", out var bootsOfSpeed));
            Assert.IsNotNull(bootsOfSpeed);
            Assert.AreEqual("Boots", bootsOfSpeed.Name);
            Assert.AreEqual(300, bootsOfSpeed.Costs.RecipeCosts);
            Assert.AreEqual(300, bootsOfSpeed.Costs.TotalCosts);
            Assert.AreEqual(210, bootsOfSpeed.Costs.SellWorth);
            Assert.IsTrue(bootsOfSpeed.Costs.IsPurchasable);
        }

        [Test]
        public async Task TestDownloadChampionSplashImage()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string championId = "Aatrox";
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ChampionSplashImageFileExtension}";

            var image = await reader.DownloadChampionSplashImageAsync(championId, new FileInfo(fileName));
            
            Assert.IsNotNull(image);
            Assert.IsTrue(File.Exists(fileName));
        }

        [Test]
        public async Task TestDownloadChampionSplashImageForInvalidId()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string championId = "NotALeagueChampion";
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ChampionSplashImageFileExtension}";

            var image = await reader.DownloadChampionSplashImageAsync(championId, new FileInfo(fileName));
            
            Assert.IsNull(image);
            Assert.IsFalse(File.Exists(fileName));
        }
        
        [Test]
        public async Task TestDownloadChampionLoadingImage()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string championId = "Aatrox";
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ChampionLoadingImageFileExtension}";

            var image = await reader.DownloadChampionLoadingImageAsync(championId, new FileInfo(fileName));
            
            Assert.IsNotNull(image);
            Assert.IsTrue(File.Exists(fileName));
        }

        [Test]
        public async Task TestDownloadChampionLoadingImageForInvalidId()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string championId = "NotALeagueChampion";
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ChampionLoadingImageFileExtension}";

            var image = await reader.DownloadChampionLoadingImageAsync(championId, new FileInfo(fileName));
            
            Assert.IsNull(image);
            Assert.IsFalse(File.Exists(fileName));
        }
        
        [Test]
        public async Task TestDownloadChampionSquareImage()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string championId = "Aatrox";
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ChampionSquareImageFileExtension}";

            var image = await reader.DownloadChampionLoadingImageAsync(championId, new FileInfo(fileName));
            
            Assert.IsNotNull(image);
            Assert.IsTrue(File.Exists(fileName));
        }

        [Test]
        public async Task TestDownloadChampionSquareImageForInvalidId()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string championId = "NotALeagueChampion";
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ChampionSquareImageFileExtension}";

            var image = await reader.DownloadChampionLoadingImageAsync(championId, new FileInfo(fileName));
            
            Assert.IsNull(image);
            Assert.IsFalse(File.Exists(fileName));
        }
        
        [Test]
        public async Task TestDownloadItemSquareImage()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string itemId = "1001";
            var gameVersion = new MultiComponentVersion(11, 8, 1);
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ItemSquareImageFileExtension}";

            var image = await reader.DownloadItemSquareImageAsync(gameVersion, itemId, new FileInfo(fileName));
            
            Assert.IsNotNull(image);
            Assert.IsTrue(File.Exists(fileName));
        }

        [Test]
        public async Task TestDownloadItemSquareImageForInvalidId()
        {
            var reader = new RemoteLeagueStaticApiReader();
            const string itemId = "NotALeagueItem";
            var fileName = $"{ImageDirectory}/{Guid.NewGuid()}{ILeagueStaticApiReader.ChampionSquareImageFileExtension}";
            var gameVersion = new MultiComponentVersion(11, 8, 1);

            var image = await reader.DownloadItemSquareImageAsync(gameVersion, itemId, new FileInfo(fileName));
            
            Assert.IsNull(image);
            Assert.IsFalse(File.Exists(fileName));
        }
    }
}