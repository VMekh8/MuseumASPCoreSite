using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MuseumSite.Domain.Configuration;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain
{
    public class MuseumDbContext : IdentityDbContext<UserEntity>
    {
        public MuseumDbContext(DbContextOptions<MuseumDbContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ExhibitConfiguration());
            builder.ApplyConfiguration(new ExhibitionConfiguration());
            builder.ApplyConfiguration(new MuseumNewsConfigration());
            builder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<ExhibitionEntity> ExhibitionEntity { get; set; }
        public DbSet<ExhitbitEntity> ExhitbitEntity { get; set;}
        public DbSet<MuseumNewsEntity> MuseumNewsEntity { get; set; }

    }
}
