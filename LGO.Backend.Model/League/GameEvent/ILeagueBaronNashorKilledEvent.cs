using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueBaronNashorKilledEvent : ILeagueKillerEvent, ILeagueAssistersEvent, ILeagueNeutralObjectiveKilledEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.BaronNashorKilled;
    }
}