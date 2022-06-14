using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using APIWeather.Models;
using Newtonsoft.Json.Schema;
using System.Text.Json;
using static Microsoft.Extensions.Configuration.IConfiguration; 
namespace APIWeather.Controllers
{

    [ApiController]
    [Route("secondAPI/[controller]")]
    public class WeatherAPIController : ControllerBase
    {




        private readonly IConfiguration _config;

        private String weatherUrl;
        public WeatherAPIController(IConfiguration config)
        {
            _config = config;
            weatherUrl = _config.GetValue<String>("WeatherUrl");
        }






        /*
         * Ia toate prognozele din BD. 
        // */
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<WeatherResponse>>> GetAll()
        {

            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync(weatherUrl);
            var responseW2 = await response.Content.ReadAsAsync<List<WeatherResponseW2>>();
            httpclient.Dispose();

            //responseW2 = raspuns de la primul controller

            if (response.IsSuccessStatusCode)
            {

                //responseW2 --> responseEntity --> response (=raspuns pt client) 

                List<Weather> listOfWeathers = Converter.ListW2ToListEntity(responseW2);

                List<WeatherResponse>? weathersResponse = Converter.WeatherToResponseList(listOfWeathers);

                return Ok(weathersResponse);


            }
            else
            {

                return NoContent();
            }
        }





        /*
         * Ia toate prognozele dintre doua date 
         */
        [HttpGet("GetTwoDates")]
        public async Task<ActionResult<List<WeatherResponse>>> GetTwoDates(DateTime date1, DateTime date2)
        {
            var httpclient = new HttpClient();
            String date1F = date1.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
            String date2F = date2.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();

            var response = await httpclient.GetAsync(weatherUrl + "/" + date1F + "/" + date2F);
            var responseW2 = await response.Content.ReadAsAsync<List<WeatherResponseW2>>();

            //responseW2 = raspuns de la primul controller

            httpclient.Dispose();



            if (response.IsSuccessStatusCode)
            {


                List<Weather> listOfWeathers = Converter.ListW2ToListEntity(responseW2);

                List<WeatherResponse>? weathersResponse = Converter.WeatherToResponseList(listOfWeathers);

                //responseW2 --> responseEntity --> response (=raspuns pt client) 

                return Ok(weathersResponse);


            }
            else
            {

                return NoContent();
            }
        }




        //returneaza prognoza/prognozele din aceasta zi, indiferent de sursa
        //  2010-11-11T00:00:00 --> apel
        // https://localhost:44391/api/Weather/forDay/2010-11-11T00%3A00%3A00 --> creat


        [HttpGet("GetForecastForDay")]
        public async Task<ActionResult<List<WeatherResponse>>> GetForecastForDay(DateTime date)
        {

            var httpclient = new HttpClient();
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();

            var response = await httpclient.GetAsync(weatherUrl + "/forDay/" + dateF);
            var responseW2 = await response.Content.ReadAsAsync<List<WeatherResponseW2>>();
            httpclient.Dispose();


            //responseW2 = raspuns de la primul controller

            if (response.IsSuccessStatusCode && responseW2 != null)
            {


                List<Weather> listOfWeathers = Converter.ListW2ToListEntity(responseW2);

                List<WeatherResponse>? weathersResponse = Converter.WeatherToResponseList(listOfWeathers);

                //responseW2 --> responseEntity --> response (=raspuns pt client) 


                return Ok(weathersResponse);


            }
            else
            {

                return NoContent();
            }
        }



        /**
         * Sterge prognozele daca sunt in interval de 30 de zile. Daca nu, eroare custom.
         */
        [HttpDelete("Delete30Days")]
        public async Task<IActionResult> Delete30Days(DateTime date)
        {

            var httpclient = new HttpClient();
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
            var response = await httpclient.DeleteAsync(weatherUrl + "/30days/" + dateF);



            if (response.IsSuccessStatusCode)
            {
                return Ok("Successfully deleted!");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound($"No weather forecast has been deleted because there aren't weather forecasts from this date. ");

            }
            else return BadRequest($"Wrong calendar date! ");



        }





        //update
        //editeaza prognoza pentru ziua data si sursa data
        //obiectul editat il pun in header
        //data ca parametru si nu trebuie pusa neaparat in obiect la apel

        [HttpPut("UpdateForDaySource")]
        public async Task<IActionResult> UpdateForDaySource(DateTime date, SourceEnum source, WeatherRequest weatherRequest)
        {
            //weatherRequest = request de la client 

            var httpclient = new HttpClient();

            //weatherRequest --> weatherRequestW2 (request cu care apelez metoda din primul controller) 

            WeatherRequestW2 w = Converter.ToWeatherRequestW2(weatherRequest);


            //json pentru weatherRequestW2 

            var jsonString = JsonConvert.SerializeObject(new
            {
                id = w.Id,
                date = w.Date,
                time = w.Time,
                minimumTemperature = w.MinimumTemperature,
                maximumTemperature = w.MaximumTemperature,
                precipitationsProbality = w.PrecipitationsProbability,
                atmosphericFenomens = w.AtmosphericFenomens,
                otherInformation = w.OtherInformation,
                dataSource = w.DataSource
            });

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
            var response = await httpclient.PutAsync(weatherUrl + "/for_day/" + dateF + "/" + source.ToString(), httpContent);
            var responseW2 = await response.Content.ReadAsAsync<WeatherResponseW2>();

            //responseW2 = raspuns de la primul controller


            httpclient.Dispose();
            if (response.IsSuccessStatusCode)
            {
               
                Weather weather = Converter.ToWeatherEntity(responseW2);
                WeatherResponse weatherResponse = Converter.WeatherToResponseElem(weather);

                //responseW2 --> responseEntity --> response (=raspuns pt client) 


                return Ok(weatherResponse);
            }
            else return NotFound($"Weather for day {date} and source {source} was not found");



        }




        //adauga prognoza pt o data

        //pt data 2013-11-11T00:00:00 nu se adauga 
        //pt data 2023-11-11T00:00:00 se adauga 

        /* nu mai pun id si data
         * 
         *   {
    "time": "13:10:11",
    "minimumTemperature": 10,
    "maximumTemperature": 20,
    "precipitationsProbability": 50,
    "atmosphericFenomens": true,
    "otherInformation": "Alte info",
    "dataSource": 3
  }
         */
        [HttpPost("AddForDay")]
        public async Task<IActionResult> AddForDay(DateTime date, WeatherRequest weatherRequest)
        {
            //weatherRequest = request de la client 

            var httpclient = new HttpClient();

            //weatherRequest --> weatherRequestW2 (request cu care apelez metoda din primul controller) 


            WeatherRequestW2 w = Converter.ToWeatherRequestW2(weatherRequest);

            var jsonString = JsonConvert.SerializeObject(new
            {
                id = w.Id,
                date = w.Date,
                time = w.Time,
                minimumTemperature = w.MinimumTemperature,
                maximumTemperature = w.MaximumTemperature,
                precipitationsProbality = w.PrecipitationsProbability,
                atmosphericFenomens = w.AtmosphericFenomens,
                otherInformation = w.OtherInformation,
                dataSource = w.DataSource
            });


            var httpContentWeather = new StringContent(jsonString, Encoding.UTF8, "application/json");
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
            var response = await httpclient.PostAsync(weatherUrl + "/" + dateF.ToString(), httpContentWeather);
            var responseW2 = await response.Content.ReadAsAsync<WeatherResponseW2>();

            //responseW2 = raspuns de la primul controller

            httpclient.Dispose();
            if (response.IsSuccessStatusCode)
            {

                Weather weather = Converter.ToWeatherEntity(responseW2);
                WeatherResponse weatherResponse = Converter.WeatherToResponseElem(weather);

                //responseW2 --> responseEntity --> response (=raspuns pt client) 


                return Ok(weatherResponse);
            }
            return BadRequest($"No weather forecast has been added because the date is invalid. Please enter a valid date for the forecast! ");




        }


    }
}
