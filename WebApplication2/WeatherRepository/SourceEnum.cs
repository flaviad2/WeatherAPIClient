using System.Runtime.Serialization;

namespace WebApplication2.WeatherRepository
{
    public enum SourceEnum
    {
        [EnumMember(Value = "Bucuresti")]
        Bucuresti = 1,

        [EnumMember(Value = "Constanta")]
        Constanta,

        [EnumMember(Value = "Cluj")]
        Cluj,

        [EnumMember(Value = "Iasi")]
        Iasi,

        [EnumMember(Value = "Sibiu")]
        Sibiu
    }
}
