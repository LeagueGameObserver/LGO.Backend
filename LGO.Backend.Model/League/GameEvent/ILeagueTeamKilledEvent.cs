using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueTeamKilledEvent : ILeagueKillerEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.TeamKilled;
    }
}