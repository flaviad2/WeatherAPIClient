using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApplication2.WeatherRepository;

namespace WebApplication2.Models
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

        public WeatherRequest( DateTime date, TimeSpan time, int minimumTemperature, int maximumTemperature, int precipitationsProbability, bool atmosphericFenomens, string otherInformation, SourceEnum dataSource)
        {
            
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
