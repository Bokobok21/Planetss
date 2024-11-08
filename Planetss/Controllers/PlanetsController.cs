using Planetss.Entities;
using Microsoft.AspNetCore.Mvc;
using Planetss.Services.IServices;
using Planetss.Services;
using Planetss.Helpers;
using System.Web;
using Planetss.DTO;
using Planetss.Models.PlanetInformation;

namespace Planetss.Controllers
{
	public class PlanetsController : Controller
	{
		private IPlanetService _planetService;
       // private IHttpContextAccessor _httpContextAccessor;
        public PlanetsController(IPlanetService PlanetService)
		{
			_planetService = PlanetService;
            //_httpContextAccessor = httpContextAccessor;
        }

        //opravi categoryviewmodel 
        public IActionResult Index()
        {

            List<PlanetInformation> list = _planetService.GetPlanetInformationList();
         
            return View(list);

        }
        public IActionResult Create()
        {
            PlanetViewModel viewModel = new PlanetViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlanetViewModel viewModel)
        {
            _planetService.SetModelStateDictionary(new ModelStateWrapper(ModelState));

            PlanetInformation planet = new PlanetInformation();
            viewModel.PopulatePlanet(planet);
            if (_planetService.AddPlanet(planet))
            {
                TempData["success"] = $"Planet {planet.Name} was created successfully!";
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid)
            {
                TempData["error"] = "Unable to create planet!";
            }

            return View(viewModel);
        }

        public IActionResult Update(int id)
        {
            PlanetInformation? planet = _planetService.GetPlanetById(id);
            if (planet == null)
            {
                TempData["error"] = "Planet with id " + id + " not found!";
                return RedirectToAction("Index");
            }
            PlanetViewModel viewModel = new PlanetViewModel();
            viewModel.PopulateFromPlanet(planet);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PlanetViewModel viewModel)
        {
            _planetService.SetModelStateDictionary(new ModelStateWrapper(ModelState));

            PlanetInformation? planet = _planetService.GetPlanetById(viewModel.Id);
            if (planet == null)
            {
                TempData["error"] = "Unable to find planet!";
                return RedirectToAction("Index");
            }
            viewModel.PopulatePlanet(planet);
            if (_planetService.UpdatePlanet(planet))
            {
                TempData["success"] = $"Planet {planet.Name} was updated successfully!";
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid)
            {
                TempData["error"] = "Unable to update planet!";
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (_planetService.DeletePlanet(id))
            {
                TempData["success"] = "Planet was deleted successfully";
            }
            else
            {
                TempData["error"] = "Unable to delete planet";
            }
            return RedirectToAction("Index");
        }

        ////opravi izkarvaneto na danni
        //public IActionResult Get(int draw, int start, int length)
        //{
        //    PrintUrlQueryParamsInConsole();
        //    string urlQuery = _httpContextAccessor.HttpContext.Request.QueryString.Value;
        //    var paramsCollection = HttpUtility.ParseQueryString(urlQuery);

        //    //Get search params
        //    string? name = paramsCollection["columns[0][search][value]"];
        //    string? defaultOrder = paramsCollection["columns[1][search][value]"];

        //    //Get sort
        //    string? sortColumnIndex = paramsCollection["order[0][column]"];
        //    string? sortColumnName = paramsCollection["columns[" + sortColumnIndex + "][data]"];
        //    string? sortDirection = paramsCollection["order[0][dir]"];
        //    string sortColumn = "";

        //    PlanetSearch searchModel = new PlanetSearch();
        //    searchModel.Name = name;
        //    if (!String.IsNullOrEmpty(defaultOrder))
        //    {
        //        searchModel.MoonCount = int.Parse(defaultOrder);
        //        searchModel.PopulationCount = int.Parse(defaultOrder);
        //    }

        //    if (sortDirection == "asc")
        //        sortColumn = sortColumnName;
        //    else
        //        sortColumn = $"-{sortColumnName}";

        //    SearchResult<PlanetInformation> result = _planetService.Search(searchModel, sortColumn, start, length);

        //    //Explanation of the responce:
        //    //https://stackoverflow.com/questions/43161353/recordstotal-recordsfiltered-explanation-jquery-datatable
        //    return Ok(new
        //    {
        //        draw = draw,
        //        recordsTotal = result.RecordsTotal,
        //        recordsFiltered = result.RecordsFiltered,
        //        data = result.Data
        //    });
        //}
        //private void PrintUrlQueryParamsInConsole()
        //{
        //    string urlQuery = _httpContextAccessor.HttpContext.Request.QueryString.Value;
        //    var paramsCollection = HttpUtility.ParseQueryString(urlQuery);
        //    foreach (var key in paramsCollection.AllKeys)
        //    {
        //        Console.WriteLine($"Key: {key} => Value: {paramsCollection[key]}");
        //    }
        //}


    }
}
