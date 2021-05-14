using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    internal sealed class MutableLeagueBaronNashorPowerPlayTimer : MutableLeaguePowerPlayTimer, ILeagueBaronNashorRespawnTimer
    {
        public override LeagueTimerType Type => LeagueTimerType.BaronNashorPowerPlay;
    }
}