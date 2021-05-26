using System;

namespace LGO.Backend.Model.League
{
    internal abstract class MutableLeagueGoldOwner : ILeagueGoldOwner
    {
        public Guid Id { get; set; } = Guid.Empty;
        public int TotalGoldOwned { get; set; } = 0;
        public int UnspentKills { get; set; } = 0;
        public int UnspentAssists { get; set; } = 0;
        public int UnspentDeaths { get; set; } = 0;
        public int TotalKills { get; set; } = 0;
        public int TotalAssists { get; set; } = 0;
        public int TotalDeaths { get; set; } = 0;
    }
}