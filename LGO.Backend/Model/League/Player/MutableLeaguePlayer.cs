using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.Player
{
    internal sealed class MutableLeaguePlayer : MutableLeagueGoldOwner, ILeaguePlayer
    {
        public string? SummonerName { get; set; } = null;
        public bool? IsActivePlayer { get; set; } = null;
        public LeagueTeamType? Team { get; set; } = null;
        public ILeagueChampion? Champion { get; set; } = null;
        public IEnumerable<ILeagueItem>? Items { get; set; } = null;
    }
}