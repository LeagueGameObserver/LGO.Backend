using System.Collections.Generic;
using LGO.LeagueClient.Model.ActivePlayer;
using LGO.LeagueClient.Model.Player;

namespace LGO.LeagueClient.Model.Game
{
    public interface ILeagueClientGame
    {
        ILeagueClientActivePlayer ActivePlayer { get; }
        
        IEnumerable<ILeagueClientPlayer> Players { get; }
        
        ILeagueClientGameEventCollection EventCollection { get; }
        
        ILeagueClientGameStats Stats { get; }
    }
}