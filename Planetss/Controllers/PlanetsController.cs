using Planetss.Entities;
using Microsoft.AspNetCore.Mvc;
using Planetss.Services.IServices;
using Planetss.Services;

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
			List<PlanetInformation> planetList = _planetService.GetPlanetInformationList();
			return View(planetList);
		}

        //public IActionResult Create()
        //{
        //    List<PlanetInformation> planetList = _planetService.GetPlanetInformationList();
        //    return View(planetList);
        //}
    }
}
