using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Core.Entities;

namespace Zappr.Infrastructure.Data.Configurations
{
    class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.ToTable("Episode");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Series).WithMany(s => s.Episodes).HasForeignKey(e => e.SeriesId);
            builder.HasMany(s => s.Comments).WithOne();

            //builder.HasMany(e => e.Comments).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}