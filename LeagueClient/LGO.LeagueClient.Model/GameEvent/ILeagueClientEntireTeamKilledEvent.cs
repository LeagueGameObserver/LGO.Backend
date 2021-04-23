using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientEntireTeamKilledEvent : ILeagueClientGameEvent
    {
        LeagueClientGameEventType ILeagueClientGameEvent.Type => LeagueClientGameEventType.EntireTeamKilled;

        string KillerSummonerName { get; }
        
        LeagueTeamType KillerTeam { get; }
    }
}