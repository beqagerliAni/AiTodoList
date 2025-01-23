using System.Text;
using Microsoft.AspNetCore.Mvc;
using Ml_todolist.src.dto;
using Ml_todolist.src.ML;
using Newtonsoft.Json;

namespace Ml_todolist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

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
        [HttpPost("TrainML")]
        public Task<IActionResult> TrainML()
        {
            string response = MLModel.TrainML();
            return Task.FromResult<IActionResult>(Ok(response));
        }
        [HttpPost("Predict")]
        public Task<IActionResult> Predict([FromBody]testDto dto)
        {;
            string response = MLModel.predictDificulty("ძაღლის გასეირნება");
            return Task.FromResult<IActionResult>(Ok(response));
        }
    }

}
