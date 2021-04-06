namespace LGO.LeagueOfLegends.ClientApi.Model.ActivePlayer.Internal
{
    internal class NullActivePlayer : ILolClientActivePlayer
    {
        public string SummonerName => string.Empty;

        public double CurrentGold => 0.0d;

        private static NullActivePlayer? _instance;

        public static NullActivePlayer Get => _instance ??= new NullActivePlayer();
        
        private NullActivePlayer() { }
    }
}