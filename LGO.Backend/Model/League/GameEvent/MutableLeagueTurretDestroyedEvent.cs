using LGO.Backend.Core.Model.League.Structure;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueTurretDestroyedEvent : MutableLeagueKillerWithAssistersEvent, ILeagueTurretDestroyedEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.TurretDestroyed;
        public ILeagueTurret Turret { get; set; } = NullLeagueTurret.Instance;
    }
}