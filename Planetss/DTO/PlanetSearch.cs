using System.ComponentModel.DataAnnotations;

namespace Planetss.DTO
{
    public class PlanetSearch
    {
        public string? Name { get; set; }
        public int? MoonCount { get; set; }
        public int? PopulationCount { get; set; }
    }
}
