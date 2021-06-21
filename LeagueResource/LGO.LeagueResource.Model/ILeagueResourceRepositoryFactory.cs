using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.Model.Static;

namespace LGO.LeagueResource.Model
{
    public interface ILeagueResourceRepositoryFactory
    {
        Task<ILeagueResourceRepository> GetOrCreateAsync(ILeagueStaticApiReader staticApiReader,
                                                         MultiComponentVersion gameVersion,
                                                         LeagueLocalizationType localization = LeagueLocalizationType.EnglishUnitedStates);
    }
}