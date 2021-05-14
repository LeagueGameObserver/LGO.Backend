using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.League;
using LGO.Backend.Model.League.Enum;
using LGO.Backend.Model.League.GameEvent;
using LGO.LeagueApi.RemoteApiReader.Static;
using LGO.LeagueClient.LocalGameReader;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueResource.LocalResourceRepository;
using NUnit.Framework;

namespace LGO.Backend.Test.League
{
    [TestFixture]
    public class LgoLeagueGameTest
    {
        private const string ClientGame0 = "SummonersRift_Classic_00";
        private const string ClientGame1 = "SummonersRift_Classic_01";
        private const string ClientGame2 = "SummonersRift_Classic_02";
        private static MultiComponentVersion GameVersion { get; } = new(11, 8, 1);

        [Test]
        public async Task TestInitializeNewGame()
        {
            var staticApiReader = new RemoteLeagueStaticApiReader();
            var resourceRepository = await LeagueResourceRepositoryFactory.LoadOrCreate(staticApiReader, GameVersion);
            
            var firstClientGame = await ReadGameFromFile(ClientGame0);
            var gameConstants = new DefaultLeagueGameConstantsFactory().ForMapAndMode(LeagueMapType.SummonersRift, LeagueGameModeType.Classic);
            var game = new LgoLeagueGame(resourceRepository, gameConstants, firstClientGame);
            
            Assert.IsNotNull(game);

            var firstSnapshot = game.CurrentSnapshot;
            
            Assert.AreEqual(LeagueGameModeType.Classic, game.CurrentSnapshot.Mode);
            Assert.AreEqual(LeagueGameStateType.InProgress, game.CurrentSnapshot.State);
            Assert.AreEqual(20, game.CurrentSnapshot.Events.Count());
            Assert.AreEqual(10, game.CurrentSnapshot.Players.Count());
            Assert.AreEqual(2, game.CurrentSnapshot.Timers.Count());
            Assert.AreEqual(2, game.CurrentSnapshot.Teams.Count());

            var secondClientGame = await ReadGameFromFile(ClientGame1);
            game.Update(secondClientGame);

            var secondSnapshot = game.CurrentSnapshot;
            
            Assert.False(ReferenceEquals(firstSnapshot, secondSnapshot));
            Assert.AreEqual(LeagueGameModeType.Classic, game.CurrentSnapshot.Mode);
            Assert.AreEqual(LeagueGameStateType.InProgress, game.CurrentSnapshot.State);
            Assert.AreEqual(21, game.CurrentSnapshot.Events.Count());

            var newEvent = game.CurrentSnapshot.Events.Last();
            Assert.IsInstanceOf<ILeagueItemsChangedEvent>(newEvent);
            Assert.AreEqual("SummonerBlueSide1", (newEvent as ILeagueItemsChangedEvent)?.SummonerName);
            CollectionAssert.AreEqual(new[] {1037}, (newEvent as ILeagueItemsChangedEvent)?.RemovedItemIds);
            CollectionAssert.AreEqual(new[] {6672}, (newEvent as ILeagueItemsChangedEvent)?.AddedItemIds);

            var thirdClientGame = await ReadGameFromFile(ClientGame2);
            game.Update(thirdClientGame);

            var thirdSnapshot = game.CurrentSnapshot;
            
            Assert.False(ReferenceEquals(secondSnapshot, thirdSnapshot));
            Assert.AreEqual(56, game.CurrentSnapshot.Events.Count());
            Assert.AreEqual(2, game.CurrentSnapshot.Timers.Count());
            CollectionAssert.AreEqual(new[] {LeagueTimerType.BaronNashorRespawn, LeagueTimerType.DragonRespawn}, game.CurrentSnapshot.Timers.Select(t => t.Type));
        }
        
        private static async Task<ILeagueClientGame> ReadGameFromFile(string fileName)
        {
            return await LocalLeagueClientGameReader.ReadFromFile(new FileInfo($"Resource/{fileName}.json"));
        }
    }
}