using System;
using System.Collections.Generic;
using System.Text;

namespace CityGovernance.Domain.Models
{
    public class City
    {

        public int Id { get; set; }

        
        public int Ibge { get;  set; }

        public string Name { get;  set; }

        public float Longitude { get;  set; }

        public float Latitude { get;  set; }

        public string Uf { get;  set; }

        public Region Region { get; set; }

    }
}
