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

        public IQueryable<City> GetAllCities(string search, string order, string sort)
        {
            return _citiesRepository.GetAllCities(search,order,sort);
        }
    }
}
