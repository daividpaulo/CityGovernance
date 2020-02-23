using AutoMapper;
using CityGovernance.Domain.Interfaces;
using CityGovernance.Domain.Models;
using CityGovernance.Utils;
using CityGovernance.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityGovernance.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        public CitiesController(IMapper mapper, ICityService cityService)
        {
            _mapper = mapper;
            _cityService = cityService;
        }


       

        public async Task<IActionResult> SearchCities(
             string sortOrder,
             string sortBy,
             string currentFilter,
             int? pageNumber,
             int? pageSize)
        {

            ViewData["sortBy"] = String.IsNullOrEmpty(sortBy) ? "Id" : sortBy;
            ViewData["sortOrder"] = String.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            ViewData["currentFilter"] = String.IsNullOrEmpty(currentFilter) ? "" : currentFilter;

            string orderBy = ViewData["sortBy"].ToString();
            string order = ViewData["sortOrder"].ToString();
            string search = ViewData["currentFilter"].ToString();

            var query = _cityService.GetAllCities(search, order, orderBy);

            return View(await PaginatedList<City>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, pageSize ?? 5));
            
        }
    }
}
