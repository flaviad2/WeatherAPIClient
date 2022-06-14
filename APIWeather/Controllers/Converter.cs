using APIWeather.Models;

namespace APIWeather.Controllers
{
    public sealed class Converter
    {

        private Converter() { }

        public static Converter instance = null;

        public static Converter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Converter();
                }
                return instance;
            }
        }
        public static List<WeatherResponse> WeatherToResponseList(List<Weather> weathers)
        {
            List<WeatherResponse> listResult = new();
            foreach (Weather w in weathers)
            {
                WeatherResponse weatherResponse = new WeatherResponse(w.Id, w.Date, w.Time, w.MinimumTemperature, w.MaximumTemperature, w.PrecipitationsProbability, w.AtmosphericFenomens, w.OtherInformation, w.DataSource);
                listResult.Add(weatherResponse);
            }
            return listResult;
        }

        public static WeatherResponse WeatherToResponseElem(Weather w)
        {
            return new WeatherResponse(w.Id, w.Date, w.Time, w.MinimumTemperature, w.MaximumTemperature, w.PrecipitationsProbability, w.AtmosphericFenomens, w.OtherInformation, w.DataSource);
        }

        public static Weather RequestToWeather(WeatherRequest w)
        {
            return new Weather(w.Id, w.Date, w.Time, w.MinimumTemperature, w.MaximumTemperature, w.PrecipitationsProbability, w.AtmosphericFenomens, w.OtherInformation, w.DataSource);

        }

        public static Weather ToWeatherEntity(WeatherResponseW2 we)
        {
            return new Weather(we.Id, we.Date, we.Time, we.MinimumTemperature, we.MaximumTemperature, we.PrecipitationsProbability,
                                        we.AtmosphericFenomens, we.OtherInformation, we.DataSource);

        }

        public static WeatherRequestW2 ToWeatherRequestW2(WeatherRequest we)
        {
            return new WeatherRequestW2(we.Id, we.Date, we.Time, we.MinimumTemperature, we.MaximumTemperature, we.PrecipitationsProbability,
                                        we.AtmosphericFenomens, we.OtherInformation, we.DataSource);

        }

        public static List<Weather> ListW2ToListEntity(List<WeatherResponseW2> wList)
        {
            List<Weather> result = new List<Weather>();
            foreach(WeatherResponseW2 we in wList)
            {
                result.Add(new Weather( we.Id, we.Date, we.Time, we.MinimumTemperature, we.MaximumTemperature, we.PrecipitationsProbability,
                                        we.AtmosphericFenomens, we.OtherInformation, we.DataSource));

            }
            return result;
        }


    }
}
