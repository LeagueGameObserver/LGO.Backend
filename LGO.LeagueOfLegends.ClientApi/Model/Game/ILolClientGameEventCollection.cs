using System.Collections.Generic;
using LGO.LeagueOfLegends.ClientApi.Model.GameEvent;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game
{
    public interface ILolClientGameEventCollection
    {
        IEnumerable<ILolClientGameEvent> Events { get; }
    }
}