using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CityGovernance.infra.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        int Commit();
    }
}
