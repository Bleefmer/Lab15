using Microsoft.AspNetCore.Mvc;
using Lr15.Net.Services.Caching;
using System.Threading.Tasks;

namespace Lr15.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly ApiCacheService _apiCacheService;

        public ExchangeRatesController(ApiCacheService apiCacheService)
        {
            _apiCacheService = apiCacheService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRates()
        {
            var rates = await _apiCacheService.GetRatesAsync();
            return Ok(rates);
        }
    }
}
