using LGO.Backend.Core.Model.LeagueOfLegends.Structure;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientTurretDestroyedEvent : ILolClientKillerEvent, ILolClientAssistersEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.TurretDestroyed;
        
        ILolTurret Turret { get; }
    }
}