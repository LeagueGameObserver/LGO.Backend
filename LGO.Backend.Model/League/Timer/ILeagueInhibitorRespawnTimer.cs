using LGO.Backend.Core.Model.League.Structure;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    public interface ILeagueInhibitorRespawnTimer : ILeagueTimer
    {
        LeagueTimerType ILeagueTimer.Type => LeagueTimerType.InhibitorRespawn;
        
        ILeagueInhibitor Inhibitor { get; }
    }
}