using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lr15.Net.Services.Caching
{
    public class ApiCacheService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public ApiCacheService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClient = httpClientFactory.CreateClient();
            _cache = cache;
        }

        public async Task<string> GetRatesAsync()
        {
            if (!_cache.TryGetValue("ExchangeRates", out string rates))
            {
                var response = await _httpClient.GetStringAsync("https://api.exchangerate.host/latest");
                rates = response;

                _cache.Set("ExchangeRates", rates, TimeSpan.FromMinutes(10));
            }

            return rates;
        }
    }
}
