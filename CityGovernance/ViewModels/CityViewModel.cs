using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGovernance.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }


        public int Ibge { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Uf { get; set; }

        public RegionViewModel Region { get; set; }

    }
}
