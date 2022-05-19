using Newtonsoft.Json;
namespace WebApplication2
{
	public class WeatherForecast
	{
		[JsonProperty("id")] public int id { get; set; }
		[JsonProperty("date")] public DateTime Date { get; set; }

		[JsonProperty("temperatureC")] public int TemperatureC { get; set; }

		[JsonProperty("temperatureF")] public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

		[JsonProperty("summary")] public string? Summary { get; set; }

		public WeatherForecast()
		{
		}
	}
}