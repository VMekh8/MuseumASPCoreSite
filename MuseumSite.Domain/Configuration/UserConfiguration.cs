using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired();

            builder.Property(e => e.LastName)
                .IsRequired();
        }
    }
}
