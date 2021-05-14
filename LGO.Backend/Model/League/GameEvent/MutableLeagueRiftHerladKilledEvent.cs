using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueRiftHerladKilledEvent : MutableLeagueNeutralObjectiveKilledEvent, ILeagueRiftHeraldKilledEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.RiftHeraldKilled;
    }
}