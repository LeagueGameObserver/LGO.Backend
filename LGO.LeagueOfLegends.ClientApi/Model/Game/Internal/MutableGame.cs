﻿using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Core.Model.Converter;
using LGO.LeagueOfLegends.ClientApi.Model.ActivePlayer;
using LGO.LeagueOfLegends.ClientApi.Model.ActivePlayer.Internal;
using LGO.LeagueOfLegends.ClientApi.Model.Player;
using LGO.LeagueOfLegends.ClientApi.Model.Player.Internal;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game.Internal
{
    internal class MutableGame : ILolClientGame
    {
        [JsonProperty("activePlayer")]
        [JsonConverter(typeof(ConcreteConverter<MutableActivePlayer>))]
        public ILolClientActivePlayer ActivePlayer { get; set; } = NullActivePlayer.Get;

        [JsonProperty("allPlayers", ItemConverterType = typeof(ConcreteConverter<MutablePlayer>))]
        public IEnumerable<ILolClientPlayer> Players { get; set; } = Enumerable.Empty<ILolClientPlayer>();

        [JsonProperty("events")]
        [JsonConverter(typeof(ConcreteConverter<MutableGameEventCollection>))]
        public ILolClientGameEventCollection EventCollection { get; set; } = NullGameEventCollection.Get;

        [JsonProperty("gameData")]
        [JsonConverter(typeof(ConcreteConverter<MutableGameStats>))]
        public ILolClientGameStats Stats { get; set; } = NullGameStats.Get;
    }
}