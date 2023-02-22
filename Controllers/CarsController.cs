using AspNetCore6.Redis.Demo.Models;
using AspNetCore6.Redis.Demo.Providers;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore6.Redis.Demo.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly ICarProvider _carProvider;
    private readonly IEasyCachingProvider _cachingProvider;
    
    public CarsController(IEasyCachingProviderFactory cachingProviderFactory, ICarProvider carProvider)
    {
        _carProvider = carProvider;
        _cachingProvider = cachingProviderFactory.GetCachingProvider("DefaultRedis");
    }

    [HttpGet]
    public IActionResult Get()
    {
        var inCache = _cachingProvider.Exists("CAR_IN_CACHE");

        if (inCache)
        {
            return Ok(_cachingProvider.Get<Car>("CAR_IN_CACHE").Value);
        }
        else
        {
            var carFromProvider = _carProvider.Get();
            _cachingProvider.TrySet("CAR_IN_CACHE", carFromProvider, TimeSpan.FromMinutes(1));
            return Ok(carFromProvider);
        }
    }
}