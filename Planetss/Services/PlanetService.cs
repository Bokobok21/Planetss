using Planetss.Entities;
using Planetss.Services.IServices;
using Planetss.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;

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

        public void SetModelStateDictionary(IValidationDictionary modelState)
        {
            _modelState = modelState;
        }

        public bool ValidatePlanet(PlanetInformation planet)
        {
            if (_modelState == null)
                throw new ArgumentNullException(nameof(_modelState));

            if (!String.IsNullOrEmpty(planet.Name) && planet.Name.ToLower() == "test")
                _modelState.AddError("", "\"Test\" is an invalid value!");

            Category? planet1 = _repository.FindByName(planet.Name);
            if (planet1 != null)
            {
                if (category.Id != planet1.Id)
                    _modelState.AddError("", $"Planet {planet1.Name} already exists.");
            }

            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(planet.Name);
            if (match.Success)
                _modelState.AddError("", "Planet name can not have a number.");


            return _modelState.IsValid;
        }

        public List<PlanetInformation> GetPlanetInformationList()
		{
			return _repository.ToList();
		}

        public bool AddPlanet(PlanetInformation planet)
        {
            try
            {
                if (!ValidatePlanet(planet))
                    return false;
                return _repository.Add(planet);
            }
            catch
            {
                return false;
            }
        }

        public PlanetInformation? GetPlanetById(int? id)
        {
            return _repository.FindById(id);
        }

        public bool UpdatePlanet(PlanetInformation planet)
        {
            try
            {
                if (!ValidatePlanet(planet))
                    return false;
                return _repository.Update(planet);
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePlanet(int id)
        {
            return _repository.Delete(id);
        }

        public SearchResult<PlanetInformation> Search(PlanetSearch searchModel, string sortColumn, int start, int length)
        {
            return _repository.GetPageData(searchModel, sortColumn, start, length);
        }

    }
}
