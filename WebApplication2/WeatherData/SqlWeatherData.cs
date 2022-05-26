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
           _weatherContext.WeatherForecasts.Add(weather);
            _weatherContext.SaveChanges();
            return weather;
        }

      
        public void DeleteWeather(int Id)
        {
            //verifica metoda din Controller daca prognoza exista si aici fac doar stergere dupa id, stiind ca exista
            _weatherContext.WeatherForecasts.Remove(_weatherContext.WeatherForecasts.Find(Id));
            _weatherContext.SaveChanges();
        }
        /*
        public Weather EditWeather(Weather weather)
        {
                _weatherContext.WeatherForecasts.Update(weather);
                _weatherContext.SaveChanges();
           
          return weather;
        } */

        public Weather EditWeather(int Id, Weather weather)
        {
            Weather oldWeather = _weatherContext.WeatherForecasts.Where(w=>w.Id==weather.Id).FirstOrDefault();
            if (oldWeather != null)
            {
                oldWeather.Date = weather.Date;
                oldWeather.Time = weather.Time;
                oldWeather.MinimumTemperature = weather.MinimumTemperature;
                oldWeather.MaximumTemperature = weather.MaximumTemperature;
                oldWeather.AtmosphericFenomens = weather.AtmosphericFenomens;
                oldWeather.PrecipitationsProbability = weather.PrecipitationsProbability;
                oldWeather.DataSource = weather.DataSource;
                _weatherContext.WeatherForecasts.Update(oldWeather);
                _weatherContext.SaveChanges();
                return oldWeather;

            }
            return null; //adica nu s-au facut modificari 

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
        public List<Weather> GetWeathersBetweenDates(DateTime date1, DateTime date2)
        {
            return _weatherContext.WeatherForecasts.Where(w => w.Date >= date1 && w.Date <= date2).ToList();
        }
 
        public List<Weather> GetWeathersFromDay(DateTime date_day)
        {
            return _weatherContext.WeatherForecasts.Where(w => w.Date == date_day).ToList(); 
        }

        public List<Weather> GetWeathersNotTooFar(DateTime today)
        {
            return _weatherContext.WeatherForecasts.Where(w => w.Date.Year == today.Year && w.Date.DayOfYear - today.DayOfYear < 30 && w.Date.DayOfYear - today.DayOfYear <30).ToList();
        }


        public Weather AddWeatherWithDate(DateTime date, Weather weather)
        {
            if (date>= DateTime.Today )
            {
                _weatherContext.WeatherForecasts.Add(weather);
                _weatherContext.SaveChanges();
                return weather;
            }
            else
            {
                throw new Exception("This method only adds weather forecast for future days!"); 
            }
        }

        public Weather EditWeatherFromDayFromSource(DateTime date, Source source, Weather weather)
        {


           Weather oldWeather =_weatherContext.WeatherForecasts.Where(w => w.Date == date && w.DataSource == source).FirstOrDefault(); 
          if(oldWeather != null)
            {
                oldWeather.Date = weather.Date;
                oldWeather.Time = weather.Time;
                oldWeather.MinimumTemperature = weather.MinimumTemperature;
                oldWeather.MaximumTemperature = weather.MaximumTemperature;
                oldWeather.AtmosphericFenomens = weather.AtmosphericFenomens;
                oldWeather.PrecipitationsProbability = weather.PrecipitationsProbability;
                oldWeather.DataSource = weather.DataSource; 
                _weatherContext.WeatherForecasts.Update(oldWeather);
                _weatherContext.SaveChanges();
                return oldWeather;

            }
            return null;

        }
    }
}
