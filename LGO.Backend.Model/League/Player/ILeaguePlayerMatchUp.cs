using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.MatchUp;

namespace LGO.Backend.Model.League.Player
{
    public interface ILeaguePlayerMatchUp : ILeagueMatchUp
    {
        LeaguePositionType Position { get; }
    }
}