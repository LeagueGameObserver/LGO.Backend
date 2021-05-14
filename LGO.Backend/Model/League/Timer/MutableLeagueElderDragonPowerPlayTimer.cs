using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    internal sealed class MutableLeagueElderDragonPowerPlayTimer : MutableLeaguePowerPlayTimer, ILeagueElderDragonPowerPlayTimer
    {
        public override LeagueTimerType Type => LeagueTimerType.ElderDragonPowerPlay;
    }
}