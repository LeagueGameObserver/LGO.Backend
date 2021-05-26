namespace LGO.Backend.Model.League.MatchUp
{
    internal sealed class MutableLeagueTeamPowerPlayMatchUp : MutableLeagueTeamMatchUp, ILeagueTeamPowerPlayMatchUp
    {
        public int GoldDifferenceIncrease { get; set; } = 0;
    }
}