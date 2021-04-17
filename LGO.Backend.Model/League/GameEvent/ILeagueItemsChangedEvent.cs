using System.Collections.Generic;
using LGO.Backend.Model.League.Enum;
using LGO.Backend.Model.League.Player;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueItemsChangedEvent : ILeagueGameEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.ItemsChanged;
        
        ILeaguePlayer Player { get; }
        
        IEnumerable<ILeagueItem> AddedItems { get; }
        
        IEnumerable<ILeagueItem> RemovedItems { get; }
    }
}