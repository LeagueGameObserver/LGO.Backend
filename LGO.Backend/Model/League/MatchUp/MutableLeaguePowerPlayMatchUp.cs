namespace LGO.Backend.Model.League.MatchUp
{
    internal sealed class MutableLeaguePowerPlayMatchUp : MutableLeagueMatchUp, ILeaguePowerPlayMatchUp
    {
        public int GoldDifferenceIncrease { get; set; } = 0;
    }
}