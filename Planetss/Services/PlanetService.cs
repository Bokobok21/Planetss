using Planetss.Entities;
using Planetss.Services.IServices;
using Planetss.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Planetss.Services
{
	public class PlanetService : IPlanetService
	{
		private ModelStateDictionary? _modelState;
		private IPlanetRepository _repository;

		public PlanetService(IPlanetRepository repository)
		{
			_repository = repository;
		}
		public List<PlanetInformation> GetPlanetInformationList()
		{
			return _repository.ToList();
		}
	}
}
