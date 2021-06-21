using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.League;
using LGO.Backend.Model;
using LGO.LeagueApi.RemoteApiReader.Static;
using LGO.LeagueClient.LocalGameReader;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueResource.LocalResourceRepository;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LGO.Backend.Test.League
{
    [TestFixture]
    public class InternalLeagueGameReaderTest
    {
        private const string ClientGame0 = "SummonersRift_Classic_00";
        private const string ClientGame1 = "SummonersRift_Classic_01";
        private const string ClientGame2 = "SummonersRift_Classic_02";
        private static MultiComponentVersion GameVersion { get; } = new(11, 8, 1);

        private static ILgoClientSettings ClientSettings { get; } = new MutableClientSettings();

        [Test]
        public async Task TestReadGame()
        {
            var staticApiReader = new RemoteLeagueStaticApiReader();
            var resourceRepository = await new LocalLeagueResourceRepositoryFactory().GetOrCreateAsync(staticApiReader, GameVersion);
            var gameConstants = new DefaultLeagueGameConstantsFactory().Of(LeagueMapType.SummonersRift, LeagueGameModeType.Classic, GameVersion);

            var firstClientGame = await ReadGameFromFile(ClientGame0);
            var game = new InternalLeagueGame(resourceRepository, gameConstants, firstClientGame);
            var gameReader = new InternalLeagueGameReader(game, resourceRepository);

            var firstPublicGame = await gameReader.ReadGameAsync(ClientSettings);
            Assert.IsNotNull(firstPublicGame);

            Assert.IsNotNull(firstPublicGame.Teams);
            Assert.AreEqual(2, firstPublicGame.Teams.Count());
            
            Assert.IsNotNull(firstPublicGame.Players);
            Assert.AreEqual(10, firstPublicGame.Players.Count());
            
            Assert.IsNotNull(firstPublicGame.MatchUps);
            Assert.AreEqual(6, firstPublicGame.MatchUps.Count());
            
            Assert.IsNotNull(firstPublicGame.Timers);
            Assert.AreEqual(2, firstPublicGame.Timers.Count());

            Assert.IsNotNull(firstPublicGame.Events);
            Assert.AreEqual(20, firstPublicGame.Events.Count());
            
            Assert.IsNotNull(firstPublicGame.EventsSinceLastUpdate);
            Assert.AreEqual(20, firstPublicGame.EventsSinceLastUpdate.Count());
        }

        private static async Task<ILeagueClientGame> ReadGameFromFile(string fileName)
        {
            return await LocalLeagueClientGameReader.ReadRawGameSnapshotFromFile(new FileInfo($"Resource/{fileName}.json"));
        }
    }
}