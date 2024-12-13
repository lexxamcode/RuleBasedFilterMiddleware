using Microsoft.AspNetCore.Mvc;

namespace TestWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<int> Get(int z, int x, int y)
        {
            logger.LogInformation("GET: {z}, {x}, {y}", z, x, y);
            return [z, x, y];
        }

        [HttpPost("SomePostMethod")]
        public async Task<IActionResult> Post()
        {
            logger.LogInformation("POST");
            return await Task.FromResult(Ok());
        }
    }
}
