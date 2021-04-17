namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueKillerEvent : ILeagueGameEvent
    {
        string KillerName { get; }
    }
}