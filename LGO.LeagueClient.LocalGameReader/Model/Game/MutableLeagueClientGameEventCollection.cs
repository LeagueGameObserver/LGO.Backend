using System.Collections.Generic;
using System.Linq;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.Game
{
    internal class MutableLeagueClientGameEventCollection : ILeagueClientGameEventCollection
    {
        [JsonProperty("Events", ItemConverterType = typeof(GameEventConverter))]
        public IEnumerable<ILeagueClientGameEvent> Events { get; set; } = Enumerable.Empty<ILeagueClientGameEvent>();
        
        public static ILeagueClientGameEventCollection Null => NullGameEventCollection.Get;
    }
}