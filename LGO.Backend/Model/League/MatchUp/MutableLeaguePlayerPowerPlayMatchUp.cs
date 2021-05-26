namespace LGO.Backend.Model.League.MatchUp
{
    internal sealed class MutableLeaguePlayerPowerPlayMatchUp : MutableLeaguePlayerMatchUp, ILeaguePlayerPowerPlayMatchUp
    {
        public int GoldDifferenceIncrease { get; set; } = 0;
    }
}