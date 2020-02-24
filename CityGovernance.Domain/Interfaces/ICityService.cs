using CityGovernance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityGovernance.Domain.Interfaces
{
    public interface ICityService
    {
        IQueryable<City> GetAllCities(string search,string order,string sort);
        City GetOne(int? id);
        City AddNew(City city);
        City UpdateCity(int id, City cityModel);
        void DeleteCity(City city);
    }
}
