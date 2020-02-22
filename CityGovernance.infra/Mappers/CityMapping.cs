using CityGovernance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityGovernance.infra.Mappers
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cidades");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .UseIdentityByDefaultColumn();

            builder.HasIndex(x => x.Ibge).IsUnique();
            
            builder.HasOne(x => x.Region)
                .WithMany()
                .IsRequired();

        }
    }
}
