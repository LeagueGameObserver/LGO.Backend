using LGO.Backend.Model.League;

namespace LGO.Backend.Model
{
    internal sealed class NullLeagueGoldOwner : ILeagueGoldOwner
    {
        private static NullLeagueGoldOwner? _instance;

        public static NullLeagueGoldOwner Instance => _instance ??= new NullLeagueGoldOwner();
        
        public int TotalGoldOwned => 0;
        public int UnspentKills => 0;
        public int UnspentAssists => 0;
        public int UnspentDeaths => 0;
        public int TotalKills => 0;
        public int TotalAssists => 0;
        public int TotalDeaths => 0;

        private NullLeagueGoldOwner()
        {
            
        }
    }
}