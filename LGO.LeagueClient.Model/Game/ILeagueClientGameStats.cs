using System;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.Model.Game
{
    public interface ILeagueClientGameStats
    {
        TimeSpan InGameTime { get; }
        
        LeagueGameModeType GameMode { get; }
        
        LeagueMap Map { get; }
    }
}