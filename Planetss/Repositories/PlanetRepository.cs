using Planetss.Data;
using Planetss.Entities;
using Planetss.DTO;
using Planetss.Repositories.IRepositories;

namespace Planetss.Repositories
{
	public class PlanetRepository  : IPlanetRepository
	{
		private readonly ApplicationDbContext _context;
        private int RecordsTotal { get; set; }
        private int RecordsFiltered { get; set; }

        public PlanetRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public bool Add(PlanetInformation planet)
		{
			try
			{
				_context.Planets.Add(planet);
				return true;
			}
			catch
			{
				return false;
			}
		}

        public bool Update(PlanetInformation planet)
        {
            try
            {
                _context.Planets.Update(planet);
                int stateNumber = _context.SaveChanges();
                return stateNumber > 0;
            }
            catch
            {
                return false;
            }
        }

        public List<PlanetInformation> ToList()
		{
			return _context.Planets.ToList();
		}

        public PlanetInformation? FindById(int? id)
        {
            if (id == null || id == 0)
                return null;

            PlanetInformation? planet = _context.Planets.Find(id);
            return planet;
        }

        public bool Delete(int id)
        {
            PlanetInformation? planet = FindById(id);
            if (planet == null)
                return false;

            try
            {
                _context.Planets.Remove(planet);
                int stateNumber = _context.SaveChanges();
                return stateNumber > 0;
            }
            catch
            {
                return false;
            }
        }

        public PlanetInformation? FindByName(string? name)
        {
            if (String.IsNullOrEmpty(name))
                return null;
            return _context.Planets.Where(s => s.Name == name).FirstOrDefault();
        }

        public SearchResult<PlanetInformation> GetPageData(PlanetSearch searchModel, string sortColumn, int start, int length)
        {
            IQueryable<PlanetInformation> query = _context.Set<PlanetInformation>();
            RecordsTotal = query.Count();

            query = Search(searchModel, query);
            RecordsFiltered = query.Count();

            query = OrderBy(sortColumn, query);
            query = WithPagination(start, length, query);

            SearchResult<PlanetInformation> result = new SearchResult<PlanetInformation>();

            result.RecordsTotal = RecordsTotal;
            result.RecordsFiltered = RecordsFiltered;
            result.Data = query.ToList();
            return result;
        }

        private IQueryable<PlanetInformation> OrderBy(string value, IQueryable<PlanetInformation> query)
        {
            switch (value)
            {
                case "-name":
                    return query.OrderByDescending(s => s.Name);
                case "-moonCount":
                    return query.OrderByDescending(s => s.MoonCount);
                case "moonCount":
                    return query.OrderBy(s => s.MoonCount);
                case "-populationCount":
                    return query.OrderByDescending(s => s.PopulationCount);
                case "populationCount":
                    return query.OrderBy(s => s.PopulationCount);
                default:
                    return query.OrderBy(s => s.Name);
            }
        }

        private IQueryable<PlanetInformation> WithPagination(int start, int length, IQueryable<PlanetInformation> query)
        {
            return query.Skip(start).Take(length);
        }

        private IQueryable<PlanetInformation> Search(PlanetSearch searchModel, IQueryable<PlanetInformation> query)
        {
            if (!String.IsNullOrEmpty(searchModel.Name))
                query = query.Where(s => s.Name!.ToUpper().Contains(searchModel.Name.ToUpper()));
            if (searchModel.MoonCount != null)
                query = query.Where(s => s.MoonCount == searchModel.MoonCount);
            if (searchModel.PopulationCount != null)
                query = query.Where(s => s.PopulationCount == searchModel.PopulationCount);
            return query;
        }
    }
}
