using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp
{
    internal sealed class MutableLeaguePlayerMatchUp : MutableLeagueMatchUp, ILeaguePlayerMatchUp
    {
        public LeaguePositionType Position { get; set; } = LeaguePositionType.Undefined;
    }
}