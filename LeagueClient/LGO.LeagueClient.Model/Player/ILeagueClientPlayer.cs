using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.Model.Player
{
    public interface ILeagueClientPlayer
    {
        string SummonerName { get; }

        string ChampionName { get; }

        bool IsDead { get; }

        TimeSpan RespawnTime { get; }
        
        bool IsBot { get; }

        LeaguePositionType Position { get; }
        
        int Level { get; }

        LeagueTeamType Team { get; }

        IEnumerable<ILeagueClientItem> Items { get; }
        
        ILeagueClientScore Score { get; }
    }
}