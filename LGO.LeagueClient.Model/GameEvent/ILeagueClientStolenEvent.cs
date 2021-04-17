namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientStolenEvent : ILeagueClientGameEvent
    {
        bool HasBeenStolen { get; }
    }
}