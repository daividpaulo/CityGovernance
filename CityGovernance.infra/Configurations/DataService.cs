using CityGovernance.Domain.Models;
using CityGovernance.infra.Interfaces;
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

        private readonly CityGovernanceContext _cityGovernanceContext;
        private readonly DbSet<City> _dbSetCity;
        private readonly DbSet<Region> _dbSetRegion;
        

        public DataService(CityGovernanceContext cityGovernanceContext, IUnitOfWork unitOfWork)
        {
            _cityGovernanceContext = cityGovernanceContext;
            _dbSetCity = _cityGovernanceContext.Citys;
            _dbSetRegion = _cityGovernanceContext.Regions;
            
        }

        public void Seed()
        {

            _cityGovernanceContext.Database.EnsureCreated();

            var cityList = File.ReadAllLines("cidades_desafio_tecnico.csv")
                                .Skip(1)
                                .Select(a => a.Split(';'))
                                .Select(city => InsertNewCity(city)
                               ).ToList();

        }

        private City InsertNewCity(string[] city)
        {

            var newCity = new City()
            {
                Ibge = Convert.ToInt32(city[0]),
                Uf = city[1].Trim(),
                Name = city[2].Trim(),
                Longitude = Convert.ToDouble(city[3]),
                Latitude = Convert.ToDouble(city[4]),
                Region = GetOrInsertRegion(city[5].Trim())
            };

            bool exists = _dbSetCity.Where(x => x.Ibge == newCity.Ibge || x.Name.Equals(newCity.Name) || x.Uf.Equals(newCity)).Any();

            if (!exists)
            {
                _dbSetCity.Add(newCity);
                _cityGovernanceContext.SaveChanges();
            }
     
            return newCity;

        }

        private Region GetOrInsertRegion(string nameRegion)
        {
            Region regionDb = _dbSetRegion.FirstOrDefault(x => x.Name.Trim().ToLower().Equals(nameRegion.Trim().ToLower())); ;

            if (regionDb != null)
            {
                return regionDb;
            }
            else
            {
                regionDb = new Region(nameRegion);
                _dbSetRegion.Add(regionDb);
                _cityGovernanceContext.SaveChanges();
                

                return regionDb;
            }

        }



    }
}
