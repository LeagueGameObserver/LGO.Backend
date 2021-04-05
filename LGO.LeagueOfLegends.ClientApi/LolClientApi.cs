using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LGO.Backend.Core.Http;
using LGO.LeagueOfLegends.ClientApi.Model.Game;
using LGO.LeagueOfLegends.ClientApi.Model.Game.Internal;

namespace LGO.LeagueOfLegends.ClientApi
{
    public sealed class LolClientApi
    {
        private static TimeSpan RequestTimeout { get; } = TimeSpan.FromMilliseconds(500);

        private const string ClientCertificateThumbprint = "8259aafd8f71a809d2b154dd1cdb492981e448bd";
        private const string Host = "https://127.0.0.1:2999/liveclientdata";

        private static string GameDataUrl { get; } = Host + "/allgamedata";

        private static LolClientApi? _instance;

        public static LolClientApi Get => _instance ??= new LolClientApi();

        private JsonHttpClient Client { get; }

        public LolClientApi(JsonHttpClient client)
        {
            Client = client;
        }

        public LolClientApi()
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

        public async Task<ILolClientGame?> GetGameAsync()
        {
            return await Client.GetAsync<MutableGame>(GameDataUrl);
        }
    }
}