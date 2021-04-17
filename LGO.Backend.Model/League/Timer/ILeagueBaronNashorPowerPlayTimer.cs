using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeagueBaronNashorPowerPlayTimer : ILeaguePowerPlayTimer
    {
        LeagueTimerType ILeagueTimer.Type => LeagueTimerType.BaronNashorPowerPlay;
    }
}