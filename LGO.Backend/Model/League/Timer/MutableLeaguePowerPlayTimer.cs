using System.Collections.Generic;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.MatchUp;

namespace LGO.Backend.Model.League.Timer
{
    internal abstract class MutableLeaguePowerPlayTimer : MutableLeagueTimer, ILeaguePowerPlayTimer
    {
        public LeagueTeamType Team { get; set; } = LeagueTeamType.Undefined;
        public bool? IsActive { get; set; } = null;
        public IEnumerable<ILeaguePowerPlayMatchUp>? MatchUps { get; set; } = null;
    }
}