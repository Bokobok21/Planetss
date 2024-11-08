using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Planetss.Models.PlanetInformation
{
    public class PlanetViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Полето \"Име\" е задължително!")]
        [DisplayName("Име")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Полето \"Ред на показване\" е задължително!")]
        [DisplayName("Ред на показване")]
        public int MoonCount { get; set; }
        public int PopulationCount { get; set; }

        public void PopulatePlanet(Planetss.Entities.PlanetInformation planet)
        {
            planet.Name = Name;
            planet.MoonCount = MoonCount;
            planet.PopulationCount = PopulationCount;
        }

        public void PopulateFromPlanet(Planetss.Entities.PlanetInformation? planet)
        {
            if (planet == null)
                return;

            Id = planet.Id;
            Name = planet.Name;
            MoonCount = planet.MoonCount;
            PopulationCount = planet.PopulationCount;
        }
    }
}
