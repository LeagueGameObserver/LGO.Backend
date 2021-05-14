using System;
using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueItemsChangedEvent : MutableLeagueGameEvent, ILeagueItemsChangedEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.ItemsChanged;
        public string SummonerName { get; set; } = string.Empty;
        public IEnumerable<int> AddedItemIds { get; set; } = Enumerable.Empty<int>();
        public IEnumerable<int> RemovedItemIds { get; set; } = Enumerable.Empty<int>();
    }
}