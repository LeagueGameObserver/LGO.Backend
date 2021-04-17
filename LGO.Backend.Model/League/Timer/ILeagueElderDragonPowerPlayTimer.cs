using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeagueElderDragonPowerPlayTimer : ILeaguePowerPlayTimer
    {
        LeagueTimerType ILeagueTimer.Type => LeagueTimerType.ElderDragonPowerPlay;
    }
}