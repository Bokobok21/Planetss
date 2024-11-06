using Planetss.Data;
using Planetss.Entities;
using Microsoft.EntityFrameworkCore;

namespace Planetss.Seeds
{
    public class SeedPlanetInformationData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any categories.
                if (context.Planets.Any())
                {
                    return;   // DB has been seeded
                }
                context.Planets.AddRange(
                    new PlanetInformation
                    {
                        Name = "Earth",
                        MoonCount = 1,
                        PopulationCount = 7000,
                    },
                    new PlanetInformation
                    {
                        Name = "Mars",
                        MoonCount = 1,
                        PopulationCount = 0,
                    },
                    new PlanetInformation
                    {
                        Name = "Neptune",
                        MoonCount = 56,
                        PopulationCount = 0,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
