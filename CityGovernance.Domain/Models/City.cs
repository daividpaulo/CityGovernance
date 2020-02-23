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

        public double Longitude { get;  set; }

        public double Latitude { get;  set; }

        public string Uf { get;  set; }

        public Region Region { get; set; }

        public void Update(City cityModel)
        {
            Ibge = cityModel.Ibge;
            Uf = cityModel.Uf;
            Name = cityModel.Name;
            Longitude = cityModel.Longitude;
            Latitude = cityModel.Latitude;
            Region = cityModel.Region;
            
        }
    }
}
