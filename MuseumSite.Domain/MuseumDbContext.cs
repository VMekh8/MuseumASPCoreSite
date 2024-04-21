using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain
{
    public class MuseumDbContext : IdentityDbContext<UserEntity>
    {
        public MuseumDbContext(DbContextOptions<MuseumDbContext> options) : base (options)
        {
        }

        public DbSet<ExhibitionEntity> ExhibitionEntity { get; set; }
        public DbSet<ExhitbitEntity> ExhitbitEntity { get; set;}
        public DbSet<MuseumNewsEntity> MuseumNewsEntity { get; set; }

    }
}
