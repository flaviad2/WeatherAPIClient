using WebApplication2.Models;


namespace WebApplication2.WeatherData
{
    public class SqlWeatherData : IWeatherData
    {
        private WeatherContext _weatherContext; 
        public SqlWeatherData(WeatherContext context)
        {
            _weatherContext = context;
        }
        public Weather AddWeather(Weather weather)
        {
            //incremented min 44
           _weatherContext.WeatherForecasts.Add(weather);
            _weatherContext.SaveChanges();
            return weather;
        }

      
        public void DeleteWeather(int Id)
        {
            // var existingWeather = _weatherContext.WeatherForecasts.Find(Id); 
            // // sau obiectul se pune in find si la param daca nu merge? 
            //  if(existingWeather!=null)
            //  {
            //     _weatherContext.WeatherForecasts.Remove(existingWeather);
            // }
            _weatherContext.WeatherForecasts.Remove(_weatherContext.WeatherForecasts.Find(Id));
            _weatherContext.SaveChanges();
        }
        public Weather EditWeather(Weather weather)
        {
        
                _weatherContext.WeatherForecasts.Update(weather);
                _weatherContext.SaveChanges();
           
          return weather;
        }

        public Weather GetWeather(int id)
        {

            var weather = _weatherContext.WeatherForecasts.Find(id);
            return weather; 
        }


        public List<Weather> getWeathers()
        {
           return  _weatherContext.WeatherForecasts.ToList();

            
        }
    }
}
