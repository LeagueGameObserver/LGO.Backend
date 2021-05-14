using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Core.Model.Converter;
using LGO.LeagueClient.LocalGameReader.Model.ActivePlayer;
using LGO.LeagueClient.LocalGameReader.Model.Player;
using LGO.LeagueClient.Model.ActivePlayer;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueClient.Model.Player;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.Game
{
    internal class MutableLeagueClientGame : ILeagueClientGame
    {
        [JsonProperty("activePlayer")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueClientActivePlayer, MutableLeagueClientActivePlayer>))]
        public ILeagueClientActivePlayer ActivePlayer { get; set; } = NullActivePlayer.Get;

        [JsonProperty("allPlayers", ItemConverterType = typeof(ConcreteConverter<ILeagueClientPlayer, MutableLeagueClientPlayer>))]
        public IEnumerable<ILeagueClientPlayer> Players { get; set; } = Enumerable.Empty<ILeagueClientPlayer>();

        [JsonProperty("events")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueClientGameEventCollection, MutableLeagueClientGameEventCollection>))]
        public ILeagueClientGameEventCollection EventCollection { get; set; } = NullGameEventCollection.Get;

        [JsonProperty("gameData")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueClientGameStats, MutableLeagueClientGameStats>))]
        public ILeagueClientGameStats Stats { get; set; } = NullGameStats.Get;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not ILeagueClientGame other)
            {
                return false;
            }

            var mySummonersSorted = Players.OrderBy(p => p.SummonerName).Select(p => p.SummonerName);
            var otherSummonersSorted = other.Players.OrderBy(p => p.SummonerName).Select(p => p.SummonerName);

            return mySummonersSorted.SequenceEqual(otherSummonersSorted);
        }

        public override int GetHashCode()
        {
            return Players.OrderBy(p => p.SummonerName).Select(p => p.SummonerName).GetHashCode();
        }
    }
}