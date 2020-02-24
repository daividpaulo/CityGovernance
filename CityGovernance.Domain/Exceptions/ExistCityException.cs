using System;
using System.Collections.Generic;
using System.Text;

namespace CityGovernance.Domain.Exceptions
{
    public class ExistCityException : Exception
    {
        public ExistCityException() : base("Já existe uma cidade cadastrada para uma das opções informadas [ Nome ou Ibge ou Uf ]")
        {  
        }
    }
}
