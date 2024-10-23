using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace IndustryConnect_Week5_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ObjectResult Get()
        {


            var weather = 
             Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            if(weather.Length < 6)
            {
                return new OkObjectResult(weather);
            }
            else
            {
                return BadRequest("Error");
            }
   
        }

        [HttpPost(Name = "PostWeather")]
        public IEnumerable<WeatherForecast> Post(WeatherForecast newWeather)
        {
            var weather =
             Enumerable.Range(1, 5).Select(index => new WeatherForecast
             {
                 Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                 TemperatureC = Random.Shared.Next(-20, 55),
                 Summary = Summaries[Random.Shared.Next(Summaries.Length)]
             })
            .ToList();


            weather.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = newWeather.TemperatureC,
                Summary = newWeather.Summary
            });

            return weather;
        }



    }
}
