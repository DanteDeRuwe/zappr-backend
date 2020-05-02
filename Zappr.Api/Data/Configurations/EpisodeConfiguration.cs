using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.ToTable("Epsisode");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Series).WithMany(s => s.Episodes).HasForeignKey(e => e.SeriesId);
            /*
            builder.HasMany(e => e.Ratings).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.Comments).WithOne().OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}