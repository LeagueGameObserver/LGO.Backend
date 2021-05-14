using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend
{
    public interface ILeagueGameConstantsFactory
    {
        ILeagueGameConstants ForMapAndMode(LeagueMapType map, LeagueGameModeType gameMode);
    }
}