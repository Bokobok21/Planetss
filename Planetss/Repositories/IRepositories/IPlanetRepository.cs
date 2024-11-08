using Planetss.Entities;
using Planetss.DTO;
namespace Planetss.Repositories.IRepositories
{
    public interface IPlanetRepository
    {
        bool Add(PlanetInformation entity);
        List<PlanetInformation> ToList();
        PlanetInformation? FindById(int? id);
        public bool Update(PlanetInformation planet);
        public bool Delete(int id);
        public PlanetInformation? FindByName(string? name);
        public SearchResult<PlanetInformation> GetPageData(PlanetSearch searchModel, string sortColumn, int start, int length);
    }
}
