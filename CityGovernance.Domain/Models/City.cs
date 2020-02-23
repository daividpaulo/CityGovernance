using System;
using System.Collections.Generic;
using System.Text;

namespace CityGovernance.Domain.Models
{
    public class City
    {
        public int Id { get; set; }

        public int Ibge { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Uf { get; set; }

        public Region Region { get; set; }

        public City() { }


        public City(string[] city)
        {
            Ibge = Convert.ToInt32(city[0]);
            Uf = city[1].Trim();
            Name = city[2].Trim();
            Longitude = Convert.ToDouble(city[3]);
            Latitude = Convert.ToDouble(city[4]);
            Region = new Region(city[5].Trim());
        }

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
