using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.RemoteApiReader.Static;
using LGO.LeagueResource.Model;
using NUnit.Framework;

namespace LGO.LeagueResource.LocalResourceRepository.Test
{
    [TestFixture]
    public class LeagueResourceRepositoryFactoryTest
    {
        private static MultiComponentVersion GameVersion { get; } = new(11, 8, 1);
        private static LeagueLocalization Localization => LeagueLocalization.EnglishUnitedStates;

        [SetUp]
        public void SetUp()
        {
            var rootDir = LeagueResourceRepositoryFactory.GetOrCreateRootDirectory(GameVersion);
            if (Directory.Exists(rootDir.FullName))
            {
                Directory.Delete(rootDir.FullName, true);
            }
        }

        [Test]
        public async Task TestParseStaticApiResponses()
        {
            var apiReader = new RemoteLeagueStaticApiReader();

            var repository = await LeagueResourceRepositoryFactory.LoadOrCreate(apiReader, GameVersion, Localization);
            Assert.IsNotNull(repository);

            var allChampions = await repository.ReadAllChampionsAsync();
            Assert.IsNotNull(allChampions);
            Assert.AreEqual(155, allChampions.Count());

            var aurelionSol = await repository.ReadChampionByKeyAsync(136);
            Assert.IsNotNull(aurelionSol);
            Assert.AreEqual("Aurelion Sol", aurelionSol.Name);

            await AssertImageCanBeRead(aurelionSol.Images.LoadingImage);
            await AssertImageCanBeRead(aurelionSol.Images.SplashImage);
            await AssertImageCanBeRead(aurelionSol.Images.SquareImage);
            
            var allItems = await repository.ReadAllItemsAsync();
            Assert.IsNotNull(allItems);
            Assert.AreEqual(199, allItems.Count());

            var boots = await repository.ReadItemByKeyAsync(1001);
            Assert.IsNotNull(boots);
            Assert.AreEqual("Boots", boots.Name);
            Assert.AreEqual(300, boots.Costs.TotalCosts);
            Assert.AreEqual(300, boots.Costs.RecipeCosts);

            await AssertImageCanBeRead(boots.Images.SquareImage);
        }

        private static async Task AssertImageCanBeRead(IImageReader imageReader)
        {
            var content = await imageReader.ReadContentAsBase64Async();
            Assert.IsNotNull(content);
            Assert.IsNotEmpty(content);
        }
    }
}