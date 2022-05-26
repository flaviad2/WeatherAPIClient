using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Enum.Ext.NewtonsoftJson;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace WebApplication2.Models
{
    // serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum Source
    {
        [EnumMember(Value ="Bucuresti")]
        Bucuresti =1 ,

        [EnumMember(Value = "Constanta")]
        Constanta,

        [EnumMember(Value = "Cluj")]
        Cluj,

        [EnumMember(Value = "Iasi")]
        Iasi,

        [EnumMember(Value = "Sibiu")]
        Sibiu
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


       [JsonConverter(typeof(StringEnumConverter))][JsonProperty("DataSource")] public Source DataSource { get; set; }


      

    }

}
