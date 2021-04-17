using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueDragonKilledEvent : ILeagueKillerEvent, ILeagueAssistersEvent, ILeagueNeutralObjectiveKilledEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.DragonKilled;
        
        LeagueDragonType Dragon { get; }
    }
}