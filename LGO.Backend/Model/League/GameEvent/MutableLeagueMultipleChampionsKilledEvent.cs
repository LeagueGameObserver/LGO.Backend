using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueMultipleChampionsKilledEvent : MutableLeagueGameEvent, ILeagueMultipleChampionsKilledEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.MultipleChampionsKilled;
        public int NumberOfKills { get; set; } = 0;
        public string KillerName { get; set; } = string.Empty;
    }
}