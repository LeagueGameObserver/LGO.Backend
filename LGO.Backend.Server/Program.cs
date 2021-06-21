using System.Threading.Tasks;
using LGO.LeagueApi.RemoteApiReader.Static;
using LGO.LeagueResource.LocalResourceRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LGO.Backend.Server
{
    public static class Program
    {
        internal static LgoBackend Backend { get; private set; } = null!;

        public static async Task Main(string[] args)
        {
            var resourceRepositoryFactory = new LocalLeagueResourceRepositoryFactory();
            var gameConstantsFactory = new DefaultLeagueGameConstantsFactory();
            var staticApiReader = new RemoteLeagueStaticApiReader();
            Backend = new LgoBackend(resourceRepositoryFactory, gameConstantsFactory, staticApiReader);

            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}