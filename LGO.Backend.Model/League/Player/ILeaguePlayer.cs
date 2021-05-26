using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.Player
{
    public interface ILeaguePlayer : ILeagueGoldOwner
    {
        string? SummonerName { get; }

        LeagueTeamType? Team { get; }

        ILeagueChampion? Champion { get; }

        IEnumerable<ILeagueItem>? Items { get; }
    }
}