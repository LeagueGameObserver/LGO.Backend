using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp
{
    public interface ILeaguePlayerMatchUp : ILeagueMatchUp
    {
        LeaguePositionType Position { get; }
    }
}