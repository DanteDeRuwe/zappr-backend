using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    class SeriesConfiguration : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            builder.ToTable("Series");
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Episodes).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(s => s.Comments).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(s => s.Characters).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}