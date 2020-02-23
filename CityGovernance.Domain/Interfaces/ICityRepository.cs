using CityGovernance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CityGovernance.Domain.Interfaces
{
    public interface ICityRepository
    {
        IQueryable<City> GetAllCities(string search, string order, string sort);
    }
}
