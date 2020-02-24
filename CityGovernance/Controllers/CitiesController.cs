using AutoMapper;
using CityGovernance.Domain.Exceptions;
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


        public IActionResult NewCity()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewCity(CityViewModel cityViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cityDb = _cityService.AddNew(_mapper.Map<City>(cityViewModel));
                    return RedirectToAction(nameof(Details), routeValues: new { id = cityDb.Id });

                }
                catch (ExistCityException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Inclusão não realizada! " +
                        "Tente novamente, caso problema persista " +
                        "entre em contato com o administrador do sistema.");
                }

            }

            return View(cityViewModel);
        }


        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var city = _cityService.GetOne(id);

            if (city == null) return NotFound();

            return View(_mapper.Map<CityViewModel>(city));
        }

        [HttpPost, ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCity(CityViewModel cityViewModel)
        {
            if (cityViewModel == null || cityViewModel.Id == 0) return NotFound();
            
            _cityService.DeleteCity(_cityService.GetOne(cityViewModel.Id));

            return RedirectToAction(nameof(SearchCities));
        }

        
        public IActionResult DeleteCity(int? id)
        {
            if (id == null) return NotFound();

            var city = _cityService.GetOne(id);

            if (city == null) return NotFound();

            return View(_mapper.Map<CityViewModel>(city));
        }


        public IActionResult Edit(int? id)
        {

            if (id == null) return NotFound();

            var city = _cityService.GetOne(id);

            if (city == null) return NotFound();

            return View(_mapper.Map<CityViewModel>(city));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CityViewModel cityViewModel)
        {
            if (id != cityViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(cityViewModel);

            try
            {
                var cityModel = _mapper.Map<City>(cityViewModel);
                cityViewModel = _mapper.Map<CityViewModel>(_cityService.UpdateCity(id, cityModel));

                return RedirectToAction(nameof(Details), routeValues: new { id = cityViewModel.Id });

            }
            catch (ExistCityException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Inclusão não realizada! " +
                    "Tente novamente, caso problema persista " +
                    "entre em contato com o administrador do sistema.");
            }

            return View(cityViewModel);
        }


    }
}
