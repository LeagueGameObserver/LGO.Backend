using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LGO.Backend.Core.Http
{
    public class JsonHttpClient
    {
        public static TimeSpan DefaultRequestTimeout { get; } = TimeSpan.FromMilliseconds(500);

        private HttpClient Client { get; }

        public JsonHttpClient(TimeSpan requestTimeout, HttpClientHandler? clientHandler = null)
        {
            Client = new HttpClient(clientHandler ?? new HttpClientHandler())
                     {
                         Timeout = requestTimeout,
                         DefaultRequestHeaders =
                         {
                             Accept = {MediaTypeWithQualityHeaderValue.Parse("application/json")},
                             UserAgent = {ProductInfoHeaderValue.Parse("request")},
                         },
                     };
        }

        public async Task<TResultType?> GetAsync<TResultType>(string url)
        {
            try
            {
                var response = await Client.GetAsync(url).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return default!;
                }

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TResultType>(json);
            }
            catch (TaskCanceledException)
            {
                return default!;
            }
        }
    }
}