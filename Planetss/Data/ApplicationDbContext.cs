using Planetss.Entities;
using Microsoft.EntityFrameworkCore;

namespace Planetss.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<PlanetInformation> Categories { get; set; } //Category is the entity and Categories is the table in the database
    }
}
