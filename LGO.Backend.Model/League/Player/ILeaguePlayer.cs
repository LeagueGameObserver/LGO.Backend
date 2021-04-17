using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.Player
{
    public interface ILeaguePlayer : ILeagueGoldOwner
    {
        Guid Id { get; }
        
        string SummonerName { get; }
        
        LeagueTeamType Team { get; }
        
        ILeagueChampion Champion { get; }
        
        IEnumerable<ILeagueItem> Items { get; }
    }
}