using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain.Configuration
{
    public class MuseumNewsConfigration : IEntityTypeConfiguration<MuseumNewsEntity>
    {
        public void Configure(EntityTypeBuilder<MuseumNewsEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(MuseumNews.MAX_NEWS_TITLE_LENGHT);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(MuseumNews.MAX_NEWS_DESCRIPTION_LENGHT);

            builder.Property(e => e.Image)
                .IsRequired();
        }
    }
}
