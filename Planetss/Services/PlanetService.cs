using Planetss.Entities;
using Planetss.Services.IServices;
using Planetss.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Planetss.Services
{
	public class PlanetService
	{
		private ModelStateDictionary? _modelState;
		private IPlanetRepository _repository;

		public PlanetService(IPlanetRepository repository)
		{
			_repository = repository;
		}
		public List<PlanetInformation> GetCategoryList()
		{
			return _repository.ToList();
		}
	}
}
