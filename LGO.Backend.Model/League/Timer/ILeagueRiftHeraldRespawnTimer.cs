using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeagueRiftHeraldRespawnTimer : ILeagueTimer
    {
        LeagueTimerType ILeagueTimer.Type => LeagueTimerType.RiftHeraldRespawn;
    }
}