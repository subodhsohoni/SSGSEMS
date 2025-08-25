using Microsoft.AspNetCore.Mvc;

namespace RestAPIDemo.Controllers
{
    /// <summary>
    /// Controller for providing weather forecast data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // Predefined weather summary descriptions.
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for the controller.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets a collection of weather forecasts for the next five days.
        /// </summary>
        /// <remarks>
        /// Each forecast includes the date, temperature in Celsius, and a summary description.
        /// </remarks>
        /// <returns>
        /// An <see cref="IEnumerable{WeatherForecast}"/> containing five weather forecast entries.
        /// </returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
