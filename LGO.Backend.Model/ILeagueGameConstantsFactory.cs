using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model
{
    public interface ILeagueGameConstantsFactory
    {
        ILeagueGameConstants Of(LeagueMapType map, LeagueGameModeType gameMode, MultiComponentVersion gameVersion);
    }
}