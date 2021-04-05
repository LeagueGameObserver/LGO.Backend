using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Player
{
    public interface ILolClientPlayer
    {
        string SummonerName { get; }

        string ChampionName { get; }

        bool IsDead { get; }

        TimeSpan RespawnTime { get; }

        LolPositionType Position { get; }

        LolTeamType Team { get; }

        IEnumerable<ILolClientItem> Items { get; }
        
        ILolClientScore Score { get; }
    }
}