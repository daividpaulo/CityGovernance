using System;
using System.Collections.Generic;
using System.Text;

namespace CityGovernance.Domain.Models
{
    public class Region
    {

        public int Id { get; set; }

        public string Name { get; set; }

        
        public Region()
        {

        }
        public Region(string name)
        {
            this.Name = name;
        }

       


    }
}
