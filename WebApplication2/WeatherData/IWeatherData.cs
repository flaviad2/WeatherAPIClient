using WebApplication2.Models;

namespace WebApplication2.WeatherData
{
    public interface IWeatherData
    {
        List<Weather> getWeathers();

        Weather GetWeather(int Id); 

        Weather AddWeather(Weather weather);

        void DeleteWeather(int Id); //sau obiectul?

        Weather EditWeather(Weather weather); 



    }
}
