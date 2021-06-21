using System.Collections.Generic;
using System.Threading.Tasks;
using LGO.Backend.Model.League;
using Microsoft.AspNetCore.Mvc;

namespace LGO.Backend.Server.Controllers
{
    [Route("api/LeagueGames")]
    [ApiController]
    public class GameSummaryController : ControllerBase
    {
        [HttpGet]
        public Task<IEnumerable<ILeagueGameSummary>> GetAllGameSummaries()
        {
            return Task.FromResult(Program.Backend.GameSummaries);
        }
    }
}