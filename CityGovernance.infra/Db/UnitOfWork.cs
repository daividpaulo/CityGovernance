using CityGovernance.infra.Interfaces;
using CityGovernance.Infra.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CityGovernance.infra.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CityGovernanceContext context;

        public UnitOfWork(CityGovernanceContext context)
        {
            this.context = context;
        }

        public int Commit()
        {
            return context.SaveChangesAsync().Result;
        }

        public Task CommitAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
