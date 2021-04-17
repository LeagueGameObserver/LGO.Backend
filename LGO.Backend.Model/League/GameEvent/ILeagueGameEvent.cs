using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueGameEvent
    {
        LeagueGameEventType Type { get; }
        
        double InGameTimeInSeconds { get; }
    }
}