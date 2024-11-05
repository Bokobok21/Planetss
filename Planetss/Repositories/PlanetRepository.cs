using Planetss.Data;
using Planetss.Entities;
using Planetss.Repositories.IRepositories;

namespace Planetss.Repositories
{
	public class PlanetRepository  //:IPlanetReposotiry
	{
		private readonly ApplicationDbContext _context;

		public PlanetRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public bool Add(PlanetInformation planet)
		{
			try
			{
				_context.Categories.Add(planet);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public List<PlanetInformation> ToList()
		{
			return _context.Categories.ToList();
		}
	}
}
