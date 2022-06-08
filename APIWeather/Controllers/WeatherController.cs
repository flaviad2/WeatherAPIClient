using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using APIWeather.Models;


namespace APIWeather.Controllers
{
    
    [ApiController]
    [Route("secondAPI/[controller]")]
    public class WeatherAPIController : ControllerBase
    {

      

      
        public WeatherAPIController()
        {
            
        }



       
        /*
         * Ia toate prognozele din BD. 
        // */
        //[HttpGet("GetAll")]
        //public async Task<IActionResult<List<WeatherResponse>> GetAll()
        //{
        //    var httpclient = new HttpClient();
        //    var response= await httpclient.GetAsync("https://localhost:44391/api/Weather");
        //    var responseString = await response.Content.ReadAsAsync<List<WeatherResponseW2>>();
        //    httpclient.Dispose();

        //    if (response.IsSuccessStatusCode)
        //    {

        //        var json = JToken.TryParse<List<WeatherResponseW2>>(responseString).ToString();
        //        //mapare de obiecte deserializarte la modele de raspuns din serviciu
                
        //        return Ok(json);//return obiect de raspuns nou
        //    }
        //    else return NoContent();
        //}

        



        /*
         * Ia toate prognozele dintre doua date 
         */ 
        [HttpGet("GetTwoDates")]
        public async Task<IActionResult> GetTwoDates(DateTime date1, DateTime date2)
        {
          var httpclient = new HttpClient();
          String date1F = date1.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
          String date2F = date2.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();

            var response = await httpclient.GetAsync("https://localhost:44391/api/Weather/" + date1F + "/" + date2F);
            var responseString = await response.Content.ReadAsStringAsync();

            
          httpclient.Dispose();

           if (response.IsSuccessStatusCode)
            {
                var json = JToken.Parse(responseString).ToString();
                return Ok(json);
            }
          else return NoContent();
           
        }




        //returneaza prognoza/prognozele din aceasta zi, indiferent de sursa
        //  2010-11-11T00:00:00 --> apel
        // https://localhost:44391/api/Weather/forDay/2010-11-11T00%3A00%3A00 --> creat
        [HttpGet("GetForecastForDay")]
        public async Task<IActionResult> GetForecastForDay(DateTime date)
        {

            var httpclient = new HttpClient();
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();

            var response = await httpclient.GetAsync("https://localhost:44391/api/Weather/forDay/" + dateF); 
            var responseString = await response.Content.ReadAsStringAsync();
            httpclient.Dispose();


            if (response.IsSuccessStatusCode)
            {
                var json = JToken.Parse(responseString).ToString();
                return Ok(json);
            }
            else return NoContent();

        }

        



        /**
         * Sterge prognozele daca sunt in interval de 30 de zile. Daca nu, eroare custom.
         */
        [HttpDelete("Delete30Days")]
        public async Task<IActionResult> Delete30Days(DateTime date)
        {

            var httpclient = new HttpClient();
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
            var response = await httpclient.DeleteAsync("https://localhost:44391/api/Weather/30days/" + dateF);
           


         
            if (response.IsSuccessStatusCode)
            {
                return Ok("Successfully deleted!");
            }
            else if(response.StatusCode== System.Net.HttpStatusCode.NotFound)
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
            
            var httpclient = new HttpClient();

            WeatherEntity w = Converter.RequestToWeather(weatherRequest);
            var jsonString = JsonConvert.SerializeObject(new { id = w.Id, date = w.Date, 
                time = w.Time, minimumTemperature = w.MinimumTemperature,
                maximumTemperature = w.MaximumTemperature,
                precipitationsProbality = w.PrecipitationsProbability,
                atmosphericFenomens = w.AtmosphericFenomens,
                otherInformation = w.OtherInformation, 
                dataSource = w.DataSource
            }) ;
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
            var response = await httpclient.PutAsync("https://localhost:44391/api/Weather/for_day/" + dateF + "/" + source.ToString(), httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
               
            httpclient.Dispose();
            if (response.IsSuccessStatusCode)
            {
                var json = JToken.Parse(responseString).ToString();
                return Ok(json);
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

            var httpclient = new HttpClient();

            WeatherEntity w = Converter.RequestToWeather(weatherRequest);
            var jsonString = JsonConvert.SerializeObject(new
            {
                
                date = w.Date,
                time = w.Time,
                minimumTemperature = w.MinimumTemperature,
                maximumTemperature = w.MaximumTemperature,
                precipitationsProbality = w.PrecipitationsProbability,
                atmosphericFenomens = w.AtmosphericFenomens,
                otherInformation = w.OtherInformation,
                dataSource = w.DataSource
            }) ;

            
            var httpContentWeather = new StringContent(jsonString, Encoding.UTF8, "application/json");
            String dateF = date.ToString("yyyy-MM-ddTHH:mm:ss").ToString().Replace(":", "%3A").ToString();
            var response = await httpclient.PostAsync("https://localhost:44391/api/Weather/" + dateF.ToString(), httpContentWeather);
            var responseString = await response.Content.ReadAsStringAsync();
            httpclient.Dispose();
           

            if (response.IsSuccessStatusCode)
            {
                var json = JToken.Parse(responseString).ToString();
                return Ok(json);
            }
            return BadRequest($"No weather forecast has been added because the date is invalid. Please enter a valid date for the forecast! ");




        }
    }
}
