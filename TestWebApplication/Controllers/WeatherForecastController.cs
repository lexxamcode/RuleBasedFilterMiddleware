using Microsoft.AspNetCore.Mvc;

namespace TestWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get(int z, int x, int y)
        {
            logger.LogInformation("GET: {z}, {x}, {y}", z, x, y);
            return Ok(HttpContext.Request.Host.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            logger.LogInformation("POST");
            return await Task.FromResult(Ok());
        }
    }
}
