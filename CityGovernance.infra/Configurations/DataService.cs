using CityGovernance.Domain.Interfaces;
using CityGovernance.Domain.Models;
using CityGovernance.Infra.Db;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CityGovernance.infra.Configurations
{

    public class DataService : IDataService
    {

        private readonly CityGovernanceContext _cityGovernanceContext;
        private readonly ICityService _cityService;

        public DataService(CityGovernanceContext cityGovernanceContext, ICityService cityService)
        {
            _cityGovernanceContext = cityGovernanceContext;
            _cityService = cityService;
        }

        public void Seed()
        {
            _cityGovernanceContext.Database.EnsureCreated();
            //LoadCsv();
            //_cityGovernanceContext.SaveChanges();
        }

        private List<City> LoadCsv()
        {
            var list = File.ReadAllLines("cidades_desafio_tecnico.csv")
                               .Skip(1)
                               .Select(a => a.Split(';'))
                               .Select(city => _cityService.AddNew(new City(city))).ToList();

            return list;
        }


    }
}
