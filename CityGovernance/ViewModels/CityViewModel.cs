﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityGovernance.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Código IBGE")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "Ibge obrigatorio!")]
        public int? Ibge { get; set; }

        [Display(Name = "Nome da Cidade")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome obrigatorio!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Longitude obrigatorio!")]
        public double? Longitude { get; set; }

        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Latitude obrigatorio!")]
        public double? Latitude { get; set; }

        [Display(Name = "Unidade Federativa - UF")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Uf obrigatorio!")]
        public string Uf { get; set; }

        public RegionViewModel Region { get; set; }

    }
}
