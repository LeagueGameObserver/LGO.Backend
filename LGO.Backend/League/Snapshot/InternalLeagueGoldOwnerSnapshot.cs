using LGO.Backend.Model.League;

namespace LGO.Backend.League.Snapshot
{
    internal abstract record InternalLeagueGoldOwnerSnapshot : ILeagueGoldOwner
    {
        public int TotalGoldOwned { get; init; }
        public int UnspentKills { get; init; }
        public int UnspentAssists { get; init; }
        public int UnspentDeaths { get; init; }
        public int TotalKills { get; init; }
        public int TotalAssists { get; init; }
        public int TotalDeaths { get; init; }
    }
}