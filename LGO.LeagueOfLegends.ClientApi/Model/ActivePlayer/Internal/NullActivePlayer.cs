namespace LGO.LeagueOfLegends.ClientApi.Model.ActivePlayer.Internal
{
    internal class NullActivePlayer : ILolClientActivePlayer
    {
        public string SummonerName => string.Empty;

        private static NullActivePlayer? _instance;

        public static NullActivePlayer Get => _instance ??= new NullActivePlayer();
        
        private NullActivePlayer() { }
    }
}