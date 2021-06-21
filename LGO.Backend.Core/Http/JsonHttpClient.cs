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
            var rawResponse = await GetRawAsync(url);
            if (string.IsNullOrEmpty(rawResponse))
            {
                return default!;
            }

            try
            {
                return JsonConvert.DeserializeObject<TResultType>(rawResponse);
            }
            catch (Exception)
            {
                return default!;
            }
        }

        public async Task<string?> GetRawAsync(string url)
        {
            try
            {
                var response = await Client.GetAsync(url).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return default!;
                }

                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                return null;
            }
        }
    }
}