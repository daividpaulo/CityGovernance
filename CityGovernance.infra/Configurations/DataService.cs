using CityGovernance.Domain.Models;
using CityGovernance.Infra.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CityGovernance.infra.Configurations
{

    public class DataService : IDataService
    {

        private readonly CityGovernanceContext cityGovernanceContext;

        public DataService(CityGovernanceContext cityGovernanceContext)
        {
            this.cityGovernanceContext = cityGovernanceContext;
        }

        public void Seed()
        {
            
            cityGovernanceContext.Database.EnsureCreated();

            var cityList = File.ReadAllLines("cidades_desafio_tecnico.csv")
                                .Skip(1)
                                .Select(a => a.Split(';'))
                                .Select(city => InsertNewCity(city)
                               ).ToList();


           

        }

        private City InsertNewCity(string[] city)
        {
            
            var newCity =  new City()
            {
                Ibge = Convert.ToInt32(city[0]),
                Uf = city[1].Trim(),
                Name = city[2].Trim(),
                Longitude = Convert.ToDouble(city[3]),
                Latitude = Convert.ToDouble(city[4]),
                Region = GetOrInsertRegion(city[5].Trim())
            };

            bool exists = cityGovernanceContext.Citys.AsNoTracking().Any(x => x.Ibge == newCity.Ibge ||
                                                                             x.Name.Equals(newCity.Name) ||
                                                                             x.Uf.Equals(newCity));

            if (!exists) { 
                cityGovernanceContext.Set<City>().Add(newCity);
                cityGovernanceContext.SaveChanges();
            }

            return newCity;

        }

        private Region GetOrInsertRegion(string nameRegion)
        {
            Region regionDb = cityGovernanceContext.Regions.Where(x => x.Name.Equals(nameRegion)).FirstOrDefault();

            if (regionDb != null) {
                return regionDb;
            }
            else
            {
                regionDb = new Region(nameRegion);
                cityGovernanceContext.Set<Region>().Add(regionDb);
                cityGovernanceContext.SaveChanges();

                return regionDb;
            }

        }

       

    }
}
