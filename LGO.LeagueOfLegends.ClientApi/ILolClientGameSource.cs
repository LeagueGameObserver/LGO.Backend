using System.Threading.Tasks;
using LGO.LeagueOfLegends.ClientApi.Model.Game;

namespace LGO.LeagueOfLegends.ClientApi
{
    public interface ILolClientGameSource
    {
        Task<ILolClientGame?> GetGameAsync();
    }
}