using System;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game
{
    public interface ILolClientGameStats
    {
        TimeSpan InGameTime { get; }
        
        LolGameModeType GameMode { get; }
    }
}