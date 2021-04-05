using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientEntireTeamKilledEvent : ILolClientGameEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.EntireTeamKilled;

        string KillerSummonerName { get; }
        
        LolTeamType KillerTeam { get; }
    }
}