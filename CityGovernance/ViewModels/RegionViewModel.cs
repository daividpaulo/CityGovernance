using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityGovernance.ViewModels
{
    public class RegionViewModel
    {
        public int Id { get; set; }


        [Display(Description = "Nome da Região")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome da Região obrigatorio!")]
        public string? Name { get; set; }
    }
}
