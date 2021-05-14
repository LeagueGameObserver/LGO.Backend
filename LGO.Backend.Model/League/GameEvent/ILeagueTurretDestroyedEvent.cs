using LGO.Backend.Core.Model.League.Structure;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueTurretDestroyedEvent : ILeagueKillerEvent, ILeagueAssistersEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.TurretDestroyed;
        
        ILeagueTurret Turret { get; }
    }
}