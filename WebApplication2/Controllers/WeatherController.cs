using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using WebApplication2.Models;
using WebApplication2.WeatherData;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace WebApplication2.Controllers
{
   // [Route("api/[controller]")]
    [Route("")]
    [ApiController]
   
    public class WeatherController : ControllerBase
    {
        private IWeatherData _weatherData; 
        public WeatherController(IWeatherData weather)
        {
            _weatherData = weather; 

        }


        //get all 
        [HttpGet]
        [Route("api/[controller]")]
        
        public IActionResult GetWeathers()
        {
            if (_weatherData.getWeathers().Count != 0)
                return Ok(_weatherData.getWeathers());
            else 
                return NoContent();

        }

       

        //get one dupa id
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetWeather(int id)
        {
            var weather = _weatherData.GetWeather(id);
            if (weather != null)
            {
                return Ok(_weatherData.GetWeather(id));
            }
            return NotFound($"The weather with id : {id} was not found"); 
         }


        //add
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostWeather(Weather weather)
        {
            _weatherData.AddWeather(weather);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + weather.Id, weather); 

        }

        //update
        [HttpPut]
        [Route("api/[controller]/{id}")]
        public IActionResult EditWeather( int id, Weather weather)
        {
          

            if(_weatherData.EditWeather(id,weather)!=null)
                 return Ok(weather);
            else
                return NotFound($"The weather with id : {id} was not found");

        }


        //delete
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



        //apel cu : 2008-11-11T00:00:00 
        //          2010-11-11T00:00:00
        //returneaza array cu toate prognozele din acest interval de timp 
        //daca nu gaseste returneaza array vid
        [HttpGet]
        [Route("api/[controller]/{date1}/{date2}")]
        public IActionResult GetWeathersBetweenDates(DateTime date1, DateTime date2)
        {
            if (date1 > date2)
                return BadRequest(); 
            List<Weather> weatherResult = _weatherData.GetWeathersBetweenDates(date1, date2); 
            if(weatherResult.Count!=0)
            {   
                return Ok(weatherResult); 
            }
            return NotFound($"No weather forecasts between dates {date1} and {date2} were found.");
            

        }


        //apel cu : 2008-11-11T00:00:00 
        //returneaza prognoza/prognozele din aceasta zi, indiferent de sursa
        [HttpGet]
        [Route("api/[controller]/forDay/{date_day}")]

        public IActionResult GetWeathersFromDate(DateTime date_day)
        {
            List<Weather> weathers = _weatherData.GetWeathersFromDay(date_day); 
            if(weathers.Count!=0)
            {
                return Ok(weathers);
            }
            return NoContent();

        }

        //apel cu : 2008-11-11T00:00:00 
        //sterge toate prognozele care sunt indepartate cu mai mult de 30 de zile de ziua data ca param
        [HttpDelete]
        [Route("api/[controller]/delete_range_30_days/{today}")]
        public IActionResult DeleteWeatherTooFar(DateTime today)
        {
            List<Weather> forDeletion = _weatherData.GetWeathersNotTooFar(today);
            if (forDeletion.Count > 0)
            {
                for (int i = 0; i < forDeletion.Count; i++)
                {
                    _weatherData.DeleteWeather(forDeletion[i].Id);

                }
                return Ok();

            }
            return NotFound($"No weather forecast has been deleted. ");

        }

        //update
        //editeaza prognoza pentru ziua data si sursa data
        //obiectul editat il pun in header
        //data ca parametru si nu trebuie pusa neaparat in obiect 

        [HttpPut]
        [Route("api/[controller]/update_weather_for_day_and_source/{date}")]
        public IActionResult EditWeather(DateTime date, Source source, Weather weather)
        {
          
            weather.DataSource= source;
            weather.Date = date;
            if (_weatherData.EditWeatherFromDayFromSource(date, source, weather) != null)
                return Ok(weather);
            else return NotFound($"Weather for day {date} and source {source} was not found");
          

        }

        //add
        //adauga o prognoza meteo noua cu datele date in header, dar data se da separat, fiind prognoza pentru ziua respectiva

        /*
         * apel cu header fara data si data o pun doar la param de intare la URI
         * date: 2022-11-11T00:00:00
    
    {"time": "14:00:11",
    "minimumTemperature": -16,
    "maximumTemperature": 30,
    "precipitationsProbability": 80,
    "atmosphericFenomens": false,
    "otherInformation": "Informatii suplimentare",
    "dataSource": 3 }    


   */
        [HttpPost]
        [Route("api/[controller]/{date}")]

        public IActionResult PostWeatherWithDate(DateTime date, Weather weather)
        {
            try
            {
                weather.Date = date; 
                _weatherData.AddWeatherWithDate(date, weather);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + weather.Id, weather);
            }
            catch(Exception e)
            {
                return BadRequest($"No weather forecast has been added because the date is invalid. Please enter a valid date for the forecast! ");
            }
        }
    }
}
