using System;
using System.Collections.Generic;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    public interface ILeagueItemsChangedEvent : ILeagueGameEvent
    {
        LeagueGameEventType ILeagueGameEvent.Type => LeagueGameEventType.ItemsChanged;

        string SummonerName { get; }

        IEnumerable<int> AddedItemIds { get; }

        IEnumerable<int> RemovedItemIds { get; }
    }
}