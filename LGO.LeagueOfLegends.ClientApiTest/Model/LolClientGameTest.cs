using System.Collections.Generic;
using System.IO;
using System.Linq;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.LeagueOfLegends.ClientApi.Model.Game.Internal;
using LGO.LeagueOfLegends.ClientApi.Model.GameEvent;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LGO.LeagueOfLegends.ClientApiTest.Model
{
    [TestFixture]
    public class LolClientGameTest
    {
        [Test]
        public void TestDeserializeClassicGame()
        {
            var game = JsonConvert.DeserializeObject<MutableGame>(File.ReadAllText("Resource/FullClassicGame.json"));

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual("Active Player", game.ActivePlayer.SummonerName);
            Assert.AreEqual(3439.0d, game.ActivePlayer.CurrentGold, 1.0d);

            Assert.NotNull(game.Stats);
            Assert.AreEqual(LolGameModeType.Classic, game.Stats.GameMode);

            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(70, game.EventCollection.Events.Count());

            var events = new List<ILolClientGameEvent>(game.EventCollection.Events);

            Assert.AreEqual(LolTurretTierType.BottomOuter, (events[27] as ILolClientTurretDestroyedEvent)?.Turret.Tier);
            Assert.AreEqual(LolTeamType.Red, (events[27] as ILolClientTurretDestroyedEvent)?.Turret.Team);

            Assert.AreEqual(LolGameResult.Win, (game.EventCollection.Events.Last() as ILolClientGameEndedEvent)?.ResultForActivePlayer);
        }

        [Test]
        public void TestDeserializeClassicGameWithBots()
        {
            var game = JsonConvert.DeserializeObject<MutableGame>(File.ReadAllText("Resource/FullClassicGameWithBots.json"));

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual("Human Summoner", game.ActivePlayer.SummonerName);
            Assert.AreEqual(5542.0d, game.ActivePlayer.CurrentGold, 1.0d);

            Assert.NotNull(game.Stats);
            Assert.AreEqual(LolGameModeType.Classic, game.Stats.GameMode);

            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(78, game.EventCollection.Events.Count());
        }

        [Test]
        public void TestDeserializeSpectatedClassicGame()
        {
            var game = JsonConvert.DeserializeObject<MutableGame>(File.ReadAllText("Resource/FullClassicSpectatedGame.json"));

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual(string.Empty, game.ActivePlayer.SummonerName);
            Assert.AreEqual(0.0d, game.ActivePlayer.CurrentGold);

            Assert.NotNull(game.Stats);
            Assert.AreEqual(LolGameModeType.Classic, game.Stats.GameMode);

            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(9, game.EventCollection.Events.Count());
            Assert.AreEqual(LolGameResult.Win, (game.EventCollection.Events.Last() as ILolClientGameEndedEvent)?.ResultForActivePlayer);
        }

        [Test]
        public void TestDeserializeAramGame()
        {
            var game = JsonConvert.DeserializeObject<MutableGame>(File.ReadAllText("Resource/FullAramGame.json"));

            Assert.NotNull(game);
            
            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual("RedSideSummoner3", game.ActivePlayer.SummonerName);
            Assert.AreEqual(163.0d, game.ActivePlayer.CurrentGold, 1.0d);
            
            Assert.NotNull(game.Stats);
            Assert.AreEqual(LolGameModeType.Aram, game.Stats.GameMode);
            
            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(143, game.EventCollection.Events.Count());
        }
    }
}