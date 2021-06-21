using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.RemoteApiReader.Static;
using NUnit.Framework;

namespace LGO.LeagueResource.LocalResourceRepository.Test
{
    [TestFixture]
    public class LocalLeagueResourceRepositoryFactoryTest
    {
        private static MultiComponentVersion GameVersion { get; } = new(11, 8, 1);
        private static LeagueLocalizationType LocalizationType => LeagueLocalizationType.EnglishUnitedStates;

        [Test]
        public async Task TestParseStaticApiResponses()
        {
            var apiReader = new RemoteLeagueStaticApiReader();

            var factory = new LocalLeagueResourceRepositoryFactory();
            var repository = await factory.GetOrCreateAsync(apiReader, GameVersion, LocalizationType);
            Assert.IsNotNull(repository);

            var allChampions = await repository.ReadAllChampionsAsync();
            Assert.IsNotNull(allChampions);
            Assert.AreEqual(155, allChampions.Count());

            var aurelionSol = await repository.ReadChampionByKeyAsync(136);
            Assert.IsNotNull(aurelionSol);
            Assert.AreEqual("Aurelion Sol", aurelionSol.Name);

            var allItems = await repository.ReadAllItemsAsync();
            Assert.IsNotNull(allItems);
            Assert.AreEqual(199, allItems.Count());

            var boots = await repository.ReadItemByKeyAsync(1001);
            Assert.IsNotNull(boots);
            Assert.AreEqual("Boots", boots.Name);
            Assert.AreEqual(300, boots.Costs.TotalCosts);
            Assert.AreEqual(300, boots.Costs.RecipeCosts);
        }
    }
}