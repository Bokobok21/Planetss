using Planetss.Entities;
using Microsoft.AspNetCore.Mvc;
using Planetss.Services.IServices;
using Planetss.Services;

namespace Planetss.Controllers
{
	public class PlanetsController : Controller
	{
		private IPlanetService _planetService;
        private IHttpContextAccessor _httpContextAccessor;
        public PlanetsController(IPlanetService PlanetService, IHttpContextAccessor httpContextAccessor)
		{
			_planetService = PlanetService;
            _httpContextAccessor = httpContextAccessor;
        }

        //opravi categoryviewmodel 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            CategoryViewModel viewModel = new CategoryViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel viewModel)
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
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.PopulateFromPlanet(planet);
            return View(viewModel);
        }

        //Dobavi oshte methodi
        //List<PlanetInformation> planetList = _planetService.GetPlanetInformationList();

    }
}
