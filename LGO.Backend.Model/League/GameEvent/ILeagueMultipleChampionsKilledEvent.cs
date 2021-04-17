using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueMultipleChampionsKilledEvent : ILeagueKillerEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.MultipleChampionsKilled;
        
        int NumberOfKills { get; }
    }
}