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

        public PlanetsController(IPlanetService PlanetService)
		{
			_planetService = PlanetService;
        }

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
                return RedirectToAction("Index", "Planets");
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
                return RedirectToAction("Index", "Planets");
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
                return RedirectToAction("Index", "Planets");
            }
            viewModel.PopulatePlanet(planet);
            if (_planetService.UpdatePlanet(planet))
            {
                TempData["success"] = $"Planet {planet.Name} was updated successfully!";
                return RedirectToAction("Index", "Planets");
            }
            else if (ModelState.IsValid)
            {
                TempData["error"] = "Unable to update planet!";
            }

            return View(viewModel);
        }

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
            return RedirectToAction("Index", "Planets");
        }


    }
}
