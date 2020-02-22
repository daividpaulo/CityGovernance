using CityGovernance.Domain.Models;
using CityGovernance.infra.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CityGovernance.Infra.Db
{
    public class CityGovernanceContext : DbContext
    {

        public DbSet<City> Citys { get; set; }
        public DbSet<Region> Regions { get; set; }

    public CityGovernanceContext(DbContextOptions options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityMapping());
            modelBuilder.ApplyConfiguration(new RegionMapping());
            
            

            base.OnModelCreating(modelBuilder);
                        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();

            base.OnConfiguring(optionsBuilder);
        }

    }
}
