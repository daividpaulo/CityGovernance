﻿using CityGovernance.Domain.Exceptions;
using CityGovernance.Domain.Interfaces;
using CityGovernance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityGovernance.Services.Services
{
    public class CityService : ICityService
    {
        ICityRepository _citiesRepository;

        public CityService(ICityRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

      
        public IQueryable<City> GetAllCities(string search, string order, string sort) =>
                                             _citiesRepository.GetAllCities(search,order,sort);
        
        public City GetOne(int? id) => _citiesRepository.GetOne(id);

        public City AddNew(City cityModel)
        {

            bool isValid = _citiesRepository.IsValid(cityModel);

            if (!isValid) throw new ExistCityException();

            cityModel.Region = GetOrInsertRegion(cityModel.Region.Name);

            return _citiesRepository.AddNew(cityModel);

        }


        public City UpdateCity(int id, City cityModel)
        {

            bool isValid = _citiesRepository.IsValid(cityModel);

            if (!isValid) throw new ExistCityException();

            var cityDb =_citiesRepository.GetOne(id);

            cityModel.Region = GetOrInsertRegion(cityModel.Region.Name);
            cityDb.Update(cityModel);

            cityDb = _citiesRepository.Update(cityDb);

            return cityDb;
            
        }

        private Region GetOrInsertRegion(string name)
        {
            Region regionDb = _citiesRepository.GetRegionByName(name); 

            if (regionDb == null)
            {
                regionDb = new Region(name);
                _citiesRepository.AddNewRegion(regionDb);
            }

            return regionDb;
        }

        public void DeleteCity(City city)
        {
            _citiesRepository.DeleteCity(city);
        }
    }
}
