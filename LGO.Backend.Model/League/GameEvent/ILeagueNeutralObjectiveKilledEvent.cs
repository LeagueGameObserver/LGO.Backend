namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueNeutralObjectiveKilledEvent : ILeagueGameEvent
    {
        bool WasStolen { get; }
    }
}