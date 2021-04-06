using System.IO;
using System.Linq;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.LeagueOfLegends.ClientApi.Model.Game.Internal;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LGO.LeagueOfLegends.ClientApiTest.Model
{
    [TestFixture]
    public class LolClientGameTest
    {
        [Test]
        public void TestDeserializeClassicBotGame()
        {
            var game = JsonConvert.DeserializeObject<MutableGame>(
                File.ReadAllText("Resource/FullClassicGameWithBots.json"));

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual("Human Summoner", game.ActivePlayer.SummonerName);
            Assert.AreEqual(5542.0d, game.ActivePlayer.CurrentGold, 1.0d);
            
            Assert.NotNull(game.Stats);
            Assert.AreEqual(LolGameModeType.Classic, game.Stats.GameMode);
            
            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(78, game.EventCollection.Events.Count());
        }
    }
}