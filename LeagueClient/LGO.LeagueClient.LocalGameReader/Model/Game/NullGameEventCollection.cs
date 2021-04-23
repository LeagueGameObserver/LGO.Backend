using System.Collections.Generic;
using System.Linq;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueClient.Model.GameEvent;

namespace LGO.LeagueClient.LocalGameReader.Model.Game
{
    internal class NullGameEventCollection : ILeagueClientGameEventCollection
    {
        public IEnumerable<ILeagueClientGameEvent> Events => Enumerable.Empty<ILeagueClientGameEvent>();

        private static NullGameEventCollection? _instance;

        public static NullGameEventCollection Get => _instance ??= new NullGameEventCollection();

        private NullGameEventCollection()
        {
        }
    }
}