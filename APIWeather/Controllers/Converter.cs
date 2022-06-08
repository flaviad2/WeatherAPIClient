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
        public static List<WeatherResponse> WeatherToResponseList(List<WeatherEntity> weathers)
        {
            List<WeatherResponse> listResult = new();
            foreach (WeatherEntity w in weathers)
            {
                WeatherResponse weatherResponse = new WeatherResponse(w.Id, w.Date, w.Time, w.MinimumTemperature, w.MaximumTemperature, w.PrecipitationsProbability, w.AtmosphericFenomens, w.OtherInformation, w.DataSource);
                listResult.Add(weatherResponse);
            }
            return listResult;
        }

        public static WeatherResponse WeatherToResponseElem(WeatherEntity w)
        {
            return new WeatherResponse(w.Id, w.Date, w.Time, w.MinimumTemperature, w.MaximumTemperature, w.PrecipitationsProbability, w.AtmosphericFenomens, w.OtherInformation, w.DataSource);
        }

        public static WeatherEntity RequestToWeather(WeatherRequest w)
        {
            return new WeatherEntity(w.Id, w.Date, w.Time, w.MinimumTemperature, w.MaximumTemperature, w.PrecipitationsProbability, w.AtmosphericFenomens, w.OtherInformation, w.DataSource);

        }


    }
}
