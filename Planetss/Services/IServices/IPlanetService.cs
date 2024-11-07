using Planetss.Entities;
namespace Planetss.Services.IServices
{
    public interface IPlanetService
    {
        public List<PlanetInformation> GetPlanetInformationList();
        bool AddPlanet(PlanetInformation planet);
        PlanetInformation? GetPlanetById(int? id);
        public bool UpdatePlanet(PlanetInformation planet);
        public bool DeletePlanet(int id);
        public void SetModelStateDictionary(IValidationDictionary modelState);
        public SearchResult<PlanetInformation> Search(PlanetSearch searchModel, string sortColumn, int start, int length);
    }
}
