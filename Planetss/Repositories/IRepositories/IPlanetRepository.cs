using Planetss.Entities;
namespace Planetss.Repositories.IRepositories
{
    public interface IPlanetRepository
    {
        bool Add(PlanetInformation entity);
        List<PlanetInformation> ToList();
    }
}
