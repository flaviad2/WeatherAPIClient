using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication2.Models
{
    public enum Source
    {
        Cluj,
        Bucuresti,
        Sibiu,
        Constanta,
        Iasi
    }

    public class Weather
    {
        [Key]
        [JsonProperty("Id")]  public int Id { get; set; }

        [JsonProperty("Date")]  public DateTime Date { get; set; }
        [JsonProperty("Time")]  public TimeSpan Time { get; set; }

        [JsonProperty("MinimumTemperatutre")]  public int MinimumTemperature { get; set; }

        [JsonProperty("MaximumTemperatutre")]  public int MaximumTemperature { get; set; }

        [JsonProperty("PrecipitationsProbability")] public int PrecipitationsProbability { get; set; }


        [JsonProperty("AtmosphericFenomens")] public bool AtmosphericFenomens { get; set; }

        [JsonProperty("OtherInformation")]  public String OtherInformation { get; set; }


        [JsonProperty("DataSource")]  public Source DataSource { get; set; }


    }
}
