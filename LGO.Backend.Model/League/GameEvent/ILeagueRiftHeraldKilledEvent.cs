using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueRiftHeraldKilledEvent : ILeagueKillerEvent, ILeagueAssistersEvent, ILeagueNeutralObjectiveKilledEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.RiftHeraldKilled;
    }
}