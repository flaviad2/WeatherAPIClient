using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class WeatherContext : DbContext
    {

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {

        }

        public DbSet<Weather> WeatherForecasts { get; set; }

    }
}
