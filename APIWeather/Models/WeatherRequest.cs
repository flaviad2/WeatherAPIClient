namespace APIWeather.Models
{

    [Serializable]
    public class WeatherRequest
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }


        public int MinimumTemperature { get; set; }


        public int MaximumTemperature { get; set; }


        public int PrecipitationsProbability { get; set; }



        public bool AtmosphericFenomens { get; set; }


        public String OtherInformation { get; set; }



        public SourceEnum DataSource { get; set; }

        public WeatherRequest(int id, DateTime date, TimeSpan time, int minimumTemperature, int maximumTemperature, int precipitationsProbability, bool atmosphericFenomens, string otherInformation, SourceEnum dataSource)
        {
            Id = id;
            Date = date;
            Time = time;
            MinimumTemperature = minimumTemperature;
            MaximumTemperature = maximumTemperature;
            PrecipitationsProbability = precipitationsProbability;
            AtmosphericFenomens = atmosphericFenomens;
            OtherInformation = otherInformation;
            DataSource = dataSource;
        }
    }
}
