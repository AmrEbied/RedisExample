using Microsoft.AspNetCore.Mvc;
using RedisExample.Model;
using RedisExample.Services;

namespace RedisExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ICachingService _cachingService;

        public HomeController(ICachingService cachingService)
        {
            _cachingService = cachingService;
        }

        [HttpGet]
        public IActionResult Get()
        { 
            return Ok(_cachingService.GetData<List<Driver>>("Driver"));
        }
        [HttpPost]
        public IActionResult Post()
        {
            List<Driver> driver =
            [
                new Driver
                {
                    Name = "test",
                    DriveNb = 10,
                    Id = 1
                },
                new Driver
                {
                    Name = "test2",
                    DriveNb = 2,
                    Id = 2
                },
            ];
            _cachingService.SetData("Driver", driver, DateTimeOffset.Now.AddHours(1));
            return Ok();
        }
    }
}
