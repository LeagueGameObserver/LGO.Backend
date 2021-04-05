namespace LGO.LeagueOfLegends.ClientApi.Model.Player.Internal
{
    internal class NullScore : ILolClientScore
    {
        public int Kills => 0;
        public int Deaths => 0;
        public int Assists => 0;
        public int MinionKills => 0;
        public double Vision => 0.0d;

        private static NullScore? _instance;

        public static NullScore Get => _instance ??= new NullScore();

        private NullScore() { }
    }
}