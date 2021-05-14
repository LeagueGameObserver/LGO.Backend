using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LGO.Backend.Core.Http;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.LocalGameReader.Model.Game;
using LGO.LeagueClient.LocalGameReader.Model.GameEvent;
using LGO.LeagueClient.Model;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader
{
    public sealed class LocalLeagueClientGameReader : ILeagueClientGameReader
    {
        private static TimeSpan RequestTimeout { get; } = TimeSpan.FromMilliseconds(500);

        private const string ClientCertificateThumbprint = "8259aafd8f71a809d2b154dd1cdb492981e448bd";
        private const string Host = "https://127.0.0.1:2999/liveclientdata";

        private static string GameDataUrl => Host + "/allgamedata";

        private static LocalLeagueClientGameReader? _instance;

        public static LocalLeagueClientGameReader Instance => _instance ??= new LocalLeagueClientGameReader();

        private JsonHttpClient Client { get; }

        public LocalLeagueClientGameReader(JsonHttpClient client)
        {
            Client = client;
        }

        public LocalLeagueClientGameReader()
        {
            Client = new JsonHttpClient(RequestTimeout, new HttpClientHandler
                                                        {
                                                            ServerCertificateCustomValidationCallback = ValidateServerCertificate,
                                                        });
        }

        private static bool ValidateServerCertificate(HttpRequestMessage message, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
            {
                return true;
            }

            if (certificate?.Thumbprint.Equals(ClientCertificateThumbprint, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }

            return false;
        }

        public async Task<ILeagueClientGame?> ReadGameAsync()
        {
            return ParseTurrets(await Client.GetAsync<MutableLeagueClientGame>(GameDataUrl));
        }

        public static async Task<ILeagueClientGame?> ReadFromFile(FileInfo file)
        {
            var fileContent = await File.ReadAllTextAsync(file.FullName, Encoding.UTF8);
            return ParseTurrets(JsonConvert.DeserializeObject<MutableLeagueClientGame>(fileContent));
        }

        private static ILeagueClientGame? ParseTurrets(ILeagueClientGame? game)
        {
            if (game == null)
            {
                return null;
            }

            foreach (var turretDestroyedEvent in game.EventCollection.Events.Where(e => e is MutableLeagueClientTurretDestroyedEvent).Cast<MutableLeagueClientTurretDestroyedEvent>())
            {
                turretDestroyedEvent.Turret = TurretConverter.CreateTurret(turretDestroyedEvent.TurretLeagueClientString, game.Stats.Map);
            }

            return game;
        }
    }
}