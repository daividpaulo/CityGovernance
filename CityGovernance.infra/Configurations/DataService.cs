using CityGovernance.Domain.Models;
using CityGovernance.Infra.Db;
using System;
using System.Collections.Generic;
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

            //cityGovernanceContext.Set<City>().Add(new City());
            //cityGovernanceContext.SaveChanges();
        }

        /*private static void AddReason(ControleMacasContext ctx, ReasonType type, string description)
        {
            bool exists = ctx.Reasons.AsNoTracking().Any(x => x.Type == type);
            if (!exists)
            {
                ctx.Reasons.Add(new Reason(description, type));
            }
        }*/

    }
}
