using LGO.Backend.Model.Event.Enum;
using LGO.Backend.Model.League;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.Event
{
    public interface ILgoLeagueGameStateChangedEvent : ILgoEvent
    {
        LgoEventType ILgoEvent.Type => LgoEventType.LeagueGameStateChanged;
        
        LeagueGameStateType OldGameState { get; }
        
        LeagueGameStateType NewGameState { get; }
        
        ILeagueGame Game { get; }
    }
}