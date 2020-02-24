using CityGovernance.Domain.Interfaces;
using CityGovernance.Domain.Models;
using CityGovernance.Infra.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityGovernance.infra.Repositories
{

    public class CityRepository : ICityRepository
    {
        private readonly CityGovernanceContext citygovernanceContext;
        public CityRepository(CityGovernanceContext citygovernanceContext)
        {

            this.citygovernanceContext = citygovernanceContext;
        }

        public City AddNew(City city)
        {

            citygovernanceContext.Citys.Add(city);

            return city;
        }

        public Region AddNewRegion(Region regionDb)
        {
            citygovernanceContext.Set<Region>().Add(regionDb);

            return regionDb;
        }

        public void DeleteCity(City city)
        {
            citygovernanceContext.Citys.Remove(city);
        }

        public IQueryable<City> GetAllCities(string search, string order, string sortBy)
        {

            var citiesAsQuerable = from s in citygovernanceContext.Citys select s;


            if (!String.IsNullOrEmpty(search)) citiesAsQuerable = citiesAsQuerable.Where(x =>
                                                                     x.Name.ToLower().Trim().Contains(search.Trim().ToLower()) ||
                                                                     x.Ibge.ToString().ToLower().Trim().Contains(search.Trim().ToLower()) ||
                                                                     x.Region.Name.ToLower().Trim().Contains(search.Trim().ToLower()) ||
                                                                     x.Uf.ToLower().Trim().Contains(search.Trim().ToLower())
                                                                   );


            citiesAsQuerable = citiesAsQuerable.Include(x => x.Region);

            if (order.Equals("asc"))
            {
                citiesAsQuerable = orderByAsc(citiesAsQuerable, sortBy);
            }
            else if (order.Equals("desc"))
            {
                citiesAsQuerable = orderByDesc(citiesAsQuerable, sortBy);
            }


            return citiesAsQuerable;
        }

        public City GetOne(int? id)
        {

            return citygovernanceContext.Citys
                               .Include(x => x.Region)
                               .AsNoTracking()
                               .FirstOrDefault(x => x.Id == id);

        }

        public Region GetRegionByName(string name)
        {
            return citygovernanceContext.Regions.Where(x => x.Name.ToLower().Trim().Equals(name.ToLower().Trim())).FirstOrDefault();
        }

        public bool IsValid(City cityModel)
        {
            bool isUpdate = cityModel.Id > 0;

            if (isUpdate)
            {
                 return !citygovernanceContext.Citys.AsNoTracking().Any(x => x.Id != cityModel.Id && (x.Ibge == cityModel.Ibge ||

                                                                         (x.Name.Trim().ToLower().Equals(cityModel.Name.Trim().ToLower()) &&
                                                                           x.Uf.Trim().ToLower().Equals(cityModel.Uf.Trim().ToLower())))
                                                                       );
            }
            else
            {
                 return !citygovernanceContext.Citys.AsNoTracking().Any(x => (x.Ibge == cityModel.Ibge ||

                                                                         (x.Name.Trim().ToLower().Equals(cityModel.Name.Trim().ToLower()) &&
                                                                           x.Uf.Trim().ToLower().Equals(cityModel.Uf.Trim().ToLower())))
                                                                       );
            }

        }

        public City Update(City cityDb)
        {
            citygovernanceContext.Citys.Update(cityDb);
            
            return cityDb;
        }

        private IQueryable<City> orderByAsc(IQueryable<City> query, string sortBy)
        {
            switch (sortBy)
            {
                case "Id":
                    query = query.OrderBy(x => x.Id);
                    break;
                case "Ibge":
                    query = query.OrderBy(x => x.Ibge);
                    break;
                case "Latitude":
                    query = query.OrderBy(x => x.Latitude);
                    break;
                case "Longitude":
                    query = query.OrderBy(x => x.Longitude);
                    break;
                case "Name":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "Region.Name":
                    query = query.OrderBy(x => x.Region.Name);
                    break;
            }

            return query;
        }

        private IQueryable<City> orderByDesc(IQueryable<City> query, string sortBy)
        {
            switch (sortBy)
            {
                case "Id":
                    query = query.OrderByDescending(x => x.Id);
                    break;
                case "Ibge":
                    query = query.OrderByDescending(x => x.Ibge);
                    break;
                case "Latitude":
                    query = query.OrderByDescending(x => x.Latitude);
                    break;
                case "Longitude":
                    query = query.OrderByDescending(x => x.Longitude);
                    break;
                case "Name":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case "Region.Name":
                    query = query.OrderByDescending(x => x.Region.Name);
                    break;
            }

            return query;
        }
    }
}