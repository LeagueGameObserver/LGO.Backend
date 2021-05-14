using System;
using System.IO;
using System.Linq;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueClient.LocalGameReader.Model.Game;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LGO.LeagueClient.LocalGameReader.Test.Model
{
    [TestFixture]
    public class LeagueClientGameDeserializationTest
    {
        [Test]
        public void TestDeserializeClassicGame()
        {
            var game = ReadGameFromFile("FullClassicGame");

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual("Active Player", game.ActivePlayer.SummonerName);

            Assert.NotNull(game.Stats);
            Assert.AreEqual(LeagueGameModeType.Classic, game.Stats.GameMode);

            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(70, game.EventCollection.Events.Count());
            Assert.AreEqual(LeagueGameResult.Win, (game.EventCollection.Events.Last() as ILeagueClientGameEndedEvent)?.ResultForActivePlayer);

            AssertTurretDestructionOrder(game,
                                         (LeagueTeamType.Red, LeagueTurretTierType.BottomOuter),
                                         (LeagueTeamType.Red, LeagueTurretTierType.MiddleOuter),
                                         (LeagueTeamType.Red, LeagueTurretTierType.TopOuter),
                                         (LeagueTeamType.Red, LeagueTurretTierType.TopInner),
                                         (LeagueTeamType.Red, LeagueTurretTierType.MiddleInner),
                                         (LeagueTeamType.Red, LeagueTurretTierType.BottomInner));
        }

        [Test]
        public void TestDeserializeClassicGameWithBots()
        {
            var game = ReadGameFromFile("FullClassicGameWithBots");

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual("Human Summoner", game.ActivePlayer.SummonerName);

            Assert.NotNull(game.Stats);
            Assert.AreEqual(LeagueGameModeType.Classic, game.Stats.GameMode);

            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(78, game.EventCollection.Events.Count());
            
            AssertTurretDestructionOrder(game,
                                         (LeagueTeamType.Red, LeagueTurretTierType.MiddleOuter),
                                         (LeagueTeamType.Red, LeagueTurretTierType.TopOuter),
                                         (LeagueTeamType.Red, LeagueTurretTierType.BottomOuter),
                                         (LeagueTeamType.Blue, LeagueTurretTierType.MiddleOuter),
                                         (LeagueTeamType.Red, LeagueTurretTierType.TopInner),
                                         (LeagueTeamType.Red, LeagueTurretTierType.TopInhibitor),
                                         (LeagueTeamType.Blue, LeagueTurretTierType.TopOuter),
                                         (LeagueTeamType.Red, LeagueTurretTierType.MiddleInner),
                                         (LeagueTeamType.Red, LeagueTurretTierType.MiddleInhibitor),
                                         (LeagueTeamType.Red, LeagueTurretTierType.NexusBottom),
                                         (LeagueTeamType.Red, LeagueTurretTierType.NexusTop));
        }

        [Test]
        public void TestDeserializeSpectatedClassicGame()
        {
            var game = ReadGameFromFile("FullClassicSpectatedGame");

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual(string.Empty, game.ActivePlayer.SummonerName);

            Assert.NotNull(game.Stats);
            Assert.AreEqual(LeagueGameModeType.Classic, game.Stats.GameMode);

            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(9, game.EventCollection.Events.Count());
            Assert.AreEqual(LeagueGameResult.Win, (game.EventCollection.Events.Last() as ILeagueClientGameEndedEvent)?.ResultForActivePlayer);
            
            AssertTurretDestructionOrder(game,
                                         (LeagueTeamType.Red, LeagueTurretTierType.BottomInner));
        }

        [Test]
        public void TestDeserializeAramGame()
        {
            var game = ReadGameFromFile("FullAramGame");

            Assert.NotNull(game);

            Assert.NotNull(game.ActivePlayer);
            Assert.AreEqual("RedSideSummoner3", game.ActivePlayer.SummonerName);

            Assert.NotNull(game.Stats);
            Assert.AreEqual(LeagueGameModeType.Aram, game.Stats.GameMode);

            Assert.NotNull(game.EventCollection);
            Assert.AreEqual(143, game.EventCollection.Events.Count());
            
            AssertTurretDestructionOrder(game,
                                         (LeagueTeamType.Blue, LeagueTurretTierType.MiddleInner),
                                         (LeagueTeamType.Red, LeagueTurretTierType.MiddleInner),
                                         (LeagueTeamType.Red, LeagueTurretTierType.MiddleInhibitor),
                                         (LeagueTeamType.Blue, LeagueTurretTierType.MiddleInhibitor),
                                         (LeagueTeamType.Red, LeagueTurretTierType.NexusBottom),
                                         (LeagueTeamType.Red, LeagueTurretTierType.NexusTop),
                                         (LeagueTeamType.Blue, LeagueTurretTierType.NexusTop),
                                         (LeagueTeamType.Blue, LeagueTurretTierType.NexusBottom));
        }

        private static ILeagueClientGame ReadGameFromFile(string fileName)
        {
            return LocalLeagueClientGameReader.ReadFromFile(new FileInfo($"Resource/{fileName}.json")).Result;
        }

        private static void AssertTurretDestructionOrder(ILeagueClientGame game, params ValueTuple<LeagueTeamType, LeagueTurretTierType>[] turrets)
        {
            var events = game.EventCollection.Events.Where(e => e is ILeagueClientTurretDestroyedEvent).Cast<ILeagueClientTurretDestroyedEvent>().ToList();

            Assert.AreEqual(turrets.Length, events.Count);

            for (var i = 0; i < turrets.Length; ++i)
            {
                var (team, tier) = turrets[i];
                Assert.AreEqual(team, events[i].Turret.Team);
                Assert.AreEqual(tier, events[i].Turret.Tier);
            }
        }
    }
}