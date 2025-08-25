using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestAPIDemo.Controllers
{
    /// <summary>
    /// Controller for providing weather forecast data and string utilities.
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

        /// <summary>
        /// Reverses the input string.
        /// </summary>
        /// <param name="input">The string to reverse.</param>
        /// <returns>The reversed string.</returns>
        /// <remarks>
        /// Requires authentication via a valid authentication header.
        /// </remarks>
        [Authorize]
        [HttpPost("ReverseString")]
        public ActionResult<string> ReverseString([FromBody] string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return BadRequest("Input string cannot be null or empty.");
            }

            var reversed = new string(input.Reverse().ToArray());
            return Ok(reversed);
        }
    }
}
