using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueBaronNashorKilledEvent : MutableLeagueNeutralObjectiveKilledEvent, ILeagueBaronNashorKilledEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.BaronNashorKilled;
    }
}