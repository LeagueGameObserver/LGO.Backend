using System.Threading.Tasks;
using LGO.LeagueClient.Model.Game;

namespace LGO.LeagueClient.Model
{
    public interface ILeagueClientGameReader
    {
        Task<ILeagueClientGame?> ReadGameAsync();
    }
}