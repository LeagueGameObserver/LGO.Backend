namespace LGO.Backend.League.Snapshot.MatchUp
{
    internal sealed record InternalLeaguePowerPlayMatchUpSnapshot : InternalLeagueMatchUpSnapshot
    {
        public int GoldDifferenceIncrease { get; init; }
    }
}