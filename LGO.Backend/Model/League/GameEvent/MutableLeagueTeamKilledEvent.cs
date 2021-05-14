using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueTeamKilledEvent : MutableLeagueGameEvent, ILeagueTeamKilledEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.TeamKilled;
        public string KillerName { get; set; } = string.Empty;
    }
}