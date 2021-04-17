using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeagueTimer
    {
        LeagueTimerType Type { get; }
        
        double RemainingTimeInSeconds { get; }
        
        double InGameStartTimeInSeconds { get; }
        
        double InGameEndTimeInSeconds { get; }
    }
}