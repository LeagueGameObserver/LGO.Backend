using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.Player
{
    internal sealed class NullLeaguePlayer : ILeaguePlayer
    {
        private static NullLeaguePlayer? _instance;

        public static NullLeaguePlayer Instance => _instance ??= new NullLeaguePlayer();

        public int TotalGoldOwned  => 0;
        public int UnspentKills  => 0;
        public int UnspentAssists  => 0;
        public int UnspentDeaths  => 0;
        public int TotalKills  => 0;
        public int TotalAssists  => 0;
        public int TotalDeaths  => 0;
        public Guid Id => Guid.Empty;
        public string? SummonerName => null;
        public LeagueTeamType? Team => null;
        public ILeagueChampion? Champion => null;
        public IEnumerable<ILeagueItem>? Items => null;

        private NullLeaguePlayer()
        {
            
        }
    }
}