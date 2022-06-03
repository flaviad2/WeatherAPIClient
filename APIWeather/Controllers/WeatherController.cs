using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Controllers;
using WebApplication2.WeatherRepository;

namespace APIWeather.Controllers
{
    [Route("secondAPI/[controller]")]
    [ApiController]

    public class WeatherAPIController : ControllerBase
    {

        private WeatherController weatherController;

        public WeatherAPIController(WeatherController weatherController)
        {
            this.weatherController = weatherController;
            /* RouteContext context = new RouteContext(ActionContext.HttpContext.Connection);
             context.RouteData = new RouteData();
             context.RouteData.Values.Add("area", moduleContext.ModuleInfo.Name); */
        }

        //get all 
        [HttpGet]
        public IActionResult GetWeathersAPI()
        {

            Console.WriteLine("aici");
            return weatherController.GetWeathers();
            /*
             // List<WeatherEntity> weathers = (List<WeatherEntity>)weatherController.GetWeathers();
              Console.WriteLine(collection[0].ToString());
              if (collection.Count != 0)
                  return Ok(Converter.weatherToResponseList((List < WeatherEntity >) collection));
              else
                  return NoContent(); 
            return NoContent(); */



        }


    }
}
