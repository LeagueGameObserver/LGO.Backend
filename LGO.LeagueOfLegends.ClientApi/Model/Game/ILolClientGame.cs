using System.Collections.Generic;
using LGO.LeagueOfLegends.ClientApi.Model.ActivePlayer;
using LGO.LeagueOfLegends.ClientApi.Model.Player;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game
{
    public interface ILolClientGame
    {
        ILolClientActivePlayer ActivePlayer { get; }
        
        IEnumerable<ILolClientPlayer> Players { get; }
        
        ILolClientGameEventCollection EventCollection { get; }
        
        ILolClientGameStats Stats { get; }
    }
}