namespace LGO.Backend.Model.League.MatchUp
{
    public interface ILeaguePowerPlayMatchUp : ILeagueMatchUp
    {
        int GoldDifferenceIncrease { get; }
    }
}