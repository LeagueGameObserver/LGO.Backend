using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.RemoteApiReader.Static;
using NUnit.Framework;

namespace LGO.LeagueResource.LocalResourceRepository.Test
{
    [TestFixture]
    public class ResourceRepositoryFromScratchTest
    {
        [Test]
        public async Task TestParseStaticApiResponses()
        {
            var apiReader = new RemoteLeagueStaticApiReader();
            var gameVersion = new MultiComponentVersion(11, 8, 1);
            const LeagueLocalization localization = LeagueLocalization.EnglishUnitedStates;

            var repository = await ResourceRepositoryFromScratch.New(apiReader, gameVersion, localization);
            Assert.IsNotNull(repository);

            var allChampions = await repository.ReadAllChampionsAsync();
            Assert.IsNotNull(allChampions);
            Assert.AreEqual(155, allChampions.Count());

            var allItems = await repository.ReadAllItemsAsync();
            Assert.IsNotNull(allItems);
            Assert.AreEqual(199, allItems.Count());
        }
    }
}