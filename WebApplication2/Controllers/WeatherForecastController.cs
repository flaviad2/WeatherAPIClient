using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication2.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		
		public static List<string> Summaries = new List<string>(new string[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" });
		//public static List<string> Summaries = new List<string>();

		public List<WeatherForecast> all = new List<WeatherForecast>();

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}
		
		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{


			int size = Summaries.Count;
			Console.WriteLine(size);
			List<WeatherForecast> forecasts = new List<WeatherForecast>();


			for (int index = 0; index < size; index++)
			{
				WeatherForecast wF = new WeatherForecast();
				wF.id = index + 1;
				wF.Date = DateTime.Now.AddDays(index);
				wF.Summary = Summaries[index].ToString();
				wF.TemperatureC = Random.Shared.Next(-20, 55);
				forecasts.Add(wF);


			}

			if(forecasts.Count==0)
				base.Response.StatusCode = (int)HttpStatusCode.NoContent;

			return forecasts;


		}
		/*
		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{

			return all;


		}*/


		[HttpPost(Name = "SaveWeatherForecast")]
		public WeatherForecast Save(WeatherForecast weatherForecast)
		{
			Summaries.Add(weatherForecast.Summary.ToString());
			all.Add(weatherForecast);
			return weatherForecast;
		}


		
		[HttpDelete(Name = "DeleteWeatherForecast")]
		public void Delete(int id)
		{
			for (int i = 0; i < all.Count; i++)
				if (id == all[i].id)
					all.Remove(all[i]);
			
		}


		[HttpPut(Name = "UpdateWeatherForecast")]
		public WeatherForecast Update(WeatherForecast wF)
		{
			for (int i = 0; i < all.Count; i++)
				if (wF.id == all[i].id)
				{
					all.Remove(all[i]);
					all.Add(wF);
					return wF; 

				}
			return null; 
		}

		

		
	}
}