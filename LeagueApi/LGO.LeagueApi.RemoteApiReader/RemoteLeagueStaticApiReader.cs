using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LGO.Backend.Core.Http;
using LGO.Backend.Core.Model;
using LGO.LeagueApi.Model.Static;

namespace LGO.LeagueApi.RemoteApiReader
{
    public sealed class RemoteLeagueStaticApiReader : ILeagueStaticApiReader
    {
        private static TimeSpan RequestTimeout { get; } = TimeSpan.FromSeconds(2);
        private const string VersionsUrl = "https://ddragon.leagueoflegends.com/api/versions.json";

        private static RemoteLeagueStaticApiReader? _instance;

        public static RemoteLeagueStaticApiReader Instance => _instance ??= new RemoteLeagueStaticApiReader();

        private JsonHttpClient Client { get; }

        public RemoteLeagueStaticApiReader(JsonHttpClient client)
        {
            Client = client;
        }

        public RemoteLeagueStaticApiReader()
        {
            Client = new JsonHttpClient(RequestTimeout);
        }

        public async Task<IEnumerable<MultiComponentVersion>?> ReadGameVersionsAsync()
        {
            var versionsAsStrings = await Client.GetAsync<List<string>>(VersionsUrl);
            if (versionsAsStrings == null)
            {
                return null;
            }

            var versions = new List<MultiComponentVersion>();
            foreach (var versionAsString in versionsAsStrings)
            {
                if (MultiComponentVersion.TryParse(versionAsString, out var version))
                {
                    versions.Add(version);
                }
            }

            return versions;
        }

        public Task<string?> ReadStaticResourceUrlAsync(MultiComponentVersion gameVersion)
        {
            var gameVersionAsString = gameVersion.ToString();
            var staticResourceFileExtension = ".tgz";
            if (gameVersionAsString.StartsWith("10.10"))
            {
                staticResourceFileExtension = ".zip";
            }

            return Task.FromResult($"https://ddragon.leagueoflegends.com/cdn/dragontail-{gameVersionAsString}{staticResourceFileExtension}");
        }
    }
}