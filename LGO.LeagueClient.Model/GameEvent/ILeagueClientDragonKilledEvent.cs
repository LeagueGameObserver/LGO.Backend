using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientDragonKilledEvent : ILeagueClientNeutralObjectiveKilledEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.DragonKilled;
        
        LeagueDragonType DragonType { get; }
    }
}