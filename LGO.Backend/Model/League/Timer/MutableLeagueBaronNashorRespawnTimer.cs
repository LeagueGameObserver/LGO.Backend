using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    internal sealed class MutableLeagueBaronNashorRespawnTimer : MutableLeagueTimer, ILeagueBaronNashorRespawnTimer
    {
        public override LeagueTimerType Type => LeagueTimerType.BaronNashorRespawn;
    }
}