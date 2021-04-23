using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using NUnit.Framework;
// ReSharper disable PossibleMultipleEnumeration

namespace LGO.LeagueApi.RemoteApiReader.Test
{
    [TestFixture]
    public class RemoteLeagueStaticApiReaderTest
    {
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
    }
}