using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp
{
    internal class MutableLeaguePlayerMatchUp : MutableLeagueMatchUp, ILeaguePlayerMatchUp
    {
        public LeaguePositionType Position { get; set; } = LeaguePositionType.Undefined;
    }
}