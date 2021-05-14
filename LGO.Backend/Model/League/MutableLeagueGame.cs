using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;
using LGO.Backend.Model.League.GameEvent;
using LGO.Backend.Model.League.MatchUp;
using LGO.Backend.Model.League.Player;
using LGO.Backend.Model.League.Team;
using LGO.Backend.Model.League.Timer;

namespace LGO.Backend.Model.League
{
    internal sealed class MutableLeagueGame : ILeagueGame
    {
        public Guid Id { get; set; } = Guid.Empty;
        public LeagueGameStateType State { get; set; } = LeagueGameStateType.Undefined;
        public LeagueGameModeType Mode { get; set; } = LeagueGameModeType.Undefined;
        public double? InGameTimeInSeconds { get; set; } = null;
        public IEnumerable<ILeagueTeam>? Teams { get; set; } = null;
        public IEnumerable<ILeaguePlayer>? Players { get; set; } = null;
        public IEnumerable<ILeagueMatchUp>? MatchUps { get; set; } = null;
        public IEnumerable<ILeagueTimer>? Timers { get; set; } = null;
        public IEnumerable<ILeagueGameEvent>? Events { get; set; } = null;
        public IEnumerable<ILeagueGameEvent>? EventsSinceLastUpdate { get; set; } = null;
    }
}