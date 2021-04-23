using LGO.LeagueClient.Model.ActivePlayer;

namespace LGO.LeagueClient.LocalGameReader.Model.ActivePlayer
{
    internal class NullActivePlayer : ILeagueClientActivePlayer
    {
        public string SummonerName => string.Empty;

        private static NullActivePlayer? _instance;

        public static NullActivePlayer Get => _instance ??= new NullActivePlayer();
        
        private NullActivePlayer() { }
    }
}