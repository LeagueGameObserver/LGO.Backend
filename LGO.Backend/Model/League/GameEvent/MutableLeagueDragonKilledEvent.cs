using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueDragonKilledEvent : MutableLeagueNeutralObjectiveKilledEvent, ILeagueDragonKilledEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.DragonKilled;
        public LeagueDragonType Dragon { get; set; } = LeagueDragonType.Undefined;
    }
}