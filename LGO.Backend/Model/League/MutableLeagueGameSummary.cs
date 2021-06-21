using System;
using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League
{
    internal sealed class MutableLeagueGameSummary : ILeagueGameSummary
    {
        public Guid Id { get; set; } = Guid.Empty;
        public LeagueGameStateType State { get; set; } = LeagueGameStateType.Undefined;
        public LeagueGameModeType Mode { get; set; } = LeagueGameModeType.Undefined;
        public double InGameTimeInSeconds { get; set; }
        public IEnumerable<string> SummonerNamesBlueTeam { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> SummonerNamesRedSide { get; set; } = Enumerable.Empty<string>();
    }
}