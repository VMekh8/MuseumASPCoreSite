using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain.Configuration
{
    public class ExhibitConfiguration : IEntityTypeConfiguration<ExhitbitEntity>
    {
        public void Configure(EntityTypeBuilder<ExhitbitEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(Exhibit.MAX_TITLE_LENGHT);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(Exhibit.MAX_DESCRIPTION_LENGHT);

            builder.Property(e => e.Image)
                .IsRequired();

            builder.HasOne<ExhibitionEntity>()
                .WithMany(e => e.ExhitbitsEntities)
                .HasForeignKey(e => e.ExhibitionEntityId);
        }
    }
}
