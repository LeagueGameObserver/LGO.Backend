using LGO.Backend.Core.Model.League.Structure;

namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientTurretDestroyedEvent : ILeagueClientKillerEvent, ILeagueClientAssistersEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.TurretDestroyed;
        
        ILeagueTurret Turret { get; }
    }
}