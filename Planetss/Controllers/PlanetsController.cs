using Planetss.Entities;
using Microsoft.AspNetCore.Mvc;
using Planetss.Services.IServices;

namespace Planetss.Controllers
{
	public class PlanetsController : Controller
	{
		private IPlanetService _categoryService;
		public PlanetsController(IPlanetService categoryService)
		{
			_categoryService = categoryService;
		}

		public IActionResult Index()
		{
			List<PlanetInformation> categoryList = _categoryService.GetPlanetInformationlist();
			return View(categoryList);
		}
	}
}
