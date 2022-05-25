using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.WeatherData;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //5 metode
        private IWeatherData _weatherData; 
        public WeatherController(IWeatherData weather)
        {
            _weatherData = weather; 

        }
        [HttpGet]
        
        public IActionResult GetWeathers()
        {
            return Ok(_weatherData.getWeathers());

        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetWeather(int id)
        {
            var weather = _weatherData.GetWeather(id);
            if (weather != null)
            {
                return Ok(_weatherData.GetWeather(id));
            }
            return NotFound("The weather with id : {id} was not found"); 
         }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostWeather(Weather weather)
        {
            _weatherData.AddWeather(weather);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + weather.Id, weather); 

        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult EditWeather( int id, Weather weather)
        {
          /*
            var existingWeather = _weatherData.GetWeather(id);
            if(existingWeather != null )
            {
                weather.Id = existingWeather.Id;
                _weatherData.EditWeather(weather); 
            }
            return Ok(weather);  

            */

            _weatherData.EditWeather(weather);
            return Ok(weather);

        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteWeather(int id)
        {
            var weather = _weatherData.GetWeather(id); 
            if(weather != null)
            {
                _weatherData.DeleteWeather(weather.Id);
                return Ok();
            }
            return NotFound($"Weather with Id: {id} was not found"); 
        }



    }
}
