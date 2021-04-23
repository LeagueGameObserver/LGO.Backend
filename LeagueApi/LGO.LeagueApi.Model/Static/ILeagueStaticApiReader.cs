using System.Collections.Generic;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;

namespace LGO.LeagueApi.Model.Static
{
    public interface ILeagueStaticApiReader
    {
        Task<IEnumerable<MultiComponentVersion>?> ReadGameVersionsAsync();

        Task<string?> ReadStaticResourceUrlAsync(MultiComponentVersion gameVersion);
    }
}