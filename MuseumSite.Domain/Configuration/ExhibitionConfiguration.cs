using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain.Configuration
{
    public class ExhibitionConfiguration : IEntityTypeConfiguration<ExhibitionEntity>
    {
        public void Configure(EntityTypeBuilder<ExhibitionEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(Exhibit.MAX_TITLE_LENGHT);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(Exhibit.MAX_DESCRIPTION_LENGHT);

            builder.Property(e => e.EventDate)
                .IsRequired();

            builder.Property(e => e.Image)
                .IsRequired();            
        }
    }
}
