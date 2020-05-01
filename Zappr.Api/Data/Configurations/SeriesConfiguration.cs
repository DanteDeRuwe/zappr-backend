using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;
using Zappr.Api.Domain;

namespace Zappr.Api.Data.Configurations
{
    class SeriesConfiguration : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            builder.ToTable("Series");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Genres).HasConversion(
                g => JsonSerializer.Serialize(g, default),
                g => JsonSerializer.Deserialize<List<string>>(g, default)
            );

            builder.HasMany(s => s.Episodes).WithOne();
            //builder.HasMany(s => s.Comments).WithOne();
            //builder.HasMany(s => s.Ratings).WithOne();
            //builder.HasMany(s => s.Characters).WithOne();
        }
    }
}