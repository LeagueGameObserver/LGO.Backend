namespace LGO.Backend.League.Snapshot
{
    internal sealed record InternalLeagueItemSnapshot
    {
        public int Id { get; init; }
        
        public int Amount { get; init; }
    }
}