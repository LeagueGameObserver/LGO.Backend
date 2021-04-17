using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeagueDragonRespawnTimer : ILeagueTimer
    {
        LeagueTimerType ILeagueTimer.Type => LeagueTimerType.DragonRespawn;
    }
}