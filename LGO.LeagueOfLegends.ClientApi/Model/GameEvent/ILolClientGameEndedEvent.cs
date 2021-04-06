using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientGameEndedEvent : ILolClientGameEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.GameEnded;
        
        LolGameResult ResultForActivePlayer { get; }
    }
}