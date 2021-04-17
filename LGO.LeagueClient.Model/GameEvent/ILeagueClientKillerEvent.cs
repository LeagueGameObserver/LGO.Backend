namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientKillerEvent : ILeagueClientGameEvent
    {
        string KillerName { get; }
    }
}