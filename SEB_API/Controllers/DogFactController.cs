using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace SEB_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogFactController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpDogFactFactoryService _httpDogFactFactoryService;

        public DogFactController(IMemoryCache memoryCache, IHttpDogFactFactoryService httpDogFactFactoryService)
        {
            _cache = memoryCache;
            _httpDogFactFactoryService = httpDogFactFactoryService;
        }

        [HttpGet]
        public async Task<ActionResult<DogFact>> GetDogFact()
        {
            var factDog = await
                _cache.GetOrCreateAsync(1, entry =>
                    {
                        entry.SlidingExpiration = TimeSpan.FromSeconds(3000);
                        return GetDogFactCache();
                    });
            return factDog;
        }

        [Route("GetDogFact/{number}")]
        [HttpGet]
        public async Task<ActionResult<DogFact>> GetDogFact(int number)
        {
            var factDog = await
                _cache.GetOrCreateAsync(number, entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(3000);
                    return GetDogFactCache(number);
                });
            return Ok(factDog);
        }

        private async Task<DogFact> GetDogFactCache(int number)
        {
            var factDog = await _httpDogFactFactoryService.ExecuteAsync(number);
            return factDog;
        }

        private async Task<DogFact> GetDogFactCache()
        {
            var factDog = await _httpDogFactFactoryService.ExecuteAsync();
            return factDog;
        }
    }
}
