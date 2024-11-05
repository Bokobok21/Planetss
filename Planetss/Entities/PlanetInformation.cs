using System.ComponentModel.DataAnnotations;
namespace Planetss.Entities
{
	public class PlanetInformation
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int MoonCount { get; set; }
		public int PopulationCount { get; set; }
	}
}
