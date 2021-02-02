using Microsoft.EntityFrameworkCore;

namespace WeatherData.Models
{
    class TDContext : DbContext
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=TemperatureData;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Average> Averages { get; set; }       
        public DbSet<MoldRisk> MoldRisk { get; set; }
    }
}
