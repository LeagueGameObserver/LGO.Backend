using System.Collections.Generic;
using System.Linq;
using LGO.LeagueOfLegends.ClientApi.Model.GameEvent;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game.Internal
{
    internal class NullGameEventCollection : ILolClientGameEventCollection
    {
        public IEnumerable<ILolClientGameEvent> Events => Enumerable.Empty<ILolClientGameEvent>();

        private static NullGameEventCollection? _instance;

        public static NullGameEventCollection Get => _instance ??= new NullGameEventCollection();
        
        private NullGameEventCollection() { }
    }
}