using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeagueBaronNashorRespawnTimer : ILeagueTimer
    {
        LeagueTimerType ILeagueTimer.Type => LeagueTimerType.BaronNashorRespawn;
    }
}