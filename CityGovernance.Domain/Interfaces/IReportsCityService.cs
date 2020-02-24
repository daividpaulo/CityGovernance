using System.IO;

namespace CityGovernance.Domain.Interfaces
{
    public interface IReportsCityService
    {
        MemoryStream CountsCitysForUF();
        MemoryStream CountsCitysForRegion();
    }
}