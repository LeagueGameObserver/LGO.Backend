using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueChampionKilledEvent : ILeagueKillerEvent, ILeagueAssistersEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.ChampionKilled;
        
        string VictimSummonerName { get; }
    }
}