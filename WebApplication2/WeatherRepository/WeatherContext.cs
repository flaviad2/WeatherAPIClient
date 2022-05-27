
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.WeatherRepository
{
    public class WeatherContext : DbContext
    {

        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {

        }

        public DbSet<WeatherEntity> WeatherForecasts { get; set; }

    }
}
